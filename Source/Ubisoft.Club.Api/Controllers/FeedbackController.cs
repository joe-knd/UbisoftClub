using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ubisoft.Club.Common.Contracts;
using Ubisoft.Club.Common.Exceptions;
using Ubisoft.Club.Common.Models;

namespace Ubisoft.Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackProvider _provider;

        public FeedbackController(IFeedbackProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("GetAllFeedback")]
        public ActionResult<IEnumerable<Feedback>> Get()
        {
            return _provider.GetFeedback(15);
        }

        [HttpGet("GetAllFeedBack/{rate}")]
        public ActionResult<IEnumerable<Feedback>> Get(int rate)
        {
            return _provider.GetFeedback(15, rate);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Feedback>> Get([FromHeader(Name = "Ubi-UserId")] string userId )
        {
            if (string.IsNullOrEmpty(userId)) 
                throw new ArgumentNullException(userId);

            List<Feedback> result = _provider.GetFeedbackByUserId(userId, 15);

            return result;
        }

        [HttpGet("{rate}")]
        public ActionResult<IEnumerable<Feedback>> Get([FromHeader(Name = "Ubi-UserId")] string userId, int rate)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(userId);

            return _provider.GetFeedbackByUserId(userId, 15, rate);
        }

        [HttpPost]
        public ActionResult<Feedback> Post([FromHeader(Name = "Ubi-UserId")] string userId, [FromBody] CreateFeedback feedback)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(userId);

            if (feedback == null)
                throw new ArgumentNullException(nameof(feedback));

            Feedback newFeedback = new Feedback {SessionId = feedback.SessionId, UserId = userId, Rate = feedback.Rate};

            try
            {
                var result = _provider.CreateFeedback(newFeedback);

                return result;
            }
            catch (AlreadyExistsFeedbackException ex)
            {
                throw;
                //TODO: Add more complex exception handling
            }
        }
    }
}
