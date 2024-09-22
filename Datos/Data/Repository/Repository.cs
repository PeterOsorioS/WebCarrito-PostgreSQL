using AccesoDatos.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AccesoDatos.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await Context.SaveChangesAsync();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public async Task<T> Get(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Se incluyen propiedades de navegación si se proporcionan
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.AsNoTracking().ToList();
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            // Se crea una consulta IQueryable a partir del DbSet del contexto
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Se incluyen propiedades de navegación si se proporcionan
            if (includeProperties != null)
            {
                // Se divide la cadena de propiedades por coma y se itera sobre ellas
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            // Se aplica el ordenamiento si se proporciona
            if (orderBy != null)
            {
                // Se ejecuta la función de ordenamiento y se convierte la consulta en una lista
                return await orderBy(query).ToListAsync();
            }

            // Si no se proporciona ordenamiento, simplemente se convierte la consulta en una lista
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            // Se crea una consulta IQueryable a partir del DbSet del contexto
            IQueryable<T> query = dbSet;

            // Se aplica el filtro si se proporciona
            if (filter != null)
            {
                query = query.Where(filter);

            }

            // Se incluyen propiedades de navegación si se proporcionan
            if (includeProperties != null)
            {
                // Se divide la cadena de propiedades por coma y se itera sobre ellas
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync();
        }
        public int GetAllCount(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);

            }
            return query.Count();
        }
        public async Task Remove(T entity)
        {
            dbSet.Remove(entity);
        }


    }
}
