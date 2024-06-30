using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.StockAdjustments.Queries.GetStockAdjustmentNumber
{
    public class GetStockAdjustmentNumberQuery : IRequest<string>
    {
        public StockAdjustmentType StockAdjustmentType { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetStockAdjustmentNumberQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetStockAdjustmentNumberQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetStockAdjustmentNumberQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    string branchCode = "";
                    string autoCode;
                    if (request.BranchId != null)
                    {
                        autoCode = await _context.StockAdjustments
                            .Where(x => x.StockAdjustmentType == request.StockAdjustmentType && x.BranchId == request.BranchId)
                            .Select(x => x.Code)
                            .OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken);

                        var branch = await _context.Branches
                            .Where(x => x.Id == request.BranchId)
                            .Select(x => x.Code)
                            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                        string firstLetter = branch.Substring(0, 1);
                        var fetchNo = Convert.ToInt32(branch.Substring(2));
                        branchCode = firstLetter + fetchNo + "-";
                    }
                    else
                    {
                        autoCode = await _context.StockAdjustments
                            .Where(x => x.StockAdjustmentType == request.StockAdjustmentType)
                            .Select(x => x.Code)
                            .OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken);
                    }
                    if (autoCode != null)
                    {
                        if (string.IsNullOrEmpty(autoCode))
                        {
                            return GenerateCodeFirstTime(request.StockAdjustmentType, branchCode);
                        }
                        return GenerateNewCode(autoCode, request.StockAdjustmentType, branchCode);
                    }

                    return GenerateCodeFirstTime(request.StockAdjustmentType, branchCode);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }

            private string GenerateCodeFirstTime(StockAdjustmentType type, string branchCode)
            {
                if (TransactionType.StockIn == (TransactionType)type)
                {
                    return branchCode+"STI-00001";
                }
                else if (TransactionType.StockProduction == (TransactionType)type)

                {
                    return branchCode + "PS-00001";
                }
                else
                {
                    return branchCode + "STO-00001";
                }
            }

            private string GenerateNewCode(string soNumber, StockAdjustmentType type, string branchCode)
            {
                string fetchNo = soNumber.Substring(soNumber.Length - 5);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                string prefix;
                if (TransactionType.StockIn == (TransactionType)type)
                {
                    prefix = "STI-";
                }
                else if (TransactionType.StockProduction == (TransactionType)type)
                {
                    prefix = "PS-";
                }
                else
                {
                    prefix = "STO-";
                }
                var newCode = prefix + autoNo.ToString(format);
                return branchCode + newCode;
            }
        }
    }
}
