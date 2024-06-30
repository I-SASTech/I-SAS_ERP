using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.CompanyActionProcess.Queries
{
    public class DefaultProcessList : IRequest<bool>
    {
        public class Handler : IRequestHandler<DefaultProcessList, bool>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context,
                ILogger<GetProcessListQuery> logger,
                IMapper mapper, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _mediator = mediator;
            }
            public async Task<bool> Handle(DefaultProcessList request, CancellationToken cancellationToken)
            {
                try
                {

                    var list = new List<CompanyProcess>();

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    //Purchase Process 
                    list.Add(new CompanyProcess
                    {
                        ProcessName = "Add",
                        ProcessNameArabic = "يضيف",
                        Type = "Purchase",
                        Status = true
                    });

                    list.Add(new CompanyProcess
                    {
                        ProcessName = "Update",
                        ProcessNameArabic = "تحديث",
                        Type = "Purchase",
                        Status = true
                    });

                    list.Add(new CompanyProcess
                    {
                        ProcessName = "Attachment",
                        ProcessNameArabic = "مرفق",
                        Type = "Purchase",
                        Status = true
                    });
                    list.Add(new CompanyProcess
                    {
                        ProcessName = "Add Payment",
                        ProcessNameArabic = "إضافة الدفع",
                        Type = "Purchase",
                        Status = true
                    });
                    //Sales Process 
                    list.Add(new CompanyProcess
                    {
                        ProcessName = "Add",
                        ProcessNameArabic = "يضيف",
                        Type = "Sales",
                        Status = true
                    });

                    list.Add(new CompanyProcess
                    {
                        ProcessName = "Update",
                        ProcessNameArabic = "تحديث",
                        Type = "Sales",
                        Status = true
                    });

                    list.Add(new CompanyProcess
                    {
                        ProcessName = "Attachment",
                        ProcessNameArabic = "مرفق",
                        Type = "Sales",
                        Status = true
                    });

                    list.Add(new CompanyProcess
                    {
                        ProcessName = "Add Payment",
                        ProcessNameArabic = "إضافة الدفع",
                        Type = "Sales",
                        Status = true
                    });

                    await _context.CompanyProcess.AddRangeAsync(list, cancellationToken);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);

                    return true;

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }

    }
}
