using Focus.Business.Accounting.Commands.AddUpdateAccount;
using Focus.Business.Accounting.Commands.RemoveAccount;
using Focus.Business.Accounting.Commands.SetupAccount;
using Focus.Business.Accounting.Queries;
using Focus.Business.Banks.Commands.AddUpdateBank;
using Focus.Business.Banks.Commands.DeleteBank;
using Focus.Business.Banks.Queries;
using Focus.Business.Banks.Queries.GetBankDetails;
using Focus.Business.Banks.Queries.GetBankList;
using Focus.Domain.Entities;
using Focus.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Noble.Api.Models;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Focus.Business.ExpenseTyp.Commands;
using Focus.Business.ExpenseTyp.Queries;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingController : BaseController
    {


        [DisplayName("Account Code")]
        [Route("api/Accounting/AccountCode")]
        [HttpGet("AccountCode")] 
        [Roles("CanAddCOA")]
        public async Task<IActionResult> AccountCode(Guid id)
        {
            var autoNo = await Mediator.Send(new GetAccountCodeQuery
            {
                Id= id
            });

            return Ok(autoNo);
        }

        [Route("api/Accounting/Charts")]
        [HttpGet("Charts")]
        [Roles("CanViewCOA", "Admin")]
        public async Task<IActionResult> Charts()
         {
            var result = await Mediator.Send(new GetAccountsQuery());
            if (result.AccountTypes.Count > 0)
            {
                return Ok(result);
            }
            return Ok(null);

        }
        
        [Route("api/Accounting/RemoveAccount")]
        [HttpGet("RemoveAccount")]
        [Roles( "Can Delete Chart Of Account", "Noble Admin")]
        public async Task<IActionResult> RemoveAccount(Guid id)
        {

            var isDelete = await Mediator.Send(
                new RemoveAccountCommand
                {
                    Id = id
                });
            return Ok(isDelete);
        }
       
        [Route("api/Accounting/AddAccount")]
        [HttpPost("AddAccount")]
        [Roles("CanEditCOA", "CanAddCOA")]
        public async Task<IActionResult> AddAccount([FromBody] Account account)
        {
            var id = await Mediator.Send(new AddUpdateAccountCommand
            {
                Code = account.Code,
                Name = account.Name,
                NameArabic = account.NameArabic,
                Description = account.Description,
                IsActive = account.IsActive,
                CostCenterId = account.CostCenterId,
                //RuningBalance = account.RuningBalance,
                //OpeningBalance = account.OpeningBalance,
                Id = new Guid()
            });

            if (id != Guid.Empty)
            {
                return Ok(new { value = true, id });
            }
            else
            {
                return Ok(new { value = false });
            }
        }
       
        //[Route("api/Accounting/COASelect")]
        //[HttpGet("COASelect")]
        //[Roles("Can View Chart Of Account", "Noble Admin")]
        //public async Task<IActionResult> COASelect()
        //{
        //    return Ok();
        //}


        [Route("api/Accounting/UpdateAccount")]
        [HttpPost("UpdateAccount")]
        [Roles("CanEditCOA", "CanAddCOA")]

        public async Task<IActionResult> UpdateAccount([FromBody] Account account)
        {
            var id = await Mediator.Send(new AddUpdateAccountCommand
            {
                Id = account.Id,
                Code = account.Code,
                Name = account.Name,
                NameArabic = account.NameArabic,
                Description = account.Description,
                IsActive = account.IsActive,
                CostCenterId = account.CostCenterId,
                //OpeningBalance = account.OpeningBalance,
                //RuningBalance = account.RuningBalance,
            });
            if (id != Guid.Empty)
            {
                return Ok(new { value = true, id });
            }
            else
            {
                return Ok(new { value = false });
            }
        }


        [Route("api/Accounting/GetAccount")]
        [HttpGet("GetAccount")]
        [Roles("CanEditCOA", "CanAddCOA", "CashInvoices", "CreditInvoices", "CanAddSaleOrder", "CanDraftSaleOrder", "CanEditSaleOrder", "CanPayAdvanceFromView", "CanDraftCPR", "CanEditCPR", "CanRejectCPR", "CanAddCPR", "CanDraftSPR", "CanEditSPR", "CanRejectSPR", "CanAddSPR", "CashPurchase", "CreditPurchase", "CanDraftPettyCash", "CanEditPettyCash", "CanRejectPettyCash", "CanAddPettyCash", "CanDraftJV", "CanEditJV", "CanAddJV", "CanDraftOC", "CanEditOC", "CanAddOC", "CanAddAdvancePayment", "CanAddOrderExpense", "CanAdvancePaymentFromList", "CanPrintAccountLedger", "CanViewAccountLedger", "CanViewBanTransaction", "CanPrintBanTransaction", "CanViewSupplierBalance", "CanViewCustomerBalance", "CanPrintSupplierBalance", "CanPrintCustomerBalance", "CanAddPosTerminal", "CanEditPosTerminal", "CanAddTerminal", "CanEditTerminal", "CanAddExpense", "CanEditExpense", "CanDraftExpense", "CanAddExpenseBill", "CanEditExpenseBill", "CanDraftExpenseBill", "Noble Admin", "CanHoldServiceInvoices", "CashServiceInvoices", "CreditServiceInvoices", "CanServicePayAdvanceFromView", "User")]
        public async Task<IActionResult> GetAccount(Guid id, Guid? companyId)
        {
            var account = await Mediator.Send(new GetAccountByIdQuery()
            {
                Id = id,
                CompanyId = companyId
            });

          
                return Ok( account );
           
        }


        [Route("api/Accounting/IsAccountExist")]
        [HttpGet("IsAccountExist")]
        [Roles("CanEditCOA", "CanAddCOA")]

        public async Task<IActionResult> IsAccountExist(string code)
        {
            var isExist = await Mediator.Send(new IsCodeExistQuery()
            {
                Code = code
            });
            return Ok(new { value = isExist });
        }
        [Route("api/Accounting/GetCOA")]
        [HttpGet("GetCOA")]
        [Roles("CashInvoices", "CreditInvoices", "CanAddSaleOrder", "CanDraftSaleOrder", "CanEditSaleOrder", "CanPayAdvanceFromView", "CanDraftCPR", "CanEditCPR", "CanRejectCPR", "CanAddCPR", "CanDraftSPR", "CanEditSPR", "CanRejectSPR", "CanAddSPR", "CashPurchase", "CreditPurchase", "CanDraftPettyCash", "CanEditPettyCash", "CanRejectPettyCash", "CanAddPettyCash", "CanDraftJV", "CanEditJV", "CanAddJV", "CanDraftOC", "CanEditOC", "CanAddOC", "CanAddAdvancePayment", "CanAddOrderExpense", "CanAdvancePaymentFromList", "CanPrintAccountLedger", "CanViewAccountLedger", "CanViewBanTransaction", "CanPrintBanTransaction", "CanViewSupplierBalance", "CanViewCustomerBalance", "CanPrintSupplierBalance", "CanPrintCustomerBalance", "CanAddPosTerminal", "CanEditPosTerminal", "CanAddTerminal", "CanEditTerminal", "CanAddExpense", "CanEditExpense", "CanDraftExpense", "CanAddExpenseBill", "CanEditExpenseBill", "CanDraftExpenseBill", "Noble Admin", "CanHoldServiceInvoices", "CashServiceInvoices", "CreditServiceInvoices", "CanServicePayAdvanceFromView", "User")]

        public async Task<IActionResult> GetCOA(bool isDropdown, Guid? companyId)
        {
            var result = await Mediator.Send(new GetAccountsQuery
            {
                IsDropdown= isDropdown,
                CompanyId = companyId
            });
            if (result.AccountTypes.Count > 0)
            {
                return Ok(result);
            }
            return Ok(null);
        }
        
        
        [Route("api/Accounting/TemplateAccountSetup")]
        [HttpGet("TemplateAccountSetup")]
        //[Roles("Can View Chart Of Account", "Noble Admin")]
        public async Task<IActionResult> TemplateAccountSetup(TemplateType template)
        {
            var id = await Mediator.Send(new CreateChartOfAccountsFromTemplateCommand
            {
                TemplateType = template
            });

            return Ok(new
            {
                id,
                value = "Chart of Account Created Successfully."
            });
        }


        #region Bank

        [DisplayName("Bank Code")]
        [Route("api/Accounting/BankCode")]
        [HttpGet("BankCode")]
        [Roles("CanAddBank", "CanEditBank")]
        public async Task<IActionResult> BankCode()
        {
            var autoNo = await Mediator.Send(new GetBankCodeQuery());

            return Ok(autoNo);
        }

        [Route("api/Accounting/SaveBank")]
        [HttpPost("SaveBank")]
        [Roles("CanAddBank", "CanEditBank")]
        public async Task<IActionResult> SaveBank([FromBody] BankVm bankVm)
        {
            var id = Guid.Empty;
            if (bankVm.Id == Guid.Empty)
            {
                id = await Mediator.Send(new AddUpdateBankCommand
                {

                    Id=bankVm.Id,
                    Code = bankVm.Code,
                    ShortName = bankVm.ShortName,
                    AccountType = bankVm.AccountType,
                    BankName = bankVm.BankName,
                    NameArabic = bankVm.NameArabic,
                    AccoutName = bankVm.AccoutName,
                    IsActive = bankVm.IsActive,
                    BranchName = bankVm.BranchName,
                    AccountNumber = bankVm.AccountNumber,
                    AccoutNameArabic = bankVm.AccoutNameArabic,
                    IBNNumber = bankVm.IBNNumber,
                    Location = bankVm.Location,
                    ContactPerson = bankVm.ContactPerson,
                    ContactName = bankVm.ContactName,
                    ManagerName = bankVm.ManagerName,
                    ManagerContectualNumber = bankVm.ManagerContectualNumber,
                    AccounType = bankVm.AccounType,
                    CurrencyId = bankVm.CurrencyId,
                    BranchAddress = bankVm.BranchAddress,
                    BranchCode = bankVm.BranchCode,
                    SwiftCode = bankVm.SwiftCode,
                    Active = bankVm.Active,
                    Reference = bankVm.Reference




                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateBankCommand
                {
                    Id = bankVm.Id,
                    Code = bankVm.Code,
                    AccountType = bankVm.AccountType,
                    ShortName = bankVm.ShortName,
                    BankName = bankVm.BankName,
                    NameArabic = bankVm.NameArabic,
                    AccoutName = bankVm.AccoutName,
                    IsActive = bankVm.IsActive,
                    BranchName = bankVm.BranchName,
                    AccountNumber = bankVm.AccountNumber,
                    AccoutNameArabic = bankVm.AccoutNameArabic,
                    IBNNumber = bankVm.IBNNumber,
                    Location = bankVm.Location,
                    ContactPerson = bankVm.ContactPerson,
                    ContactName = bankVm.ContactName,
                    ManagerName = bankVm.ManagerName,
                    ManagerContectualNumber = bankVm.ManagerContectualNumber,
                    AccounType = bankVm.AccounType,
                    CurrencyId = bankVm.CurrencyId,
                    BranchAddress = bankVm.BranchAddress,
                    BranchCode = bankVm.BranchCode,
                    SwiftCode = bankVm.SwiftCode,
                    Active = bankVm.Active,
                    Reference = bankVm.Reference
                });
            }
            if (id != Guid.Empty)
            {
                var color = await Mediator.Send(new GetBankDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, color = color });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Accounting/BankList")]
        [HttpGet("BankList")]
        public async Task<IActionResult> BankList(bool isActive)
        {
            var bank = await Mediator.Send(new GetBankListQuery
            {
                isActive= isActive
            });
            return Ok(bank);
        }
        [Route("api/Accounting/BankDelete")]
        [HttpGet("BankDelete")]
        //[Roles( "Can Delete Bank", "Noble Admin")]
        public async Task<IActionResult> BankDelete(Guid id)
        {
            var colorId = await Mediator.Send(new DeleteBankCommand
            {
                Id = id
            });
            return Ok(colorId);

        }
        [Route("api/Accounting/BankDetail")]
        [HttpGet("BankDetail")]
        [Roles("CanAddBank", "CanEditBank")]
        public async Task<IActionResult> BankDetail(Guid Id)
        {
            var color = await Mediator.Send(new GetBankDetailQuery()
            {
                Id = Id
            });
            return Ok(color);

        }
        #endregion
        
        [Route("api/Accounting/SaveExpenseType")]
        [HttpPost("SaveExpenseType")]
        [Roles("CanEditExpenseType", "CanAddExpenseType")]
        public async Task<IActionResult> SaveExpenseType([FromBody] ExpenseTypeLookUpModel expenseType)
        {
            Guid id;
            if (expenseType.Id == Guid.Empty)
            {
                id = await Mediator.Send(new AddUpdateExpenseTypeCommand
                {
                    ExpenseType = expenseType

                });
            }
            else
            {
                id = await Mediator.Send(new AddUpdateExpenseTypeCommand
                {
                    ExpenseType = expenseType

                });
            }
            if (id != Guid.Empty)
            {
                var color = await Mediator.Send(new ExpenseTypeDetailQuery
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, color = color });
            }
            return Ok(new { IsSuccess = false });
        }
        [Route("api/Accounting/ExpenseTypeList")]
        [HttpGet("ExpenseTypeList")]
        [Roles("CanViewExpenseType")]
        public async Task<IActionResult> ExpenseTypeList(string searchTerm, int? pageNumber, bool isActive)
        {
            var color = await Mediator.Send(new ExpenseTypeListQuery
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(color);
        }
        [Route("api/Accounting/ExpenseTypeDetail")]
        [HttpGet("ExpenseTypeDetail")]
        [Roles("CanEditExpenseType")]
        public async Task<IActionResult> ExpenseTypeDetail(Guid id)
        {
            var color = await Mediator.Send(new ExpenseTypeDetailQuery()
            {
                Id = id
            });
            return Ok(color);

        }
    }
}
