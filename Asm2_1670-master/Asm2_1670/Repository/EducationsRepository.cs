using Asm2_1670.Data;
using Asm2_1670.Models;
using Asm2_1670.Repository.IRepository;

namespace Asm2_1670.Repository
{
	public class EducationsRepository : Repository<Education>, IEducationsRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public EducationsRepository(ApplicationDBContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public void Update(Education entity)
		{
			_dbContext.Educations.Update(entity);
		}
	}
}
