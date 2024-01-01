using Microsoft.AspNetCore.Mvc;
using RespositoryPatternWithUOW.Core.Interfaces;

namespace RespositoryPatternWithUOW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(_unitOfWork.Authors.GetById(2));
        }

        [HttpGet("GetByIdAysnc")]
        public async Task<IActionResult> GetByIdAysnc()
        {
            return Ok(await _unitOfWork.Authors.GetByIdAsync(2));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Authors.GetAllAsync());
        }
    }
}
