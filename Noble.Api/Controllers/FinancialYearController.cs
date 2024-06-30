using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Focus.Business.FinancialYearSettings.Commands;
using Focus.Business.FinancialYearSettings.Models;
using Focus.Business.FinancialYearSettings.Queries;
using Focus.Business.FinancialYearTemporaryClosing.Commands;
using Focus.Business.FinancialYearTemporaryClosing.Models;
using Focus.Business.FinancialYearTemporaryClosing.Queries;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialYearController : BaseController
    {
        [Route("api/FinancialYear/SaveFinancialYearSetting")]
        [HttpPost("SaveFinancialYearSetting")]
        public async Task<IActionResult> SaveFinancialYearSetting(FinancialYearSettingModel financialYear)
        {
            var result = await Mediator.Send(new AddUpdateFinancialYearSettingCommand
            {
                FinancialYearSettingModel = financialYear
            });

            return Ok(result);
        }

        [Route("api/FinancialYear/GetFinancialYearSetting")]
        [HttpGet("GetFinancialYearSetting")]
        public async Task<IActionResult> GetFinancialYearSetting()
        {
            var result = await Mediator.Send(new GetFinancialYearSettingDetailQuery
            {
            });

            return Ok(result);
        }

        #region FinancialYearClosing

        [Route("api/FinancialYear/GetAccountBalanceList")]
        [HttpGet("GetAccountBalanceList")]
        public async Task<IActionResult> GetAccountBalanceList(DateTime fromDate, DateTime toDate)
        {
            var result = await Mediator.Send(new GetAccountBalanceQuery
            {
                FromDate= fromDate,
                ToDate = toDate,
            });

            return Ok(result);
        }
        
        [Route("api/FinancialYear/MultiPeriodAccountBalanceList")]
        [HttpPost("MultiPeriodAccountBalanceList")]
        public async Task<IActionResult> MultiPeriodAccountBalanceList([FromBody] List<ClosingTypePeriodListModel> closingTypePeriodList)
        {
            var result = await Mediator.Send(new GetAccountBalanceQuery
            {
                ClosingTypePeriodList = closingTypePeriodList,
                IsReport = true,
            });

            return Ok(result);
        }

        [Route("api/FinancialYear/GetFinancialYearDetail")]
        [HttpGet("GetFinancialYearDetail")]
        public async Task<IActionResult> GetFinancialYearDetail()
        {
            var result = await Mediator.Send(new FinancialYearSettingDetailQuery
            {
            });

            return Ok(result);
        }

        [Route("api/FinancialYear/SaveFinancialYearClosingBalance")]
        [HttpPost("SaveFinancialYearClosingBalance")]
        public async Task<IActionResult> SaveFinancialYearClosingBalance(FinancialYearClosingLookUpModel financialYear)
        {
            var result = await Mediator.Send(new AddFinancialYearClosingBalanceCommand
            {
                FinancialYearClosing = financialYear
            });

            return Ok(result);
        }

        [Route("api/FinancialYear/ReOpenFinancialYear")]
        [HttpPost("ReOpenFinancialYear")]
        public async Task<IActionResult> ReOpenFinancialYear(FinancialYearClosingLookUpModel financialYear)
        {
            var result = await Mediator.Send(new ChangeFinancialYearStatusCommand
            {
                FinancialYearClosing = financialYear
            });

            return Ok(result);
        }
        #endregion
    }
}
