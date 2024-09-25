using Asm2_1670.Data;
using Asm2_1670.Repository.IRepository;

namespace Asm2_1670.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		public ApplicationDBContext _dbContext;
		public ICategoriesRepository CategoriesRepository { get; private set; }
		public IEducationsRepository EducationsRepository { get; private set; }
		public IAwardsRepository AwardsRepository { get; private set; }
		public IPortfoliosRepository PortfoliosRepository { get; private set; }
		public IWorkExperiencesRepository WorkExperiencesRepository { get; private set; }
		public IUsersRepository UsersRepository { get; private set; }
		public IJobsRepository JobsRepository { get; private set; }
		public IApplicationsRepository ApplicationsRepository { get; private set; }

		public UnitOfWork(ApplicationDBContext dbContext)
		{
			_dbContext = dbContext;
			CategoriesRepository = new CategoriesRepository(dbContext);
			EducationsRepository = new EducationsRepository(dbContext);
			AwardsRepository = new AwardsRepository(dbContext);
			PortfoliosRepository = new PortfoliosRepository(dbContext);
			WorkExperiencesRepository = new WorkExperiencesRepository(dbContext);
			UsersRepository = new UsersRepository(dbContext);
			JobsRepository = new JobsRepository(dbContext);
			ApplicationsRepository = new ApplicationsRepository(dbContext);
		}
		public void Save()
		{
			_dbContext.SaveChanges();
		}
	}
}
