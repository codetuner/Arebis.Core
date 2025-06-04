using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sample.AspNetMvc.Models.Home
{
    public class IndexModel
    {
        public int PageIndex { get; set; } = 1;
        
        public int PageSize { get; set; } = 10;
    }
}
