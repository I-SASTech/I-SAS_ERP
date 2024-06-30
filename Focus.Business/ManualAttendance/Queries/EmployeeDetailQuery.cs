using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ManualAttendance.Queries
{
    public class EmployeeDetailQuery : IRequest<ManualAttendenceLookUpModel>
    {
        public Guid Id { get; set; }
        public bool IsVariance { get; set; }
        public Guid ProductId { get; set; }

        public class Handler : IRequestHandler<EmployeeDetailQuery, ManualAttendenceLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<EmployeeDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<ManualAttendenceLookUpModel> Handle(EmployeeDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var color = await _context.ManualAttendances.FindAsync(request.Id);



                    if (color != null)
                    {
                       

                        return new ManualAttendenceLookUpModel()
                        {
                            Id = color.Id,
                            CheckIn = color.CheckIn,
                            CheckOut = color.CheckOut,
                            Date = color.Date,
                            EmployeeName = color.EmployeeRegistration.EnglishName,
                            EmployeeNameAr = color.EmployeeRegistration.ArabicName,
                        };
                    }
                    throw new NotFoundException(nameof(color), request.Id);
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