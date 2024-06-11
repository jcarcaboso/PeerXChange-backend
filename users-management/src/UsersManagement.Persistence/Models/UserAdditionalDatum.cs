using System;
using System.Collections.Generic;

namespace UsersManagement.Persistence.Models;

public partial class UserAdditionalDatum
{
    public string Wallet { get; set; } = null!;

    public string? Email { get; set; }

    public string? DefaultCurrency { get; set; }

    public virtual Wallet WalletNavigation { get; set; } = null!;
}
