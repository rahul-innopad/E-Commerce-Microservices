using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.APIResponseLibrary.Constant.RoleManager
{
    public class MasterRoleManager
    {
        public const string Admin = "Admin";
        public const string SuperAdmin = "SuperAdmin";
        public const string User = "User";
        public const string AdminOrSuperAdmin = Admin + "," + SuperAdmin;
        public const string AdminOrSuperAdminOrUser = Admin + "," + SuperAdmin + "," + User;

    }
}
