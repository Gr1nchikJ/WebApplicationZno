using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationZno.Models;

namespace WebApplicationZno.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet(Name = "GetAllQuestions")]
        public async Task<IEnumerable<QuestionModel>> GetAll()
        {
            return await _questionRepository.GetAllQuestions();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            QuestionModel question = await _questionRepository.GetById(id);

            if (question == null)
            {
                return NotFound();
            }

            return new ObjectResult(question);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuestionModel question)
        {
            if (question == null)
            {
                return BadRequest();
            }
            await _questionRepository.Create(question);
            return CreatedAtRoute("GetQuestionItem", new { id = question.Id }, question);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] QuestionModel question)
        {
            if (question == null || question.Id != id)
            {
                return BadRequest();
            }

            var questionItem = _questionRepository.GetById(id);
            if (questionItem == null)
            {
                return NotFound();
            }

            await _questionRepository.Update(question);
            return new OkObjectResult(question);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedQuestionItem = await _questionRepository.Delete(id);

            if (deletedQuestionItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedQuestionItem);
        }

    }
}
