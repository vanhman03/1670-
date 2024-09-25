using Asm2_1670.Models;

namespace Asm2_1670.Repository.IRepository
{
	public interface IEducationsRepository : IRepository<Education>
	{
		void Update (Education entity);
	}
}
