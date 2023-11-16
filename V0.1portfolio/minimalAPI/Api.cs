namespace minimalAPI;

public static class Api
{
    // API endpoint mapping
    // Only public method exposed - mapping endpoints
    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("/Users", GetUsers);
        app.MapGet("/Users/{id}", GetUser);
        app.MapPost("/Users", InsertUser);
        app.MapPut("/Users", UpdateUser);
        app.MapDelete("/Users", DeleteUser);
    }
    

    // Endpoint for UserData GetUsers()
    // Wraps result as IResult - could be oneline :" await data.GetUsers(); "
    // This way makes it possible to add logging and more
    private static async Task<IResult> GetUsers(IUserData data)
    {
        try
        {
            return Results.Ok(await data.GetUsers());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    
    // Endpoint for UserData GetUser(id)
    // Id parameter from Api call, but IUserData from Dependency injection
    private static async Task<IResult> GetUser(int id, IUserData data)
    {
        try
        {
            var result = await data.GetUser(id);
            if (result == null) return Results.NotFound();
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    // Endpoint for UserData InsertUser(user)
    private static async Task<IResult> InsertUser(UserModel user, IUserData data)
    {
        try
        {
            await data.InsertUser(user);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    // Endpoint for UserData UpdatetUser(user)
    private static async Task<IResult> UpdateUser(UserModel user, IUserData data)
    {
        try
        {
            await data.UpdateUser(user);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    // Endpoint for UserData DeleteUser(id)
    private static async Task<IResult> DeleteUser(int id, IUserData data)
    {
        try
        {
            await data.DeleteUser(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
        
}
