using publishingcompanies;
using publishRepo;
namespace publishingmutation
{
    public class publishingmutation
    {
        public async Task<publishingcompany> AddBook( publishingcompany book,
        [Service] publishRepo.publishRepo repo)
    {
        return await repo.AddPublishingCompany(book);
    }
      public async Task<publishingcompany> UpdateBook(
        int id, publishingcompany book,
        [Service] publishRepo.publishRepo repo)
        {
            return await repo.UpdatePublishingCompany(book, id);
        }
    
        public async Task<publishingcompany> DeleteBook(int id,
        [Service] publishRepo.publishRepo repo)
        {
            return await repo.DeletePublishingCompany(id);
        }

}
}