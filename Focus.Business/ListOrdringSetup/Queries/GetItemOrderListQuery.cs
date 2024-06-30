using Focus.Business.Interface;
using Focus.Business.ListOrdringSetup.Models;
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

namespace Focus.Business.ListOrdringSetup.Queries
{
    public class GetItemOrderListQuery : IRequest<ListOrderSetupModel>
    {
        public class Handler : IRequestHandler<GetItemOrderListQuery, ListOrderSetupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<GetItemOrderListQuery> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<ListOrderSetupModel> Handle(GetItemOrderListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var customerListerOrderCount = _context.ListOrderSetups.Count(x => x.DocumentName == "Customer");
                    var supplierListerOrderCount = _context.ListOrderSetups.Count(x => x.DocumentName == "Supplier");

                    

                    if (customerListerOrderCount == 0)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var list = new List<ListOrderSetup>();
                        list.Add(new ListOrderSetup
                        {
                            DisplayName = "Code",
                            Name = "code",
                            Sequence = 1,
                            DocumentName = "Customer"
                        });
                        list.Add(new ListOrderSetup
                        {
                            DisplayName = "Customer Type",
                            Name = "customerType",
                            Sequence = 2,
                            DocumentName = "Customer"
                        });
                        list.Add(new ListOrderSetup
                        {
                            DisplayName = "Customer Name English",
                            Name = "customerNameEn",
                            Sequence = 3,
                            DocumentName = "Customer"
                        });
                        list.Add(new ListOrderSetup
                        {
                            DisplayName = "Customer Name Arabic",
                            Name = "customerNameAr",
                            Sequence = 4,
                            DocumentName = "Customer"
                        });
                        list.Add(new ListOrderSetup
                        {
                            DisplayName = "Company Name English",
                            Name = "companyNameEn",
                            Sequence = 5,
                            DocumentName = "Customer"
                        });
                        list.Add(new ListOrderSetup
                        {
                            DisplayName = "Commercial Reg No",
                            Name = "crn",
                            Sequence = 6,
                            DocumentName = "Customer"
                        });
                        list.Add(new ListOrderSetup
                        {
                            DisplayName = "Registration Date",
                            Name = "registrationDate",
                            Sequence = 7,
                            DocumentName = "Customer"
                        });
                        list.Add(new ListOrderSetup
                        {
                            DisplayName = "Customer Category",
                            Name = "customerCategory",
                            Sequence = 8,
                            DocumentName = "Customer"
                        });
                        list.Add(new ListOrderSetup
                        {
                            DisplayName = "Customer Running Balance",
                            Name = "runningBalance",
                            Sequence = 9,
                            DocumentName = "Customer"
                        });
                        list.Add(new ListOrderSetup
                        {
                            DisplayName = "Payment Terms",
                            Name = "paymentTerms",
                            Sequence = 10,
                            DocumentName = "Customer"
                        });

                        await _context.ListOrderSetups.AddRangeAsync(list);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,

                        }, cancellationToken);

                        await _context.SaveChangesAsync();

                    }
                    if (supplierListerOrderCount == 0)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var listSupplier = new List<ListOrderSetup>();
                        listSupplier.Add(new ListOrderSetup
                        {
                            DisplayName = "Code",
                            Name = "code",
                            Sequence = 1,
                            DocumentName = "Supplier"
                        });
                        listSupplier.Add(new ListOrderSetup
                        {
                            DisplayName = "Supplier Type",
                            Name = "customerType",
                            Sequence = 2,
                            DocumentName = "Supplier"
                        });
                        listSupplier.Add(new ListOrderSetup
                        {
                            DisplayName = "Supplier Name English",
                            Name = "customerNameEn",
                            Sequence = 3,
                            DocumentName = "Supplier"
                        });
                        listSupplier.Add(new ListOrderSetup
                        {
                            DisplayName = "Supplier Name Arabic",
                            Name = "customerNameAr",
                            Sequence = 4,
                            DocumentName = "Supplier"
                        });
                        listSupplier.Add(new ListOrderSetup
                        {
                            DisplayName = "Company Name English",
                            Name = "companyNameEn",
                            Sequence = 5,
                            DocumentName = "Supplier"
                        });
                        listSupplier.Add(new ListOrderSetup
                        {
                            DisplayName = "Commercial Reg No",
                            Name = "crn",
                            Sequence = 6,
                            DocumentName = "Supplier"
                        });
                        listSupplier.Add(new ListOrderSetup
                        {
                            DisplayName = "Registration Date",
                            Name = "registrationDate",
                            Sequence = 7,
                            DocumentName = "Supplier"
                        });
                        listSupplier.Add(new ListOrderSetup
                        {
                            DisplayName = "Supplier Category",
                            Name = "customerCategory",
                            Sequence = 8,
                            DocumentName = "Supplier"
                        });
                        listSupplier.Add(new ListOrderSetup
                        {
                            DisplayName = "Supplier Running Balance",
                            Name = "runningBalance",
                            Sequence = 9,
                            DocumentName = "Supplier"
                        });
                        listSupplier.Add(new ListOrderSetup
                        {
                            DisplayName = "Payment Terms",
                            Name = "paymentTerms",
                            Sequence = 10,
                            DocumentName = "Supplier"
                        });

                        await _context.ListOrderSetups.AddRangeAsync(listSupplier);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,

                        }, cancellationToken);

                        await _context.SaveChangesAsync();
                    }

                    var customerListerOrder = await _context.ListOrderSetups.Where(x => x.DocumentName == "Customer").Select(x => new ListOrderSetupLookupModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        DisplayName = x.DisplayName,
                        DocumentName = x.DocumentName,
                        Sequence = x.Sequence,
                    }).ToListAsync();

                    var supplierListerOrder = await _context.ListOrderSetups.Where(x => x.DocumentName == "Supplier").Select(x => new ListOrderSetupLookupModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        DisplayName = x.DisplayName,
                        DocumentName = x.DocumentName,
                        Sequence = x.Sequence,
                    }).ToListAsync();

                    return new ListOrderSetupModel
                    {
                        CustomerListView = customerListerOrder,
                        SupplierListView = supplierListerOrder,
                    };
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
