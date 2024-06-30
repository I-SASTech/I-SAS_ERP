using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DayStarts.Queries.DayReport
{
    public class DayReportQuery : IRequest<DayReportLookupModel>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        public Guid? DayId { get; set; }

        public bool IsOpeningCash { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<DayReportQuery, DayReportLookupModel>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;


            //Create logger interface variable for log error
            public readonly ILogger Logger;


            //Constructor
            public Handler(IApplicationDbContext context, ILogger<DayReportQuery> logger, IUserHttpContextProvider contextProvider)
            {
                Context = context;
                Logger = logger;

            }

            public async Task<DayReportLookupModel> Handle(DayReportQuery request, CancellationToken cancellationToken)
            {
                try
                {


                    var dayStartList = Context.DayStarts.AsNoTracking().Where(x => !x.IsDayStart).ToList();

                    

                 
                  


                    return null;
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }

            }
        }
    }
}
