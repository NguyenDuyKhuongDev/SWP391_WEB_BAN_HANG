﻿using System;
using System.Collections.Generic;

namespace OnlineShop.Models;

public partial class AdPosition
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<AdPlacement> AdPlacements { get; set; } = new List<AdPlacement>();
}
