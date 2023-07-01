using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Application.Interfaces;
using Domain.Entities;
using Application.Services;
using Application.Interfaces.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using Application.ViewModels.FilterModels;
using Application.ViewModels.BatchOfBuildings;

namespace WebAPI.Controllers
{
    public class SessionController : BaseController, IWebController<BatchOfBuilding>
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsync(BatchOfBuildingRequestDTO entity)
        {
            var result = await _sessionService.AddAsync(entity);
            return result ? Ok() : BadRequest();
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(Guid id, BatchOfBuildingRequestDTO entity)
        {
            var result = await _sessionService.UpdateAsync(id, entity);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{entityId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteById(Guid entityId)
        {
            var result = await _sessionService.RemoveAsync(entityId);
            return result ? Ok() : NoContent();
        }

        [HttpGet("{entityId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetByIDAsync(Guid entityId)
        {
            var result = await _sessionService.GetByIdAsync(entityId);
            return result != null ? Ok(result) : NotFound(result);
        }

       

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCount()
        {
            var result = await _sessionService.GetCount();
            return result > 0 ? Ok(result) : NotFound();
        }
        [HttpGet("{pageIndex?}/{pageSize?}")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _sessionService.GetAllAsync(pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }

        [HttpPost("{pageIndex?}/{pageSize?}")]
        [Authorize]
        public async Task<IActionResult> GetListWithFilter(SessionFilteringModel filter,
                                                            int pageIndex = 0,
                                                            int pageSize = 10)
        {
            var result = await _sessionService.GetFilterAsync(filter, pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }
    }
}