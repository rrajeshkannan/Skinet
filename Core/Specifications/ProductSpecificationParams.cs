using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecificationParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 6;

        public int PageSize { get => _pageSize; set => _pageSize = Math.Min (value, MaxPageSize); }

        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public String Sort { get; set; }

        private String _search;

        public String Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}