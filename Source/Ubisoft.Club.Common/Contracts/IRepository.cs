using System;
using System.Collections.Generic;
using LiteDB;

namespace Ubisoft.Club.Common.Contracts
{
    public interface IRepository<T> where T : class
    {
        T Create(T model);
        void Update(T model);
        List<T> Read();
        T Read(ObjectId id);
        void Delete(ObjectId id);
    }
}
