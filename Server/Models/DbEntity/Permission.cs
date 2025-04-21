using System;
using System.Collections.Generic;

namespace Server.Models.DbEntity;

public partial class Permission
{
    public int Id { get; set; }

    public string TableName { get; set; } = null!;

    public string Operation { get; set; } = null!;

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
