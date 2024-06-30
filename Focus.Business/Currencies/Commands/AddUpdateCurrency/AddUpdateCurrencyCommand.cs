using Focus.Business.CompanyAccountSetups.Command.AddUpdateCommand;
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

namespace Focus.Business.Currencies.Commands.AddUpdateCurrency
{
    public class AddUpdateCurrencyCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Sign { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }
        public bool Setup { get; set; }
        public string ArabicSign { get; set; }
        public string NameArabic { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateCurrencyCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;
            private readonly IMemoryCache _memoryCache;
            private readonly IUserHttpContextProvider _httpContextProvider;
            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private readonly IMediator _mediator;
            private string _code;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateCurrencyCommand> logger, IMediator mediator, IMemoryCache memoryCache,
            IUserHttpContextProvider httpContextProvider)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
                _memoryCache = memoryCache;
                _httpContextProvider = httpContextProvider;
            }

            public async Task<Guid> Handle(AddUpdateCurrencyCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var cacheKey = "CurrencyList" + _httpContextProvider.GetUserId();
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    if (request.Id != Guid.Empty)
                    {
                        //Data already exist ; Update data
                        //get data from database using id

                        var currencyDetail = await Context.Currencies.FindAsync(request.Id);
                        if (currencyDetail == null)
                            throw new NotFoundException(request.Name, request.Id);

                        if (request.Name != "" && request.NameArabic != "")
                        {
                            //var isCurrencyNameExist = await Context.Currencies.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken);
                            //if (currencyDetail.Name != request.Name && isCurrencyNameExist != null)
                            //    throw new ObjectAlreadyExistsException("Currency " + request.Name  + " Already Exist");
                            //if (currencyDetail.NameArabic != request.NameArabic && isCurrencyNameExist != null)
                            //    throw new ObjectAlreadyExistsException("Currency " + request.NameArabic + " Already Exist");
                        }

                        else if (request.Name != "")
                        {
                            var isCurrencyNameExist = await Context.Currencies.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
                            if (currencyDetail.Name != request.Name && isCurrencyNameExist != null)
                                throw new ObjectAlreadyExistsException("Currency " + request.Name + " Already Exist");
                        }

                        else
                        {
                            var isCurrencyNameExist = await Context.Currencies.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken);
                            if (currencyDetail.NameArabic != request.NameArabic && isCurrencyNameExist != null)
                                throw new ObjectAlreadyExistsException("Currency " + request.NameArabic + " Already Exist");
                        }

                        currencyDetail.Name = request.Name;
                        currencyDetail.NameArabic = request.NameArabic;
                        currencyDetail.Sign = request.Sign;
                        currencyDetail.IsActive = request.IsActive;
                        currencyDetail.Image = request.Image;
                        currencyDetail.ArabicSign = request.ArabicSign;

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
                                //    BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);

                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer=true
                        }, cancellationToken);


                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);
                        _memoryCache.Remove(cacheKey);
                        // Return Department id after successfully updating data
                        return currencyDetail.Id;
                        //Check Department exist

                    }

                    //Check Department name is already exists

                    if (request.Name != "" && request.NameArabic != "")
                    {
                        var isCurrencyNameExist = await Context.Currencies.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic==request.NameArabic,cancellationToken);
                        if (isCurrencyNameExist!=null)
                            throw new ObjectAlreadyExistsException("Currency " + request.Name + " " + request.NameArabic + " Already Exist");
                    }

                    else if (request.Name != "")
                    {
                        var isCurrencyNameExist = await Context.Currencies.FirstOrDefaultAsync(x => x.Name == request.Name,cancellationToken);
                        if (isCurrencyNameExist != null)
                            throw new ObjectAlreadyExistsException("Currency " + request.Name + " Already Exist");
                    }

                    else
                    {
                        var isCurrencyNameExist = await Context.Currencies.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic,cancellationToken);
                        if (isCurrencyNameExist != null)
                            throw new ObjectAlreadyExistsException("Currency " + request.NameArabic + " Already Exist");
                    }

                    var defaultCurrency = await Context.Currencies.FirstOrDefaultAsync(cancellationToken: cancellationToken);
                    string defaultValueEnglish = "";
                    string defaultValueArabic = "";
                    if (defaultCurrency == null)
                    {
                        defaultValueEnglish = " (Default)";
                        defaultValueArabic = " (تقصير)";
                    }
                    // Create a object of Department class that made in entity

                    var currency = new Currency
                    {
                        Name = request.Name + defaultValueEnglish,
                        NameArabic = request.NameArabic + defaultValueArabic,
                        Sign = request.Sign,
                        IsActive = request.IsActive,
                        ArabicSign = request.ArabicSign,
                        Image = request.Image
                    };

                    //Add Department to database
                    await Context.Currencies.AddAsync(currency, cancellationToken);

                    var log1 = Context.SyncLog();
                    var branches1 = await Context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                    var list1 = new List<SyncRecordModel>();
                    if (branches1.Any())
                    {
                        foreach (var branch in branches1)
                        {
                            var syncRecords = log1.Select(x => new SyncRecordModel
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

                            list1.AddRange(syncRecords);
                        }
                    }
                    else
                    {
                        var syncRecords = log1.Select(x => new SyncRecordModel
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
                            //    BranchId = branch.Id,
                            Code = _code,
                        }).ToList();

                        list1.AddRange(syncRecords);

                    }
                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = list1,
                        IsServer=true
                    }, cancellationToken);


                    await Context.SaveChangesAsync(cancellationToken);
                    if (request.Setup)
                    {
                        var companyAccountSetups = Context.CompanyAccountSetups.FirstOrDefault();
                        await _mediator.Send(new AddUpdateCompanyAccountSetupCommand()
                        {
                            CurrencyId = currency.Name,
                            Id = companyAccountSetups?.Id ?? Guid.Empty,
                            TaxMethod = companyAccountSetups?.TaxMethod,
                            TaxRateId = companyAccountSetups?.TaxRateId

                        }, cancellationToken);
                    }
                    _memoryCache.Remove(cacheKey);
                    return currency.Id;



                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Department Name Already Exist");
                }
            }
        }
    }
}
