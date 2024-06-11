using System;
using System.Collections.Generic;

namespace UsersManagement.Persistence.Models;

public partial class Wallet
{
    public string Address { get; set; } = null!;

    public string Language { get; set; } = null!;

    public DateTime? CreationTimestamp { get; set; }

    public bool? IsActive { get; set; }
    
    public bool? IsDeleted { get; set; }
    
    public DateTime? DeleteDeadline { get; set; }

    public virtual UserAdditionalDatum? UserAdditionalDatum { get; set; }
}
