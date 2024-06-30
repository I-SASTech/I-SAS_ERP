using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

using System.Linq;
using Focus.Business.Exceptions;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.ManualAttendance.Command
{
    public class AddManualAttendence : IRequest<Guid>
    {

        public ManualAttendenceLookUpModel ManualAttendence { get; set; }
        public class Handler : IRequestHandler<AddManualAttendence, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;
            public readonly IMediator _mediator;
            private string _code;
            //Create logger interface variable for log error
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<AddManualAttendence> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddManualAttendence request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.ManualAttendence.Id == Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        DateTime? checkInDate = null;
                        DateTime? checkOutDate = null;
                        
                        if(request.ManualAttendence.IsPreviousAttendence)
                        {
                            checkInDate = request.ManualAttendence.CheckIn;
                            checkOutDate = request.ManualAttendence.CheckOut;
                        }
                        else if (!request.ManualAttendence.IsOnLeave)
                        {
                             if (request.ManualAttendence.IsAbsent)
                            {
                                request.ManualAttendence.Date = DateTime.Now;
                            }
                            else
                            {
                                if (request.ManualAttendence.CheckType == "checkIn")
                                {
                                    checkInDate = DateTime.Now;
                                    request.ManualAttendence.Date = DateTime.Now;
                                }
                                if (request.ManualAttendence.CheckType == "checkOut")
                                {
                                    throw new ObjectAlreadyExistsException("First Select CheckIn");

                                }
                            }

                            

                        }
                        else if (request.ManualAttendence.IsOnLeave)
                        {
                            request.ManualAttendence.Date= DateTime.Now;
                        }
                       

                        var manual = new Focus.Domain.Entities.ManualAttendance
                        {

                            CheckIn = checkInDate,
                            Date = request.ManualAttendence.Date,
                            CheckOut = checkOutDate,
                            EmployeeId = request.ManualAttendence.EmployeeId,
                            IsCheckIn = request.ManualAttendence.IsCheckIn,
                            IsCheckOut = request.ManualAttendence.IsCheckOut,
                            IsOnLeave = request.ManualAttendence.IsOnLeave,
                            IsAbsent = request.ManualAttendence.IsAbsent,


                        };
                        await _context.ManualAttendances.AddAsync(manual, cancellationToken);





                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                          
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);
                        // Return Product id after successfully updating data
                        return manual.Id;
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        if (request.ManualAttendence.CheckType == "checkOutAll")
                        {
                            var employeeList= _context.ManualAttendances.AsNoTracking()
                        .Where(x => x.Date.Date == DateTime.Now.Date)
                        .ToList();
                            if(employeeList.Count==0)
                            {
                                throw new ObjectAlreadyExistsException("There is No Any attendence of Employee");
                            }
                            foreach (var employee in employeeList)
                            {
                                if(employee.IsCheckIn)
                                {
                                    employee.CheckOut = DateTime.Now;
                                    employee.IsCheckOut = true;
                                    _context.ManualAttendances.Update(employee);
                                }
                            }
                            await _context.SaveChangesAsync(cancellationToken);
                            return request.ManualAttendence.Id;
                        }
                        else
                        {
                            var purchase = await _context.ManualAttendances
                         .FindAsync(request.ManualAttendence.Id);

                            if (request.ManualAttendence.CheckType == "Not Check")
                            {
                                purchase.CheckIn = request.ManualAttendence.CheckIn;
                                purchase.CheckOut = request.ManualAttendence.CheckOut;
                            }
                            else if (request.ManualAttendence.CheckType == "onLeave")
                            {
                                purchase.CheckIn = null;
                                purchase.CheckOut = null;
                            }
                            else if (request.ManualAttendence.CheckType == "absent")
                            {
                                purchase.CheckIn = null;
                                purchase.CheckOut = null;
                            }

                            else if (request.ManualAttendence.CheckType == "checkIn")
                            {
                                throw new ObjectAlreadyExistsException("You Cannot Change CheckIn Time Again");
                            }
                          else  if (request.ManualAttendence.CheckType == "checkOut")
                            {
                                purchase.CheckOut = DateTime.Now;
                            }





                            purchase.EmployeeId = request.ManualAttendence.EmployeeId;
                            purchase.IsCheckIn = request.ManualAttendence.IsCheckIn;
                            purchase.IsCheckOut = request.ManualAttendence.IsCheckOut;
                            purchase.IsOnLeave = request.ManualAttendence.IsOnLeave;
                            purchase.IsAbsent = request.ManualAttendence.IsAbsent;

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                              
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);
                            return purchase.Id;
                        }
                       
                    }
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }
        }
    }
}