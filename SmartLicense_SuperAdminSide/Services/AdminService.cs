using System.Collections.Generic;
using MongoDB.Driver;
using SmartLicense_SuperAdminSide.Data;
using SmartLicense_SuperAdminSide.Models;

namespace SmartLicense_SuperAdminSide.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMongoCollection<Admin> _admins;

        public AdminService(MongoDbContext context)
        {
            _admins = context.Admins;
        }

        public List<Admin> GetAllAdmins()
        {
            return _admins.Find(admin => true).ToList();
        }

        public void CreateAdmin(Admin admin)
        {
            _admins.InsertOne(admin);
        }

        public void EnableAdmin(string id)
        {
            var update = Builders<Admin>.Update.Set(a => a.IsEnabled, true);
            _admins.UpdateOne(a => a.Id == id, update);
        }

        public void DisableAdmin(string id)
        {
            var update = Builders<Admin>.Update.Set(a => a.IsEnabled, false);
            _admins.UpdateOne(a => a.Id == id, update);
        }
    }
}