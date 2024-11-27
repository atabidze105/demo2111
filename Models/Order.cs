using System;
using System.Collections.Generic;

namespace demo_tab_21_11_2024.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateOnly DateOrdered { get; set; }

    public DateOnly DateDeliver { get; set; }

    public int Vidacha { get; set; }

    public string? ClientName { get; set; }

    public int Code { get; set; }

    public int Status { get; set; }

    public int? ClientId { get; set; }

    public virtual User? Client { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual Status StatusNavigation { get; set; } = null!;

    public virtual Vidacha VidachaNavigation { get; set; } = null!;
}
