using Asm2_1670.Data;
using Asm2_1670.Models;
using Asm2_1670.Repository.IRepository;

namespace Asm2_1670.Repository
{
	public class ApplicationsRepository:Repository<Application>, IApplicationsRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public ApplicationsRepository(ApplicationDBContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public void Update(Application entity)
		{
			_dbContext.Applications.Update(entity);
		}
	}
}
