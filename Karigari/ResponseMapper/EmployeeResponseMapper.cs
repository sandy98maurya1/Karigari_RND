using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Karigari.ResponseMapper
{
    public static class EmployeeResponseMapper
    {
       public static ApiListResponse<IList<Labour>> GetAllEmployeeResponce(this IList<Labour> employees)
        {
            return new ApiListResponse<IList<Labour>>
            {
                Message = string.Format(ErrorMessages.Success),
                IsSuccess = true,
                Data = employees,
                StatusCode = 200
            };
        }
        public static ApiResponse<Labour> CreateProfileResponse(this bool status, Labour employee)
        {
            if (status)
            {
                return new ApiResponse<Labour>
                {
                    Message = string.Format(ErrorMessages.CreateSucess, employee.FirstName + " " + employee.LastName),
                    IsSuccess = status,
                    StatusCode = 200
                };
            }
            else
            {
                return new ApiResponse<Labour>
                {
                    Message = string.Format(ErrorMessages.CreateFail, employee.FirstName + " " + employee.LastName),
                    IsSuccess = status,
                    StatusCode = 400
                };
            }
        }

        public static ApiResponse<Labour> UpdateProfileResponse(this bool status, Labour employee)
        {
            if (status)
            {
                return new ApiResponse<Labour>
                {
                    Message = string.Format(ErrorMessages.UpdateSucess, employee.FirstName + " " + employee.LastName),
                    IsSuccess = status,
                    StatusCode = 200
                };
            }
            else
            {
                return new ApiResponse<Labour>
                {
                    Message = string.Format(ErrorMessages.UpdateFail, employee.FirstName + " " + employee.LastName),
                    IsSuccess = status,
                    StatusCode = 400
                };
            }
        }
        
        public static ApiResponse<Labour> DeleteProfileResponse(this bool status)
        {
            if (status)
            {
                return new ApiResponse<Labour>
                {
                    Message = string.Format(ErrorMessages.DeleteSucess, "Profile"),
                    IsSuccess = status,
                    StatusCode = 200
                };
            }
            else
            {
                return new ApiResponse<Labour>
                {
                    Message = string.Format(ErrorMessages.DeleteFail, "Profile"),
                    IsSuccess = status,
                    StatusCode = 400
                };
            }
        }

        public static ApiResponse<Labour> ValidateEmployeeResponse( Labour employee)
        {
            return new ApiResponse<Labour>
            {
                Message = string.Format(ErrorMessages.ValueExists, employee.Contact, employee.FirstName + " " + employee.LastName),
                IsSuccess = false,
                StatusCode = 0
            };
            
        }

        public static ApiResponse<Labour> CacheExceptionResponse(this Exception ex)
        {
            return new ApiResponse<Labour>
            {
                IsSuccess = false,
                Data = null,
                Message = ex.Message
            };
        }

       
        public static ApiListResponse<IList<Labour>> GetEmployeeByContactResponse(this IList<Labour> employees)
        {
            if (employees != null)
            {
                return new ApiListResponse<IList<Labour>>
                {
                    Message = string.Format(ErrorMessages.Success),
                    IsSuccess = true,
                    Data= employees,
                    StatusCode = 200
                };
            }
            else
            {
                return new ApiListResponse<IList<Labour>>
                {
                    Message = string.Format(ErrorMessages.NoRecordFound),
                    Data = null,
                    IsSuccess = false,
                    StatusCode = 400
                };
            }
        }

        public static ApiListResponse<IList<Labour>> CacheListExceptionResponse(this Exception ex)
        {
            return new ApiListResponse<IList<Labour>>
            {
                IsSuccess = false,
                Data = null,
                Message = ex.Message
            };
        }

    }
}
