using Asm2_1670.Models;

namespace Asm2_1670.Repository.IRepository
{
	public interface IApplicationsRepository : IRepository<Application>
	{
		void Update(Application entity);
	}
}
