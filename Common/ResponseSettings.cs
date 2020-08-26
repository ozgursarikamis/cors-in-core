using Microsoft.AspNetCore.Http;

namespace CorsInCore.Common
{
    public class ResponseSettings
    {  
        public static void SetPaginationHeader(HttpContext context, int pageSize, int pageNo, int pageCount, int totalRecords)
        {
            context.Response.Headers.Add("PageNo", pageNo.ToString());
            context.Response.Headers.Add("PageSize", pageSize.ToString());
            context.Response.Headers.Add("PageCount", pageCount.ToString());
            context.Response.Headers.Add("PageTotalRecords", totalRecords.ToString());
        }
    }
}
