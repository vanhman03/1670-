using Asm2_1670.Data;
using Asm2_1670.Models;
using Asm2_1670.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Asm2_1670.Repository
{
	public class AwardsRepository : Repository<Award>, IAwardsRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public AwardsRepository(ApplicationDBContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public void Update(Award entity)
		{
			_dbContext.Awards.Update(entity);
		}
	}
}
