using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ChequeBooks.Commands
{
    public class AddUpdateChequeBook : IRequest<Guid>
    {
        //Get all variable in entity
        public ChequeBookLookUpModel ChequeBook { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateChequeBook, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateChequeBook> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }

            public string Code { get; private set; }

            public async Task<Guid> Handle(AddUpdateChequeBook request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.ChequeBook.Id != Guid.Empty)
                    {

                        var chequebook = await Context.ChequeBooks.FindAsync(request.ChequeBook.Id);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        chequebook.BankId = request.ChequeBook.BankId;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                           
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        return chequebook.Id;

                    }
                    else
                    {

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var chequebook = new ChequeBook
                        {
                            BankId = request.ChequeBook.BankId,
                            IsActive = request.ChequeBook.IsActive,
                            BankNo = request.ChequeBook.BankNo,
                            NoOfCheques = request.ChequeBook.NoOfCheques,
                            StartingNo = request.ChequeBook.StartingNo,
                            IsBlock = request.ChequeBook.IsBlock,
                            LastNo = request.ChequeBook.LastNo,
                            Date = request.ChequeBook.Date,
                            Used = request.ChequeBook.Used,
                            Remaining = request.ChequeBook.Remaining,
                            Reason = request.ChequeBook.Reason,
                            BookNo = request.ChequeBook.BookNo,
                        };
                        int srNo = 1;
                        await Context.ChequeBooks.AddAsync(chequebook, cancellationToken);
                        if(request.ChequeBook.NoOfCheques>150)
                        {
                            throw new NotFoundException("Only Create 150 Cheque", null);
                        }

                        string serialNo = request.ChequeBook.StartingNo;
                        for ( int i=0; i<request.ChequeBook.NoOfCheques;i++)
                        {
                            
                            Int32 autoNo = Convert.ToInt32(serialNo) + i;
                            var format = "";

                            for (int j = 0; j < serialNo.Length; j++)
                            {
                                format += "0";
                            }

                            var newCode = autoNo.ToString(format);
                            var chequeBookItem = new ChequeBookItem
                            {
                                Code= GenerateNewCode(i),
                                BookNo = request.ChequeBook.BookNo,
                                SerialNo = (i+ srNo).ToString(),
                                ChequeNo = newCode.ToString(),
                                BankId = request.ChequeBook.BankId,
                                IsActive = i==0? request.ChequeBook.IsActive:false,
                                IsBlock = request.ChequeBook.IsBlock,
                                Date = request.ChequeBook.Date,
                                ChequeBookId= chequebook.Id
                            };
                            //serialNo = newCode;
                            await Context.ChequeBookItems.AddAsync(chequeBookItem, cancellationToken);
                        }


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                            
                        }, cancellationToken);

                        //Add Department to database
                        await Context.SaveChangesAsync(cancellationToken);
                        return chequebook.Id;



                    }

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }

            private string GenerateNewCode(int i)
            {
                

                if(i==0)
                {
                    Code = "CB-00001";
                    return "CB-00001";
                }
                else
                {
                    string fetchNo = Code.Substring(3);
                    Int32 autoNo = Convert.ToInt32((fetchNo));
                    var format = "CB-00000";
                    autoNo++;
                    var newCode = autoNo.ToString(format);
                    Code = newCode;
                    return newCode;
                }

                
            }
        }
    }
}
