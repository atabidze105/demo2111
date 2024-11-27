using System;
using System.Collections.Generic;

namespace demo_tab_21_11_2024.Models;

public partial class User
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Lastname { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role RoleNavigation { get; set; } = null!;
}
