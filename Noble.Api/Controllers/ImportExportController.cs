using Focus.Business.ImportExportTypes.Commands;
using Focus.Business.ImportExportTypes.Queries;
using Focus.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Noble.Api.Models;
using System;
using System.Threading.Tasks;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportExportController : BaseController
    {
        #region For ImportExportType

      
        [Route("api/ImportExport/SaveImportExportTypeInformation")]
        [HttpPost("SaveImportExportTypeInformation")]
        [Roles("CanAddStuffingLocation", "CanAddPartOfLoading", "CanAddPartOfDestination", "CanAddOrderType", "CanAddService", "CanAddIncoterms", "CanAddCommodity", "CanAddCarrier", "CanAddExportExw", "CanAddImportFob", "CanAddQuantityContainer", "CanEditStuffingLocation", "CanEditPartOfLoading", "CanEditPartOfDestination", "CanEditOrderType", "CanEditService", "CanEditIncoterms", "CanEditCommodity", "CanEditCarrier", "CanEditExportExw", "CanEditImportFob", "CanEditQuantityContainer")]
        public async Task<IActionResult> SaveImportExportTypeInformation([FromBody] ImportExportTypeLookUpModel deduction)
        {
            if (deduction.Id == Guid.Empty)
            {
                var id = await Mediator.Send(new AddUpdateImportExportType()
                {
                    ImportExportType = deduction
                });

            }
            else
            {
                var id = await Mediator.Send(new AddUpdateImportExportType()
                {
                    ImportExportType = deduction
                });
            }


            return Ok(new { IsSuccess = true });
        }

        [Route("api/ImportExport/ImportExportTypeList")]
        [HttpGet("ImportExportTypeList")]
        [Roles("CanViewStuffingLocation", "CanViewPartOfLoading", "CanViewPartOfDestination", "CanViewOrderType", "CanViewService", "CanViewIncoterms", "CanViewCommodity", "CanViewCarrier", "CanViewExportExw", "CanViewImportFob", "CanViewQuantityContainer", "CanAddQuotation", "CanDraftQuotation", "CanEditQuotation")]
        public async Task<IActionResult> ImportExportTypeList(string searchTerm, int? pageNumber, bool isDropdown,ImportExportTypes importExportTypes)
        {
            var result = await Mediator.Send(new ImportExportTypeListQuery
            {
                SearchTerm = searchTerm,
                ImportExportTypes = importExportTypes,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(result);
        }

        [Route("api/ImportExport/ImportExportTypeDetail")]
        [HttpGet("ImportExportTypeDetail")]
        [Roles("CanEditStuffingLocation", "CanEditPartOfLoading", "CanEditPartOfDestination", "CanEditOrderType", "CanEditService", "CanEditIncoterms", "CanEditCommodity", "CanEditCarrier", "CanEditExportExw", "CanEditImportFob", "CanEditQuantityContainer")]
        public async Task<IActionResult> ImportExportTypeDetail(Guid id)
        {
            var deduction = await Mediator.Send(new GetImportExportTypeDetailQuery()
            {
                Id = id
            });
            return Ok(deduction);

        }

        #endregion
    }
}
