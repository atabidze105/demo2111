using System;
using System.Collections.Generic;

namespace demo_tab_21_11_2024.Models;

public partial class OrderProduct
{
    public int Order { get; set; }

    public int Product { get; set; }

    public int Quantity { get; set; }

    public virtual Order OrderNavigation { get; set; } = null!;

    public virtual Product ProductNavigation { get; set; } = null!;
}
