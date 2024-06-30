using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.BranchSetups.Quries;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Branches.Queries
{
    public class GetBranchCodeQuery : IRequest<string>
    {
        public bool ChangePrefixes { get; set; }
        public Guid BusinessId { get; set; }

        public class Handler : IRequestHandler<GetBranchCodeQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<GetBranchCodeQuery> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;

            }

            public async Task<string> Handle(GetBranchCodeQuery request, CancellationToken cancellationToken)
            {
                var prefixRecord = await _mediator.Send(new BranchSetupDetails
                {

                    BranchId =request.BusinessId 
                }, cancellationToken);

                //if (request.ChangePrefixes)
                //{
                //    var branches =  _context.Branches.AsNoTracking().Where(x=>x.BusinessId==request.BusinessId).ToList();
                //    foreach (var val in branches)
                //    {
                //        string[] parts = val.Code.Split('-');
                //        string numericPart = parts[1];
                //        val.Code = prefixRecord.Prefix + "-" + numericPart;
                //    }

                //    _context.Branches.UpdateRange(branches);
                //    _context.SaveChanges();
                //    return null;

                //}
               
                string generatedCode;

                var autoCode = await _context.Branches.Where(x => x.BusinessId == request.BusinessId).OrderByDescending(x => x.Code).Select(x=>x.Code).FirstOrDefaultAsync(cancellationToken);

                if (autoCode == null)
                {
                    return prefixRecord.Prefix + "-" + prefixRecord.StartingNumber;
                }
                else
                {
                    string[] parts = autoCode.Split('-');
                    string numericPart = parts[1];



                 
                    Int32 autoNo = Convert.ToInt32((numericPart));
                    autoNo++;

                    // Check if autoNo is between 1 and 9
                    if (autoNo >= 1 && autoNo <= 9)
                    {
                        numericPart = autoNo.ToString("D2");
                        generatedCode = parts[0] + "-" + numericPart;

                    }
                    else
                    {
                        generatedCode = prefixRecord.Prefix + "-" + autoNo;

                    }


                }

                return generatedCode;


            }

           
        }
    }
}
