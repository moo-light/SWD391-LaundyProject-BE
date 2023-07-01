using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Application.Interfaces;
using Domain.Entities;
using Application.Services;
using Application.Interfaces.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Application.ViewModels.FilterModels;
using Application.ViewModels.Buildings;
using Microsoft.IdentityModel.Tokens;
using Application.ViewModels.Batchs;

namespace WebAPI.Controllers
{
    public class BuildingController : BaseController, IWebController<Building>
    {
        private readonly IBuildingService _buildingService;
        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(BuildingRequestDTO entity)
        {
            var result = await _buildingService.AddAsync(entity);
            return result ? Ok() : BadRequest();
        }
        [HttpDelete("{entityId:guid}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteById(Guid entityId)
        {
            var result = _buildingService.Remove(entityId);
            return result != null ? Ok(new
            {
                message = "Delete Successfully"
            }) : BadRequest();
        }
        //[HttpGet]
        //[Authorize]
        //public async Task<IActionResult> GetAllAsync()
        //{
        //    var result = await _buildingService.GetAllAsync();
        //    return result.Items.Count() > 0 ? Ok(result) : NotFound();
        //}
        [HttpGet("{entityId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetByIDAsync(Guid entityId)
        {
            var result = await _buildingService.GetByIdAsync(entityId);
            return (result != null ? Ok(result) : BadRequest());
        }
        [HttpPut]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Update(Guid id, BuildingRequestDTO entity)
        {
            var exist = await IsExisted(id);
            if (!exist) return NotFound();
            bool result;
            result = await _buildingService.Update(id, entity);

            return result ? Ok(new
            {
                message = "Update Successfully"
            }) : BadRequest();
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetCount()
        {
            var result = await _buildingService.GetCountAsync();
            return result > 0 ? Ok(result) : NotFound();
        }
        [HttpPost("{pageIndex?}/{pageSize?}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetListWithFilter(BuildingFilteringModel? entity, 
                                                        int pageIndex = 0,
                                                        int pageSize = 10)
        {
            var result = await _buildingService.GetFilterAsync(entity, pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }
        [HttpGet("{pageIndex?}/{pageSize?}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetAllAsync(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _buildingService.GetCustomerListPagi(pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }
        private async Task<bool> IsExisted(Guid id)
        {
            var customer = await _buildingService.GetByIdAsync(id);
            if (customer == null) return false;
            return true;
        }
    }
}
