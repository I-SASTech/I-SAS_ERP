using Focus.Business.Common;
using Focus.Business.ContactCustomerGroup.Model;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Focus.Business.Exceptions;
using Focus.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.ContactCustomerGroup.Commands
{
    public class CustomerGroupAddUpdateCommand : IRequest<Message>
    {
        public CustomerGroupLookupModel customerGroup {get; set;}

        public class Handler : IRequestHandler<CustomerGroupAddUpdateCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<CustomerGroupAddUpdateCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(CustomerGroupAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if(request.customerGroup.Id != Guid.Empty)
                    {
                        var customer = await Context.CustomerGroups.FindAsync(request.customerGroup.Id);
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        if (customer == null)
                        {
                            return new Message
                            {
                                Id = Guid.Empty,
                                IsAddUpdate = "Customer Group Not Found",
                            };
                        }

                        customer.Code = request.customerGroup.Code;
                        customer.Name = request.customerGroup.Name;
                        customer.NameArabic = request.customerGroup.NameArabic;
                        customer.Description = request.customerGroup.Description;
                        customer.Status = request.customerGroup.Status;

                        var contactsUpdate = await Context.Contacts.Where(x => x.CustomerGroupId == request.customerGroup.Id).ToListAsync(cancellationToken: cancellationToken);
                        foreach (var item in contactsUpdate)
                        {
                            item.CustomerGroupId = null;
                            Context.Contacts.Update(item);
                        }

                        foreach (var item in request.customerGroup.ContactsList)
                        {
                            var contacts = await Context.Contacts.FindAsync(item.ContactId);
                            if (contacts != null)
                            {
                                contacts.CustomerGroupId = customer.Id;
                            }
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            Id = customer.Id,
                            IsAddUpdate = "Data has been save successfully.",
                        };
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var customer = new CustomerGroup
                        {
                            Code = request.customerGroup.Code,
                            Name = request.customerGroup.Name,
                            NameArabic = request.customerGroup.NameArabic,
                            Description = request.customerGroup.Description,
                            Status = request.customerGroup.Status,
                        };

                        await Context.CustomerGroups.AddAsync(customer, cancellationToken);

                        foreach (var item in request.customerGroup.ContactsList)
                        {
                            var contacts = await Context.Contacts.FindAsync(item.ContactId);
                            if (contacts != null)
                            {
                                contacts.CustomerGroupId = customer.Id;
                            }
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            Id = customer.Id,
                            IsAddUpdate = "Data has been save successfully.",
                        };
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
        }
    }
}
