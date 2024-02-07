using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NutritionAPI.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace NutritionAPI.Presentation.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IAuthenticationManager _authenticationManager;

    public AuthenticationController(IMapper mapper, UserManager<User> userManager,
        IAuthenticationManager authenticationManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _authenticationManager = authenticationManager;
    }

    /// <summary>
    /// Registers a new user
    /// </summary>
    /// <returns>Returns 201 Created if registration is successful; otherwise, returns BadRequest</returns>
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
    {
        User? user = _mapper.Map<User>(userForRegistration);

        IdentityResult? result = await _userManager.CreateAsync(user, userForRegistration.Password);
        if (!result.Succeeded)
        {
            foreach (IdentityError? error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }

        await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

        return StatusCode(201);
    }

    /// <summary>
    /// Authenticates a user
    /// </summary>
    /// <returns>Returns OK with a JWT token if authentication is successful; otherwise, returns Unauthorized</returns>
    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto
        user)
    {
        if (!await _authenticationManager.ValidateUser(user))
        {
            return Unauthorized();
        }

        return Ok(new {Token = await _authenticationManager.CreateToken()});
    }
}