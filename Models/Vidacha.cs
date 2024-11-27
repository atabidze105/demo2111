using System;
using System.Collections.Generic;

namespace demo_tab_21_11_2024.Models;

public partial class Vidacha
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
