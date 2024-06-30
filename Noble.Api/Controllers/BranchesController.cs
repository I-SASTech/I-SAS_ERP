using System;
using Focus.Business.Branches.Commands;
using Focus.Business.Branches.Models;
using Focus.Business.Branches.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Focus.Business.BranchUsers.Queries;
using Focus.Business.BranchUsers.Commands;
using Focus.Business.BranchUsers.Models;
using Focus.Business.BranchSetups.Quries;
using Focus.Business.BranchSetups.Model;
using Focus.Business.BranchSetups.Commands;
using System.Security.Principal;
using Focus.Business.Extensions;
using Focus.Business.ProductBranches.Models;
using Focus.Business.ProductBranches.Commands;
using Focus.Business.TransactionApplicationLogs.Commands;
using Focus.Business.TransactionApplicationLogs.Model;
using Focus.Business.TransactionApplicationLogs.Quries;
using Focus.Business.PushPullQuery;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : BaseController
    {
        #region Braches

        private readonly IPrincipal _principal;
        public BranchesController( IPrincipal principal )
        {
         
            _principal = principal;
           
        }


        [Route("api/Branches/BranchPrefixDetail")]
        [HttpGet("BranchPrefixDetail")]
        public async Task<IActionResult> BranchPrefixDetail(Guid branchId)
        {
            var result = await Mediator.Send(new BranchSetupDetails
            {
                BranchId = branchId
            });

            return Ok(result);
        }

        [Route("api/Branches/SaveBranchPrefix")]
        [HttpPost("SaveBranchPrefix")]
        public async Task<IActionResult> SaveBranchPrefix([FromBody] BranchSetupLookupModel branchesModel)
        {
            var message = await Mediator.Send(new AddUpdateBranchSetupCommand
            {
                Pre = branchesModel,
            });
            return Ok(message);
        }


        [Route("api/Branches/GetBranchAutoCode")]
        [HttpGet("GetBranchAutoCode")]
        public async Task<IActionResult> GetBranchAutoCode(Guid businessId)
        {
            var result = await Mediator.Send(new GetBranchCodeQuery
            {
                BusinessId = businessId
            });

            return Ok(result);
        }

        [Route("api/Branches/SaveBranch")]
        [HttpPost("SaveBranch")]
        public async Task<IActionResult> SaveBranch([FromBody] BranchesModel branchesModel)
        {
            //var business = _principal.Identity.BusinessId();
            //var location = _principal.Identity.CompanyId();


            var message = await Mediator.Send(new AddUpdateBranchesCommand
            {
                BranchesModel = new BranchesModel
                {
                    Id = branchesModel.Id,
                    Code = branchesModel.Code,
                    BranchName = branchesModel.BranchName,
                    ContactNo = branchesModel.ContactNo,
                    Address = branchesModel.Address,
                    City = branchesModel.City,
                    State = branchesModel.State,
                    PostalCode = branchesModel.PostalCode,
                    Country = branchesModel.Country,
                    LocationName = branchesModel.LocationName,
                    BusinessId = branchesModel.BusinessId,
                    LocationId = branchesModel.LocationId,
                    IsActive = branchesModel.IsActive,
                    MainBranch = branchesModel.MainBranch,
                    IsOnline = branchesModel.IsOnline,
                    IsCentralized = branchesModel.IsCentralized,
                    IsApproval = branchesModel.IsApproval,
                    BranchType = branchesModel.BranchType,

                },
            });
            return Ok(message);
        }


        [Route("api/Branches/BranchList")]
        [HttpGet("BranchList")]
        public async Task<IActionResult> BranchList(string searchTerm, int? pageNumber, bool isDropdown, bool isLayout,Guid locationId)
        {
            var contact = await Mediator.Send(new GetBranchesListQuery
            {
                LocationId = locationId,
                SearchTerm = searchTerm,
                IsDropdown = isDropdown,
                IsLayout = isLayout,
                PageNumber = pageNumber ?? 1,

            });
            return Ok(contact);
        }

        [Route("api/Branches/GetBranchDetail")]
        [HttpGet("GetBranchDetail")]
        public async Task<IActionResult> GetBranchDetail(Guid id)
        {
            var result = await Mediator.Send(new GetBranchesDetailQuery()
            {
                Id = id
            });

            return Ok(result);
        }
        #endregion

        #region BranchUser

        [Route("api/Branches/SaveBranchUser")]
        [HttpPost("SaveBranchUser")]
        public async Task<IActionResult> SaveBranchUser([FromBody] BranchUserModel branchesModel)
        {
            var message = await Mediator.Send(new AddUpdateBranchUserCommand
            {
                BranchUserModel = branchesModel,
            });
            return Ok(message);
        }


        [Route("api/Branches/BranchUserList")]
        [HttpGet("BranchUserList")]
        public async Task<IActionResult> BranchUserList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var contact = await Mediator.Send(new GetBranchUserListQuery
            {
                SearchTerm = searchTerm,
                IsDropdown = isDropdown,
                PageNumber = pageNumber ?? 1,

            });
            return Ok(contact);
        }

        [Route("api/Branches/GetBranchUserDetail")]
        [HttpGet("GetBranchUserDetail")]
        public async Task<IActionResult> GetBranchUserDetail(string id)
        {
            var result = await Mediator.Send(new GetBranchUserDetailQuery()
            {
                Id = id
            });

            return Ok(result);
        }
        #endregion

        #region Product Branches
        [Route("api/Branches/SaveBranchesProducts")]
        [HttpPost("SaveBranchesProducts")]
        public async Task<IActionResult> SaveBranchesProducts([FromBody] ProductBranchesLookupModel branches)
        {
            var contact = await Mediator.Send(new ProductBranchesAddUpdateCommands
            {
                Branches = branches
            }) ;
            return Ok(contact);
        }
        
        [Route("api/Branches/SavePromotionBranches")]
        [HttpPost("SavePromotionBranches")]
        public async Task<IActionResult> SavePromotionBranches([FromBody] ProductBranchesLookupModel branches)
        {
            var contact = await Mediator.Send(new PromotionBranchesAddUpdateCommand
            {
                Branches = branches
            }) ;
            return Ok(contact);
        } 
        
        [Route("api/Branches/SaveBundleOfferBranches")]
        [HttpPost("SaveBundleOfferBranches")]
        public async Task<IActionResult> SaveBundleOfferBranches([FromBody] ProductBranchesLookupModel branches)
        {
            var contact = await Mediator.Send(new BundleOfferAddUpdateCommand
            {
                Branches = branches
            }) ;
            return Ok(contact);
        }
        #endregion

        #region TransactionLog

        [Route("api/Branches/SaveTransactionLog")]
        [HttpPost("SaveTransactionLog")]
        public async Task<IActionResult> SaveTransactionLog([FromBody] TransactionApplicationLogsLookupModel branchesModel)
        {
            var message = await Mediator.Send(new AddUpdateTransactionApplicationLogsCommand
            {
                Pre = branchesModel,
            });
            return Ok(message);
        }


        [Route("api/Branches/GetTransactionLogDetail")]
        [HttpGet("GetTransactionLogDetail")]
        public async Task<IActionResult> GetTransactionLogDetail(Guid locationId)
        {
            var result = await Mediator.Send(new TransactionApplicationLogsDetails()
            {
                LocationId = locationId
            });

            return Ok(result);
        }




        [Route("api/Branches/PushPullDetailQuery")]
        [HttpGet("PushPullDetailQuery")]
        public async Task<IActionResult> PushPullDetailQuery(string documentNumber,string logType)
        {
            var result = await Mediator.Send(new PushPullDetailQuery()
            {
                DocumentCode = documentNumber,
                LogType = logType,
            });

            return Ok(result);
        }


        [Route("api/Branches/PushPullRecordList")]
        [HttpGet("PushPullRecordList")]
        public async Task<IActionResult> PushPullRecordList(string searchTerm, int? pageNumber, bool isDropdown,DateTime? fromDate, DateTime? toDate,string logType)
        {
            var contact = await Mediator.Send(new PushPullListQuery
            {
                LogType = logType,
                FromDate = fromDate,
                ToDate = toDate,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,

            });
            return Ok(contact);
        }

        #endregion

    }
}
