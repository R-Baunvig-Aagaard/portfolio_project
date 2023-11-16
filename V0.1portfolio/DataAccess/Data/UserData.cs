using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data;

/// <summary>
/// Use SqlDataAccess to connect to db and get the Load and Save Data methods
/// LoadData<Usermodel, Parameter>(StoreProcedure, Parameter)
/// SaveData<Parameter>(StoreProcedure, Parameter)
/// </summary>

public class UserData : IUserData
{
    private readonly ISqlDataAccess _db;

    public UserData(ISqlDataAccess db)
    {
        _db = db;
    }
    // dbo.spUser.GetAll - ZERO parameters - Returns [Id] [FirstName] [LastName]
    // IEnumerable return ALL Users
    // Loadtata return Usermodel, dynamic = any type or here none - (Stored precudure, parameter)
    public Task<IEnumerable<UserModel>> GetUsers() =>
        _db.LoadData<UserModel, dynamic>("dbo.spUser.GetAll", new { });

    // dbo.spUser.Get - [Id] parameter - Returns where [Id]
    // Return ONE User if any ? = nullable
    // Id = id to match Usermodel with capital I and parameter with lowercase i
    public async Task<UserModel?> GetUser(int id)
    {
        var results = await _db.LoadData<UserModel, dynamic>("dbo.spUser.Get", new { Id = id });

        return results.FirstOrDefault();
    }

    // dbo.spUser.Insert - [FirstName] [LastName] parameters - Insert Values
    public Task InsertUser(UserModel user) =>
        _db.SaveData("dbo.spUser.Insert", new { user.FirstName, user.LastName });

    // dbo.spUser.Update - [Id][FirstName] [LastName] parameters - Update entire User
    public Task UpdateUser(UserModel user) =>
         _db.SaveData("dbo.spUser.Update", user);

    // dbo.spUser.Delete - [Id] parameter - Delete where [Id]
    public Task DeleteUser(int id) =>
        _db.SaveData("dbo.spUser.Delete", new { Id = id });

}
