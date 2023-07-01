using Application.ViewModels.FilterModels;
using Application.Interfaces.Services;
using Application.Services;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Application.ViewModels.Batchs;
using Microsoft.IdentityModel.Tokens;
using Application.ViewModels.Customer;

namespace WebAPI.Controllers
{
    public class BatchController : BaseController, IWebController<Batch>
    {
        private readonly IBatchService _batchService;
        public BatchController(IBatchService batchService)
        {
            _batchService = batchService;
        }

        [HttpPost]
        [Authorize(Roles = "Driver")]
        public async Task<IActionResult> Add(BatchRequestDTO entity)
        {
            var result = await _batchService.AddAsync(entity);
            return result ? Ok(new
            {
                message = "Add successfully"
            }) : BadRequest();
        }
        [HttpDelete("{entityId:guid}")]
        [Authorize(Roles = "Admin,Driver,Customer")]

        public IActionResult DeleteById(Guid entityId)
        {
            var result = _batchService.Remove(entityId);
            return result != null ? Ok(new
            {
                message = "Delete Successfully"
            }) : BadRequest();
        }
        [HttpGet]
        [Authorize(Roles = "Driver,Admin")]
        public async Task<IActionResult> GetAllAsync(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _batchService.GetBatchListPagi(pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }
        [HttpGet("{entityId:guid}")]
        [Authorize(Roles = "Driver,Customer,Admin")]
        public async Task<IActionResult> GetByIDAsync(Guid entityId)
        {
            var result = await _batchService.GetByIdAsync(entityId);
            return (result != null ? Ok(result) : NotFound());
        }
        [HttpPut]
        [Authorize(Roles = "Driver,Admin")]
        public async Task<IActionResult> Update(Guid id, BatchRequestDTO entity)
        {
            var exist = await ExistCustomer(id);
            if (!exist) return NotFound();
            bool result;
            result = await _batchService.Update(id, entity);
            return result ? Ok(new
            {
                message = "Update Successfully"
            }) : BadRequest();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCount()
        {
            var result = await _batchService.GetCountAsync();
            return result > 0 ? Ok(result) : NotFound();
        }
        [HttpPost("{pageIndex?}/{pageSize?}")]
        [Authorize(Roles = "Driver,Admin")]
        public async Task<IActionResult> GetListWithFilter(BatchFilteringModel? entity,
                                                    int pageIndex = 0,
                                                    int pageSize = 10)
        {
            var result = await _batchService.GetFilterAsync(entity, pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }
        private async Task<bool> ExistCustomer(Guid id)
        {
            var customer = await _batchService.GetByIdAsync(id);
            if (customer == null) return false;
            return true;
        }
    }
}
