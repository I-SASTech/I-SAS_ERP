using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Focus.Business.Common;
using Focus.Business.EmailConfigurationSetup.Model;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.SaleOrders.Commands.AddSaleOrder;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DeliveryChallans.Commands.AddDeliveryChallan
{
    public class AddUpdateDeliveryChallanCommand : IRequest<Message>
    {
        //Get all variable in entity
        public DeliveryChallanLookUp DeliveryChallan { get; set; }
        public bool IsClose { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateDeliveryChallanCommand, Message>
        {
            private readonly IApplicationDbContext _context;

            private readonly IHostingEnvironment _hostingEnvironment;
            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private readonly IUserHttpContextProvider _contextProvider;
            private string _code;
            private readonly ISendEmail sendEmail;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateDeliveryChallanCommand> logger, IMediator mediator, IUserHttpContextProvider contextProvider, IHostingEnvironment hostingEnvironment, ISendEmail _sendEmail)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
                _contextProvider = contextProvider;
                _hostingEnvironment = hostingEnvironment;
                sendEmail = _sendEmail;

            }


            public async Task<Message> Handle(AddUpdateDeliveryChallanCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    // Check Financial Year
                    var period = await _context.CompanySubmissionPeriods.AsNoTracking().FirstOrDefaultAsync(x => x.PeriodStart.Year == request.DeliveryChallan.Date.Year && x.PeriodStart.Month == request.DeliveryChallan.Date.Month, cancellationToken: cancellationToken);

                    if (period == null)
                        throw new NotFoundException("Financial Year Not Found", "");

                    if (period.IsPeriodClosed)
                        throw new ApplicationException("Financial year period closed");

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.DeliveryChallan.Id != Guid.Empty)
                    {

                        if (request.DeliveryChallan.IsReserved)
                        {
                            var deliveryChallans = await _context.DeliveryChallanReserveds.FindAsync(request.DeliveryChallan.Id);


                            if (deliveryChallans == null)
                                throw new NotFoundException("Good Receive", request.DeliveryChallan.RegistrationNo);


                            deliveryChallans.DeliveryId = request.DeliveryChallan.DeliveryId;
                            deliveryChallans.ShippingId = request.DeliveryChallan.ShippingId;
                            deliveryChallans.BillingId = request.DeliveryChallan.BillingId;
                            deliveryChallans.NationalId = request.DeliveryChallan.NationalId;
                            deliveryChallans.IsClose = request.DeliveryChallan.IsClose;
                            deliveryChallans.IsService = request.DeliveryChallan.IsService;
                            deliveryChallans.SaleInvoiceId = request.DeliveryChallan.SaleInvoiceId;
                            deliveryChallans.SaleOrderId = request.DeliveryChallan.SaleOrderId;
                            deliveryChallans.Date = request.DeliveryChallan.Date;
                            deliveryChallans.TemplateName = request.DeliveryChallan.TemplateName;
                            deliveryChallans.Description = request.DeliveryChallan.Description;
                            deliveryChallans.RegistrationNo = request.DeliveryChallan.RegistrationNo;
                            deliveryChallans.ApprovalStatus = request.DeliveryChallan.ApprovalStatus;
                            deliveryChallans.BilingAddress = request.DeliveryChallan.BilingAddress;
                            deliveryChallans.CustomerAddress = request.DeliveryChallan.CustomerAddress;

                            _context.DeliveryChallanReserverdItems.RemoveRange(deliveryChallans.DeliveryChallanReserverdItems);
                            deliveryChallans.DeliveryChallanReserverdItems = request.DeliveryChallan.DeliveryChallanItems.Select(x =>
                                new DeliveryChallanReserverdItem()
                                {
                                    IsActive = x.IsActive,
                                    ProductId = x.ProductId,
                                    UnitName = x.UnitName,
                                    ServiceProductId = x.ServiceProductId,
                                    Quantity = x.Quantity,
                                    Description = x.Description,
                                    UnitPrice = Math.Round(x.UnitPrice, 2),

                                }).ToList();

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                BranchId = deliveryChallans.BranchId,
                                Code = _code,
                                DocumentNo = deliveryChallans.RegistrationNo
                            }, cancellationToken);

                            //Save changes to database
                            await _context.SaveChangesAsync(cancellationToken);

                            var message = new Message
                            {
                                Id = deliveryChallans.Id,
                                IsAddUpdate = "Data has been Updated successfully"
                            };
                            return message;
                        }
                        else
                        {
                            var deliveryChallans = await _context.DeliveryChallans.FindAsync(request.DeliveryChallan.Id);

                            if (deliveryChallans == null)
                                throw new NotFoundException("Good Receive", request.DeliveryChallan.RegistrationNo);


                            deliveryChallans.IsClose = request.DeliveryChallan.IsClose;
                            deliveryChallans.DeliveryId = request.DeliveryChallan.DeliveryId;
                            deliveryChallans.ShippingId = request.DeliveryChallan.ShippingId;
                            deliveryChallans.BillingId = request.DeliveryChallan.BillingId;
                            deliveryChallans.NationalId = request.DeliveryChallan.NationalId;
                            deliveryChallans.IsService = request.DeliveryChallan.IsService;
                            deliveryChallans.SaleInvoiceId = request.DeliveryChallan.SaleInvoiceId;
                            deliveryChallans.SaleOrderId = request.DeliveryChallan.SaleOrderId;
                            deliveryChallans.Date = request.DeliveryChallan.Date;
                            deliveryChallans.TemplateName = request.DeliveryChallan.TemplateName;
                            deliveryChallans.Description = request.DeliveryChallan.Description;
                            deliveryChallans.RegistrationNo = request.DeliveryChallan.RegistrationNo;
                            deliveryChallans.ApprovalStatus = request.DeliveryChallan.ApprovalStatus;
                            deliveryChallans.BilingAddress = request.DeliveryChallan.BilingAddress;
                            deliveryChallans.CustomerAddress = request.DeliveryChallan.CustomerAddress;
                            deliveryChallans.CustomerId = request.DeliveryChallan.CustomerId;

                            _context.DeliveryChallanItems.RemoveRange(deliveryChallans.DeliveryChallanItems);
                            deliveryChallans.DeliveryChallanItems = request.DeliveryChallan.DeliveryChallanItems.Select(x =>
                                new DeliveryChallanItem()
                                {
                                    ProductId = x.ProductId,
                                    ServiceProductId = x.ServiceProductId,
                                    Description = x.Description,
                                    Quantity = x.Quantity,
                                    UnitName = x.UnitName,
                                    UnitPrice = Math.Round(x.UnitPrice, 2),

                                }).ToList();

                            if (request.DeliveryChallan.IsDeliveryChallan)
                            {
                                var reservedChallan = _context.DeliveryChallanReserveds.AsNoTracking()
                                    .Include(x => x.DeliveryChallanReserverdItems).FirstOrDefault(x => x.DeliveryChallanId == request.DeliveryChallan.Id);
                                if (reservedChallan != null)
                                {

                                    reservedChallan.IsClose = request.DeliveryChallan.IsClose;
                                    deliveryChallans.DeliveryId = request.DeliveryChallan.DeliveryId;
                                    deliveryChallans.ShippingId = request.DeliveryChallan.ShippingId;
                                    deliveryChallans.BillingId = request.DeliveryChallan.BillingId;
                                    deliveryChallans.NationalId = request.DeliveryChallan.NationalId;
                                    reservedChallan.IsService = request.DeliveryChallan.IsService;
                                    reservedChallan.SaleInvoiceId = request.DeliveryChallan.SaleInvoiceId;
                                    reservedChallan.SaleOrderId = request.DeliveryChallan.SaleOrderId;
                                    reservedChallan.Date = request.DeliveryChallan.Date;
                                    reservedChallan.TemplateName = request.DeliveryChallan.TemplateName;
                                    reservedChallan.Description = request.DeliveryChallan.Description;
                                    reservedChallan.RegistrationNo = request.DeliveryChallan.RegistrationNo;
                                    reservedChallan.ApprovalStatus = request.DeliveryChallan.ApprovalStatus;
                                    reservedChallan.BilingAddress = request.DeliveryChallan.BilingAddress;
                                    reservedChallan.CustomerAddress = request.DeliveryChallan.CustomerAddress;

                                    _context.DeliveryChallanReserverdItems.RemoveRange(reservedChallan.DeliveryChallanReserverdItems);
                                    reservedChallan.DeliveryChallanReserverdItems = request.DeliveryChallan.DeliveryChallanItems.Select(x =>
                                        new DeliveryChallanReserverdItem()
                                        {

                                            ProductId = x.ProductId,
                                            ServiceProductId = x.ServiceProductId,
                                            Description = x.Description,
                                            Quantity = x.Quantity,
                                            UnitName = x.UnitName,
                                            UnitPrice = Math.Round(x.UnitPrice, 2),

                                        }).ToList();




                                    //Add Department to database
                                    _context.DeliveryChallanReserveds.Update(reservedChallan);

                                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                                    {
                                        SyncRecordModels = _context.SyncLog(),
                                        BranchId = reservedChallan.BranchId,
                                        Code = _code,
                                        DocumentNo = reservedChallan.RegistrationNo
                                    }, cancellationToken);
                                }
                                //Save changes to database
                                await _context.SaveChangesAsync(cancellationToken);
                                
                            }
                            var message = new Message
                            {
                                Id = deliveryChallans.Id,
                                IsAddUpdate = "Data has been Updated successfully"
                            };
                            return message;
                        }
                    }
                    else
                    {
                        if (request.DeliveryChallan.IsReserved)
                        {
                            var deliveryChallan = new DeliveryChallanReserved
                            {
                                DeliveryId = request.DeliveryChallan.DeliveryId,
                                ShippingId = request.DeliveryChallan.ShippingId,
                                BillingId = request.DeliveryChallan.BillingId,
                                NationalId = request.DeliveryChallan.NationalId,
                                IsClose = request.DeliveryChallan.IsClose,
                                Date = request.DeliveryChallan.Date,
                                DeliveryChallanId = request.DeliveryChallan.DeliveryChallanId,
                                SaleOrderId = request.DeliveryChallan.SaleOrderId,
                                SaleInvoiceId = request.DeliveryChallan.SaleInvoiceId,
                                IsService = request.DeliveryChallan.IsService,
                                RegistrationNo = request.DeliveryChallan.RegistrationNo,
                                Description = request.DeliveryChallan.Description,
                                TemplateName = request.DeliveryChallan.TemplateName,
                                ApprovalStatus = request.DeliveryChallan.ApprovalStatus,
                                CustomerAddress = request.DeliveryChallan.CustomerAddress,
                                BilingAddress = request.DeliveryChallan.BilingAddress,
                                BranchId = request.DeliveryChallan.BranchId,
                                DeliveryChallanReserverdItems = request.DeliveryChallan.DeliveryChallanItems.Select(x =>
                                  new DeliveryChallanReserverdItem()
                                  {
                                      ProductId = x.ProductId,
                                      ServiceProductId = x.ServiceProductId,
                                      UnitPrice = x.UnitPrice,
                                      Quantity = x.Quantity,
                                      UnitName = x.UnitName,
                                      Description = x.Description,
                                      IsActive = x.IsActive,

                                  }).ToList()
                            };
                            //Add Department to database
                            await _context.DeliveryChallanReserveds.AddAsync(deliveryChallan, cancellationToken);

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                BranchId = deliveryChallan.BranchId,
                                Code = _code,
                                DocumentNo = deliveryChallan.RegistrationNo
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);
                            var message = new Message
                            {
                                Id = deliveryChallan.Id,
                                IsAddUpdate = "Data has been added successfully"
                            };
                            return message;
                        }
                        else
                        {
                            if (request.DeliveryChallan.SaleInvoiceId != null)
                            {
                                var deliveryChallanrEC = _context.DeliveryChallanReserveds
                                 .AsNoTracking()
                                 .Include(x => x.DeliveryChallanReserverdItems)
                                 .FirstOrDefault(x => x.SaleInvoiceId == request.DeliveryChallan.SaleInvoiceId);

                                if (deliveryChallanrEC != null)
                                {
                                    foreach (var reservd in deliveryChallanrEC.DeliveryChallanReserverdItems)
                                    {
                                        foreach (var x in request.DeliveryChallan.DeliveryChallanItems)
                                        {
                                            if (reservd.ProductId != null)
                                            {
                                                if (x.ProductId == reservd.ProductId)
                                                {
                                                    if (reservd.Quantity > 0)
                                                    {
                                                        reservd.Quantity -= x.Quantity;
                                                    }
                                                }
                                            }

                                            if (x.ServiceProductId == reservd.ServiceProductId)
                                            {
                                                if (reservd.Quantity > 0)
                                                {
                                                    reservd.Quantity -= x.Quantity;
                                                }
                                            }
                                        }

                                    }
                                    deliveryChallanrEC.IsClose = deliveryChallanrEC.DeliveryChallanReserverdItems.All(x => x.Quantity <= 0);
                                    _context.DeliveryChallanReserveds.Update(deliveryChallanrEC);
                                    if (deliveryChallanrEC.IsClose)
                                    {
                                        request.IsClose = true;
                                    }

                                }
                            }
                            else
                            {
                                if (request.DeliveryChallan.SaleOrderId != null)
                                {
                                    var deliveryChallanrEC = _context.DeliveryChallanReserveds
                                .AsNoTracking()
                                .Include(x => x.DeliveryChallanReserverdItems)
                                .FirstOrDefault(x => x.SaleOrderId == request.DeliveryChallan.SaleOrderId);

                                    if (deliveryChallanrEC != null)
                                    {
                                        foreach (var reservd in deliveryChallanrEC.DeliveryChallanReserverdItems)
                                        {
                                            foreach (var x in request.DeliveryChallan.DeliveryChallanItems)
                                            {
                                                if (reservd.ProductId != null)
                                                {
                                                    if (x.ProductId == reservd.ProductId)
                                                    {
                                                        if (reservd.Quantity > 0)
                                                        {
                                                            reservd.Quantity -= x.Quantity;

                                                        }
                                                    }
                                                }

                                                if (x.ServiceProductId == reservd.ServiceProductId)
                                                {
                                                    if (reservd.Quantity > 0)
                                                    {
                                                        reservd.Quantity -= x.Quantity;

                                                    }
                                                }
                                            }

                                        }
                                        deliveryChallanrEC.IsClose = deliveryChallanrEC.DeliveryChallanReserverdItems.All(x => x.Quantity <= 0);
                                        _context.DeliveryChallanReserveds.Update(deliveryChallanrEC);
                                    }
                                }
                            }

                            var deliveryChallan = new DeliveryChallan
                            {
                                IsClose = request.IsClose || request.DeliveryChallan.IsClose,
                                Date = request.DeliveryChallan.Date,
                                DeliveryId = request.DeliveryChallan.DeliveryId,
                                ShippingId = request.DeliveryChallan.ShippingId,
                                BillingId = request.DeliveryChallan.BillingId,
                                NationalId = request.DeliveryChallan.NationalId,
                                SaleOrderId = request.DeliveryChallan.SaleOrderId,
                                SaleInvoiceId = request.DeliveryChallan.SaleInvoiceId,
                                IsService = request.DeliveryChallan.IsService,
                                RegistrationNo = request.DeliveryChallan.RegistrationNo,
                                Description = request.DeliveryChallan.Description,
                                TemplateName = request.DeliveryChallan.TemplateName,
                                ApprovalStatus = request.DeliveryChallan.ApprovalStatus,
                                CustomerId = request.DeliveryChallan.CustomerId,
                                CustomerAddress = request.DeliveryChallan.CustomerAddress,
                                BilingAddress = request.DeliveryChallan.BilingAddress,
                                BranchId = request.DeliveryChallan.BranchId,
                                DeliveryChallanItems = request.DeliveryChallan.DeliveryChallanItems.Select(x =>
                                  new DeliveryChallanItem()
                                  {
                                      
                                      Description = x.Description,
                                      ProductId = x.ProductId,
                                      UnitName = x.UnitName,
                                      ServiceProductId = x.ServiceProductId,
                                      UnitPrice = x.UnitPrice,
                                      Quantity = x.Quantity,

                                  }).ToList(),




                            };
                            //Add Department to database
                            await _context.DeliveryChallans.AddAsync(deliveryChallan, cancellationToken);
                            if (request.DeliveryChallan.IsDeliveryChallan)
                            {
                                var challanReserved = new DeliveryChallanReserved
                                {
                                    IsClose = request.DeliveryChallan.IsClose,
                                    Date = request.DeliveryChallan.Date,
                                    DeliveryId = request.DeliveryChallan.DeliveryId,
                                    ShippingId = request.DeliveryChallan.ShippingId,
                                    BillingId = request.DeliveryChallan.BillingId,
                                    NationalId = request.DeliveryChallan.NationalId,
                                    DeliveryChallanId = deliveryChallan.Id,
                                    SaleOrderId = request.DeliveryChallan.SaleOrderId,
                                    SaleInvoiceId = request.DeliveryChallan.SaleInvoiceId,
                                    IsService = request.DeliveryChallan.IsService,
                                    RegistrationNo = request.DeliveryChallan.RegistrationNo,
                                    Description = request.DeliveryChallan.Description,
                                    TemplateName = request.DeliveryChallan.TemplateName,
                                    ApprovalStatus = request.DeliveryChallan.ApprovalStatus,
                                    CustomerAddress = request.DeliveryChallan.CustomerAddress,
                                    BilingAddress = request.DeliveryChallan.BilingAddress,
                                    BranchId = request.DeliveryChallan.BranchId,
                                    DeliveryChallanReserverdItems = request.DeliveryChallan.DeliveryChallanItems.Select(x =>
                                      new DeliveryChallanReserverdItem()
                                      {
                                          ProductId = x.ProductId,
                                          ServiceProductId = x.ServiceProductId,
                                          UnitPrice = x.UnitPrice,
                                          UnitName = x.UnitName,
                                          Quantity = x.Quantity,
                                          Description = x.Description,
                                          IsActive = x.IsActive,

                                      }).ToList(),



                                };
                                //Add Department to database
                                await _context.DeliveryChallanReserveds.AddAsync(challanReserved, cancellationToken);

                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = _context.SyncLog(),
                                    BranchId = challanReserved.BranchId,
                                    Code = _code,
                                    DocumentNo = challanReserved.RegistrationNo
                                }, cancellationToken);
                            }

                            await _context.SaveChangesAsync(cancellationToken);


                          


                            var message = new Message
                            {
                                Id = deliveryChallan.Id,
                                IsAddUpdate = "Data has been added successfully"
                            };
                            return message;
                        }
                    }


                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}
