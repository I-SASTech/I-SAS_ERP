using Focus.Business.ProductionBatchs;
using Focus.Business.ProductionBatchs.Commands.AddProductionBatch;
using Focus.Business.ProductionBatchs.Queries.GetProductionBatchDetails;
using Focus.Business.ProductionBatchs.Queries.GetProductionBatchList;
using Focus.Business.ProductionBatchs.Queries.GetProductionBatchRegistrationNo;
using Focus.Business.RecipeNos;
using Focus.Business.RecipeNos.Commands.AddRecipeNo;
using Focus.Business.RecipeNos.Queries.GetRecipeNoDetails;
using Focus.Business.RecipeNos.Queries.GetRecipeNoList;
using Focus.Business.RecipeNos.Queries.GetRecipeNoRegistrationNo;
using Focus.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.Inventories.Queries.GetLatestInventory;
using Focus.Business.ProductionBatchs.ProcessContractor;
using Focus.Business.ProductionBatchs.Queries.GetProductionBatchDetails.ProductionStockTransfeDetail;
using Focus.Business.ProductionModule.GatePasses.Queries.GetGatePassesDetails;
using Focus.Business.ProductionModule.SampleRequests;
using Focus.Business.ProductionModule.SampleRequests.Commands;
using Focus.Business.ProductionModule.SampleRequests.Queries.GetSampleRequestDetails;
using Focus.Business.ProductionModule.SampleRequests.Queries.GetSampleRequestList;
using Focus.Business.ProductionModule.SampleRequests.Queries.GetSampleRequestRegistrationNo;
using Focus.Business.ProductionModule.Processes;
using Focus.Business.ProductionModule.Processes.Commands;
using Focus.Business.ProductionModule.Processes.Queries.GetProcessesDetails;
using Focus.Business.ProductionModule.Processes.Queries.GetProcessesList;
using Focus.Business.ProductionModule.Processes.Queries.GetProcessesRegistrationNo;
using Focus.Domain.Enum;
using Noble.Api.Models;
using Focus.Business.SyncRecords.Commands;

