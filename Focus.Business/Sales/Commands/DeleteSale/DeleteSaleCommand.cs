using Dapper;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Sales.Commands.DeleteSale
{
    public class DeleteSaleCommand : IRequest<Message>
    {
        public Guid Id { get; set; }
        public bool IsNormal { get; set; }

        public class Handler : IRequestHandler<DeleteSaleCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IConfiguration _configuration;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, IConfiguration configuration, ILogger<DeleteSaleCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _configuration = configuration;
                _mediator = mediator;
            }
            public async Task<Message> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.IsNormal)
                    {
                        var sales = _context.Sales.AsNoTracking().IgnoreQueryFilters().FirstOrDefault(x => x.Id == request.Id);
                        using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                        string sb = "Update Sales set IsDeleted = 0,SaleHoldTypes = 1 where Id=" + "'" + sales.Id + "'";

                        conn.Query<Sale>(sb).AsQueryable();

                        var message = new Message
                        {
                            Id = request.Id,
                            IsAddUpdate = "Data has been Converted successfully"
                        };
                        return message;
                    }
                    else
                    {
                        var sale = await _context.Sales.FindAsync(request.Id);
                        sale.SaleHoldTypes = Domain.Enum.SaleHoldTypes.Deleted;
                        _context.Sales.Remove(sale);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId=sale.BranchId,
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        var message = new Message
                        {
                            Id = request.Id,
                            IsAddUpdate = "Data has been deleted successfully"
                        };
                        return message;
                    }
                }

                catch (Exception exception)
                {
                    var message = new Message
                    {
                        Id = Guid.Empty,
                        IsAddUpdate = "Some Error Occurred /n " + exception.Message
                    };
                    _logger.LogError(exception.Message);
                    return message;
                }
            }
        }
    }
}
