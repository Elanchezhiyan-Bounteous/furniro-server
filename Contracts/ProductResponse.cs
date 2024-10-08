using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using furniro_server.Models;

namespace furniro_server.Contracts
{
    public class ProductResponse
    {
        public long Id {get; set;}
        public string Name {get; set;}
        public string Desc  {get; set;}
        public string Src {get; set;}
        public string Category {get; set;}
        public int Price { get; set; }
        public int OriginalPrice { get; set; }
        public string Discount { get; set; }
        public List<Review> Reviews { get; set; }
        public float Rating { get; set; }
        public string Sku { get; set; }
        public string[] Tags { get; set; }
        public string[] Sizes { get; set; }
        public DateTime CreatedAt {get; set;}

         public List<ProductColor> Colors { get; set; }
        public List<Image> ProductGallery {get; set;}
        public List<Image> DescriptionImages {get; set;}
    }
}