using System;
using System.Collections.Generic;

namespace TaskManagementMVC.Models;

public partial class UserRole
{
    public short RoleId { get; set; }

    public string RoleName { get; set; } = null!;
}
