using CorporateQnA.Models;
using CorporateQnA.Models.ViewModels;
using CorporateQnA.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Controllers
{
    [Route("answer")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService answerService;

        public AnswerController(IAnswerService answerService)
        {
            this.answerService = answerService;
        }

        [HttpPost]
        public IActionResult CreateAnswer(Answer answer)
        {
            try
            {
                return Ok(this.answerService.CreateAnswer(answer));
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPost]
        [Route("list")]
        public IActionResult GetAnswersForQuestion(GetAnswerForQuestion answer)
        {
            try
            {
                return Ok(this.answerService.GetAnswersForQuestion(answer));
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [Route("setsolution")]
        [HttpPost]
        public int SetAnswerAsSolution(AnswerAsSolution state)
        {
            return this.answerService.SetAnswerAsSolution(state);
        }

    }
}
