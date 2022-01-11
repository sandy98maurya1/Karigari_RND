using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Karigari.ResponseMapper
{
    public static class SkillSetResponseMapper
    {
        public static ApiResponse<SkillSet> CreateSkillSetResponse(this bool status, SkillSet employee)
        {
            if (status)
            {
                return new ApiResponse<SkillSet>
                {
                    Message = string.Format(ErrorMessages.CreateSucess, "Skill Set"),
                    IsSuccess = status,
                    StatusCode = 200
                };
            }
            else
            {
                return new ApiResponse<SkillSet>
                {
                    Message = string.Format(ErrorMessages.CreateFail, "Skill Set"),
                    IsSuccess = status,
                    StatusCode = 400
                };
            }
        }


        public static ApiResponse<SkillSet> CacheExceptionSkillSetResponse(this Exception ex)
        {
            return new ApiResponse<SkillSet>
            {
                IsSuccess = false,
                Data = null,
                Message = ex.Message
            };
        }
    }
}
