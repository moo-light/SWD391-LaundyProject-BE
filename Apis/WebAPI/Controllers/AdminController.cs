using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Application.Interfaces;
using Domain.Entities;
using Application.Interfaces.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Application.ViewModels.UserViewModels;

namespace WebAPI.Controllers
{
    public class AdminController : BaseController
    {
        private readonly ICustomerService _customerService;

        public AdminController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpPost]
        public UserLoginDTOResponse Login(UserLoginDTO loginObject)
        {
            return _customerService.LoginAdmin(loginObject);
        }
    }
}