using System.Collections.Generic;
using SmartLicense_SuperAdminSide.Models;

namespace SmartLicense_SuperAdminSide.Services
{
    public interface IAdminService
    {
        List<Admin> GetAllAdmins();
        void CreateAdmin(Admin admin);
        void EnableAdmin(string id);
        void DisableAdmin(string id);
    }
}