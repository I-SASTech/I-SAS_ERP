using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;

namespace Focus.Business.Currencies.Queries.GetCurrencyList
{
    public class GetCurrencyListQuery : IRequest<CurrencyListModel>
    {
        public class Handler : IRequestHandler<GetCurrencyListQuery, CurrencyListModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;
            private readonly IHostingEnvironment _hostingEnvironment;
            private readonly IMemoryCache _memoryCache;
            private readonly IUserHttpContextProvider _httpContextProvider;
            public Handler(IApplicationDbContext context,
                ILogger<GetCurrencyListQuery> logger,
                IMapper mapper, IHostingEnvironment hostingEnvironment, IMemoryCache memoryCache,
                IUserHttpContextProvider httpContextProvider)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _hostingEnvironment = hostingEnvironment;
                _memoryCache = memoryCache;
                _httpContextProvider = httpContextProvider;
            }
            public async Task<CurrencyListModel> Handle(GetCurrencyListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var cacheKey = "CurrencyList" + _httpContextProvider.GetUserId();
                    if (!_memoryCache.TryGetValue(cacheKey, out List<CurrencyLookUpModel> currencyList))
                    {

                        var currency = _context.Currencies.AsQueryable();

                        currencyList = await currency
                            .OrderByDescending(x => x.Name)
                            .ProjectTo<CurrencyLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        currencyList = currencyList.Select(x => new CurrencyLookUpModel()
                        {
                            Id = x.Id,
                            Image = GetBaseImage(x.Image).Result,
                            Sign = x.Sign,
                            ArabicSign = x.ArabicSign,
                            IsActive = x.IsActive,
                            Name = x.Name,
                            NameArabic = x.NameArabic,
                        }).ToList();

                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            // Keep in cache for this time, reset time if accessed.
                            .SetSlidingExpiration(TimeSpan.FromSeconds(120));

                        // Save data in cache only if it exists
                        if (currencyList.Any())
                        {
                            _memoryCache.Set(cacheKey, currencyList, cacheEntryOptions);
                        }
                    }

                    return new CurrencyListModel
                        {
                            Currencies = currencyList
                        };
                    
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }

            public async Task<string> GetBaseImage(string filePath)
            {
                if (string.IsNullOrEmpty(filePath))
                    return filePath;

                var imageExist = File.Exists(_hostingEnvironment.WebRootPath + filePath);

                if (!imageExist)
                    return "";

                var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
                var bytes = await System.IO.File.ReadAllBytesAsync(path);
                var base64 = Convert.ToBase64String(bytes);
                return base64;
            }
        }
    }
}
