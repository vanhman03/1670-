using Asm2_1670.Models;

namespace Asm2_1670.Repository.IRepository
{
	public interface IUsersRepository : IRepository<User>
	{
		void Update(User entity);
	}
}
