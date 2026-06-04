public interface IReviewRepository
{
    Task<List<Review>> GetByReferenceAsync(string reviewType, Guid referenceId);
    Task AddAsync(Review review);
    Task DeleteAsync(Guid id);
}