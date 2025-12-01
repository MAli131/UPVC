using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UPVC.Data;

namespace UPVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatbotApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions(string lang = "ar")
        {
            var faqs = await _context.ChatbotFAQs
                .Where(f => f.IsActive && !f.IsDeleted)
                .OrderBy(f => f.DisplayOrder)
                .Select(f => new
                {
                    id = f.Id,
                    question = lang == "en" ? f.QuestionEn : f.QuestionAr,
                    answer = lang == "en" ? f.AnswerEn : f.AnswerAr,
                    category = f.Category
                })
                .ToListAsync();

            return Ok(faqs);
        }

        [HttpGet("question/{id}")]
        public async Task<IActionResult> GetQuestionById(int id, string lang = "ar")
        {
            var faq = await _context.ChatbotFAQs
                .Where(f => f.Id == id && f.IsActive && !f.IsDeleted)
                .Select(f => new
                {
                    id = f.Id,
                    question = lang == "en" ? f.QuestionEn : f.QuestionAr,
                    answer = lang == "en" ? f.AnswerEn : f.AnswerAr,
                    category = f.Category
                })
                .FirstOrDefaultAsync();

            if (faq == null)
            {
                return NotFound();
            }

            return Ok(faq);
        }
    }
}
