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

    public UserService([FromServices] UserRepository repository)
    {
        _repository = repository;
    }  

    public void CreateUser(CreateUserReq newUser) 
    {
        var existing = _repository.GetUserByEmail(newUser.Email);
        if(existing is not null)
        {
            throw new ExistingEmailException("Email alredy exists"); 
        }

        var user = newUser.Adapt<User>();
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.CreatedAt = DateTime.Now;

        _repository.CreateUser(user); 

    }
}
