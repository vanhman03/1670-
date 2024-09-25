using Asm2_1670.Data;
using Asm2_1670.Models;
using Asm2_1670.Repository.IRepository;

namespace Asm2_1670.Repository
{
	public class PortfoliosRepository : Repository<Portfolio>, IPortfoliosRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public PortfoliosRepository(ApplicationDBContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public void Update(Portfolio entity)
		{
			_dbContext.Portfolios.Update(entity);
		}
	}
}
