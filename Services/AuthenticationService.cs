using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using cookbook_api.Dtos.User;
using cookbook_api.Models;
using cookbook_api.Repositories;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace cookbook_api.Services;

public class AuthenticationService
{
    private readonly UserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthenticationService(
        [FromServices] UserRepository repository,
        [FromServices] IConfiguration configuration)
    {
        _userRepository = repository;
        _configuration = configuration;
    }

    public TokenResponse Login(LoginReq login)
    {
        var user = _userRepository.GetUserByEmail(login.Email);
        if ((user is null) || (!BCrypt.Net.BCrypt.Verify(login.Password, user.Password)))
        {
            throw new Exception("incorrect username or password!");
        }

        var tokenJWT = GenerateJWT(user);

        return new TokenResponse
        {
            Token = tokenJWT,
        };
    }

    private string GenerateJWT(User user)
    {
        //Pegando a chave JWT
        var JWTKey = Encoding.ASCII.GetBytes(_configuration["JWTKey"]);

        //Criando as credenciais
        var credenciais = new SigningCredentials(
                new SymmetricSecurityKey(JWTKey),
                SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name)
        };


        //Criando o token
        var tokenJWT = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(8),
            signingCredentials: credenciais,
            claims: claims
        );

        //Escrevendo o token e retornando
        return new JwtSecurityTokenHandler().WriteToken(tokenJWT);
    }

}