namespace Noble.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : BaseController
    {
        private readonly IApplicationDbContext _context;
        private string _code;

        public BatchController(IApplicationDbContext context)
        {
            _context = context;
        }

        #region For RecipeNo
        [Route("api/Purchase/RecipeNoAutoGenerateNo")]
        [HttpGet("RecipeNoAutoGenerateNo")]
        public async Task<IActionResult> RecipeNoAutoGenerateNo()
        {
            var autoNo = await Mediator.Send(new GetRecipeNoRegistrationNoQuery());
            return Ok(autoNo);
        }

        [Route("api/Purchase/SaveRecipeNoInformation")]
        [HttpPost("SaveRecipeNoInformation")]
        public async Task<IActionResult> SaveRecipeNoInformation([FromBody] RecipeNoLookupModel recipeNo)
        {
            if (recipeNo.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetRecipeNoRegistrationNoQuery());
                recipeNo.RegistrationNo = autoNo;
                var id = await Mediator.Send(new AddRecipeNoCommand()
                {
                    recipeNo = recipeNo
                });

            }
            else
            {
                var id = await Mediator.Send(new AddRecipeNoCommand()
                {
                    recipeNo = recipeNo
                });
            }


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Purchase/RecipeNoList")]
        [HttpGet("RecipeNoList")]
        public async Task<IActionResult> RecipeNoList(string searchTerm, int? pageNumber, ApprovalStatus status, bool isDropdown)
        {
            var recipeNo = await Mediator.Send(new GetRecipeNoListQuery
            {
                SearchTerm = searchTerm,
                Status = status,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(recipeNo);
        }

        [Route("api/Purchase/RecipeNoDetail")]
        [HttpGet("RecipeNoDetail")]
        public async Task<IActionResult> RecipeNoDetail(Guid id)
        {
            var recipeNo = await Mediator.Send(new GetRecipeNoDetailQuery()
            {
                Id = id
            });
            return Ok(recipeNo);
        }

        [Route("api/Purchase/ChangeRecipeStatus")]
        [HttpGet("ChangeRecipeStatus")]
        public async Task<IActionResult> ChangeRecipeStatus(Guid id, bool isActive)
        {
            var recipeNo = await _context.RecipeNos.FindAsync(id);
            recipeNo.IsActive = isActive;

            Random rnd = new Random();
            for (int i = 0; i < 11; i++)
            {
                _code += rnd.Next(0, 9).ToString();
            }
            await Mediator.Send(new AddUpdateSyncRecordCommand()
            {
                SyncRecordModels = _context.SyncLog(),
                Code = _code,
            });
            await _context.SaveChangesAsync();
            return Ok(true);
        }
        [Route("api/Batch/WarehouseItemDetail")]
        [HttpGet("WarehouseItemDetail")] 
        [Roles("CanAddProductionBatch")]
        public async Task<IActionResult> WarehouseItemDetail(Guid productId, Guid wareHouseId)
        {
            var currentInventory = await Mediator.Send(new GetLatestInventoryQuery()
            {
             ProductId   = productId,
             WareHouseId = wareHouseId
            });
            return Ok(currentInventory);
        }

        #endregion
        #region For ProductionBatch
        [Route("api/Purchase/ProductionBatchAutoGenerateNo")]
        [HttpGet("ProductionBatchAutoGenerateNo")]
        public async Task<IActionResult> ProductionBatchAutoGenerateNo()
        {
            var autoNo = await Mediator.Send(new GetProductionBatchRegistrationNoQuery());
            return Ok(autoNo);
        }

        [Route("api/Purchase/BatchStatus")]
        [HttpPost("BatchStatus")]
        public async Task<IActionResult> BatchStatus([FromBody] Focus.Business.ProductionBatchs.ProductionBatchStatusModel productionBatch)
        {
            var autoNo = await Mediator.Send(new ProductionBatchStatus
            {
                productionBatch = productionBatch
            });
            return Ok(autoNo);
        }

        [Route("api/Purchase/SaveProductionBatchInformation")]
        [HttpPost("SaveProductionBatchInformation")]
        public async Task<IActionResult> SaveProductionBatchInformation([FromBody] ProductionBatchLookupModel productionBatch)
        {
            if (productionBatch.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetProductionBatchRegistrationNoQuery());
                productionBatch.RegistrationNo = autoNo;
                var id = await Mediator.Send(new AddProductionBatchCommand()
                {
                    ProductionBatch = productionBatch
                });
                return Ok(id);
            }
            else
            {
                var id = await Mediator.Send(new AddProductionBatchCommand()
                {
                    ProductionBatch = productionBatch
                });
                return Ok(id);
            }



        }

        [Route("api/Purchase/ProductionBatchList")]
        [HttpGet("ProductionBatchList")]
        public async Task<IActionResult> ProductionBatchList(string searchTerm, int? pageNumber, ApprovalStatus status, bool isDropdown, Guid? branchId)
        {
            var productionBatch = await Mediator.Send(new GetProductionBatchListQuery
            {
                SearchTerm = searchTerm,
                Status = status,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
                BranchId = branchId,
            });

            return Ok(productionBatch);
        }

        [Route("api/Purchase/ProductionBatchDetail")]
        [HttpGet("ProductionBatchDetail")]
        public async Task<IActionResult> ProductionBatchDetail(Guid id)
        {
            var productionBatch = await Mediator.Send(new GetProductionBatchDetailQuery()
            {
                Id = id
            });
            return Ok(productionBatch);
        }

        #endregion

        #region Production Stock Transfer for Production
        [Route("api/Batch/ProductionStockTransferBatchDetail")]
        [HttpGet("ProductionStockTransferBatchDetail")]
        public async Task<IActionResult> ProductionStockTransferBatchDetail(Guid id)
        {
            var productionBatch = await Mediator.Send(new ProductionStockTransferDetailQuery()
            {
                Id = id
            });
            return Ok(productionBatch);
        }
        #endregion


        #region For SampleRequest
        [Route("api/Batch/SampleRequestAutoGenerateNo")]
        [HttpGet("SampleRequestAutoGenerateNo")]
        public async Task<IActionResult> SampleRequestAutoGenerateNo()
        {
            var autoNo = await Mediator.Send(new GetSampleRequestRegistrationNoQuery());
            return Ok(autoNo);
        }

        [Route("api/Batch/SaveSampleRequestInformation")]
        [HttpPost("SaveSampleRequestInformation")]
        public async Task<IActionResult> SaveSampleRequestInformation([FromBody] SampleRequestLookupModel sampleRequest)
        {
            if (sampleRequest.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddSampleRequestCommand()
                {
                    sampleRequest = sampleRequest
                });

            }
            else
            {
                var id = await Mediator.Send(new AddSampleRequestCommand()
                {
                    sampleRequest = sampleRequest
                });
            }


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Batch/SampleRequestList")]
        [HttpGet("SampleRequestList")]
        public async Task<IActionResult> SampleRequestList(string searchTerm, int? pageNumber, ApprovalStatus status, bool isDropdown)
        {
            var recipeNo = await Mediator.Send(new GetSampleRequestList
            {
                SearchTerm = searchTerm,
                Status = status,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(recipeNo);
        }

        [Route("api/Batch/SampleRequestDetail")]
        [HttpGet("SampleRequestDetail")]
        public async Task<IActionResult> SampleRequestDetail(Guid id)
        {
            var sampleRequest = await Mediator.Send(new GetSampleRequestDetailQuery()
            {
                Id = id
            });
            return Ok(sampleRequest);
        }
        [Route("api/Batch/ActionSampleRequestDetailQuery")]
        [HttpGet("ActionSampleRequestDetailQuery")]
        public async Task<IActionResult> ActionSampleRequestDetailQuery(Guid id, ApprovalStatus status)
        {
            var sampleRequest = await Mediator.Send(new ActionSampleRequestDetailQuery()
            {
                Id = id,
                Status = status,
            });
            return Ok(sampleRequest);
        }

        #endregion
        
        #region For Process
        [Route("api/Batch/ProcessAutoGenerateNo")]
        [HttpGet("ProcessAutoGenerateNo")]
        public async Task<IActionResult> ProcessAutoGenerateNo()
        {
            var autoNo = await Mediator.Send(new GetProcessesRegistrationNoQuery());
            return Ok(autoNo);
        }

        [Route("api/Batch/SaveProcessInformation")]
        [HttpPost("SaveProcessInformation")]
        public async Task<IActionResult> SaveProcessInformation([FromBody] ProcessesLookUpModel process)
        {
            if (process.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddProcessesCommand()
                {
                    process = process
                });

            }
            else
            {
                var id = await Mediator.Send(new AddProcessesCommand()
                {
                    process = process
                });
            }


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Batch/ProcessList")]
        [HttpGet("ProcessList")]
        public async Task<IActionResult> ProcessList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var process = await Mediator.Send(new GetProcessesListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(process);
        }

        [Route("api/Batch/ProcessDetail")]
        [HttpGet("ProcessDetail")]
        public async Task<IActionResult> ProcessDetail(Guid id)
        {
            var process = await Mediator.Send(new GetProcessesDetailQuery()
            {
                Id = id
            });
            return Ok(process);
        }

        [Route("api/Batch/AddProcessContractorItems")]
        [HttpPost("AddProcessContractorItems")]
        public async Task<IActionResult> AddProcessContractorItems([FromBody] ProcessContractorLookup contractorLookup)
        {
            if (contractorLookup.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddContractorCommand()
                {
                    ContractorLookup = contractorLookup
                });
                return Ok(id);

            }
            else
            {
                var id = await Mediator.Send(new AddContractorCommand()
                {
                    ContractorLookup = contractorLookup
                });
                return Ok(id);
            }


           
        }
        [Route("api/Batch/ProcessContractorList")]
        [HttpGet("ProcessContractorList")]
        public async Task<IActionResult> ProcessContractorList(Guid id)
        {
            var processList = await Mediator.Send(new GetBatchProcessDetailQuery
            {
                Id = id,
            });
            return Ok(processList);
        }
        #endregion

        #region ContractorPayment

        [Route("api/Batch/ProcessContractorPaymentList")]
        [HttpGet("ProcessContractorPaymentList")]
        public async Task<IActionResult> ProcessContractorPaymentList(Guid id)
        {
            var payment = await Mediator.Send(new ProcessContractorPaymentList
            {
                Id = id,
            });
            return Ok(payment);
        }

        #endregion
        #region ContractorPayment

        [Route("api/Batch/GatePassDetail")]
        [HttpGet("GatePassDetail")]
        public async Task<IActionResult> GatePassDetail(Guid id)
        {
            var gatePasses = await Mediator.Send(new GetGatePassesDetailQuery
            {
                Id = id,
            });
            return Ok(gatePasses);
        }

        #endregion

    }
}
