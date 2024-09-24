using System;
using System.Collections.Generic;

namespace TacoFastFoodAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ApiKey { get; set; }
}
