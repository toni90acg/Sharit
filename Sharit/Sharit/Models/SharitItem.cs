using Microsoft.WindowsAzure.MobileServices;
using System;

namespace Sharit.Models
{
    public class SharitItem : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime? Date { get; set; }

        [Version]
        public string AzureVersion { get; set; }
    }
}
