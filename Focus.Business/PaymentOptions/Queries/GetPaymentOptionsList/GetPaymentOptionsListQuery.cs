using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using Microsoft.Extensions.Caching.Memory;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.PaymentOptions.Queries.GetPaymentOptionsList
{
    public class GetPaymentOptionsListQuery : IRequest<PaymentOptionsListModel>
    {
        public bool IsActive { get; set; }

        public class Handler : IRequestHandler<GetPaymentOptionsListQuery, PaymentOptionsListModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;
            private readonly IHostingEnvironment _hostingEnvironment;
            private readonly IMemoryCache _memoryCache;
            private readonly IUserHttpContextProvider _httpContextProvider;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<GetPaymentOptionsListQuery> logger, IMapper mapper, IHostingEnvironment hostingEnvironment, IMemoryCache memoryCache, IUserHttpContextProvider httpContextProvider, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _hostingEnvironment = hostingEnvironment;
                _memoryCache = memoryCache;
                _httpContextProvider = httpContextProvider;
                _mediator = mediator;
            }
            public async Task<PaymentOptionsListModel> Handle(GetPaymentOptionsListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsActive)
                    {
                        var cacheKey = "paymentOptions" + _httpContextProvider.GetUserId();
                        if (!_memoryCache.TryGetValue(cacheKey, out List<PaymentOptionsLookUpModel> paymentOptionsList))
                        {
                            var paymentOptions = _context.PaymentOptions.Where(x => x.IsActive).AsQueryable();

                            paymentOptionsList = await paymentOptions
                                .OrderBy(x => x.Name)
                                .ProjectTo<PaymentOptionsLookUpModel>(_mapper.ConfigurationProvider)
                                .ToListAsync(cancellationToken);

                            paymentOptionsList = paymentOptionsList.Select(x => new PaymentOptionsLookUpModel()
                            {
                                Id = x.Id,
                                Image = GetBaseImage(x.Image).Result,
                                IsActive = x.IsActive,
                                Name = x.Name,
                                NameArabic = x.NameArabic,
                            }).ToList();
                            var cacheEntryOptions = new MemoryCacheEntryOptions()
                                // Keep in cache for this time, reset time if accessed.
                                .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                            // Save data in cache only if it exists
                            if (paymentOptionsList.Any())
                            {
                                _memoryCache.Set(cacheKey, paymentOptionsList, cacheEntryOptions);
                            }


                        }
                        return new PaymentOptionsListModel
                        {
                            PaymentOptions = paymentOptionsList
                        };
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var paymentOptions = _context.PaymentOptions.AsQueryable();

                        List<PaymentOptionsLookUpModel> paymentOptionsList;

                        paymentOptionsList = await paymentOptions
                            .OrderBy(x => x.Name)
                            .ProjectTo<PaymentOptionsLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        var listOfPayment = new List<string>
                            {
                                "MasterCard",
                                "Visa",
                                "Mada",
                                "AmericanExpress",
                                "ApplePay",
                                "Discover",
                                "PayPal",
                                "StcPay",
                                "WesternUnion"
                            };
                        var listOfPaymentArabic = new List<string>
                            {
                                "بطاقة ماستر بطاقة ائتمان",
                                "تأشيرة دخول",
                                "مدى",
                                "أمريكان اكسبريس",
                                "أجر أبل",
                                "يكتشف",
                                "باي بال",
                                "دفعStc",
                                "الاتحاد الغربي"
                            };
                        var optionsList = new List<PaymentOption>();
                        if (paymentOptionsList.Count <= 7)
                        {
                            foreach (var options in paymentOptionsList)
                            {
                                if (listOfPayment.Contains(options.Name) || listOfPaymentArabic.Contains(options.NameArabic))
                                {
                                    var index = listOfPayment.IndexOf(options.Name);
                                    var data = _context.PaymentOptions.First(x => x.Id == options.Id);
                                    data.Name = listOfPayment[index];
                                    data.NameArabic = listOfPaymentArabic[index];
                                    data.Image = "/PaymentOptions/" + listOfPayment[index] + ".png";
                                    await _context.SaveChangesAsync(cancellationToken);
                                    listOfPayment.RemoveAt(index);
                                    listOfPaymentArabic.RemoveAt(index);
                                }

                            }

                            if (listOfPayment.Count > 0)
                            {
                                foreach (var remainingOptions in listOfPayment)
                                {
                                    if (remainingOptions == "MasterCard" || remainingOptions == "Mada" || remainingOptions == "Visa")
                                    {
                                        optionsList.Add(new PaymentOption()
                                        {
                                            Name = remainingOptions,
                                            NameArabic = listOfPaymentArabic[listOfPayment.IndexOf(remainingOptions)],
                                            Image = "/PaymentOptions/" + remainingOptions + ".png",
                                            IsActive = true
                                        });
                                    }
                                    else
                                    {
                                        optionsList.Add(new PaymentOption()
                                        {
                                            Name = remainingOptions,
                                            NameArabic = listOfPaymentArabic[listOfPayment.IndexOf(remainingOptions)],
                                            Image = "/PaymentOptions/" + remainingOptions + ".png",
                                            IsActive = false
                                        });
                                    }

                                }

                                await _context.PaymentOptions.AddRangeAsync(optionsList, cancellationToken);

                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = _context.SyncLog(),
                                    Code = _code,
                                }, cancellationToken);

                                await _context.SaveChangesAsync(cancellationToken);
                            }
                        }
                        paymentOptionsList = await paymentOptions
                            .OrderBy(x => x.Name)
                            .ProjectTo<PaymentOptionsLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        paymentOptionsList = paymentOptionsList.Select(x => new PaymentOptionsLookUpModel()
                        {
                            Id = x.Id,
                            Image = GetBaseImage(x.Image).Result,
                            IsActive = x.IsActive,
                            Name = x.Name,
                            NameArabic = x.NameArabic,
                        }).ToList();
                        
                        return new PaymentOptionsListModel
                        {
                            PaymentOptions = paymentOptionsList
                        };
                    }

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }

            private async Task<string> GetBaseImage(string filePath)
            {
                if (string.IsNullOrEmpty(filePath))
                    return "";
                var imageExist = File.Exists(_hostingEnvironment.WebRootPath + filePath);

                if (!imageExist)
                    return filePath;

                var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
                var bytes = await File.ReadAllBytesAsync(path);
                var base64 = Convert.ToBase64String(bytes);
                return base64;
            }
        }
    }
}
