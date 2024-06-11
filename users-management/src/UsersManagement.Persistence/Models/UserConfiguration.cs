using System;
using System.Collections.Generic;

namespace UsersManagement.Persistence.Models;

public partial class UserConfiguration
{
    public string UserWallet { get; set; } = null!;

    public string? Email { get; set; }

    public string? DefaultCurrency { get; set; }

    public virtual User UserWalletNavigation { get; set; } = null!;
}
