using System;

namespace EWG.Infrastructure.Dal
{
    public interface IGenericTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}