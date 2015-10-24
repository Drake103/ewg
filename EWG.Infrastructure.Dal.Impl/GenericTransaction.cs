using NHibernate;

namespace EWG.Infrastructure.Dal.Impl
{
    public class GenericTransaction : IGenericTransaction
    {
        private readonly ITransaction _transaction;

        public GenericTransaction(ITransaction transaction)
        {
            _transaction = transaction;
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }

        public void Commit()
        {
            if (!_transaction.WasCommitted)
            {
                _transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (!_transaction.WasRolledBack)
            {
                _transaction.Rollback();
            }
        }
    }
}