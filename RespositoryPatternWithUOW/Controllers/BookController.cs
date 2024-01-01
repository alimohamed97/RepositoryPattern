using Microsoft.AspNetCore.Mvc;
using RespositoryPatternWithUOW.Core.Const;
using RespositoryPatternWithUOW.Core.Interfaces;
using RespositoryPatternWithUOW.Core.Models;

namespace RespositoryPatternWithUOW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(_unitOfWork.Books.GetById(2));
        }

        [HttpGet("GetByIdAysnc")]
        public async Task<IActionResult> GetByIdAysnc()
        {
            return Ok(await _unitOfWork.Books.GetByIdAsync(2));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Books.GetAllAsync());
        }

        [HttpGet("GetBynName")]
        public IActionResult GetBynName()
        {
            return Ok(_unitOfWork.Books.Find(b => b.Title == "book1", new[] { "Author" }));
        }


        [HttpGet("GetAllBynName")]
        public IActionResult GetAllBynName()
        {
            return Ok(_unitOfWork.Books.FindAll(b => b.Title == "book1", new[] { "Author" }));
        }

        [HttpGet("GetAllBynNameSkip")]
        public IActionResult GetAllBynNameSkip()
        {
            return Ok(_unitOfWork.Books.FindAll(b => b.Title.Contains("book"), 1, 1, new[] { "Author" }));
        }


        [HttpGet("GetOrdered")]
        public IActionResult GetOrdered()
        {
            return Ok(_unitOfWork.Books.FindAll(b => b.Title.Contains("book"), null, null, b => b.Id, OrderBy.Descending, new[] { "Author" }));
        }

        [HttpPost("ADD")]
        public IActionResult Add()
        {
            var book = _unitOfWork.Books.Add(new Book { Title = "Title1", AuthorID = 1 });
            _unitOfWork.Complete();
            return Ok(book);
        }

        [HttpPost("Update")]
        public IActionResult Update()
        {
            var book = _unitOfWork.Books.GetById(1);
            book.Title = "EL Masl7a";
            _unitOfWork.Books.Update(book);
            _unitOfWork.Complete();
            return Ok(book);
        }
        [HttpPost("Delete")]
        public IActionResult Delete()
        {
            var book = _unitOfWork.Books.GetById(2);
            _unitOfWork.Books.Delete(book);
            _unitOfWork.Complete();
            return Ok(book);
        }

        [HttpPost("Attach")]
        public IActionResult Attach()
        {
            var book = _unitOfWork.Books.GetById(3);
            _unitOfWork.Books.Attach(book);
            _unitOfWork.Complete();
            return Ok(book);
        }


    }
}
