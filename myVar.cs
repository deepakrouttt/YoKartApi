using Microsoft.EntityFrameworkCore;
using YoKartApi.Models;

namespace YoKartApi
{
    public static class myVar
    {

        public static int pageSize = 3;
        public static int pageCount { get; set; }
        public static int totalProduct { get; set; }
        public static int currentPage { get; set; }

    }
}
