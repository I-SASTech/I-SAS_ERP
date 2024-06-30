using Dapper;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Sales.Commands.DeleteSalePrmanently
{
    public class DeleteSalePermanentlyCommand : IRequest<Message>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeleteSalePermanentlyCommand, Message>
        {
            private readonly IConfiguration _configuration;
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            public Handler(IApplicationDbContext context, ILogger<DeleteSalePermanentlyCommand> logger, IConfiguration configuration)
            {
                _configuration = configuration;
                _context = context;
                _logger = logger;
            }
            public async Task<Message> Handle(DeleteSalePermanentlyCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                    string sb = "DELETE FROM Sales WHERE Id =" + "'"+ request.Id + "'";

                    var query = conn.Query<Sale>(sb).AsQueryable();
                    
                    var message = new Message
                    {
                        Id = Guid.Empty,
                        IsAddUpdate = "Data has been deleted successfully"
                    };
                    return message;
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
