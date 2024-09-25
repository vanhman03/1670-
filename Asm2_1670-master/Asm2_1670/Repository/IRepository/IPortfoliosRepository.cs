using Asm2_1670.Models;

namespace Asm2_1670.Repository.IRepository
{
	public interface IPortfoliosRepository : IRepository<Portfolio>
	{
		void Update(Portfolio entity);
	}
}
