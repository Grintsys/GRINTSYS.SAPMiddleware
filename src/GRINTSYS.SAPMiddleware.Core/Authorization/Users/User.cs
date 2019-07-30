using System;
using System.Collections.Generic;
using Abp.Authorization.Users;
using Abp.Extensions;
using GRINTSYS.SAPMiddleware.M2;

namespace GRINTSYS.SAPMiddleware.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";

        public String PrintBluetoothAddress { get; set; }
        public Int32 SalesPersonId { get; set; }
        public Int32 CollectId { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                CollectId = 0,
                SalesPersonId = 0,
                PrintBluetoothAddress = "",
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}
