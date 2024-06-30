using Focus.Business.Exceptions;
using Focus.Business.FinancialYearSettings.Models;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.FinancialYearSettings.Commands
{
    public class AddUpdateFinancialYearSettingCommand : IRequest<Guid>
    {
        public FinancialYearSettingModel FinancialYearSettingModel { get; set; }
        public class Handler : IRequestHandler<AddUpdateFinancialYearSettingCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateFinancialYearSettingCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            
            public async Task<Guid> Handle(AddUpdateFinancialYearSettingCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.FinancialYearSettingModel.Id != Guid.Empty)
                    {
                        var financialYear = await _context.FinancialYearSettings.FindAsync(request.FinancialYearSettingModel.Id);
                        if (financialYear == null)
                            throw new NotFoundException("Financial Year Setting", "");

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }


                        financialYear.ClosingType = request.FinancialYearSettingModel.ClosingType;
                        financialYear.IsAutoClosing = request.FinancialYearSettingModel.IsAutoClosing;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return financialYear.Id;
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var financialYear = new FinancialYearSetting
                        {
                            ClosingType = request.FinancialYearSettingModel.ClosingType,
                            IsAutoClosing = request.FinancialYearSettingModel.IsAutoClosing,
                        };

                        await _context.FinancialYearSettings.AddAsync(financialYear, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return financialYear.Id;
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Department Name Already Exist");
                }
            }
        }

    }
}
