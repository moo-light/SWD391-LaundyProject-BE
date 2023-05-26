using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Application.Interfaces;
using Domain.Entities;
using Application.Services;
using Application.Interfaces.Services;

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
        public async Task<IActionResult> AddAsync(Store entity)
        {
            var result = await _storeService.AddAsync(entity);
            return result ? Ok() : BadRequest();
        }
        [HttpPut]

        public IActionResult Update(Store entity)
        {
            var result = _storeService.Update(entity);
            return result ? Ok() : BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetByIDAsync(Guid entityId)
        {
            var result = await _storeService.GetByIdAsync(entityId);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]

        public IActionResult DeleteById(Guid entityId)
        {
            var result = _storeService.Remove(entityId);
            return result ? Ok() : BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _storeService.GetAllAsync();
            return result.Count() > 0 ? Ok(result) : BadRequest(result);
        }

      
    }
}