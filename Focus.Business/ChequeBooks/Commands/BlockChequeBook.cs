using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ChequeBooks.Commands
{
    public class BlockChequeBook : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Reason { get; set; }

        public class Handler : IRequestHandler<BlockChequeBook, Guid>
        {
            //Create an object of IApplicationDbContext for delete the data from database
            public readonly IApplicationDbContext _context;

            //Create object of logger for all type of message show
            public readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor
            public Handler(IApplicationDbContext context, ILogger<BlockChequeBook> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(BlockChequeBook request, CancellationToken cancellationToken)
            {
                //Get Data from database in specific id which we want to delete
                var chequeBook = await _context.ChequeBooks.AsNoTracking().Include(x=>x.GetChequeBookItems).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                try
                {
                    if (chequeBook != null)
                    {
                        chequeBook.Reason = request.Reason;
                        chequeBook.IsBlock = true;

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        foreach (var cheques in chequeBook.GetChequeBookItems)
                        {
                            if(!cheques.IsUsed)
                            {
                                cheques.IsBlock = true;
                            }
                        }
                        _context.ChequeBooks.Update(chequeBook);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);


                    }
                    return request.Id;
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
