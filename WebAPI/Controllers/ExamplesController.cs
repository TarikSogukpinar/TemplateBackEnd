using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamplesController : ControllerBase
    {
        private readonly IExampleService _exampleService;

        public ExamplesController(IExampleService exampleService)
        {
            _exampleService = exampleService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _exampleService.GetAll();
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int exampleId)
        {
            var result = _exampleService.GetById(exampleId);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Example example)
        {
            var result = _exampleService.Add(example);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Example example)
        {
            var result = _exampleService.Delete(example);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Example example)
        {
            var result = _exampleService.Update(example);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }
    }
}