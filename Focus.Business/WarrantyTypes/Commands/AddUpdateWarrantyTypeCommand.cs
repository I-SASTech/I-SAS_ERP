using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.WarrantyTypes.Commands
{
    public class AddUpdateWarrantyTypeCommand : IRequest<Guid>
    {
        public WarrantyTypeLookUpModel WarrantyTypeLook { get; set; }
        public class Handler : IRequestHandler<AddUpdateWarrantyTypeCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateWarrantyTypeCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(AddUpdateWarrantyTypeCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    //Update
                    if (request.WarrantyTypeLook.Id != Guid.Empty)
                    {
                        var warrantyType = await _context.WarrantyTypes.FindAsync(request.WarrantyTypeLook.Id);
                        if (warrantyType == null)
                            throw new NotFoundException("warranty Type", request.WarrantyTypeLook.Name);


                        warrantyType.Name = request.WarrantyTypeLook.Name;
                        warrantyType.NameArabic = request.WarrantyTypeLook.NameArabic;
                        warrantyType.IsActive = request.WarrantyTypeLook.IsActive;

                        var log = _context.SyncLog();
                        var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        var list = new List<SyncRecordModel>();
                        if (branches.Any())
                        {


                            foreach (var branch in branches)
                            {
                                var syncRecords = log.Select(x => new SyncRecordModel
                                {
                                    Table = x.Table,
                                    ColumnId = x.ColumnId,
                                    CompanyId = x.CompanyId,
                                    ColumnType = x.ColumnType,
                                    Push = x.Push,
                                    Pull = x.Pull,
                                    Action = x.Action,
                                    ChangeDate = DateTime.Now,
                                    PullDate = x.PullDate,
                                    PushDate = x.PushDate,
                                    ColumnName = x.ColumnName,
                                    BranchId = branch.Id,
                                    Code = _code
                                }).ToList();

                                list.AddRange(syncRecords);
                            }
                        }
                        else
                        {
                            var syncRecords = log.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                //         BranchId = branch.Id,
                                Code = _code
                            }).ToList();

                            list.AddRange(syncRecords);
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer = true
                        }, cancellationToken);
                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        return warrantyType.Id;
                    }
                    else
                    {
                        var autoCode = await _context.WarrantyTypes.AsNoTracking().OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken);
                        if (autoCode != null)
                        {
                            if (string.IsNullOrEmpty(autoCode.Code))
                            {
                                request.WarrantyTypeLook.Code = GenerateCodeFirstTime();
                            }
                            request.WarrantyTypeLook.Code = GenerateNewCode(autoCode.Code);
                        }
                        else
                        {
                            request.WarrantyTypeLook.Code = GenerateCodeFirstTime();
                        }


                        var warranty = await _context.WarrantyTypes.FirstOrDefaultAsync(x => x.Code == request.WarrantyTypeLook.Code, cancellationToken: cancellationToken);

                        if (warranty != null)
                            throw new ObjectAlreadyExistsException("warranty " + request.WarrantyTypeLook.Name + " Already Exist");


                        var warrantyTypes = new WarrantyType
                        {
                            Code = request.WarrantyTypeLook.Code,
                            Name = request.WarrantyTypeLook.Name,
                            NameArabic = request.WarrantyTypeLook.NameArabic,
                            IsActive = request.WarrantyTypeLook.IsActive,
                        };

                        //Add Department to database
                        await _context.WarrantyTypes.AddAsync(warrantyTypes, cancellationToken);
                        var log = _context.SyncLog();
                        var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        var list = new List<SyncRecordModel>();
                        if (branches.Any())
                        {


                            foreach (var branch in branches)
                            {
                                var syncRecords = log.Select(x => new SyncRecordModel
                                {
                                    Table = x.Table,
                                    ColumnId = x.ColumnId,
                                    CompanyId = x.CompanyId,
                                    ColumnType = x.ColumnType,
                                    Push = x.Push,
                                    Pull = x.Pull,
                                    Action = x.Action,
                                    ChangeDate = DateTime.Now,
                                    PullDate = x.PullDate,
                                    PushDate = x.PushDate,
                                    ColumnName = x.ColumnName,
                                    BranchId = branch.Id,
                                    Code = _code
                                }).ToList();

                                list.AddRange(syncRecords);
                            }
                        }
                        else
                        {
                            var syncRecords = log.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                //         BranchId = branch.Id,
                                Code = _code
                            }).ToList();

                            list.AddRange(syncRecords);
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer = true
                        }, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                        return warrantyTypes.Id;
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
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }

            public string GenerateCodeFirstTime()
            {
                return "WT-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "WT-00000";
                autoNo++;
                var newCode = autoNo.ToString(format);
                return newCode;
            }
        }

    }
}
