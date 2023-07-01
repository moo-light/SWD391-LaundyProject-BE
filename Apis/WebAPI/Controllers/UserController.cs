using Microsoft.AspNetCore.Mvc;
using Application.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Mvc.Controllers;
using Application.Interfaces;
using Domain.Entities;
using Application.Interfaces.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Application.ViewModels.FilterModels;
using Application.Utils;

namespace WebAPI.Controllers
{
    public class UserController : BaseController
    {
        private readonly IBaseUserService _userService;
        private readonly IClaimsService _claimService;

        public UserController(IBaseUserService userService, IClaimsService claimService)
        {
            _userService = userService;
            _claimService = claimService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<UserLoginDTOResponse> LoginAsync(UserLoginDTO loginObject)
        {
            return await _userService.LoginAsync(loginObject);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByIDAsync(Guid Id)
        {
            var result = await _userService.GetByIdAsync(Id);
            return result != null ? Ok(result) : BadRequest(result);
        }
        [HttpGet("{pageIndex}/{pageSize}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _userService.GetAllAsync(pageIndex, pageSize);
            return result.Items.IsNullOrEmpty() ? NotFound() : Ok(result);
        }
        [HttpPost("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(Guid id , string newPassword)
        {
            if (id == Guid.Empty) id = _claimService.GetCurrentUserId;
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            var result = false;
            var roleClaim = HttpContext.User.Claims.FirstOrDefault(x => x.ToString().Contains("role"));
            if (roleClaim.Value.Equals("Admin",StringComparison.OrdinalIgnoreCase)
                || _claimService.GetCurrentUserId == id) 
            {
                user.PasswordHash = newPassword.Hash();
                result = _userService.Update(user);
            }
            else
            {
                return Forbid("Change password is forbidden");
            }
            return result? Ok() : BadRequest();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCount()
        {
            var result = await _userService.GetCountAsync();
            return result > 0 ? Ok(result) : BadRequest();
        }

        [HttpPost("{pageIndex}/{pageSize}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetListWithFilter(UserFilteringModel? entity, int pageIndex = 0, int pageSize = 10)
        {
            var result = await _userService.GetFilterAsync(entity, pageIndex, pageSize);
            return result.Items.IsNullOrEmpty()? NotFound() : Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(string accessToken, string refreshtoken)
        {
            var newToken = await _userService.RefreshToken(accessToken, refreshtoken);
            if (newToken == null)
            {
                return BadRequest();
            }
            return Ok(newToken);
        }
    }
}