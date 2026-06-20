using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Services;
namespace UserManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return Ok(_userService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = _userService.GetById(id);
        if (user == null)
        {
            return NotFound(); // Returns 404
        }
        return Ok(user);       // Returns 200
    }

    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        var createdUser = _userService.Add(user);
        // Returns 201 Created and points to the GetUser endpoint to retrieve it
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, User user)
    {
        var updated = _userService.Update(id, user);
        if (!updated)
        {
            return NotFound();
        }
        return NoContent();    // Returns 204 (Successful update, no content to return)
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var deleted = _userService.Delete(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();    // Returns 204
    }
}