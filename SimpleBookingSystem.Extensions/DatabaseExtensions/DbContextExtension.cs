using ErrorHandling.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleBookingSystem.Extensions.ErrorHandling.Exceptions;
using SimpleBookingSystem.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Extensions.DatabaseExtensions
{
    public static class DbContextExtension
    {
        public static async Task<T> PostAsync<T>(this DbContext context, T entity, ILogger logger) where T : class
        {
            if (entity != null)
            {
                context.Add(entity);
                var result = await context.SaveChangesAsync();
                if (result <= 0)
                {
                    using (logger.BeginScope(JsonSerializer.Serialize(entity)))
                    {
                        logger.LogWarning(LogEvents.InsertItem, $"Saving {entity.GetType().Name} failed");
                        throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.DbUpdateException.ToString());
                    }
                }
                return entity;
            }
            else
                throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.EntityNullException.ToString());
        }
        public static async Task<IEnumerable<T>> PostRangeAsync<T>(this DbContext context, IEnumerable<T> entities, ILogger logger) where T : class
        {
            if (entities != null)
            {
                context.AddRange(entities);
                var result = await context.SaveChangesAsync();
                if (result <= 0)
                {
                    using (logger.BeginScope(JsonSerializer.Serialize(entities)))
                    {
                        logger.LogWarning(LogEvents.InsertItem, $"Saving multiple entities failed");
                        throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.DbUpdateException.ToString());
                    }
                }
                return entities;
            }
            else
                throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.EntityNullException.ToString());
        }
        public static async Task<T> PutAsync<T>(this DbContext context, T entity, ILogger logger) where T : class
        {
            if (entity != null)
            {
                context.Attach(entity);
                context.Update(entity);
                var result = await context.SaveChangesAsync();
                if (result <= 0)
                {
                    using (logger.BeginScope(JsonSerializer.Serialize(entity)))
                    {
                        logger.LogWarning(LogEvents.UpdateItemFailed, $"Updating {entity.GetType().Name} {entity.GetType().GetProperty("Id").GetValue(entity)} failed");
                        throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.DbUpdateException.ToString());
                    }
                }
                return entity;
            }
            else
                throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.EntityNullException.ToString());

        }

        public static async Task<IEnumerable<T>> PutRangeAsync<T>(this DbContext context, IEnumerable<T> entities, ILogger logger) where T : class
        {
            if (entities != null)
            {
                context.AttachRange(entities);
                context.UpdateRange(entities);
                var result = await context.SaveChangesAsync();
                if (result <= 0)
                {
                    using (logger.BeginScope(JsonSerializer.Serialize(entities)))
                    {
                        logger.LogWarning(LogEvents.UpdateItemFailed, $"Updating range of entities failed");
                        throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.DbUpdateException.ToString());
                    }
                }
                return entities;
            }
            else
                throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.EntityNullException.ToString());

        }

        public static T Put<T>(this DbContext context, T entity, ILogger logger) where T : class
        {
            if (entity != null)
            {
                //var savedEntity = await context.FindAsync<T>(entity.GetType().GetProperty("Id").GetValue(entity));
                //if (savedEntity == null)
                //{
                //    logger.LogInformation(LogEvents.GetItemNotFound, $"{entity.GetType().Name} {entity.GetType().GetProperty("Id").GetValue(entity)} wasn't found.");
                //    throw new NotFoundException();
                //}
                context.Attach(entity);
                context.Update(entity);
                var result = context.SaveChanges();
                if (result <= 0)
                {
                    using (logger.BeginScope(JsonSerializer.Serialize(entity)))
                    {
                        logger.LogWarning(LogEvents.UpdateItemFailed, $"Updating {entity.GetType().Name} {entity.GetType().GetProperty("Id").GetValue(entity)} failed");
                        throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.DbUpdateException.ToString());
                    }
                }
                return entity;
            }
            else
                throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.EntityNullException.ToString());

        }
        public static async Task<int> DeleteAsync<T>(this DbContext context, int id, ILogger logger) where T : class
        {
            var entity = await context.FindAsync<T>(id);
            if (entity == null)
            {
                throw new NotFoundException();
            }
            return await context.DeleteByEntityAsync<T>(entity, logger);

        }
        public static async Task<int> DeleteByEntityAsync<T>(this DbContext context, T entity, ILogger logger) where T : class
        {
            if (entity != null)
            {
                context.Remove(entity);
                var result = await context.SaveChangesAsync();
                if (result <= 0)
                {
                    logger.LogWarning(LogEvents.DeleteItemFailed, $"Deleting {entity.GetType().Name} {entity.GetType().GetProperty("Id").GetValue(entity)} Failed");
                    throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.DbUpdateException.ToString());
                }
                return result;
            }
            else
                throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.EntityNullException.ToString());

        }
        public static async Task<int> DeleteRangeAsync<T>(this DbContext context, List<int> ids, ILogger logger) where T : class
        {
            if (ids != null)
            {
                if (ids.Any())
                {
                    var entities = context.Set<T>().Where(x => ids.Contains((int)x.GetType().GetProperty("Id").GetValue(x)));
                    context.RemoveRange(entities);
                    var result = await context.SaveChangesAsync();
                    if (result <= 0)
                    {
                        logger.LogWarning(LogEvents.DeleteItemFailed, $"Deleting {typeof(T).Name} of Ids {ids} Failed");
                        throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.DbUpdateException.ToString());
                    }
                    return result;
                }
                return 0;
            }
            else
                throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.EntityNullException.ToString());
        }
        public static async Task<int> DeleteRangeAsync<T>(this DbContext context, IEnumerable<T> entities, ILogger logger) where T : class
        {
            if (entities != null)
            {
                if (entities.Any())
                {
                    context.RemoveRange(entities);
                    var result = await context.SaveChangesAsync();
                    if (result <= 0)
                    {
                        string ids = string.Join(",", entities.Select(x => x.GetType().GetProperty("Id").GetValue(x)));
                        logger.LogWarning(LogEvents.DeleteItemFailed, $"Deleting {typeof(T).Name} of Ids {ids} Failed");
                        throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.DbUpdateException.ToString());
                    }
                    return result;
                }
                return 0;

            }
            else
                throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.EntityNullException.ToString());
        }

        public static IQueryable<T> IncludeAll<T>(this DbContext context) where T : class
        {
            var query = context.Set<T>().AsQueryable();
            foreach (var property in context.Model.FindEntityType(typeof(T)).GetNavigations())
                query = query.Include(property.Name);
            return query;
        }

        public static async Task<int> TruncateAsync<T>(this DbContext context, ILogger logger) where T : class
        {
            string tableName = typeof(T).Name;
            if (!string.IsNullOrEmpty(tableName) && context.Database.IsSqlServer())
            {
                var result = await context.Database.ExecuteSqlRawAsync($"Truncate table {tableName}");
                if (result <= 0)
                {
                    logger.LogWarning(LogEvents.DeleteItemFailed, $"Truncate {tableName} Failed");
                    throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.DbUpdateException.ToString());
                }
                return result;
            }
            else
                throw new ApiException(HttpStatusCode.InternalServerError, ErrorCodes.EntityNullException.ToString());
        }
    }
}