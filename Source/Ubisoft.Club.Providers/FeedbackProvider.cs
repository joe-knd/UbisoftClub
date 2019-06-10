using System;
using System.Collections.Generic;
using Ubisoft.Club.Common.Contracts;
using Ubisoft.Club.Common.Enums;
using Ubisoft.Club.Common.Exceptions;
using Ubisoft.Club.Common.Models;

namespace Ubisoft.Club.Providers
{
    public class FeedbackProvider : IFeedbackProvider
    {
        private readonly IFeedbackRepository _repository;

        public FeedbackProvider(IFeedbackRepository repository)
        {
            _repository = repository;
        }

        public Feedback CreateFeedback(Feedback feedback)
        {
            if (_repository.Read(feedback.SessionId, feedback.UserId) != null)
                throw new AlreadyExistsFeedbackException();

            return _repository.Create(feedback);
        }

        public List<Feedback> GetFeedbackByUserId(string userId, int records, int? rate = null)
        {
            return rate == null
                ? _repository.Read(userId, records, true)
                : _repository.Read(userId, rate.Value, records, true);
        }   

        public List<Feedback> GetFeedback(int records, int? rate = null)
        {
            return rate == null 
                ? _repository.Read(records, true) 
                : _repository.Read(rate.Value, records, true);
        }
    }
}
