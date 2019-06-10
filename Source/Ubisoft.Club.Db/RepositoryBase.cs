using System;
using System.Collections.Generic;
using LiteDB;
using Microsoft.Extensions.Options;
using Ubisoft.Club.Common.Contracts;
using Ubisoft.Club.Common.Models;

namespace Ubisoft.Club.Db
{
    public abstract class RepositoryBase<T> : IRepository<T>, IDisposable where T : class
    {
        protected readonly LiteRepository Repository;

        protected RepositoryBase(IOptions<LiteDbOptions> dbOptions)
        {
            Repository = new LiteRepository(dbOptions.Value.ConnectionString);
            ApplyIndexes();
        }

        protected RepositoryBase(LiteRepository repository)
        {
            Repository = repository;
            ApplyIndexes();
        }

        protected virtual void ApplyIndexes()
        {
            //If overrides, be sure no to call in any child class any object from the derived class, it will cause that doesn't work or thrown an exception
            //this method is only intended to ensure indexes in the repository that has being created in this class first
        }

        public virtual T Create(T model)
        {
                var result = Repository.Insert(model);
                return Read(result);

        }

        public virtual void Update(T model)
        {
            Repository.Update(model);
        }

        public virtual List<T> Read()
        {
            return Repository.Query<T>().ToList();
        }

        public T Read(ObjectId id)
        {
            return Repository.Query<T>().SingleById(id);
        }

        public void Delete(ObjectId id)
        {
            Repository.Delete<T>(id);
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Repository.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
