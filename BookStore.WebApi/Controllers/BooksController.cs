using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Dto;
using BookStore.Entities;
using BookStore.Response;
using BookStore.WebApi.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebApi.Controllers
{
    [ApiController]
    public class BooksController : ControllerBase
    {
        BookStoreContext _bookStoreContext;
        IMapper _mapper;
        public BooksController(BookStoreContext bookStoreContext, IMapper mapper)
        {
            _bookStoreContext = bookStoreContext;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("get-all-book")]
        public async Task<ActionResult<ServiceResponse<List<BookListDto>>>> Get()
        {
            var response = new ServiceResponse<List<BookListDto>>();
            try
            {
                var books = await _bookStoreContext.Books.Include("Categories").Include("Publishers").Include("Authors").Select(x => new BookListDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CategoryName = x.Categories.Name,
                    PublisherName = x.Publishers.Name,
                    AuthorName = x.Authors.Name,
                    NumberOfPage = x.NumberOfPage

                }).ToListAsync();
                response.IsSuccessful = true;
                response.Entity = books;
                response.Message = "Book listing successful";
                return Ok(response);
            }
            catch (Exception exception)
            {
                response.Message = exception.Message;
                response.Entity = null;
                response.IsSuccessful = false;
                return BadRequest(response);
            }

        }

        [HttpPost]
        [Route("add-book")]
        public async Task<ActionResult<ServiceResponse<BookDto>>> Add(BookDto booksDto)
        {
            var response = new ServiceResponse<BookDto>();
            try
            {
                var entry = _bookStoreContext.Entry(_mapper.Map<Books>(booksDto));
                entry.State = EntityState.Added;
                await _bookStoreContext.SaveChangesAsync();
                response.Entity = _mapper.Map<BookDto>(entry.Entity);
                response.IsSuccessful = true;
                response.Message = "Book registration successful";
                return Ok(response);
            }
            catch (Exception exception)
            {
                response.Message = exception.Message;
                response.Entity = null;
                response.IsSuccessful = false;
                return BadRequest(response);
            }
        }
        [HttpDelete]
        [Route("delete-book/{bookId}")]
        public async Task<ActionResult<ServiceResponse<BookDto>>> Delete(int bookId)
        {
            var response = new ServiceResponse<BookDto>();
            try
            {
                var book = await _bookStoreContext.Books.FirstOrDefaultAsync(x => x.Id == bookId);
                if (book == null)
                {
                    response.Message = "Book Record not found";
                    response.Entity = null;
                    response.IsSuccessful = false;
                    return NotFound(response);
                }
                var entry = _bookStoreContext.Entry(book);
                entry.State = EntityState.Deleted;
                await _bookStoreContext.SaveChangesAsync();
                response.Entity = _mapper.Map<BookDto>(entry.Entity);
                response.IsSuccessful = true;
                response.Message = "Book deletion successful";
                return Ok(response);
            }
            catch (Exception exception)
            {
                response.Message = exception.Message;
                response.Entity = null;
                response.IsSuccessful = false;
                return BadRequest(response);
            }
        }


        [HttpPut]
        [Route("update-book")]
        public async Task<ActionResult<ServiceResponse<BookDto>>> Update(BookDto booksDto)
        {
            var response = new ServiceResponse<BookDto>();
            try
            {
                var entry = _bookStoreContext.Entry(_mapper.Map<Books>(booksDto));
                entry.State = EntityState.Modified;
                await _bookStoreContext.SaveChangesAsync();
                response.Entity = _mapper.Map<BookDto>(entry.Entity);
                response.IsSuccessful = true;
                response.Message = "Book editing successful";
                return Ok(response);
            }
            catch (Exception exception)
            {
                response.Message = exception.Message;
                response.Entity = null;
                response.IsSuccessful = false;
                return BadRequest(response);
            }
        }


        [HttpGet]
        [Route("get-book/{bookId}")]
        public async Task<ActionResult<ServiceResponse<BookDto>>> GetBookById(int bookId)
        {
            var response = new ServiceResponse<BookDto>();
            try
            {
                var book = await _bookStoreContext.Books.FirstOrDefaultAsync(x => x.Id == bookId);
                response.Entity = _mapper.Map<BookDto>(book);
                response.IsSuccessful = true;
                response.Message = "Book editing successful";
                return Ok(response);
            }
            catch (Exception exception)
            {
                response.Message = exception.Message;
                response.Entity = null;
                response.IsSuccessful = false;
                return BadRequest(response);
            }

        }
    }
}