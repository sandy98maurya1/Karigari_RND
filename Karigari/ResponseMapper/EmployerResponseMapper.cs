using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Karigari.ResponseMapper
{
    public static class EmployerResponseMapper
    {

        public static ApiListResponse<IList<Company>> GetAllEmployerResponce(this IList<Company> employers)
        {
            return new ApiListResponse<IList<Company>>
            {
                Message = string.Format(ErrorMessages.Success),
                IsSuccess = true,
                Data = employers,
                StatusCode = 200
            };
        }
        public static ApiResponse<Company> CreateProfileResponse(this bool status, Company employer)
        {
            if (status)
            {
                return new ApiResponse<Company>
                {
                    Message = string.Format(ErrorMessages.CreateSucess, employer.BusinessName),
                    IsSuccess = status,
                    StatusCode = 200
                };
            }
            else
            {
                return new ApiResponse<Company>
                {
                    Message = string.Format(ErrorMessages.CreateFail, employer.BusinessName),
                    IsSuccess = status,
                    StatusCode = 400
                };
            }
        }

        public static ApiResponse<Company> UpdateProfileResponse(this bool status, Company employer)
        {
            if (status)
            {
                return new ApiResponse<Company>
                {
                    Message = string.Format(ErrorMessages.UpdateSucess, employer.BusinessName),
                    IsSuccess = status,
                    StatusCode = 200
                };
            }
            else
            {
                return new ApiResponse<Company>
                {
                    Message = string.Format(ErrorMessages.UpdateFail, employer.BusinessName),
                    IsSuccess = status,
                    StatusCode = 400
                };
            }
        }

        public static ApiResponse<Company> DeleteProfileResponseMapper(this bool status)
        {
            if (status)
            {
                return new ApiResponse<Company>
                {
                    Message = string.Format(ErrorMessages.DeleteSucess, "Profile"),
                    IsSuccess = status,
                    StatusCode = 200
                };
            }
            else
            {
                return new ApiResponse<Company>
                {
                    Message = string.Format(ErrorMessages.DeleteFail, "Profile"),
                    IsSuccess = status,
                    StatusCode = 400
                };
            }
        }

        public static ApiResponse<Company> ValidateEmployerResponse(Company employer)
        {
            return new ApiResponse<Company>
            {
                Message = string.Format(ErrorMessages.ValueExists, employer.BusinessEmail, employer.BusinessName),
                IsSuccess = false,
                StatusCode = 0
            };

        }

        public static ApiResponse<Company> CacheExceptionResponse(this Exception ex)
        {
            return new ApiResponse<Company>
            {
                IsSuccess = false,
                Data = null,
                Message = ex.Message
            };
        }

        public static ApiListResponse<IList<Company>> GetEmployerByContactResponse(this IList<Company> employers)
        {
            if (employers != null)
            {
                return new ApiListResponse<IList<Company>>
                {
                    Message = string.Format(ErrorMessages.Success),
                    IsSuccess = true,
                    Data = employers,
                    StatusCode = 200
                };
            }
            else
            {
                return new ApiListResponse<IList<Company>>
                {
                    Message = string.Format(ErrorMessages.NoRecordFound),
                    Data = null,
                    IsSuccess = false,
                    StatusCode = 400
                };
            }
        }

        public static ApiListResponse<IList<Company>> CacheListExceptionResponse(this Exception ex)
        {
            return new ApiListResponse<IList<Company>>
            {
                IsSuccess = false,
                Data = null,
                Message = ex.Message
            };
        }

    }
}
