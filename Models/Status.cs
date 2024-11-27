using System;
using System.Collections.Generic;

namespace demo_tab_21_11_2024.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
