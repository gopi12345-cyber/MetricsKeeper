using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Core.Context;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T>
        where T : class, IEntityBase, new()
{
 
    protected CoreContext _context;
        public string x = "s";
 
    
    public EntityBaseRepository(CoreContext context)
    {
            _context = context;
    }
    
    public virtual async Task<IEnumerable<T>> GetAll()
    {
            return await Task.FromResult(_context.Set<T>().AsEnumerable());
    }

 
    public virtual async Task<int> Count()
    {
        return await _context.Set<T>().CountAsync();
    }


    public virtual async Task<IEnumerable<T>> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _context.Set<T>();
        foreach (var includeProperty in includeProperties)
        {
                query = query.Include(includeProperty);
        }
        return await Task.FromResult(query.AsEnumerable());
    }
 
    public T GetSingle(int id)
    {
        return _context.Set<T>().FirstOrDefault(x => x.Id == id);
    }
 
    public T GetSingle(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().FirstOrDefault(predicate);
    }
 
    public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _context.Set<T>();
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
 
        return query.Where(predicate).FirstOrDefault();
    }
 
    public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Where(predicate);
    }
 
    public virtual void Add(T entity)
    {
        EntityEntry dbEntityEntry = _context.Entry<T>(entity);
        _context.Set<T>().Add(entity);
    }
 
    public virtual void Update(T entity)
    {
        EntityEntry dbEntityEntry = _context.Entry<T>(entity);
        dbEntityEntry.State = EntityState.Modified;
    }
    public virtual void Delete(T entity)
    {
        EntityEntry dbEntityEntry = _context.Entry<T>(entity);
        dbEntityEntry.State = EntityState.Deleted;
    }
 
    public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
    {
        IEnumerable<T> entities = _context.Set<T>().Where(predicate);
 
        foreach(var entity in entities)
        {
            _context.Entry<T>(entity).State = EntityState.Deleted;
        }
    }
 
    public virtual void Commit()
    {
        _context.SaveChanges();
    }
}
}
