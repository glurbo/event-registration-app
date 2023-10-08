using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using DAL;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace webapi.Controllers.Identity;

[ApiController]
[Route("api/identity/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AccountController> _logger;
    private readonly EventRegistrationDbContext _context;

    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, 
        IConfiguration configuration, ILogger<AccountController> logger, EventRegistrationDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
        _context = context;
    }
    
    [HttpPost]
    public async Task<ActionResult<string>> LogIn([FromBody] Login loginData)
    {
        // verify username
        var appUser = await _userManager.FindByEmailAsync(loginData.Email);
        if (appUser == null)
        {
            _logger.LogWarning("WebApi login failed, email {} not found", loginData.Email);
        
            return NotFound(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.NotFound,
                Error = "Username problem"
            });
        }
        
        // verify username and password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginData.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("WebApi login failed, password problem for user {}", loginData.Email);
            return NotFound(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.NotFound,
                Error = "User/Password problem"
            });
        }
        
        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        
        // generate jwt
        var jwt = IdentityHelpers.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"]!,
            _configuration["JWT:Issuer"]!,
            _configuration["JWT:Audience"]!
        );
        return Ok(jwt);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    
}

public static class IdentityHelpers
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        return Guid.Parse(
            user.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
    
    public static string GenerateJwt(IEnumerable<Claim> claims, string key, string issuer, string audience)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddSeconds(50000);
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}



public class Login
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class RestApiErrorResponse
{
    public HttpStatusCode Status { get; set; }
    public string Error { get; set; } = default!;
}



