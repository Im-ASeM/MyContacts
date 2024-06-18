using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[Action]")]
[ApiController]
public class UserController:Controller
{
    private IUserProcces uc;
    public UserController(IUserProcces _uc)
    {
        uc = _uc;
    }

    [HttpPost]
    public IActionResult Login(LoginUser user){
        return Ok(uc.Login(user));
    }
    
    [HttpPost]
    public IActionResult Register(NewUser user){
        return Ok(uc.Register(user));
    }

    [HttpPost]
    public IActionResult ResetPassword(string Username){
        return Ok(uc.ResetPassword(Username));
    } 

    [HttpPost]
    public IActionResult VerfiyPassword(ResetPassUser user){
        return Ok(uc.VerfiyPassword(user));
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public IActionResult GetUsers(){
        return Ok(uc.GetAllUsers());
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public IActionResult GetUser(string Username){
        return Ok(uc.GetUser(Username));
    }

    [HttpDelete]
    [Authorize(Roles = "admin")]
    public IActionResult DeleteUser(string Username){
        return Ok(uc.DeleteUser(Username));
    }

    [HttpPost]
    [Authorize(Roles = "owner")]
    public IActionResult Promote(string Username){
        return Ok(uc.Promote(Username));
    }

    [HttpPost]
    [Authorize(Roles ="owner")]
    public IActionResult Demote(string Username){
        return Ok(uc.Demote(Username));
    }
}