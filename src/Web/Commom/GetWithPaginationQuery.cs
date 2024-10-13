﻿namespace Web.Commom
{
    public record GetWithPaginationQuery
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
