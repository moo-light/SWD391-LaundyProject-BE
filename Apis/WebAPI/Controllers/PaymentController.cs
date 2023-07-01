using Application.ViewModels.FilterModels;
using Application.Interfaces.Services;
using Application.Services;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Application.ViewModels.Payments;

namespace WebAPI.Controllers
{
    public class PaymentController : BaseController, IWebController<Payment>
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsync(PaymentRequestDTO entity)
        {
            var result = await _paymentService.AddAsync(entity);
            return result ? Ok() : BadRequest();
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(Guid id, PaymentRequestDTO entity)
        {
            var result = await _paymentService.UpdateAsync(id,entity);
            return result ? Ok() : NotFound();
        }

        //[HttpDelete("{entityId:guid}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> DeleteByIdAsync(Guid entityId)
        //{
        //    var result = await _paymentService.RemoveAsync(entityId);
        //    return result ? Ok() : NoContent();
        //}

        [HttpGet("{entityId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetByIDAsync(Guid entityId)
        {
            var result = await _paymentService.GetByIdAsync(entityId);
            return result != null ? Ok(result) : NotFound();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCount()
        {
            var result = await _paymentService.GetCountAsync();
            return result > 0 ? Ok(result) : NotFound();
        }

        [HttpGet("{pageIndex?}/{pageSize?}")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _paymentService.GetAllAsync(pageIndex, pageSize);
            return result.Items.IsNullOrEmpty()? NotFound() : Ok(result);
        }

        [HttpPost("{pageIndex?}/{pageSize?}")]
        [Authorize]
        public async Task<IActionResult> GetListWithFilter(PaymentFilteringModel? entity,
                                                            int pageIndex = 0,
                                                            int pageSize = 10)
        {
            var result = await _paymentService.GetFilterAsync(entity, pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }


    }
}
