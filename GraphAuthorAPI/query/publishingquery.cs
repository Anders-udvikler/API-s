using publishingcompanies;
using publishRepo;

namespace authorquery
{
    public class publishingquery
    {
        public async Task<List<publishingcompany>> GetAllPublishingCompanies(
        [Service] publishRepo.publishRepo repo)
    {
        var allPublishingCompanies = await repo.GetPublishingCompanies();
        return allPublishingCompanies;
    }

    public async Task<publishingcompany> GetPublishingCompanyById(
        int id,
        [Service] publishRepo.publishRepo repo)
    {
        return await repo.GetPublishingCompanyById(id);
    }
    

    public async Task<publishingcompany> UpdatePublishingCompany(
        int id,publishingcompany company,
        [Service] publishRepo.publishRepo repo)
    {
        return await repo.UpdatePublishingCompany(company, id);
    }
    

    public async Task<publishingcompany> AddPublishingCompany(
        int id,publishingcompany company,
        [Service] publishRepo.publishRepo repo)
    {
        return await repo.AddPublishingCompany(company);
    }

        public async Task<publishingcompany> DeletePublishingCompany(int id,
        [Service] publishRepo.publishRepo repo)
    {
        return await repo.DeletePublishingCompany(id);
    }



    }
    
}