using BookingAppStoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BookingAppStoreMVC.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            using (UserContext userContext = new UserContext())
            {
                User user = userContext.Users.FirstOrDefault(u => u.Email == username);
                if (user != null)
                {
                    Role role = userContext.Roles.Find(user.RoleId);
                    if (role != null)
                        roles = new string[] { role.Name };
                }
            }
            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;
            using (UserContext userContext = new UserContext())
            {
                User user = userContext.Users.FirstOrDefault(u => u.Email == roleName);
                if (user != null)
                {
                    Role role = userContext.Roles.Find(user.RoleId);
                    if (role != null && role.Name ==roleName)
                    {
                        outputResult = true;
                    }
                }
            }
            return outputResult;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}