using Asm2_1670.Data;
using Asm2_1670.Models;
using Asm2_1670.Repository.IRepository;

namespace Asm2_1670.Repository
{
	public class WorkExperiencesRepository : Repository<WorkExperience>, IWorkExperiencesRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public WorkExperiencesRepository(ApplicationDBContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public void Update(WorkExperience entity)
		{
			_dbContext.WorkExperiences.Update(entity);
		}
	}
}
