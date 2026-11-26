using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrmShop.Models;

[Table("products")]
public class Product : BaseModel
{
    [Column("name")]
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Column("price", TypeName = "numeric(10,2)")]
    public decimal Price { get; set; }

    [Column("stock")]
    public int Stock { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}