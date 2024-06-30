using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Focus.Business.Models;

namespace Focus.Business.Interface
{
   public interface ICompanyComponent
    {
        bool VatRegistrationNumberExists(string vrn);
        List<CompanyDto> GetCompaniesList(Guid id);
        CompanyDto GetCompanyById(Guid companyId);
        //CompanyDto GetCompanySetupAccount();
        CompanyDto GetCompanyByName(string companyName);
        CompanyDto UpdateCompany(CompanyDto company);
        string GetCompanyLogo(Guid companyId);
        void UpdateCompanyLogo(CompanyDto company);
        Task<bool> DeleteCompany(Guid Id);

    }
}
