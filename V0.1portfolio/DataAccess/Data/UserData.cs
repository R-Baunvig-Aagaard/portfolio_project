using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data;
public class UserData : IUserData
{
    private readonly ISqlDataAccess _db;

    public UserData(ISqlDataAccess db)
    {
        _db = db;
    }
    // IEnumerable return ALL Users
    public Task<IEnumerable<UserModel>> GetUsers() =>
        _db.LoadData<UserModel, dynamic>("dbo.spUser.GetAll", new { });
    // Loadtata return Usermodel, dynamic = any or here none - (Stored precudure, parameter)

    // Return ONE User if any ? = nullable
    public async Task<UserModel?> GetUser(int id)
    {
        var results = await _db.LoadData<UserModel, dynamic>("dbo.spUser.Get", new { Id = id });

        return results.FirstOrDefault();
    }

    public Task InsertUser(UserModel user) =>
        _db.SaveData("dbo.spUser.Insert", new { user.FirstName, user.LastName });

    public Task UpdateUser(UserModel user) =>
         _db.SaveData("dbo.spUser.Update", user);

    public Task DeleteUser(int id) =>
        _db.SaveData("dbo.spUser.Delete", new { Id = id });

}
