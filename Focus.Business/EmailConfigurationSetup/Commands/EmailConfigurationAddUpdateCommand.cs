using Focus.Business.Common;
using Focus.Business.EmailConfigurationSetup.Model;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.EmailConfigurationSetup.Commands
{
    public class EmailConfigurationAddUpdateCommand : IRequest<Message>
    {
        public EmailConfigurationLookupModel EmailConfig { get; set; }
        public class Handler : IRequestHandler<EmailConfigurationAddUpdateCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            private readonly ISendEmail sendEmail;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<EmailConfigurationAddUpdateCommand> logger, IMediator mediator, ISendEmail _sendEmail)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
                sendEmail = _sendEmail;
            }
            public async Task<Message> Handle(EmailConfigurationAddUpdateCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    //Update
                    if (request.EmailConfig.Id != Guid.Empty)
                    {
                        var emailConfig = await Context.EmailConfiguration.FindAsync(request.EmailConfig.Id);
                        if (emailConfig == null)
                            throw new NotFoundException("No Email Setup Found", "");



                        emailConfig.Email = request.EmailConfig.Email;
                        emailConfig.Password = request.EmailConfig.Password;
                        emailConfig.SmtpServer = request.EmailConfig.SmtpServer;
                        emailConfig.Port = request.EmailConfig.Port;
                        emailConfig.Server = request.EmailConfig.Server;
                        emailConfig.IsActive = request.EmailConfig.IsActive;
                        emailConfig.IsActive = false;

                       


                        var log = Context.SyncLog();
                        var branches = await Context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        var list = new List<SyncRecordModel>();
                        if (branches.Any())
                        {
                            foreach (var branch in branches)
                            {
                                var syncRecords = log.Select(x => new SyncRecordModel
                                {
                                    Table = x.Table,
                                    ColumnId = x.ColumnId,
                                    CompanyId = x.CompanyId,
                                    ColumnType = x.ColumnType,
                                    Push = x.Push,
                                    Pull = x.Pull,
                                    Action = x.Action,
                                    ChangeDate = DateTime.Now,
                                    PullDate = x.PullDate,
                                    PushDate = x.PushDate,
                                    ColumnName = x.ColumnName,
                                    BranchId = branch.Id,
                                    Code = _code,
                                }).ToList();

                                list.AddRange(syncRecords);
                            }
                        }
                        else
                        {
                            var syncRecords = log.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer = true
                        }, cancellationToken);




                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            IsSuccess = true,
                            IsAddUpdate = "Data has been Updated Successfully."
                        };

                    }
                    else
                    {
                        var isExist = await Context.EmailConfiguration.FirstOrDefaultAsync(x => x.Email == request.EmailConfig.Email);
                        if (isExist != null)
                        {
                            return new Message
                            {
                                IsSuccess = false,
                                IsAddUpdate = "Email Already Exist."
                            };
                        }


                        var emailConfig = new EmailConfiguration
                        {
                            Email = request.EmailConfig.Email,
                            Password = request.EmailConfig.Password,
                            SmtpServer = request.EmailConfig.SmtpServer,
                            Port = request.EmailConfig.Port,
                            Server = request.EmailConfig.Server,
                            IsActive = request.EmailConfig.IsActive,
                            IsDefault = false,
                        };

                        //Add Department to database
                        await Context.EmailConfiguration.AddAsync(emailConfig, cancellationToken);

                        var log = Context.SyncLog();
                        var branches = await Context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        var list = new List<SyncRecordModel>();
                        if (branches.Any())
                        {
                            foreach (var branch in branches)
                            {
                                var syncRecords = log.Select(x => new SyncRecordModel
                                {
                                    Table = x.Table,
                                    ColumnId = x.ColumnId,
                                    CompanyId = x.CompanyId,
                                    ColumnType = x.ColumnType,
                                    Push = x.Push,
                                    Pull = x.Pull,
                                    Action = x.Action,
                                    ChangeDate = DateTime.Now,
                                    PullDate = x.PullDate,
                                    PushDate = x.PushDate,
                                    ColumnName = x.ColumnName,
                                    BranchId = branch.Id,
                                    Code = _code,
                                }).ToList();

                                list.AddRange(syncRecords);
                            }
                        }
                        else
                        {
                            var syncRecords = log.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                //     BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer = true
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            IsSuccess = true,
                            IsAddUpdate = "Data has been Added Successfully."
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
