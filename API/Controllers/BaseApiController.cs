using System;
using API.RequestHelpers;
using Core.Entities;
using Core.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected async Task<ActionResult> CreatePageResult<T>(IGenericRepository<T> repo, ISpecification<T> spec, int pageIndex, int pageSize) where T : BaseEntity
    {
        var items = await repo.ListAsync(spec);
        var count = await repo.CountAsync(spec);

        var pagination = new Pagination<T>(pageIndex, pageSize, count, items);

        return Ok(pagination);
    }

    protected async Task<ActionResult> CreatePageResult<T, TDto>(IGenericRepository<T> repo, ISpecification<T> spec, int pageIndex, int pageSize, Func<T, TDto> toDto) where T 
        : BaseEntity, IDtoConvertible
    {
        var items = await repo.ListAsync(spec);
        var count = await repo.CountAsync(spec);

        var dtoitems = items.Select(toDto).ToList();

        var pagination = new Pagination<TDto>(pageIndex, pageSize, count, dtoitems);

        return Ok(pagination);
    }
}
