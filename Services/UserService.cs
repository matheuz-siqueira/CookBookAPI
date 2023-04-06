using cookbook_api.Dtos.User;
using cookbook_api.Exceptions;
using cookbook_api.Models;
using cookbook_api.Repositories;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace cookbook_api.Services;

public class UserService
{
    private readonly UserRepository _repository;

    private readonly AuthenticationService _authService; 

    public UserService(
        [FromServices] UserRepository repository,
        [FromServices] AuthenticationService authService)
    {
        _repository = repository;
        _authService = authService;
    }  

    public string CreateUser(CreateUserReq newUser) 
    {
        var existing = _repository.GetUserByEmail(newUser.Email);
        if(existing is not null)
        {
            throw new ExistingEmailException("Email alredy exists"); 
        }

        var user = newUser.Adapt<User>();
        var userLogin = newUser.Adapt<User>();
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.CreatedAt = DateTime.Now;
        _repository.CreateUser(user);
        
        
        var login = userLogin.Adapt<LoginReq>(); 
        return _authService.Login(login);  
    }
}
