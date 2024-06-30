using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Branches.Models;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Branches.Commands
{
    public class AddUpdateBranchesCommand : IRequest<Message>
    {
        public BranchesModel BranchesModel { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateBranchesCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateBranchesCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;


            }
            public async Task<Message> Handle(AddUpdateBranchesCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    //var locationId= _contextProvider.
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.BranchesModel.Id != Guid.Empty)
                    {
                        var branchDetail =  _context.Branches.FirstOrDefault(x=>x.Id== request.BranchesModel.Id);
                        if (branchDetail == null)
                            throw new NotFoundException("Branch Name", request.BranchesModel.BranchName);
                        

                        branchDetail.BranchName = request.BranchesModel.BranchName;
                        branchDetail.BranchType = request.BranchesModel.BranchType;
                        branchDetail.ContactNo = request.BranchesModel.ContactNo;
                        branchDetail.Address = request.BranchesModel.Address;
                        branchDetail.City = request.BranchesModel.City;
                        branchDetail.State = request.BranchesModel.State;
                        branchDetail.PostalCode = request.BranchesModel.PostalCode;
                        branchDetail.Country = request.BranchesModel.Country;
                        branchDetail.MainBranch = request.BranchesModel.MainBranch;
                        branchDetail.IsActive = request.BranchesModel.IsActive;
                        branchDetail.IsCentralized = request.BranchesModel.IsCentralized;
                        branchDetail.IsOnline = request.BranchesModel.IsOnline;
                        branchDetail.IsApproval = request.BranchesModel.IsApproval;

                        if (request.BranchesModel.MainBranch)
                        {
                            var record =  _context.Branches.AsNoTracking().Where(x => x.Id != request.BranchesModel.Id).ToList();
                            foreach (var item in record)
                            {
                                item.MainBranch = false;
                            }
                            _context.Branches.UpdateRange(record);
                            await _context.SaveChangesAsync(cancellationToken);

                        }

                        _context.Branches.Update(branchDetail);


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = branchDetail.Id,
                            Code = _code,
                            DocumentNo=branchDetail.Code
                        }, cancellationToken);
                        //Save changes to database
                         await _context.SaveChangesAsync(cancellationToken);

                        return new Message()
                        {
                            Id = branchDetail.Id,
                            IsSuccess = true,
                        };
                    }

                    var mainBranch = _context.Branches.Any(x => x.LocationId == request.BranchesModel.LocationId);
                    if (!mainBranch)
                    {
                        request.BranchesModel.MainBranch = true;
                    }

                    var branch = new Branch
                    {
                        Code = request.BranchesModel.Code,
                        BranchType = request.BranchesModel.BranchType,
                        BranchName = request.BranchesModel.BranchName,
                        ContactNo = request.BranchesModel.ContactNo,
                        Address = request.BranchesModel.Address,
                        City = request.BranchesModel.City,
                        State = request.BranchesModel.State,
                        PostalCode = request.BranchesModel.PostalCode,
                        Country = request.BranchesModel.Country,
                        LocationId = request.BranchesModel.LocationId,
                        BusinessId = request.BranchesModel.BusinessId,
                        Date = DateTime.Now,
                        MainBranch = request.BranchesModel.MainBranch,
                        IsCentralized = request.BranchesModel.IsCentralized,
                        IsOnline = request.BranchesModel.IsOnline,
                        IsApproval = request.BranchesModel.IsApproval,
                    IsActive = request.BranchesModel.IsActive,

                    };

                    //Add Department to database
                    await _context.Branches.AddAsync(branch, cancellationToken);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        BranchId = branch.Id,
                        Code = _code,
                        DocumentNo=branch.Code
                    }, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    return new Message()
                    {
                        Id = branch.Id,
                        IsSuccess = true,
                    };

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
        }
    }
}
