using AutoMapper;
using Focus.Business.Common;
using Focus.Business.EmailConfigurationSetup.Model;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.EmailConfigurationSetup.Queries
{
    public class GetEmailConfigurationList : PagedRequest, IRequest<PagedResult<EmailConfigurationListModel>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropdown { get; set; }

        public class Handler : IRequestHandler<GetEmailConfigurationList, PagedResult<EmailConfigurationListModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetEmailConfigurationList> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<EmailConfigurationListModel>> Handle(GetEmailConfigurationList request, CancellationToken cancellationToken)
            {
                try
                {
                   
                    if (!request.IsDropdown)
                    {
                        var query = await _context.EmailConfiguration.ToListAsync();

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;

                            query = query.Where(x => x.Email.ToLower().Contains(searchTerm.ToLower()) ||
                                                     x.SmtpServer.ToLower().Contains(searchTerm.ToLower())).ToList();
                        }

                        var count =  query.Count();
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();

                        var colorList = new List<EmailConfigurationLookupModel>();
                        foreach (var item in query)
                        {
                            colorList.Add(new EmailConfigurationLookupModel()
                            {
                                Id = item.Id,
                                Email = item.Email,
                                Password = item.Password,
                                SmtpServer = item.SmtpServer,
                                Port = item.Port,
                                Server = item.Server,
                                IsActive = item.IsActive,
                                IsDefault = item.IsDefault,
                            });
                        }

                        return new PagedResult<EmailConfigurationListModel>
                        {
                            Results = new EmailConfigurationListModel
                            {
                                EmailConfigurations = colorList
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = colorList.Count / request.PageSize
                        };
                    }
                    else
                    {
                        var query = await _context.EmailConfiguration.Where(x => x.IsActive).ToListAsync();

                        var colorList = new List<EmailConfigurationLookupModel>();
                        foreach (var item in query)
                        {
                            colorList.Add(new EmailConfigurationLookupModel()
                            {
                                Id = item.Id,
                                Email = item.Email,
                                Password = item.Password,
                                SmtpServer = item.SmtpServer,
                                Port = item.Port,
                                Server = item.Server,
                                IsActive = item.IsActive,
                                IsDefault = item.IsDefault,
                            });
                        }


                        return new PagedResult<EmailConfigurationListModel>
                        {
                            Results = new EmailConfigurationListModel
                            {
                                EmailConfigurations = colorList
                            },
                        };
                    }


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
