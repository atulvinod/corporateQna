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
        public ActionResult<int> CreateQuestion(Question question)
        {
            try
            {
                var id = this.questionService.Create(question);
                return id;
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetQuestions()
        {
            return Ok(this.questionService.GetQuestions());
        }

        [HttpPost]
        [Route("search")]
        public IActionResult SearchQuestion(SearchFilter searchFilter)
        {
            return Ok(this.questionService.SearchQuestion(searchFilter));
        }

        [Route("answeredBy")]
        [HttpGet]
        public IActionResult QuestionsAnsweredBy(int userId)
        {
            return Ok(this.questionService.QuestionsAnsweredByUser(userId));
        }

        [Route("askedBy")]
        [HttpGet]
        public IActionResult UserQuestions(int userId)
        {
            return Ok(this.questionService.QuestionsByUser(userId));

        }

        [Route("setsolution")]
        public IActionResult SetQuestionSolution(QuestionSolution solution)
        {
            this.questionService.SetQuestionSolution(solution);
            return Ok();
        }

    }
}
