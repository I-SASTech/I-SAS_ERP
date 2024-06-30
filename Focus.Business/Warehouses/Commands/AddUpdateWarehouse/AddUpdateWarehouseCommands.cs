using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Domain.Interface;
using Microsoft.Extensions.Caching.Memory;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Models;
using System.Collections.Generic;

namespace Focus.Business.Warehouses.Commands.AddUpdateWarehouse
{
    public class AddUpdateWarehouseCommands : IRequest<Message>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        public string StoreID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string LicenseNo { get; set; }
        public DateTime? LicenseExpiry { get; set; }
        public string CivilDefenceLicenseNo { get; set; }
        public DateTime? CivilDefenceLicenseExpiry { get; set; }
        public string CCTVLicenseNo { get; set; }
        public DateTime? CCTVLicenseExpiry { get; set; }
        public bool isActive { get; set; }
        public Guid CompanyId { get; set; }
        public string NameArabic { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateWarehouseCommands, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;
            private readonly IPrincipal _principal;
            private readonly IMemoryCache _memoryCache;
            private readonly IUserHttpContextProvider _httpContextProvider;
            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateWarehouseCommands> logger, IMediator mediator, IPrincipal principal, IMemoryCache memoryCache,
                IUserHttpContextProvider httpContextProvider)
            {
                _context = context;
                _logger = logger;
                _memoryCache = memoryCache;
                _httpContextProvider = httpContextProvider;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AddUpdateWarehouseCommands request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    var cacheKey = "WareHouse" + _httpContextProvider.GetUserId();
                    if (request.Id != Guid.Empty)
                    {
                        //Data already exist ; Update data
                        //get data from database using id
                        if (request.CompanyId != Guid.Empty)
                        {
                            _context.DisableTenantFilter = true;
                        }
                        var warehouseInformations = await _context.Warehouses.FindAsync(request.Id);
                        if (warehouseInformations == null)
                            throw new NotFoundException(request.Name, request.Id);

                        if (request.Name != "")
                        {
                            var isWarehouseNameExist = await _context.Warehouses.AnyAsync(x => x.Name == request.Name, cancellationToken);
                            if (warehouseInformations.Name != request.Name && isWarehouseNameExist)
                                throw new ObjectAlreadyExistsException("Warehouse " + request.Name + " Already Exist");
                            if (warehouseInformations.NameArabic != request.NameArabic && isWarehouseNameExist)
                                throw new ObjectAlreadyExistsException("Warehouse " + request.NameArabic + " Already Exist");
                        }
                        else
                        {
                            var isWarehouseNameExist = await _context.Warehouses.AnyAsync(x => x.NameArabic == request.NameArabic, cancellationToken);
                            if (warehouseInformations.NameArabic != request.NameArabic && isWarehouseNameExist)
                                throw new ObjectAlreadyExistsException("Warehouse " + request.NameArabic + " Already Exist");
                        }


                        warehouseInformations.StoreID = request.StoreID;
                        warehouseInformations.Name = request.Name;
                        warehouseInformations.NameArabic = request.NameArabic;
                        warehouseInformations.Address = request.Address;
                        warehouseInformations.City = request.City;
                        warehouseInformations.Country = request.Country;
                        warehouseInformations.ContactNo = request.ContactNo;
                        warehouseInformations.Email = request.Email;
                        warehouseInformations.LicenseNo = request.LicenseNo;
                        warehouseInformations.LicenseExpiry = request.LicenseExpiry;
                        warehouseInformations.CivilDefenceLicenseNo = request.CivilDefenceLicenseNo;
                        warehouseInformations.CivilDefenceLicenseExpiry = request.CivilDefenceLicenseExpiry;
                        warehouseInformations.CCTVLicenseNo = request.CCTVLicenseNo;
                        warehouseInformations.CCTVLicenseExpiry = request.CCTVLicenseExpiry;
                        warehouseInformations.isActive = request.isActive;

                        //Save changes to database
                        if (request.CompanyId != Guid.Empty)
                        {
                            warehouseInformations.isActive = request.isActive;
                        }

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
                        if (request.CompanyId != Guid.Empty)
                        {
                            _context.SetModified(warehouseInformations, "CompanyId", request.CompanyId);
                            _context.SaveChangesAfter();
                        }
                        _memoryCache.Remove(cacheKey);
                        // Return Additional Company Information id after successfully updating data
                        var message = new Message();

                        message.Id = warehouseInformations.Id;
                        message.IsAddUpdate = "Data has been Updated successfully";
                        return message;
                    }
                    else
                    {
                        if (request.CompanyId != Guid.Empty)
                        {
                            _context.DisableTenantFilter = true;
                        }
                        //Check Additional Company Information name is already exists
                        if (request.Name != "" && request.NameArabic != "")
                        {
                            var isWarehouseNameExist = await _context.Warehouses.AnyAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken);
                            if (isWarehouseNameExist)
                                throw new ObjectAlreadyExistsException("Warehouse " + request.Name + " " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            var isWarehouseNameExist = await _context.Warehouses.AnyAsync(x => x.Name == request.Name, cancellationToken);
                            if (isWarehouseNameExist)
                                throw new ObjectAlreadyExistsException("Warehouse " + request.Name + " Already Exist");
                        }
                        else
                        {
                            var isWarehouseNameExist = await _context.Warehouses.AnyAsync(x => x.NameArabic == request.NameArabic, cancellationToken);
                            if (isWarehouseNameExist)
                                throw new ObjectAlreadyExistsException("Warehouse " + request.NameArabic + " Already Exist");
                        }



                        var message = new Message();
                        // Create a object of Additional Company Information class that made in entity

                        var warehouseNameExist = new Warehouse
                        {
                            StoreID = request.StoreID,
                            Name = request.Name,
                            NameArabic = request.NameArabic,
                            Address = request.Address,
                            City = request.City,
                            Country = request.Country,
                            ContactNo = request.ContactNo,
                            Email = request.Email,
                            LicenseNo = request.LicenseNo,
                            LicenseExpiry = request.LicenseExpiry,
                            CivilDefenceLicenseNo = request.CivilDefenceLicenseNo,
                            CivilDefenceLicenseExpiry = request.CivilDefenceLicenseExpiry,
                            CCTVLicenseNo = request.CCTVLicenseNo,
                            CCTVLicenseExpiry = request.CCTVLicenseExpiry,
                            isActive = request.isActive,

                        };

                        //Add Additional Company Information to database
                        await _context.Warehouses.AddAsync(warehouseNameExist, cancellationToken);

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
                                //                  BranchId = branch.Id,
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

                        if (request.CompanyId != Guid.Empty)
                        {
                            _context.SetModified(warehouseNameExist, "CompanyId", request.CompanyId);
                            _context.SaveChangesAfter();
                        }
                        message.Id = warehouseNameExist.Id;
                        message.IsAddUpdate = "Data has been added successfully";
                        return message;
                    }

                }
                catch (NotFoundException notFoundException)
                {
                    var message = new Message
                    {
                        IsAddUpdate = notFoundException.Message
                    };
                    _logger.LogError(notFoundException.Message);

                    return message;
                }
                catch (Exception e)
                {
                    var message = new Message
                    {
                        IsAddUpdate = "Something went wrong! Contact to Support"
                    };
                    _logger.LogError(e.Message);

                    return message;
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
        }
    }
}
