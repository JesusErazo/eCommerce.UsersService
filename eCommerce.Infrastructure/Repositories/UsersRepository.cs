using Dapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;

namespace eCommerce.Infrastructure.Repositories;

internal class UsersRepository : IUsersRepository
{
  private readonly DapperDbContext _dbContext;

  public UsersRepository(DapperDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<ApplicationUser?> AddUser(ApplicationUser user)
  {
    //Generate a new unique user ID for the user.
    user.UserID = Guid.NewGuid();

    string query = @"INSERT INTO public.""Users""
                   (""UserID"",""Email"",""PersonName"",""Gender"",""Password"")
                   VALUES(@UserID,@Email,@PersonName,@Gender,@Password)";

    int rowCountAffected = await _dbContext.DbConnection.ExecuteAsync(query, user);

    if (rowCountAffected > 0) {
      return user;
    }

    return null;
  }

  public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
  {
    return new ApplicationUser()
    {
      UserID = Guid.NewGuid(),
      Email = email,
      Password = password,
      PersonName = "Person name",
      Gender = GenderOptions.Male.ToString(),
    };
  }
}
