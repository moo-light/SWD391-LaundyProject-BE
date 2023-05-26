using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Application.Interfaces;
using Domain.Entities;
using Application.Services;
using Application.Interfaces.Services;

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
        public async Task<IActionResult> AddAsync(Service entity)
        {
            var result = await _serviceService.AddAsync(entity);
            return result ? Ok() : BadRequest();
        }
        [HttpPut]

        public IActionResult Update(Service entity)
        {
            var result = _serviceService.Update(entity);
            return result ? Ok() : BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetByIDAsync(Guid entityId)
        {
            var result = await _serviceService.GetByIdAsync(entityId);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]

        public IActionResult DeleteById(Guid entityId)
        {
            var result = _serviceService.Remove(entityId);
            return result ? Ok() : BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _serviceService.GetAllAsync();
            return result.Count() > 0 ? Ok(result) : BadRequest(result);
        }

      
    }
}