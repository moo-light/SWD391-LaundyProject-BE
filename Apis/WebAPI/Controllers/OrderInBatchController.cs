using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Application.Interfaces;
using Domain.Entities;
using Application.Services;
using Application.Interfaces.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using Application.ViewModels.FilterModels;
using Application.ViewModels.OrderInBatch;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPI.Controllers
{
    public class OrderInBatchController : BaseController, IWebController<OrderInBatch>
    {
        private readonly IOrderInBatchService _orderInBatchService;

        public OrderInBatchController(IOrderInBatchService orderInBatchService)
        {
            _orderInBatchService = orderInBatchService;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsync(OrderInBatchRequestDTO entity)
        {
            var result = await _orderInBatchService.AddAsync(entity);
            return result ? StatusCode(201) : BadRequest();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(Guid id, OrderInBatchRequestDTO entity)
        {
            var result = await _orderInBatchService.UpdateAsync(id, entity);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Driver,Admin")]
        public async Task<IActionResult> DeleteAsync(Guid entityId)
        {
            var result = await _orderInBatchService.RemoveAsync(entityId);
            return result ? Ok() : NoContent();
        }
        [HttpGet]
        [Authorize(Roles = "Driver,Admin")]
        public async Task<IActionResult> GetByIDAsync(Guid entityId)
        {
            var result = await _orderInBatchService.GetByIdAsync(entityId);
            return result != null ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCount()
        {
            var result = await _orderInBatchService.GetCountAsync();
            return result > 0 ? Ok(result) : BadRequest();
        }

        [HttpGet("{pageIndex?}/{pageSize?}")]
        [Authorize(Roles = "Driver,Admin")]
        public async Task<IActionResult> GetAllAsync(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _orderInBatchService.GetAllAsync(pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }

        [HttpPost("{pageIndex?}/{pageSize?}")]
        [Authorize(Roles = "Driver,Admin")]
        public async Task<IActionResult> GetListWithFilter(OrderInBatchFilteringModel? entity,
                                                            int pageIndex = 0,
                                                            int pageSize = 10)
        {
            var result = await _orderInBatchService.GetFilterAsync(entity, pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }
       
    }
}