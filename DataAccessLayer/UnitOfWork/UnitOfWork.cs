using DataAccessLayer.DbContexts;
using LastStudy.Core.Interfaces;
using LastStudy.Core.Interfaces.DependencyInjector;
using LastStudy.Core.Interfaces.Repositories;
using LastStudy.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private INSDbContext _inscontext;
        private LSDbContext _lscontext;
        private bool _disposed = false;
        private string _connectionName;
        private IServiceLocator _serviceLocator;
        private bool isINSDB = false;
        private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

        public UnitOfWork(IServiceLocator serviceLocator)
        {
            this._serviceLocator = serviceLocator;
        }

        public IInsituteRepository Insitutes
        {
            get { return GetRepository<IInsituteRepository>(); }
        }

        public IInstituteConnectionRepository InsituteConnections
        {
            get { return GetRepository<IInstituteConnectionRepository>(); }
        }

        public IUserInstituteRepository UserInstitutes
        {
            get { return GetRepository<IUserInstituteRepository>(); }
        }
        private T GetRepository<T>() where T : class
        {
            if (string.IsNullOrEmpty(this._connectionName))
            {
                InitLSDb();
            }
            Type type = typeof(T);
            string name = type.IsGenericType ? type.GetGenericArguments()[0].Name : type.Name;
            object repositoryObject;
            if (!_cache.TryGetValue(name, out repositoryObject))
            {
                T repository = _serviceLocator.Resolve<T>();
                repository.GetType().GetMethod("Init").Invoke(repository, new object[] { this._connectionName });
                _cache.Add(name, repository);
                return repository;
            }
            return (T)repositoryObject;
        }

        //private T GetRepository<T>()
        //{
        //    Init();
        //}

        public DbSet<TModel> Collection<TModel>() where TModel : class
        {
            throw new NotImplementedException();
        }

        //Disposing pattern improving finalization efficiency > Book - Under the hood of memory management pg -53

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_inscontext != null)
                    {
                        _inscontext.Database.Connection.Close();
                        _inscontext.Dispose();
                    }

                    if (_lscontext != null)
                    {
                        _lscontext.Database.Connection.Close();
                        _lscontext.Dispose();
                    }
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); //
        }

        public void InitINS(string connectionName)
        {
            this._connectionName = connectionName;
            if (_inscontext == null)
            {
                _inscontext = new INSDbContext(connectionName);
                isINSDB = true;
            }
        }

        public void InitLSDb()
        {
            if (_lscontext == null)
            {
                _lscontext = new LSDbContext();
                isINSDB = false;
            }
        }

        public DbSet<TEntity> Set<TEntity>(string connectionName) where TEntity : class
        {
            if (string.IsNullOrEmpty(connectionName))
            {
                this.InitLSDb();
                return _lscontext.Set<TEntity>();
            }
            else
            {
                this.InitINS(connectionName);
                return _inscontext.Set<TEntity>();
            }
        }

        public int Save()
        {
            if (string.IsNullOrEmpty(this._connectionName))
            {
                InitLSDb();
                try
                {
                    return _lscontext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                InitINS(this._connectionName);
                try
                {
                    return _inscontext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
