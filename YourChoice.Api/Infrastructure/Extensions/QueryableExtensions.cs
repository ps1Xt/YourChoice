using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Text;
using YourChoice.Api.Infrastructure.Models;
using System;
using System.Reflection;

namespace YourChoice.Api.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public async static Task<PaginatedResult<TDto>> CreatePaginatedResultAsync<TEntity, TDto>(this IQueryable<TEntity> query, PagedRequest pagedRequest, IMapper mapper)
            where TEntity : class
            where TDto : class
        {

            var projectionResult = query.ProjectTo<TDto>(mapper.ConfigurationProvider);

            projectionResult = projectionResult.Sort(pagedRequest);

            projectionResult = projectionResult.ApplyFilters<TDto>(pagedRequest);

            var total = await projectionResult.CountAsync();

            projectionResult = projectionResult.Paginate(pagedRequest);

            var listResult = await projectionResult.ToListAsync();

            return new PaginatedResult<TDto>()
            {
                Items = listResult,
                PageSize = pagedRequest.PageSize,
                PageIndex = pagedRequest.PageIndex,
                Total = total
            };
        }

        private static IQueryable<T> Paginate<T>(this IQueryable<T> query, PagedRequest pagedRequest)
        {
            var entities = query.Skip((pagedRequest.PageIndex) * pagedRequest.PageSize).Take(pagedRequest.PageSize);
            return entities;
        }

        private static IQueryable<T> Sort<T>(this IQueryable<T> query, PagedRequest pagedRequest)
        {
            if (!string.IsNullOrWhiteSpace(pagedRequest.ColumnNameForSorting))
            {
                query = query.OrderBy(pagedRequest.ColumnNameForSorting + " " + pagedRequest.SortDirection);
            }
            return query;
        }

        private static IQueryable<TDto> ApplyFilters<TDto>(this IQueryable<TDto> query, PagedRequest pagedRequest)
        {
            var predicate = new StringBuilder();
            var requestFilters = pagedRequest.RequestFilters;
            for (int i = 0; i < requestFilters.Filters.Count; i++)
            {
                if (i > 0)
                {
                    predicate.Append($" {requestFilters.LogicalOperator} ");
                }

                var path = requestFilters.Filters[i].Path;
                var operation = requestFilters.Filters[i].Operation;

                predicate.Append(path + $" {ParseOperation(operation, i)}");

            }

            if (requestFilters.Filters.Any())
            {
                var propertyValues = requestFilters.Filters.Select(filter => filter.Value.ToString()).ToArray();

                query = query.Where(predicate.ToString(), propertyValues);
            }

            return query;
        }


        private static string ParseOperation(string operation, int i)
        {
            switch (operation)
            {
                case "equal":
                    return $" == (@{i})";
                case "notEqual":
                    return $" != (@{i})";
                case "greaterThan":
                    return $" > (@{i})";
                case "greaterThanOrEqual":
                    return $" >= (@{i})";
                case "lessThan":
                    return $" < (@{i})";
                case "lessThanOrEqual":
                    return $" <= (@{i})";
                case "contains":
                    return $".Contains (@{i})";
                case "notContains":
                    return $".Contains (@{i}) == false";
                default:
                    return "==";
            }
        }
    }
}
