using Microsoft.Azure.Mobile.Server;
using System;

namespace Sharit.Azure.Backend.DataObjects
{
    public class SharitItem : EntityData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public CategoryItem Category { get; set; }
        public DateTime? Date { get; set; }
    }
}