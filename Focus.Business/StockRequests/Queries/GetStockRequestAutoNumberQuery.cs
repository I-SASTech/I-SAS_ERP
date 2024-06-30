﻿using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.StockRequests.Queries
{
    public class GetStockRequestAutoNumberQuery : IRequest<string>
    {
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetStockRequestAutoNumberQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetStockRequestAutoNumberQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetStockRequestAutoNumberQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var branchCode = "";
                    string wareHouseTransfer;
                    if (request.BranchId != null)
                    {
                        wareHouseTransfer = await _context.StockRequests
                            .Where(x => x.BranchId == request.BranchId)
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
                        wareHouseTransfer = await _context.StockRequests
                            .Select(x => x.Code)
                            .OrderBy(x => x)
                            .LastOrDefaultAsync(cancellationToken);
                    }

                    if (wareHouseTransfer != null)
                    {
                        if (string.IsNullOrEmpty(wareHouseTransfer))
                        {
                            return GenerateCodeFirstTime(branchCode);
                        }

                        return GenerateNewCode(wareHouseTransfer, branchCode);
                    }

                    return GenerateCodeFirstTime(branchCode);
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }

            private string GenerateCodeFirstTime(string branchCode)
            {
                return branchCode + "SR-00001";
            }

            private string GenerateNewCode(string soNumber, string branchCode)
            {
                var fetchNo = soNumber.Substring(soNumber.Length - 5);
                var autoNo = Convert.ToInt32((fetchNo));
                const string format = "00000";
                autoNo++;
                var newCode = "SR-" + autoNo.ToString(format);
                return branchCode + newCode;
            }
        }

    }
}
