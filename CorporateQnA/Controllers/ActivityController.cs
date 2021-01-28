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
    [Route("activity")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService activityService;

        public ActivityController(IActivityService activityService)
        {
            this.activityService = activityService;
        }

        [Route("answer")]
        [HttpPost]
        public IActionResult CreateAnswerAcitvity(AnswerActivity answerActivity)
        {
            try
            {
                return Ok(this.activityService.CreateAnswerActivity(answerActivity));
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }


        [Route("question")]
        [HttpPost]
        public IActionResult CreateQuestionAcitvity(QuestionActivity questionAcitvity)
        {
            try
            {
                return Ok(this.activityService.CreateQuestionActivity(questionAcitvity));
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }


    }
}
