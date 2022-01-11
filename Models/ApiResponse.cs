using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class ApiExposeResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Error { get; set; }
    }

    public class ApiListResponse<T> : ApiResponse<T>
    {
        public int TotalRecords { get; set; } //1000
        public int TotalRecordsAfterFilter { get; set; } //900
        public int RecordsInOneSlot { get; set; } //50
        public int PageSize { get; set; } //900/50=18        
    }

    //public class DeleteModel
    //{
    //    public List<int> Ids { get; set; }
    //}

    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }
    }
}
