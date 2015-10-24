using System;
using System.Data;

namespace EWG.Infrastructure.Dal
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted);
        void Clear();
        void Flush();
    }
}