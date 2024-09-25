using Asm2_1670.Data;
using Asm2_1670.Models;
using Asm2_1670.Repository.IRepository;

namespace Asm2_1670.Repository
{
	public class UsersRepository : Repository<User>, IUsersRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public UsersRepository(ApplicationDBContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public void Update(User entity)
		{
			_dbContext.Users.Update(entity);
		}
	}
}
