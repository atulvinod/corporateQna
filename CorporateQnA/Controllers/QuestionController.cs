using CorporateQnA.Models;
using CorporateQnA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Controllers
{

    [Route("questions")]
    [AllowAnonymous]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService questionService;

        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpPost]
        public IActionResult CreateQuestion(Question question)
        {
            try
            {
                return Ok(this.questionService.Create(question));
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet]
        public IEnumerable<QuestionDetails> GetQuestions()
        {
            return this.questionService.GetQuestions();
        }

        [HttpPost]
        [Route("search")]
        public IEnumerable<QuestionDetails> SearchQuestion(SearchFilter searchFilter)
        {
            return this.questionService.SearchQuestion(searchFilter);
        }

        [Route("answeredBy")]
        [HttpGet]
        public IEnumerable<QuestionDetails> QuestionsAnsweredBy(int userId)
        {
            return this.questionService.QuestionsAnsweredByUser(userId);
        }

        [Route("askedBy")]
        [HttpGet]
        public IEnumerable<QuestionDetails> UserQuestions(int userId)
        {
            return this.questionService.QuestionsByUser(userId);

        }
    }
}
