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
    public class ServiceController : BaseController, IWebController<Service>
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }


        [HttpPost]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> AddAsync(ServiceRequestDTO entity)
        {
            var result = await _serviceService.AddAsync(entity);
            return result ? Ok() : BadRequest();
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> UpdateAsync(Guid id, ServiceRequestDTO entity)
        {
            var result = await _serviceService.UpdateAsync(id, entity);
            return result ? Ok() : BadRequest();
        }

        [HttpDelete("{entityId:guid}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> DeleteAsync(Guid entityId)
        {
            var result = await _serviceService.RemoveAsync(entityId);
            return result ? Ok() : BadRequest();
        }
        [HttpGet("{entityId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetByIDAsync(Guid entityId)
        {
            var result = await _serviceService.GetByIdAsync(entityId);
            return result != null ? Ok(result) : BadRequest(result);
        }
        [HttpGet("{pageIndex?}/{pageSize?}")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync(int pageIndex = 0,int pageSize = 10)
        {
            var result = await _serviceService.GetAllAsync(pageIndex,pageSize);
            return result.Items.IsNullOrEmpty() ? BadRequest() : Ok(result);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCount()
        {
            var result = await _serviceService.GetCountAsync();
            return result > 0 ? Ok(result) : BadRequest();
        }
        [HttpPost("{pageIndex?}/{pageSize?}")]
        [Authorize]
        public async Task<IActionResult> GetListWithFilter(ServiceFilteringModel? entity,
                                                            int pageIndex = 0,
                                                            int pageSize = 10)
        {
            var result = await _serviceService.GetFilterAsync(entity, pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? BadRequest() : Ok(result);
        }
    }
}