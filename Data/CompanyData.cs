using Contracts.DataContracts;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class CompanyData : ICompanyData
    {
        public bool CreateCompany(Company model)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCompany(int userId)
        {
            throw new NotImplementedException();
        }

        public List<Company> GetAllCompanys()
        {
            throw new NotImplementedException();
        }

        public Company GetCompanyByEmail(string Email)
        {
            throw new NotImplementedException();
        }

        public Company GetCompanyById(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Company> GetCompanysByEmail(string Contact)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCompany(Company model)
        {
            throw new NotImplementedException();
        }
    }
}
