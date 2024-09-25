using Asm2_1670.Data;
using Asm2_1670.Models;
using Asm2_1670.Repository.IRepository;

namespace Asm2_1670.Repository
{
	public class CategoriesRepository : Repository<Categories>, ICategoriesRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public CategoriesRepository(ApplicationDBContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public void Update(Categories entity)
		{
			_dbContext.Categories.Update(entity);
		}
	}
}
