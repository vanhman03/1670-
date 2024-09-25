using Asm2_1670.Models;

namespace Asm2_1670.Repository.IRepository
{
	public interface IWorkExperiencesRepository : IRepository<WorkExperience>
	{
		void Update(WorkExperience entity);
	}
}
