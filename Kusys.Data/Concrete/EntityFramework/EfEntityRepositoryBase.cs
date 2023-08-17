using System.Linq.Expressions;
using Kusys.Core.Abstract;
using Kusys.Core.Concrete;
using Kusys.Core.DataAccess;
using Kusys.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kusys.Data.Concrete.EntityFramework;

public abstract class EfEntityRepositoryBase<TEntity,TContext>: RepositoryBase, IEntityRepository<TEntity>
    where TEntity: class, IEntity, new()
    where TContext : DbContext,new()
{

    private readonly DbSet<TEntity> _objectSet;
    private readonly ICommon _common;
    
    protected EfEntityRepositoryBase(ICommon common)
    {
        _common = common;
        _objectSet = context.Set<TEntity>();
    }

    public List<TEntity> List()
    {
        return _objectSet.ToList();
    }
    
    public IQueryable<TEntity> ListQueryable()
    {
        return _objectSet.AsQueryable<TEntity>();
    }
    
    public List<TEntity> List(Expression<Func<TEntity, bool>> where)
    {
        return _objectSet.Where(where).ToList();
    }
    
    public void Add(TEntity entity) 
    {
        //IDisposable pattern implementation of c#
        using (TContext context = new TContext())
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            context.SaveChanges();
        }
    }
    
    public int Insert(TEntity obj)
    {
        _objectSet.Add(obj);
    
        if (obj is BaseEntity)
        {
            BaseEntity o = obj as BaseEntity;
            DateTime now = DateTime.Now;
    
            o.CreatedDate = now;
            o.ModifiedDate = now;
            o.ModifiedUsername = _common.GetCurrentUsername();
        }
    
        return Save();
    }
    
    public int Update(TEntity obj)
    {
        if (obj is BaseEntity)
        {
            BaseEntity o = obj as BaseEntity;
    
            o.ModifiedDate = DateTime.Now;
            o.ModifiedUsername = _common.GetCurrentUsername();
        }
    
        return Save();
    }
    
    public int Delete(TEntity obj)
    {
        _objectSet.Remove(obj);
        return Save();
    }
    
    public int Save()
    {
        return context.SaveChanges();
    }
    
    public TEntity Find(Expression<Func<TEntity, bool>> where)
    {
        return context.Set<TEntity>().SingleOrDefault(where);
    }
    

}