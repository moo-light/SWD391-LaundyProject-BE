using Application.ViewModels.FilterModels;
using Application.Interfaces.Services;
using Application.Services;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Application.ViewModels.OrderDetails;

namespace WebAPI.Controllers
{
    public class OrderDetailController : BaseController, IWebController<OrderDetail>
    {
        private readonly IOrderDetail _orderDetailService;
        public OrderDetailController(IOrderDetail packageService)
        {
            _orderDetailService = packageService;
        }
        [HttpPost]
        [Authorize(Roles ="Customer,Admin")]
        public async Task<IActionResult> Add(OrderDetailRequestDTO entity)
        {
            var result = await _orderDetailService.AddAsync(entity);
            return result ? Ok(new
            {
                message = "Add successfully"
            }) : BadRequest();
        }
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> UpdateAsync(Guid id, OrderDetailRequestDTO entity)
        {
            var result = await _orderDetailService.UpdateAsync(id, entity);
            return result ? Ok(new
            {
                message = "Update Successfully"
            }) : NotFound();
        }
        [HttpDelete("{entityId:guid}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> DeleteById(Guid entityId)
        {
            var result = await _orderDetailService.RemoveAsync(entityId);
            return result ? Ok(result) : NoContent();
        }
        [HttpGet("{entityId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetByIDAsync(Guid entityId)
        {
            var result = await _orderDetailService.GetByIdAsync(entityId);
            return (result != null ? Ok(result) : NotFound());
        }
        [HttpGet]
        [Authorize(Roles ="Customer,Admin")]
        public async Task<IActionResult> GetCount()
        {
            var result = await _orderDetailService.GetCountAsync();
            return result > 0 ? Ok(result) : NotFound();
        }
        [HttpGet("{pageIndex?}/{pageSize?}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetAllAsync(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _orderDetailService.GetAllAsync(pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }

        [HttpPost("{pageIndex?}/{pageSize?}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetListWithFilter(OrderDetailFilteringModel? entity,
                                                            int pageIndex = 0,
                                                            int pageSize = 10)
        {
            var result = await _orderDetailService.GetFilterAsync(entity, pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }
    }
}
