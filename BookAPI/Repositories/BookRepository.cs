using BookAPI.Data;
using BookAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetAllAsync(string? filterOn =null, string? filterQuery = null,
          string? sortBy = null ,bool asc =true,
          int pageNumber = 1, int pageSize = 10000)
        {
            var book = _context.Books.AsQueryable();
            //Filter 
            if(string.IsNullOrWhiteSpace(filterOn) ==false && string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                if (filterOn.Equals("BookName" ,StringComparison.OrdinalIgnoreCase))
                {
                    book =book.Where(x=>x.BookName.Contains(filterQuery));
                }
               
            }
            //Sorting 
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("BookName", StringComparison.OrdinalIgnoreCase))
                {
                    book = asc ? _context.Books.OrderBy(x=>x.BookName): _context.Books.OrderByDescending(x => x.BookName);
                }
                else if(sortBy.Equals("BookPrice", StringComparison.OrdinalIgnoreCase))
                {
                    book = asc ? _context.Books.OrderBy(x => x.BookPrice) : _context.Books.OrderByDescending(x => x.BookPrice);
                }

            }
            //Pagging

            var skipResult = (pageNumber - 1) * pageSize;
             
          //  book = book.Skip(skipResult);

            return await book.Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}
