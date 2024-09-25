using Asm2_1670.Models;

namespace Asm2_1670.Repository.IRepository
{
	public interface IJobsRepository : IRepository<Job>
	{
		void Update(Job entity);
	}
}
