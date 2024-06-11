using System;
using System.Collections.Generic;

namespace UsersManagement.Persistence.Models;

public partial class User
{
    public string Wallet { get; set; } = null!;

    public int Role { get; set; }

    public string Language { get; set; } = null!;

    public DateTime? CreationTimestamp { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DeleteDeadline { get; set; }

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual UserConfiguration? UserConfiguration { get; set; }
}
