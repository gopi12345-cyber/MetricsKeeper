using System;
using System.Linq;
using Core.Context;
using Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repository
{
	public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    //todo: replace IEnumerable with IQueryable
    //todo: replace voids with Task<IActionResult>
    //todo: replace sync with async methods
		{
			Task<IEnumerable<T>> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
			Task<IEnumerable<T>> GetAll();
			Task<int> Count();
			T GetSingle(int id);
			T GetSingle(Expression<Func<T, bool>> predicate);
			T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
			IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
			void Add(T entity);
			void Update(T entity);
			void Delete(T entity);
			void DeleteWhere(Expression<Func<T, bool>> predicate);
			void Commit();
		}        
}
