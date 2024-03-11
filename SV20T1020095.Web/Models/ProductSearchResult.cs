﻿using SV20T1020095.DomainModels;

namespace SV20T1020095.Web.Models
{
    public class ProductSearchResult : BasePaginationResult
    {
        public List<Product> Data { get; set; }
    }
}
