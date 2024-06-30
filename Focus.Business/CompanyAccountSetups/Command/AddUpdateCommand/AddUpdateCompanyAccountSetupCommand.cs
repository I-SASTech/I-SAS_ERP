using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.CompanyAccountSetups.Command.AddUpdateCommand
{
    public class AddUpdateCompanyAccountSetupCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public Guid? InventoryAccountId { get; set; }
        public Guid? VatPayableAccountId { get; set; }
        public Guid? VatReceiableAccountId { get; set; }
        public Guid? SaleAccountId { get; set; }
        public Guid? DiscountPayableAccountId { get; set; }
        public Guid? DiscountReceivableAccountId { get; set; }
        public Guid? FreeofCostAccountId { get; set; }
        public Guid? StockInAccountId { get; set; }
        public Guid? StockOutAccountId { get; set; }
        public Guid? BundleAccountId { get; set; }
        public Guid? PromotionAccountId { get; set; }
        public Guid? BankId { get; set; }
        public Guid? CashId { get; set; }
        public string CurrencyId { get; set; }
        public string BarcodeType { get; set; }
        public string TaxMethod { get; set; }
        public Guid? TaxRateId { get; set; }


        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateCompanyAccountSetupCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateCompanyAccountSetupCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateCompanyAccountSetupCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    //FOR UPDATE
                    if (request.Id != Guid.Empty)
                    {
                        var companyAccountSetups = await Context.CompanyAccountSetups.FindAsync(request.Id);
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var isCompanyAccountSetupExist = await Context.CompanyAccountSetups
                            .Where(x => x.Id == request.Id)
                            .AnyAsync(cancellationToken);

                        if (companyAccountSetups == null)
                            throw new NotFoundException("NULL", request.Id);

                        if (companyAccountSetups.InventoryAccountId != request.InventoryAccountId && isCompanyAccountSetupExist)
                            return Guid.Empty;

                        companyAccountSetups.InventoryAccountId = request.InventoryAccountId;
                        companyAccountSetups.VatPayableAccountId = request.VatPayableAccountId;
                        companyAccountSetups.VatReceiableAccountId = request.VatReceiableAccountId;
                        companyAccountSetups.SaleAccountId = request.SaleAccountId;
                        companyAccountSetups.DiscountPayableAccountId = request.DiscountPayableAccountId;
                        companyAccountSetups.DiscountReceivableAccountId = request.DiscountReceivableAccountId;
                        companyAccountSetups.FreeofCostAccountId = request.FreeofCostAccountId;
                        companyAccountSetups.StockInAccountId = request.StockInAccountId;
                        companyAccountSetups.StockOutAccountId = request.StockOutAccountId;
                        companyAccountSetups.BundleAccountId = request.BundleAccountId;
                        companyAccountSetups.PromotionAccountId = request.PromotionAccountId;
                        companyAccountSetups.BankId = request.BankId;
                        companyAccountSetups.CashId = request.CashId;
                        companyAccountSetups.BarcodeType = request.BarcodeType;
                        companyAccountSetups.CurrencyId = request.CurrencyId;
                        companyAccountSetups.TaxMethod = request.TaxMethod;
                        companyAccountSetups.TaxRateId = request.TaxRateId;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);


                        await Context.SaveChangesAsync(cancellationToken);

                        return companyAccountSetups.Id;
                    }
                    //FOR NEW
                    else
                    {

                        var isCompanyAccountSetupExist = await Context.CompanyAccountSetups
                            .Where(x => x.Id == request.Id)
                            .AnyAsync(cancellationToken);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        if (!isCompanyAccountSetupExist)
                        {
                            var companyAccountSetups = new CompanyAccountSetup
                            {
                                InventoryAccountId = request.InventoryAccountId,
                                VatPayableAccountId = request.VatPayableAccountId,
                                VatReceiableAccountId = request.VatReceiableAccountId,
                                SaleAccountId = request.SaleAccountId,
                                DiscountPayableAccountId = request.DiscountPayableAccountId,
                                DiscountReceivableAccountId = request.DiscountReceivableAccountId,
                                FreeofCostAccountId = request.FreeofCostAccountId,
                                StockInAccountId = request.StockInAccountId,
                                StockOutAccountId = request.StockOutAccountId,
                                BundleAccountId = request.BundleAccountId,
                                PromotionAccountId = request.PromotionAccountId,
                                BankId=request.BankId,
                                CashId = request.CashId,
                                BarcodeType = request.BarcodeType,
                                CurrencyId = request.CurrencyId,
                                TaxMethod = request.TaxMethod,
                                TaxRateId = request.TaxRateId
                            };

                            await Context.CompanyAccountSetups.AddAsync(companyAccountSetups, cancellationToken);

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = Context.SyncLog(),
                                Code = _code,
                            }, cancellationToken);


                            await Context.SaveChangesAsync(cancellationToken);

                            return companyAccountSetups.Id;
                        }

                        return Guid.Empty;
                    }

                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Detail Already Exist");
                }
            }
        }
    }
}
