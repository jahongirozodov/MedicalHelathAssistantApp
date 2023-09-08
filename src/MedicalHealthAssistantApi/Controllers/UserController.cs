﻿using Domain.Configuration;
using Domain.Enums;
using MedicalHealthAssistantApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Attachments;
using Service.DTOs.Users;
using Service.Interfaces;
using System.Security.Claims;

namespace MedicalHealthAssistantApi.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;
        public UserController(IUserService userService,IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }

        [HttpPost("User")]
        public async Task<IActionResult> PostAsync(UserCreationDto dto)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Succes",
                Data = await userService.CreateAsync(dto)
            });
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Succes",
                Data = await userService.DeleteAsync(id)
            });

        [HttpPut("Update")]

        public async Task<IActionResult> PutAsync(UserUpdateDto dto)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Succes",
                Data = await userService.UpdateAsync(dto)
            });

        [HttpGet("api/get/id")]
        public async Task<IActionResult> GetById(long Id)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Succes",
                Data = await userService.GetAsync(Id)
            });
        }
        [HttpGet("api/get/current/user")]
        public async Task<IActionResult> GetByCurrentUser()
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Succes",
                Data = await userService.GetAsync(id)
            });
        }

        [Authorize(Roles="SuperAdmin")]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams @params)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Succes",
                Data = await userService.GetAllUsersAsync(@params)
            });

        [HttpPatch("upgrade-role")]
        public async ValueTask<IActionResult> UpgradeRoleAsync(long id, UserRole role)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.userService.UpgradeRoleAsync(id, role)
        });

        [HttpPost("image-upload")]
        public async Task<IActionResult> UploadImageAsync(long id, [FromForm] AttachmentCreationDto dto)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Succes",
                Data = await userService.ImageUploadAsync(id, dto)
            }
                );

    }
}

