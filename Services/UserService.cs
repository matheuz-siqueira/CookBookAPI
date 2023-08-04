using System.Security.Claims;
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

    public TokenResponse CreateUser(CreateUserReq newUser)
    {
        var existing = _repository.GetUserByEmail(newUser.Email);
        if (existing is not null)
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

    public GetProfileResponse GetProfile(ClaimsPrincipal logged)
    {
        var userId = UserId(logged);
        var response = _repository.GetProfile(userId);
        return response.Adapt<GetProfileResponse>();
    }

    public void UpdatePassword(UpdatePasswordReq updatePass, ClaimsPrincipal logged)
    {
        var id = int.Parse(logged.FindFirstValue(ClaimTypes.NameIdentifier));
        var user = GetById(id, true);

        if (!BCrypt.Net.BCrypt.Verify(updatePass.CurrentPassword, user.Password))
        {
            throw new IncorretPassword("Incorret password");
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(updatePass.NewPassword);
        _repository.UpdatePassword();

    }

    public User GetById(int id, bool tracking = true)
    {
        var user = _repository.GetById(id, tracking) ?? throw new UserException("User not found");
        return user;
    }

    private static int UserId(ClaimsPrincipal logged)
    {
        var id = int.Parse(logged.FindFirstValue(ClaimTypes.NameIdentifier));
        return id;
    }
}
