using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Interface;
using Microsoft.Extensions.Caching.Memory;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Models;
using System.Collections.Generic;

namespace Focus.Business.PaymentOptions.Commands.AddUpdatePaymentOptions
{
    public class AddUpdatePaymentOptionsCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }
        public string NameArabic { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdatePaymentOptionsCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMemoryCache _memoryCache;
            private readonly IUserHttpContextProvider _httpContextProvider;
            //Constructor
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddUpdatePaymentOptionsCommand> logger, IMemoryCache memoryCache,
                IUserHttpContextProvider httpContextProvider, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _memoryCache = memoryCache;
                _httpContextProvider = httpContextProvider;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddUpdatePaymentOptionsCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    var cacheKey = "paymentOptions" + _httpContextProvider.GetUserId();

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.Id != Guid.Empty)
                    {
                        var paymentOptions = await _context.PaymentOptions.FindAsync(request.Id);
                        if (paymentOptions == null)
                            throw new NotFoundException(request.Name, request.Id);

                        if (request.Name != "" && request.NameArabic != "")
                        {
                            //var isPaymentOptionExist = await Context.PaymentOptions.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken);
                            //if (paymentOptions.Name != request.Name && isPaymentOptionExist != null)
                            //    throw new ObjectAlreadyExistsException("PaymentOption " + request.Name + " Already Exist");
                            //if (paymentOptions.NameArabic != request.NameArabic && isPaymentOptionExist != null)
                            //    throw new ObjectAlreadyExistsException("PaymentOption " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            var isPaymentOptionExist = await _context.PaymentOptions.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
                            if (paymentOptions.Name != request.Name && isPaymentOptionExist != null)
                                throw new ObjectAlreadyExistsException("PaymentOption " + request.Name + " Already Exist");
                        }
                        else
                        {
                            var isPaymentOptionExist = await _context.PaymentOptions.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken);
                            if (paymentOptions.NameArabic != request.NameArabic && isPaymentOptionExist != null)
                                throw new ObjectAlreadyExistsException("PaymentOption " + request.NameArabic + " Already Exist");
                        }


                        paymentOptions.Name = request.Name;
                        paymentOptions.NameArabic = request.NameArabic;
                        paymentOptions.IsActive = request.IsActive;
                        paymentOptions.Image = request.Image;

                        var log = _context.SyncLog();
                        var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
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
                                //BranchId = branch.Id,
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
                        await _context.SaveChangesAsync(cancellationToken);
                        _memoryCache.Remove(cacheKey);
                        // Return Department id after successfully updating data
                        return paymentOptions.Id;
                        //Check Department exist

                    }
                    else
                    {
                        if (request.Name != "" && request.NameArabic != "")
                        {
                            var isPaymentOptionExist = await _context.PaymentOptions.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken);
                            if (isPaymentOptionExist != null)
                                throw new ObjectAlreadyExistsException("PaymentOption " + request.Name + " " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            var isPaymentOptionExist = await _context.PaymentOptions.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
                            if (isPaymentOptionExist != null)
                                throw new ObjectAlreadyExistsException("PaymentOption " + request.Name + " Already Exist");
                        }
                        else
                        {
                            var isPaymentOptionExist = await _context.PaymentOptions.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken);
                            if (isPaymentOptionExist != null)
                                throw new ObjectAlreadyExistsException("PaymentOption " + request.NameArabic + " Already Exist");
                        }


                        var paymentOptions = new PaymentOption
                        {
                            Name = request.Name,
                            NameArabic = request.NameArabic,
                            IsActive = request.IsActive,
                            Image = request.Image
                        };

                        //Add Department to database
                        await _context.PaymentOptions.AddAsync(paymentOptions, cancellationToken);

                        var log = _context.SyncLog();
                        var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
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
                                //BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer = true
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return paymentOptions.Id;
                    }

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Department Name Already Exist");
                }
            }
        }
    }
}
