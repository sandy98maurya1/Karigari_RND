using Contracts;
using Contracts.DataContracts;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class SkillSetRepo : ISkillSet
    {
        private readonly ISkillSetData _skillSetData;
        public SkillSetRepo(ISkillSetData skillSetData)
        {
            _skillSetData = skillSetData;
        }

        public bool CreateSkillSet(SkillSet skillSet)
        {
            return _skillSetData.CreateSkillSet(skillSet);
        }

        public bool DeleteSkillSet(int userId)
        {
            return _skillSetData.DeleteSkillSet(userId);
        }

        public List<SkillSet> GetAllSkill()
        {
            return _skillSetData.GetAllSkill();
        }

        public List<SkillSet> GetSkillSetByEmpId(string Contact)
        {
            return _skillSetData.GetSkillSetByEmpId(Contact);
        }

        public SkillSet GetSkillSetById(int Id)
        {
            return _skillSetData.GetSkillSetById(Id);
        }

        public bool UpdateSkillSet(SkillSet model)
        {
            return _skillSetData.UpdateSkillSet(model);
        }
    }
}
