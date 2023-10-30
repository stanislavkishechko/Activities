using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace API.Extentions
{
    public static class HttpExtentions
    {
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int imagePerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new
            {
                currentPage,
                imagePerPage,
                totalItems,
                totalPages
            };
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
