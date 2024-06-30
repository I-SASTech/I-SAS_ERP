using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.CompaniesOption.Commands.AddUpdateOption;
using Focus.Business.PrintOptions.Commands.AddPrintOption;
using Focus.Domain.Entities;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.PrintOptions.Queries
{
    public class GetPrintOptionListQuery : IRequest<List<PrintOptionLookUp>>
    {
        public string SearchTerm { get; set; }
        public class Handler : IRequestHandler<GetPrintOptionListQuery, List<PrintOptionLookUp>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context,
                ILogger<GetPrintOptionListQuery> logger,
                IMapper mapper, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _mediator = mediator;
            }
            public async Task<List<PrintOptionLookUp>> Handle(GetPrintOptionListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var count = _context.PrintOptions.Count();


                    if (count == 0)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var pritntOptions = new List<PrintOption>()
                        {
                            new PrintOption
                            {
                                Label="Print Logo",
                                Value=true


                            },
                            new PrintOption
                            {
                                Label="Add/Remove Logo",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Invoice Type(English)",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Invoice Type(Arabic)",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Invoice Number(English)",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Invoice Number(Arabic)",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Bar Code",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Business Name (English)",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Business Name (Arabic)",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Company Name (English)",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Company Name (Arabic)",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Address (English)",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Address (Arabic)",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="VAT/Tax ID (English)",
                                Value=true
                            }, 
                            new PrintOption
                            {
                                Label="VAT/Tax ID (Arabic)",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Tag/Welcome Line (English)",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Tag/Welcome Line (Arabic)",
                                Value=true
                            }, 
                            new PrintOption
                            {
                                Label="User Name (English)",
                                Value=true
                            }, 
                            new PrintOption
                            {
                                Label="User Name (Arabic)",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Counter Name (English)",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Counter Number (Arabic)",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Closing Message (English)",
                                Value=true
                            },
                            new PrintOption
                            {
                                Label="Closing Message (Arabic)",
                                Value=true
                            },
                        };

                        await _context.PrintOptions.AddRangeAsync(pritntOptions, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                    }


                    var query = _context.PrintOptions.AsQueryable();

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        query = query.Where(x =>
                            x.Label.Contains(request.SearchTerm));

                    }
                    var list = new List<PrintOptionLookUp>();

                    foreach (var item in query)
                    {
                        list.Add(new PrintOptionLookUp()
                        {
                            Id = item.Id,
                            PrintSettingId = item.PrintSettingId,
                            Label = item.Label,
                            Value = item.Value,
                        });
                    }


                    return list;




                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
