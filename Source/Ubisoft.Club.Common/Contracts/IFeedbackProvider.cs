using System;
using System.Collections.Generic;
using System.Text;
using Ubisoft.Club.Common.Models;

namespace Ubisoft.Club.Common.Contracts
{
    public interface IFeedbackProvider
    {
        Feedback CreateFeedback(Feedback feedback);
        List<Feedback> GetFeedbackByUserId(string userId, int records, int? rate = null);
        List<Feedback> GetFeedback(int records, int? rate = null);
    }
}
