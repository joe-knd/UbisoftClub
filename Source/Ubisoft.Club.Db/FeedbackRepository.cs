using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using LiteDB;
using Microsoft.Extensions.Options;
using Ubisoft.Club.Common.Contracts;
using Ubisoft.Club.Common.Exceptions;
using Ubisoft.Club.Common.Models;

namespace Ubisoft.Club.Db
{
    public class FeedbackRepository : RepositoryBase<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(IOptions<LiteDbOptions> dbOptions) : base(dbOptions)
        {
        }

        public FeedbackRepository(LiteRepository repository) : base(repository)
        {
        }

        protected override void ApplyIndexes()
        {
            Repository.Engine.EnsureIndex("Feedback", "SessionId");
            Repository.Engine.EnsureIndex("Feedback", "UserId");
            Repository.Engine.EnsureIndex("Feedback", "unique_session_and_user", "[$.sessionId, $.userId]");
            Repository.Engine.EnsureIndex("Feedback", "rate");
        }

        public List<Feedback> Read(int records, bool bottomRecords)
        {
            var count = Repository.Query<Feedback>().Count();

            return bottomRecords
                ? Repository.Query<Feedback>().Skip(count > records ? count - records : 0).ToList()
                : Repository.Query<Feedback>().Limit(records).ToList();
        }

        public List<Feedback> Read(string userId, int records, bool bottomRecords)
        {
            var count = Repository.Query<Feedback>().Count();

            return bottomRecords
                ? Repository.Query<Feedback>().Where(x => x.UserId.Equals(userId))
                    .Skip(count > records ? count - records : 0).ToList()
                : Repository.Query<Feedback>().Where(x => x.UserId.Equals(userId))
                    .Limit(records).ToList();
        }

        public List<Feedback> Read(int rateFilter, int records, bool bottomRecords)
        {
            var count = Repository.Query<Feedback>().Count();

            return bottomRecords
            ? Repository.Query<Feedback>().Where(x => x.Rate.Equals(rateFilter))
                .Skip(count > records ? count - records : 0).ToList()
            : Repository.Query<Feedback>().Where(x => x.Rate.Equals(rateFilter))
                .Limit(records).ToList();
        }

        public List<Feedback> Read(string userId, int rateFilter, int records, bool bottomRecords)
        {
            var count = Repository.Query<Feedback>().Count();

            return bottomRecords
            ? Repository.Query<Feedback>().Where(x => x.UserId.Equals(userId))
                .Where(x => x.Rate.Equals(rateFilter)).Skip(count > records ? count - records : 0).ToList()
            : Repository.Query<Feedback>().Where(x => x.UserId.Equals(userId))
                .Where(x => x.Rate.Equals(rateFilter)).Limit(records).ToList();
        }

        public Feedback Read(Guid sessionId, string userId)
        {
            return Repository.Query<Feedback>().Where(x => x.SessionId.Equals(sessionId))
                .Where(x => x.UserId.Equals(userId)).SingleOrDefault();
        }
    }
}
