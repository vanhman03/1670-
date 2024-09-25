namespace Asm2_1670.Repository.IRepository
{
	public interface IUnitOfWork
	{
		ICategoriesRepository CategoriesRepository { get; }
		IEducationsRepository EducationsRepository { get; }
		IAwardsRepository AwardsRepository { get; }
		IPortfoliosRepository PortfoliosRepository { get; }
		IWorkExperiencesRepository WorkExperiencesRepository { get; }
		IUsersRepository UsersRepository { get; }
		IJobsRepository JobsRepository { get; }
		IApplicationsRepository ApplicationsRepository { get; }
		void Save();
	}
}
