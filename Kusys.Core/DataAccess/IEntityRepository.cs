using System.Linq.Expressions;
using Kusys.Core.Entities;

namespace Kusys.Core.DataAccess;

//generic constraint
//class : referans tip
//IEntity : IEntity olabilir veya IEntity implemente eden bir nesne olabilir
//new() : new'lenebilir olmalı
public interface IEntityRepository<T> where T : class, IEntity, new()
{
    List<T> List();
    IQueryable<T> ListQueryable();
    List<T> List(Expression<Func<T, bool>> where);
    int Insert(T obj);
    int Update(T obj);
    int Delete(T obj);
    int Save();
    T Find(Expression<Func<T, bool>> where);
    
    // List<T> List(Expression<Func<T,bool>> filter=null);
    // IQueryable<T> ListQueryable();
    // T Find(Expression<Func<T, bool>> filter);
    // void Insert(T entity);
    // void Update(T entity);
    // void Delete(T entity);
}