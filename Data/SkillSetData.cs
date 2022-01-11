using Contracts.DataContracts;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class SkillSetData : ISkillSetData
    {
        private readonly ApplicationDbContext _dbcontext;

        public SkillSetData(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public bool CreateSkillSet(SkillSet skillSet)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSkillSet(int userId)
        {
            throw new NotImplementedException();
        }

        public List<SkillSet> GetAllSkill()
        {
            throw new NotImplementedException();
        }

        public List<SkillSet> GetSkillSetByEmpId(string Contact)
        {
            throw new NotImplementedException();
        }

        public SkillSet GetSkillSetById(int Id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSkillSet(SkillSet model)
        {
            throw new NotImplementedException();
        }
    }
}
