﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.APIService.Request;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Service;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Service.Base;

namespace Net1711_231_ASM3_SE172092_NGUYENHOANGSON.APIService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IConfiguration conf)
    {
        _userService ??= new UserService(conf);
    }

    [HttpGet]
    public async Task<IBusinessResult> GetUsers()
    {
        return await _userService.GetAll();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var msg = await _userService.Login(request.UsernameOrEmail, request.Password);

        return Ok(msg);
    }
}