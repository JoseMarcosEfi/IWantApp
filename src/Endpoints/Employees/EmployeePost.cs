using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;

namespace IWantApp.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest EmployeeRequest, UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser { UserName = EmployeeRequest.Email, Email = EmployeeRequest.Email };
        var result = userManager.CreateAsync(user, EmployeeRequest.Password).Result;

        if(!result.Succeeded)
            return Results.BadRequest(result.Errors.First());
 
        return Results.Created($"/employees/{user.Id}", user.Id);
    }
}
