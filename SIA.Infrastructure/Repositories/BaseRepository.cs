using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SIA.Infrastructure.Data;

namespace SIA.Infrastructure.Repositories
{
    public class BaseRepository(AppDBContext? dbContext)
    {
        private static void ThrowError(DbUpdateException ex)
        {
            if (ex.InnerException is SqlException sqlExp)
            {
                throw sqlExp.Number switch
                {
                    1451 => new Exception("You cannot delete the item since it was locked by another transaction."),
                    1452 => new Exception("Cannot add or update a child row: a foreign key constraint fails"),
                    2627 => new Exception("Unique constraint error"),
                    547 => new Exception("Constraint check violation"),
                    2601 => new Exception("Duplicated key row error/Constraint violation exception"),
                    1062 => new Exception(ex.InnerException.Message.Split("for")[0].Trim()),
                    _ => new Exception(sqlExp.Message),
                };
            }
            throw new Exception(ex.Message);
        }
        private static void DetachedEntries(DbUpdateException ex)
        {
            foreach (var entry in ex.Entries)
                entry.State = EntityState.Detached;
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                if (dbContext != null)
                    await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                DetachedEntries(ex);
                ThrowError(ex);
            }
        }
    }
}
