using Focus.Business.Citys.Commands.AddUpdateCity;
using Focus.Business.Citys.Commands.DeleteCity;
using Focus.Business.Citys.Queries;
using Focus.Business.Citys.Queries.GetCityDetails;
using Focus.Business.Citys.Queries.GetCityList;
using Focus.Business.Regions.Commands.AddUpdateRegion;
using Focus.Business.Regions.Commands.DeleteRegion;
using Focus.Business.Regions.Queries;
using Focus.Business.Regions.Queries.GetRegionDetails;
using Focus.Business.Regions.Queries.GetRegionList;
using Microsoft.AspNetCore.Mvc;
using Noble.Api.Models;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Focus.Business.Logistics.Commands.AddUpdateLogistic;
using Focus.Business.Logistics.Queries;
using Focus.Business.Logistics.Queries.GetLogisticDetails;
using Focus.Business.Logistics.Queries.GetLogisticList;
using Focus.Domain.Enum;
using Focus.Business.HoldTypeSetup.Queries;
using Focus.Business.Sales.Commands.AddUpdateSale;
using Focus.Business.HoldTypeSetup.Models;
using Focus.Business.HoldTypeSetup.Commands;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : BaseController
    {
        #region Region

        [DisplayName("Region Code")]
        [Route("api/Region/RegionCode")]
        [HttpGet("RegionCode")]
        public async Task<IActionResult> RegionCode()
        {
            var autoNo = await Mediator.Send(new GetRegionCodeQuery());

            return Ok(autoNo);
        }

        [Route("api/Region/SaveRegion")]
        [HttpPost("SaveRegion")]
        public async Task<IActionResult> SaveRegion([FromBody] RegionVm regionVm)
        {
            var id = Guid.Empty;
            if (regionVm.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetRegionCodeQuery());

                id = await Mediator.Send(new AddUpdateRegionCommand
                {
                    Id = new Guid(),

                    Code = autoNo,
                    CountryId = regionVm.CountryId,
                    StateId = regionVm.StateId,
                    CityId = regionVm.CityId,
                    Area = regionVm.Area,
                    Description = regionVm.Description,
                    isActive = regionVm.isActive,



                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateRegionCommand
                {
                    Id = regionVm.Id,
                    Code = regionVm.Code,
                    CountryId = regionVm.CountryId,
                    StateId = regionVm.StateId,
                    CityId = regionVm.CityId,
                    Area = regionVm.Area,
                    Description = regionVm.Description,
                    isActive = regionVm.isActive,

                });
            }
            if (id != Guid.Empty)
            {
                var region = await Mediator.Send(new GetRegionDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, region = region });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Region/RegionList")]
        [HttpGet("RegionList")]

        public async Task<IActionResult> RegionList(bool isActive)
        {
            var region = await Mediator.Send(new GetRegionListQuery { isActive = isActive });
            return Ok(region);
        }

        [Route("api/Region/RegionDelete")]
        [HttpGet("RegionDelete")]
        public async Task<IActionResult> RegionDelete(Guid id)
        {
            var regionId = await Mediator.Send(new DeleteRegionCommand
            {
                Id = id
            });
            return Ok(regionId);

        }
        [Route("api/Region/RegionDetail")]
        [HttpGet("RegionDetail")]
        public async Task<IActionResult> RegionDetail(Guid Id)
        {
            var region = await Mediator.Send(new GetRegionDetailQuery()
            {
                Id = Id
            });
            return Ok(region);

        }
        #endregion

        #region City

        [DisplayName("City Code")]
        [Route("api/Region/CityCode")]
        [HttpGet("CityCode")]
        public async Task<IActionResult> CityCode()
        {
            var autoNo = await Mediator.Send(new GetCityCodeQuery());

            return Ok(autoNo);
        }

        [Route("api/Region/SaveCity")]
        [HttpPost("SaveCity")]
        public async Task<IActionResult> SaveCity([FromBody] CityVm cityVm)
        {
            var id = Guid.Empty;
            if (cityVm.Id == Guid.Empty)
            {
                var autoNo = await Mediator.Send(new GetCityCodeQuery());

                id = await Mediator.Send(new AddUpdateCityCommand
                {
                    Id = new Guid(),

                    Code = autoNo,
                    Name = cityVm.Name,
                    Description = cityVm.Description,
                    isActive = cityVm.isActive,



                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateCityCommand
                {
                    Id = cityVm.Id,
                    Code = cityVm.Code,
                    Name = cityVm.Name,
                    Description = cityVm.Description,
                    isActive = cityVm.isActive,

                });
            }
            if (id != Guid.Empty)
            {
                var city = await Mediator.Send(new GetCityDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, city = city });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Region/CityList")]
        [HttpGet("CityList")]

        public async Task<IActionResult> CityList(bool isActive)
        {
            var city = await Mediator.Send(new GetCityListQuery { isActive = isActive });
            return Ok(city);
        }

        [Route("api/Region/CityDelete")]
        [HttpGet("CityDelete")]
        public async Task<IActionResult> CityDelete(Guid id)
        {
            var cityId = await Mediator.Send(new DeleteCityCommand
            {
                Id = id
            });
            return Ok(cityId);

        }
        [Route("api/Region/CityDetail")]
        [HttpGet("CityDetail")]
        public async Task<IActionResult> CityDetail(Guid Id)
        {
            var city = await Mediator.Send(new GetCityDetailQuery()
            {
                Id = Id
            });
            return Ok(city);

        }
        #endregion

        #region Logistic

        [DisplayName("Logistic Code")]
        [Route("api/Region/LogisticCode")]
        [HttpGet("LogisticCode")]
        [Roles("CanAddTransporter", "CanAddClearanceAgent", "CanAddShippingLiner")]
        public async Task<IActionResult> LogisticCode(Logistics logisticsForm)
        {
            var autoNo = await Mediator.Send(new GetLogisticCodeQuery { LogisticsForm = logisticsForm });

            return Ok(autoNo);
        }

        [Route("api/Region/SaveLogistic")]
        [HttpPost("SaveLogistic")]
        [Roles("CanAddTransporter", "CanAddClearanceAgent", "CanAddShippingLiner", "CanEditTransporter", "CanEditClearanceAgent", "CanEditShippingLiner")]
        public async Task<IActionResult> SaveLogistic([FromBody] LogisticVm logisticVm)
        {
            Guid id;
            if (logisticVm.Id == Guid.Empty)
            {

                id = await Mediator.Send(new AddUpdateLogisticCommand
                {
                    Id = new Guid(),

                    Code = logisticVm.Code,
                    EnglishName = logisticVm.EnglishName,
                    ContactName = logisticVm.ContactName,
                    ArabicName = logisticVm.ArabicName,
                    LicenseNo = logisticVm.LicenseNo,
                    Address = logisticVm.Address,
                    ContactNo = logisticVm.ContactNo,
                    Email = logisticVm.Email,
                    Website = logisticVm.Website,
                    TermsAndCondition = logisticVm.TermsAndCondition,
                    Xcoordinates = logisticVm.Xcoordinates,
                    Ycoordinates = logisticVm.Ycoordinates,
                    Ports = logisticVm.Ports,
                    ServiceFor = logisticVm.ServiceFor,
                    ClearanceAgent = logisticVm.ClearanceAgent,
                    LogisticsForm = logisticVm.LogisticsForm,
                    IsActive = logisticVm.IsActive,
                    BranchId = logisticVm.BranchId,



                });
                return Ok(new { id = id, type = "Add" });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateLogisticCommand
                {
                    Id = logisticVm.Id,
                    EnglishName = logisticVm.EnglishName,
                    ArabicName = logisticVm.ArabicName,
                    LicenseNo = logisticVm.LicenseNo,
                    ContactName = logisticVm.ContactName,
                    Address = logisticVm.Address,
                    ContactNo = logisticVm.ContactNo,
                    Email = logisticVm.Email,
                    Website = logisticVm.Website,
                    TermsAndCondition = logisticVm.TermsAndCondition,
                    Xcoordinates = logisticVm.Xcoordinates,
                    Ycoordinates = logisticVm.Ycoordinates,
                    Ports = logisticVm.Ports,
                    ServiceFor = logisticVm.ServiceFor,
                    ClearanceAgent = logisticVm.ClearanceAgent,
                    LogisticsForm = logisticVm.LogisticsForm,
                    IsActive = logisticVm.IsActive,

                });
                return Ok(new { id = id, type = "Edit" });
            }
           




        }
        [Route("api/Region/LogisticList")]
        [HttpGet("LogisticList")]
        [Roles("CanViewTransporter", "CanViewClearanceAgent", "CanViewShippingLiner", "CanAddServiceSaleOrder", "CanDraftServiceSaleOrder", "CanEditServiceSaleOrder", "CanDuplicateServiceSaleOrder", "CanAddSaleOrder", "CanDraftSaleOrder", "CanEditSaleOrder", "CanDuplicateSaleOrder", "CanSelectTransporter")]
        public async Task<IActionResult> LogisticList(Logistics logisticsForm,string searchTerm, int? pageNumber, bool isActive, Guid? branchId)
        {
            var logistic = await Mediator.Send(new GetLogisticListQuery
            {
                isActive = isActive,
                LogisticsForm = logisticsForm,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                BranchId = branchId,
            });
            return Ok(logistic);
        }
     
        [Route("api/Region/LogisticDetail")]
        [HttpGet("LogisticDetail")]
        public async Task<IActionResult> LogisticDetail(Guid Id)
        {
            var logistic = await Mediator.Send(new GetLogisticDetailQuery()
            {
                Id = Id
            });
            return Ok(logistic);

        }
        #endregion

        #region HoldTypeSetup
        [Route("api/Region/SaveHoldTypeSetup")]
        [HttpPost("SaveHoldTypeSetup")]
        public async Task<IActionResult> SaveHoldTypeSetup([FromBody] HoldSetupLookupModel holdSetups)
        {
            var message = await Mediator.Send(new HoldSetupAddUpdateCommand
            {
                HoldSetups = holdSetups
            });
            return Ok(message);
        }

        [Route("api/Region/HoldTypeSetupDetail")]
        [HttpGet("HoldTypeSetupDetail")]
        public async Task<IActionResult> HoldTypeSetupDetail()
        {
            var hold = await Mediator.Send(new GetHoldSetupDetailsQuery(){});
            return Ok(hold);

        }
        [Route("api/Region/SavePermanentDeleteHoldTypeSetup")]
        [HttpPost("SavePermanentDeleteHoldTypeSetup")]
        public async Task<IActionResult> SavePermanentDeleteHoldTypeSetup([FromBody] HoldSetupLookupModel holdSetups)
        {
            var message = await Mediator.Send(new PermanentDeleteHoldSetupAddUpdateCommand
            {
                HoldSetups = holdSetups
            });
            return Ok(message);
        }

        [Route("api/Region/GetPermanentDeleteHoldTypeSetupDetails")]
        [HttpGet("GetPermanentDeleteHoldTypeSetupDetails")]
        public async Task<IActionResult> GetPermanentDeleteHoldTypeSetupDetails()
        {
            var hold = await Mediator.Send(new GetPermanentDeleteHoldSetupDetailsQuery()
            {

            });
            return Ok(hold);

        }
        #endregion
    }
}
