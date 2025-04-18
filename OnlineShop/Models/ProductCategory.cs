using System;
using System.Collections.Generic;

namespace OnlineShop.Models;

public partial class ProductCategory
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<AdCategory> AdCategories { get; set; } = new List<AdCategory>();
}
