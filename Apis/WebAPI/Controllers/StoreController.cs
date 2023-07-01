using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Application.Interfaces;
using Domain.Entities;
using Application.Services;
using Application.Interfaces.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Application.ViewModels.FilterModels;
using Application.ViewModels.Stores;

namespace WebAPI.Controllers
{
    public class StoreController : BaseController, IWebController<Store>
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        [HttpPost]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> AddAsync(StoreRequestDTO entity)
        {
            var result = await _storeService.AddAsync(entity);
            return result ? Ok() : BadRequest();
        }
        [HttpPut("{id:guid}")]

        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> UpdateAsync(Guid id, StoreRequestDTO entity)
        {
            var result = await _storeService.UpdateAsync(id, entity);
            return result ? Ok() : NotFound();
        }
        [HttpDelete("{entityId:guid}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> DeleteByIdAsync(Guid entityId)
        {
            var result = await _storeService.RemoveAsync(entityId);
            return result ? Ok() : NoContent();
        }

        [HttpGet("{entityId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetByIDAsync(Guid entityId)
        {
            var result = await _storeService.GetByIdAsync(entityId);
            return result != null ? Ok(result) : NotFound();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCount()
        {
            var result = await _storeService.GetCountAsync();
            return result > 0 ? Ok(result) : NotFound();
        }
        [HttpGet("{pageIndex?}/{pageSize?}")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _storeService.GetAllAsync(pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }
        [HttpPost("{pageIndex?}/{pageSize?}")]
        [Authorize]
        public async Task<IActionResult> GetListWithFilter(StoreFilteringModel entity, int pageIndex = 0, int pageSize = 10)
        {
            var result = await _storeService.GetFilterAsync(entity, pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }
    }
}