using CorporateQnA.Models;
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
        public IActionResult Answer(Answer answer)
        {
            try
            {
                return Ok(this.answerService.Create(answer));
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAnswerForQuestion([FromQuery] int qId)
        {
            try
            {
                return Ok(this.answerService.GetAnswersForQues(qId));
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

    }
}
