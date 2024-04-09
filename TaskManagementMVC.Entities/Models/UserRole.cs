using System;
using System.Collections.Generic;

namespace TaskManagementMVC.Entities.Models;

public partial class UserRole
{
    public short RoleId { get; set; }

    public string RoleName { get; set; } = null!;
}
