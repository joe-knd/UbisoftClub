using System;
using System.Collections.Generic;
using Ubisoft.Club.Common.Models;

namespace Ubisoft.Club.Common.Contracts
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        List<Feedback> Read(string userId, int rateFilter);

        Feedback Read(Guid sessionId, string userId);
    }
}
