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
    // dbo.spUser_GetAll - ZERO parameters - Returns [Id] [FirstName] [LastName]
    // IEnumerable return ALL Users
    // Loadtata return Usermodel, dynamic = any type or here none - (Stored precudure, parameter)
    public Task<IEnumerable<UserModel>> GetUsers() =>
        _db.LoadData<UserModel, dynamic>("dbo.spUser_GetAll", new { });

    // dbo.spUser_Get - [Id] parameter - Returns where [Id]
    // Return ONE User if any ? = nullable
    // Id = id to match Usermodel with capital I and parameter with lowercase i
    public async Task<UserModel?> GetUser(int id)
    {
        var results = await _db.LoadData<UserModel, dynamic>("dbo.spUser_Get", new { Id = id });

        return results.FirstOrDefault();
    }

    // dbo.spUser_Insert - [FirstName] [LastName] parameters - Insert Values
    public Task InsertUser(UserModel user) =>
        _db.SaveData("dbo.spUser_Insert", new { user.FirstName, user.LastName });

    // dbo.spUser_Update - [Id][FirstName] [LastName] parameters - Update entire User
    public Task UpdateUser(UserModel user) =>
         _db.SaveData("dbo.spUser_Update", user);

    // dbo.spUser_Delete - [Id] parameter - Delete where [Id]
    public Task DeleteUser(int id) =>
        _db.SaveData("dbo.spUser_Delete", new { Id = id });

}
