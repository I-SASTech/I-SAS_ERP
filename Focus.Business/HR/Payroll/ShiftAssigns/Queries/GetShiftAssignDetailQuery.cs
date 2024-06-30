using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.HR.Payroll.ShiftAssigns.Model;

namespace Focus.Business.HR.Payroll.ShiftAssigns.Queries
{
    public class GetShiftAssignDetailQuery : IRequest<ShiftAssignModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetShiftAssignDetailQuery, ShiftAssignModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetShiftAssignDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<ShiftAssignModel> Handle(GetShiftAssignDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var shift = await _context.ShiftAssigns.FindAsync(request.Id);

                    if (shift != null)
                    {
                        return new ShiftAssignModel()
                        {
                            Id = shift.Id,
                            ShiftName = shift.ShiftName,
                            EmployeeId = shift.EmployeeId,
                            IsActive = shift.IsActive,
                            FromDate = shift.FromDate,
                            ToDate = shift.ToDate,
                            Description = shift.Description,
                            ReasonOfClosingShift = shift.ReasonOfClosingShift,
                            Monday = shift.Monday,
                            Tuesday = shift.Tuesday,
                            Wednesday = shift.Wednesday,
                            Thursday = shift.Thursday,
                            Friday = shift.Friday,
                            Saturday = shift.Saturday,
                            Sunday = shift.Sunday,
                            ShiftEmployeeAssigns = shift.ShiftEmployeeAssigns.Select(x => new ShiftAssignItemModel
                            {
                                Id = x.Id,
                                ShiftAssignId = x.ShiftAssignId,
                                EmployeeId = x.EmployeeId,
                                EmployeeName = x.Employee.EnglishName,
                                IsActive = x.IsActive,
                                FromDate = x.FromDate,
                                ToDate = x.ToDate,
                                Description = x.Description,
                                ReasonOfClosingShift = x.ReasonOfClosingShift
                            }).ToList()
                        };
                    }
                    throw new NotFoundException(nameof(shift), request.Id);
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
