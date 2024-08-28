using Jose;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task_Management_System.Core.Dto;
using Task_Management_System.Service;
using JwtSettings = Task_Management_System.Core.ModelEntity.JwtSettings;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{

    private readonly IUserService _userService;
    private readonly JwtSettings _jwtSettings;

    public UsersController(IOptions<JwtSettings> jwtSettings, IUserService userService)
    {
        _jwtSettings = jwtSettings.Value;
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<bool>> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userService.AuthenticateAsync(loginDto.Username, loginDto.PasswordUser);
        if (user == null)
        {
            return Unauthorized();
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, user.Username),
            ]),
            Expires = DateTime.UtcNow.AddHours(12),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = signingCredentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
    }

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers(string? search, int? pageIndex, int? pageSize)
    {
        var currentPageIndex = pageIndex ?? 1;
        var currentPageSize = pageSize ?? 10;

        var users = await _userService.GetAllUsers(search, currentPageIndex, currentPageSize);
        return Ok(users);
    }

    [HttpGet("GetUserById/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserById(id);
        return Ok(user); 
    }


    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateandUpdateUserDto dto)
    {
        var createdUserDto = await _userService.CreateUser(dto);
        return Ok(createdUserDto);
    }


    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser([FromBody] CreateandUpdateUserDto dto, int Id)
    {
        await _userService.UpdateUser(dto, Id);
        return Ok(Id);
    }

    [HttpDelete("DeleteUser/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok (await _userService.DeleteUser(id));
    }


    [HttpGet("GetAllUserByTaskId/{Id}")]
    public async Task<IActionResult> GetAllUserByTaskId(int Id)
    {
        return Ok(await _userService.GetAllUserByUserTaskId(Id));
    }
}
