using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services;

internal class UsersService : IUsersService
{
  private readonly IUsersRepository _usersRepository;
  public UsersService(IUsersRepository usersRepository)
  {
    _usersRepository = usersRepository;
  }

  public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
  {
    ApplicationUser? user = await _usersRepository
      .GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

    if (user == null) {
      return null;
    }

    return new AuthenticationResponse(
      UserID:user.UserID,
      Email:user.Email,
      PersonName: user.PersonName,
      Gender: user.Gender,
      Token: "Token",
      Success: true
    );
  }

  public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
  {
    //Create a new ApplicationUser object from RegisterRequest
    ApplicationUser user = new ApplicationUser()
    {
      PersonName = registerRequest.PersonName,
      Email = registerRequest.Email,
      Password = registerRequest.Password,
      Gender = registerRequest.Gender.ToString(),
    };

    ApplicationUser? registeredUser = await _usersRepository.AddUser(user);

    if (registeredUser == null) {
      return null;
    }

    return new AuthenticationResponse(
      UserID: registeredUser.UserID,
      Email: registeredUser.Email,
      PersonName: registeredUser.PersonName,
      Gender: registeredUser.Gender,
      Token: "Token",
      Success: true
      );
  }
}
