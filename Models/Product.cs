using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using ColumnAttribute = Supabase.Postgrest.Attributes.ColumnAttribute;
using TableAttribute = Supabase.Postgrest.Attributes.TableAttribute;

namespace furniro_server.Models
{
    [Table("product")]
    public class Product : BaseModel
    {
        [JsonIgnore]
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("desc")]
        public string Desc { get; set; }

        [Column("src")]
        public string Src { get; set; }

        [Column("category")]
        public string Category { get; set; }

        [Column("price")]
        public int Price { get; set; }

        [Column("original_price")]
        public int OriginalPrice { get; set; }

        [Column("discount")]
        public string Discount { get; set; }

        [Column("reviews")]
        public List<Review> Reviews { get; set; }

        [Column("rating")]
        public float Rating { get; set; }

        [Column("sku")]
        public string Sku { get; set; }

        [Column("tags")]
        public string[] Tags { get; set; }

        [Column("sizes")]
        public string[] Sizes { get; set; }

        [Column("colors")]
        public List<ProductColor> Colors { get; set; }

        [Column("product_gallery")]
        public List<Image> ProductGallery { get; set; }

        [Column("description_images")]
        public List<Image> DescriptionImages { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

    }
    public class ProductColor
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("value")]
        public string Value { get; set; }
    }

    public class Image
    {
        [Column("alt")]
        public string Alt { get; set; }

        [Column("image")]
        public string ImageUrl { get; set; }
    }

    public class Review
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("feedback")]
        public string Feedback { get; set; }
    }
}
