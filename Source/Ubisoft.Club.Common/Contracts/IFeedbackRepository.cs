using System;
using System.Collections.Generic;
using Ubisoft.Club.Common.Models;

namespace Ubisoft.Club.Common.Contracts
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        List<Feedback> Read(int records, bool bottomRecords);
        List<Feedback> Read(string userId, int records, bool bottomRecords);
        List<Feedback> Read(int rateFilter, int records, bool bottomRecords);
        List<Feedback> Read(string userId, int rateFilter, int records, bool bottomRecords);
        Feedback Read(Guid sessionId, string userId);
    }
}
