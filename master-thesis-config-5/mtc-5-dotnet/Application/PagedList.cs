using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PagedList() { }
        
        public PagedList(IEnumerable<T> items, int count, int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalCount = count;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            AddRange(items);
        }

        public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();

            var skipCount = (pageNumber - 1) * pageSize;
            var pageCount = pageSize;
            
            var items = await source
                .Skip(skipCount)
                .Take(pageCount)
                .ToListAsync();
            
            return new PagedList<T>(items, count, pageSize, pageNumber);
        }
        
        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageSize, pageNumber);
        }
    }
}