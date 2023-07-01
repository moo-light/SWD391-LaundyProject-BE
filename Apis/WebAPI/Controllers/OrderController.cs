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
using Microsoft.IdentityModel.Tokens;
using Application.ViewModels.LaundryOrders;

namespace WebAPI.Controllers
{
    public class OrderController : BaseController, IWebController<LaundryOrder>
    {
        private readonly IOrderService _orderService;
        private readonly IClaimsService _claimsService;

        public OrderController(IOrderService orderService, IClaimsService claimsService)
        {
            _orderService = orderService;
            _claimsService = claimsService;
        }


        [HttpPost]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> AddAsync(LaundryOrderRequestAddDTO entity)
        {

            var result = await _orderService.AddAsync(entity);
            return result ? Ok(new
            {
                message = "Add Successfully"
            }) : BadRequest();
        }
        [HttpPut("{id:guid}")]

        [Authorize(Roles = "Admin")]
        //Customer co the Update voi dieu kien la Chua duoc bo vao Batch
        public async Task<IActionResult> UpdateAsync(Guid id,LaundryOrderRequestDTO entity)
        {
            var result = await _orderService.UpdateAsync(id,entity);
            return result ? Ok(new
            {
                message = "Update Successfully"
            }) : BadRequest();
        }
        [HttpGet("{entityId:guid}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetByIDAsync(Guid entityId)
        {
            var result = await _orderService.GetByIdAsync(entityId);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{entityId:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(Guid entityId)
        {
            var result = await _orderService.RemoveAsync(entityId);
            return result ? Ok(new
            {
                message = "Delete Successfully"
            }) : NoContent();
        }
        [HttpGet("{pageIndex?}/{pageSize?}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _orderService.GetAllAsync(pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCount()
        {
            var result = await _orderService.GetCountAsync();
            return result > 0 ? Ok(result) : NotFound();
        }

        [HttpPost("{pageIndex?}/{pageSize?}")]
        [Authorize]
        public async Task<IActionResult> GetListWithFilter(LaundryOrderFilteringModel? entity, int pageIndex = 0, int pageSize = 10)
        {
            var result = await _orderService.GetFilterAsync(entity, pageIndex, pageSize);
                return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }
    }
}