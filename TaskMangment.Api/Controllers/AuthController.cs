using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskMangment.Api.Authentications;
using TaskMangment.Api.DTOs;
using TaskMangment.Api.Middlewares.Attributes;
using TaskMangment.Api.Routes;
using TaskMangment.Buisness.Models;
using TaskMangment.Buisness.Models.Users;
using TaskMangment.Buisness.Services.SUser;
using TaskMangment.Data.Entities;
using TaskMangment.Data.Repositories.RUser;

[ApiController]
[Route(AuthRoute.Base)]
[SkipValidateId]
public class AuthController : ControllerBase
{
    private readonly JwtOptions _jwtOptions;
    private readonly IUserService _service;
    private readonly IMapper _mapper;

    public AuthController(JwtOptions jwtOptions, IUserService service,IMapper mapper)
    {
        _jwtOptions = jwtOptions;
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    [Route(AuthRoute.Login)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult>Login([FromBody] LoginDto user)
    {
        var model = _mapper.Map<LoginModel>(user);
        // Validate user credentials (this is just an example)
        var result = await _service.AuthorizeAsync(model);
        
        if (result is null)
            return Unauthorized();
        var token = GenerateJwtToken(result.Value);
        return Ok(new { token });

    }

    private string GenerateJwtToken(AuthorizeUserModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role!)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationInMinutes),
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}