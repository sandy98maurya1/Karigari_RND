using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.DataContracts
{
    public interface ISkillSetData
    {
        public bool CreateSkillSet(SkillSet skillSet);
        public bool DeleteSkillSet(int userId);
        public bool UpdateSkillSet(SkillSet model);
        public List<SkillSet> GetAllSkill();
        public SkillSet GetSkillSetById(int Id);
        public List<SkillSet> GetSkillSetByEmpId(string Contact);
    }
}
