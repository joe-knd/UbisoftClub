using System;
using System.Collections.Generic;
using LiteDB;
using Ubisoft.Club.Common.Contracts;
using Ubisoft.Club.Common.Models;

namespace Ubisoft.Club.Db
{
    public class FeedbackRepository : IFeedbackRepository, IDisposable
    {
        private readonly string _connectionString;
        private readonly LiteRepository _repository;

        public FeedbackRepository(string connectionString)
        {
            _repository = new LiteRepository(connectionString);
        }

        public List<Feedback> Read(string userId, int rateFilter)
        {
            return _repository.Query<Feedback>().Where(x => x.UserId.Equals(userId)).Where(x => x.Rate.Equals(rateFilter)).Limit(15).ToList();
        }
                
        public Feedback Read(Guid sessionId, string userId)
        {
            return _repository.Query<Feedback>().Where(x => x.SessionId.Equals(sessionId)).Where(x => x.UserId.Equals(userId)).SingleOrDefault();
        }
        public Feedback Create(Feedback model)
        {
            if (Read(model.SessionId, model.UserId) == null)
            {
                var result = _repository.Insert(model);
                return Read(result);
            }

            return null;
        }

        public void Update(Feedback model)
        {_repository.Update(model);
        }

        public List<Feedback> Read()
        {
            return _repository.Query<Feedback>().Limit(15).ToList();
        }

        public Feedback Read(ObjectId id)
        {
            return _repository.Query<Feedback>().SingleById(id);
        }

        public void Delete(ObjectId id)
        {
            _repository.Delete<Feedback>(id);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _repository.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
