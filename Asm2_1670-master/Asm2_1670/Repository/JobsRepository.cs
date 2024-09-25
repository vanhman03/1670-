using Asm2_1670.Data;
using Asm2_1670.Models;
using Asm2_1670.Repository.IRepository;

namespace Asm2_1670.Repository
{
	public class JobsRepository : Repository<Job>, IJobsRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public JobsRepository(ApplicationDBContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public void Update(Job entity)
		{
			_dbContext.Jobs.Update(entity);
		}
	}
}
