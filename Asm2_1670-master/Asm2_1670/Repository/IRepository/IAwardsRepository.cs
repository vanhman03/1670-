using Asm2_1670.Models;

namespace Asm2_1670.Repository.IRepository
{
	public interface IAwardsRepository : IRepository<Award>
	{
		void Update(Award entity);
	}
}
