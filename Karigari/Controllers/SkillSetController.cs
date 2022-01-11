using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Karigari.ResponseMapper;
using Utility;

namespace Karigari.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillSetController : ControllerBase
    {
        private readonly ISkillSet _skillset;

        public SkillSetController(ISkillSet skillSet)
        {
            _skillset = skillSet;
        }

        [HttpPost, Route("SaveSkillSet")]
        [Authorize(Roles = "EMPLOYEE")]
        public IActionResult SaveSkillSet([FromBody] SkillSet skill)
        {

            ApiResponse<SkillSet> responce = new ApiResponse<SkillSet>();
            try
            {
                ApiExposeResponse<Dictionary<string, string>> modelErrors = GetModelErrors();
                responce = _skillset.CreateSkillSet(skill).CreateSkillSetResponse(skill);

            }
            catch (Exception ex)
            {
                responce = SkillSetResponseMapper.CacheExceptionSkillSetResponse(ex);
            }
            return Ok(responce);
        }

        private ApiExposeResponse<Dictionary<string, string>> GetModelErrors()
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    string errordetails = string.Empty;
                    foreach (var error in state.Value.Errors)
                    {
                        errordetails = errordetails + error.ErrorMessage;
                    }

                    errors.Add(state.Key.Contains(".") ? state.Key.Split('.')[1] : state.Key, errordetails);
                }
            }


            return new ApiExposeResponse<Dictionary<string, string>>
            {
                IsSuccess = false,
                Message = ErrorMessages.InValidInputMsg,
                Error = errors
            };
        }
    }
}