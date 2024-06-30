using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DispatchNotes.Queries.GetDispatchNoteDetails
{
    public class GetDispatchNoteDetailQuery : IRequest<DispatchNotesDetailLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetDispatchNoteDetailQuery, DispatchNotesDetailLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetDispatchNoteDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<DispatchNotesDetailLookUpModel> Handle(GetDispatchNoteDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var dispatchNote = await _context.DispatchNotes.FindAsync(request.Id);


                    if (dispatchNote != null)
                    {

                        var dispatchNoteItem = new List<DispatchNoteItemLookupModel>();
                        foreach (var item in dispatchNote.DispatchNoteItems)
                        {
                            dispatchNoteItem.Add(new DispatchNoteItemLookupModel
                            {
                                Id = item.Id,
                                ProductId = item.ProductId,
                                Quantity = item.Quantity,
                                UnitPrice = item.UnitPrice,
                                Product = new ProductDropdownLookUpModel
                                {
                                    Id = item.Product.Id,
                                    BarCode = item.Product.BarCode,
                                    TaxMethod = item.Product.TaxMethod,
                                    Code = item.Product.Code,
                                    EnglishName = item.Product.EnglishName,
                                    ArabicName = item.Product.ArabicName,
                                    //Inventory = (item.Product.Inventories == null || item.Product.Inventories == 0) ? null : new Inventory()
                                    //{
                                    //    CurrentQuantity = item.Product.Inventories.Where(x => x.ProductId == item.Product.Id).LastOrDefault().CurrentQuantity,
                                    //},
                                }

                            });
                        }

                        return new DispatchNotesDetailLookUpModel
                        {
                            Id = dispatchNote.Id,
                            Date = dispatchNote.Date,
                            SaleOrderId = dispatchNote.SaleOrderId,
                            IsClose = dispatchNote.IsClose,
                            ApprovalStatus = dispatchNote.ApprovalStatus,
                            RegistrationNo = dispatchNote.RegistrationNo,
                            CustomerId = dispatchNote.CustomerId,
                            Refrence = dispatchNote.Refrence,
                            Note = dispatchNote.Note,
                            DispatchNoteItems = dispatchNoteItem,
                        };
                    }
                    throw new NotFoundException(nameof(dispatchNote), request.Id);
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
