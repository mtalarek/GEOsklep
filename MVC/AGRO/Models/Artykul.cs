using Microsoft.AspNetCore.SignalR;
using Microsoft.Build.Framework;

namespace GEOsklep.Models
{
    public class Artykul
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }

    }
}
