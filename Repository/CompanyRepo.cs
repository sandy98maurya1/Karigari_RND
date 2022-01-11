using Contracts;
using Contracts.DataContracts;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class CompanyRepo : ICompanys
    {
        private readonly ICompanyData _Company;

        public CompanyRepo(ICompanyData Company)
        {
            _Company = Company;
        }
        public bool CreateCompany(Company model)
        {
            return _Company.CreateCompany(model);
        }

        public bool DeleteCompany( int userId)
        {
            return _Company.DeleteCompany(userId);
        }

        public List<Company> GetAllCompanys()
        {
            return _Company.GetAllCompanys();
        }

        public Company GetCompanyByEmail(string Email)
        {
            return _Company.GetCompanyByEmail(Email);
        }

        public Company GetCompanyById(int Id)
        {
            return _Company.GetCompanyById(Id);
        }

        public List<Company> GetCompanysByEmail(string Contact)
        {
            return _Company.GetCompanysByEmail(Contact);
        }

        public bool UpdateCompany(Company model)
        {
            return _Company.UpdateCompany(model);
        }
    }
}
