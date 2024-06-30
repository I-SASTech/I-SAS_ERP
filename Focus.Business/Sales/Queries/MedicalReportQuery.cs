using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Sales.Queries
{
   public class MedicalReportQuery : IRequest<List<MedicalReportModel>>
    {
        public Guid CategoryId { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public class Handler : IRequestHandler<MedicalReportQuery, List<MedicalReportModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, ILogger<MedicalReportQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<List<MedicalReportModel>> Handle(MedicalReportQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var productName = _context.Products.Where(x => x.CategoryId == request.CategoryId).Select(x => x.Id)
                        .ToList();
                    var inventory = _context.Inventories.Where(x =>
                            x.Date.Date >= request.FromTime.Date&& x.Date.Date <= request.ToTime.Date && productName.Contains(x.ProductId) &&
                            x.TransactionType == TransactionType.SaleInvoice)
                        .ToList();
                    var sale = _context.Sales.Where(x => inventory.Select(x => x.DocumentId).Contains(x.Id)).ToList();
                    var medicalReportModel = new List<MedicalReportModel>();
                    foreach (var inv in inventory)
                    {
                        medicalReportModel.Add(new MedicalReportModel()
                        {
                            InvoiceNumber = sale.FirstOrDefault(x=>x.Id==inv.DocumentId)?.RegistrationNo,
                            CurrentStock = inv.CurrentQuantity,
                            DoctorName = sale.FirstOrDefault(x => x.Id == inv.DocumentId)?.DoctorName,
                            HospitalName = sale.FirstOrDefault(x => x.Id == inv.DocumentId)?.HospitalName,
                            ProductName = inv.Product.EnglishName,
                            StockValue = inv.Quantity,
                            Time = inv.Date,

                        });
                    }
                   
                    return medicalReportModel;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }

   public class MedicalReportModel
   {
       public string InvoiceNumber { get; set; }
       public string CustomerName { get; set; }
       public string DoctorName { get; set; }
       public string HospitalName { get; set; }
       public decimal CurrentStock { get; set; }
       public decimal StockValue { get; set; }
       public DateTime Time { get; set; }
       public string ProductName { get; set; }
   }

}
