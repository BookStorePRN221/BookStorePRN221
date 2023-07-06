﻿using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.IService
{
    public interface IBookService
    {
        Task<bool> CreateBook(Book book, string url);
        Task<IEnumerable<BookDTO>> GetAllBook();
        Task<IEnumerable<BookDTO>> TakePageBook(int num, IEnumerable<Book> listBooks);
        Task<IEnumerable<BookDTO>> GetBookByCategory(int cateId);
        Task<BookDetailDTO> GetBookById(Guid bookId);
        Task<IEnumerable<Book>> GetBookByName(string bookName);
        Task<bool> UpdateBook(Book book);
        Task<bool> DeleteBook(Guid bookId);
        Task<bool> RestoreBook(Guid bookId);
        Task<IEnumerable<Book>> GetBook();
    }
}
