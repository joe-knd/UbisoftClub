using System;
using System.Collections.Generic;
using LiteDB;
using Ubisoft.Club.Common.Contracts;
using Xunit;
using Moq;
using Ubisoft.Club.Common.Models;

namespace Ubisoft.Club.Db.Test
{
    public class FeedbackProvider
    {
        private readonly Providers.FeedbackProvider _provider;

        public FeedbackProvider()
        {
            var repository = new Mock<IFeedbackRepository>();

            Feedback feedback = new Feedback
            {
                Id = new ObjectId(), SessionId = Guid.NewGuid(), UserId = "User-X", Rate = 4
            };

            List<Feedback> feedbackList = new List<Feedback>
            {
                new Feedback {Id = new ObjectId(), SessionId = Guid.NewGuid(), UserId = "User-X", Rate = 5},
                new Feedback {Id = new ObjectId(), SessionId = Guid.NewGuid(), UserId = "User-XY", Rate = 5}
            };

            repository.Setup(x => x.Read(It.IsAny<Guid>(), It.IsAny<string>())).Returns(feedback);
            repository.Setup(x => x.Read(It.IsAny<ObjectId>())).Returns(feedback);
            repository.Setup(x => x.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(feedbackList);
            repository.Setup(x => x.Read(It.IsAny<int>(), It.IsAny<bool>())).Returns(feedbackList);
            repository.Setup(x => x.Create(It.IsAny<Feedback>())).Returns(feedback);

            _provider = new Providers.FeedbackProvider(repository.Object);
        }

        [Fact]
        public void ReadTest()
        {
            var result = _provider.GetFeedback(2);
            var result3 = _provider.GetFeedback(2, 5);

            Assert.NotNull(result);
            Assert.Equal(2, result3.Count);
        }
    }
}
