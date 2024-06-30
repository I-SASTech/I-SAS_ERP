using AutoMapper;
using Focus.Business.Interface;
using Focus.Business.MonthlyCosts.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.MonthlyCosts.Queries
{
  public  class MonthlyCostDetailQuery : IRequest<MonthlyCostLookUpModel>
    {
        public Guid? Id { get; set; }

        public class Handler : IRequestHandler<MonthlyCostDetailQuery, MonthlyCostLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<MonthlyCostDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<MonthlyCostLookUpModel> Handle(MonthlyCostDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var priceRecord = _context.MonthlyCosts.AsNoTracking().Include(x=>x.MonthlyCostItems)
                        .FirstOrDefault();


                    if (priceRecord != null)
                    {



                        return new MonthlyCostLookUpModel
                        {
                            Id = priceRecord.Id,
                            Date = priceRecord.Date,
                            MonthlyCostItems = priceRecord.MonthlyCostItems.Select(x =>
                              new MonthlyCostItemLookUpModel()
                              {
                                  Id = x.Id,
                                  Description = x.Description,
                                  MonthlyCosts = x.MonthlyCosts,
                                  YearlyCost = x.YearlyCost,
                                  Daily = x.Daily,
                                  Total = x.Total,


                              }).ToList(),


                        };
                    }
                    else
                    {
                        return  null;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
