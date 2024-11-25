using BookAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync(string? filterOn =null, string? filterQuery = null ,string? sortBy=null, bool asc=true ,
                int pageNumber = 1, int pageSize = 10000);
        Task<Book> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
    }
}
