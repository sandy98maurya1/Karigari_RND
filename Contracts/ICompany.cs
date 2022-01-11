using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ICompanys
    {
        public List<Company> GetAllCompanys();
        public bool CreateCompany(Company model);
        public Company GetCompanyByEmail(string Email);
        public bool DeleteCompany(int userId);
        public bool UpdateCompany(Company model);
        public Company GetCompanyById(int Id);
        public List<Company> GetCompanysByEmail(string Contact);

    }
}
