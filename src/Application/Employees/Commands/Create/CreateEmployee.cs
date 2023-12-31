﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using hrOT.Application.Common.Exceptions;

namespace hrOT.Application.Employees.Commands.Create
{
    public record CreateEmployee : IRequest<string>
    {
        // Thẻ căn cước
        public Guid PositionId { get; set; }
        public string? CitizenIdentificationNumber { get; set; }
        public DateTime? CreatedDateCIN { get; set; }
        public string? PlaceForCIN { get; set; }
        public DateTime BirthDay { get; set; }
        public string FullName { get; set; }
        public string UserName { get;  set; }
       
        public string Email { get;  set; }
        public string PhoneNumber { get;  set; }
        public string Password { get; set; }
        public string SelectedRole { get; set; }
        //Địa chỉ
        public string? Address { get; set; }
        public string? District { get; set; }
        public string? Province { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployee, string>
    {
        private readonly IIdentityService _identityService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IApplicationDbContext _context;
        public CreateEmployeeCommandHandler(IApplicationDbContext context, IIdentityService identityService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _identityService = identityService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<string> Handle(CreateEmployee request, CancellationToken cancellationToken)
        {
            
                var userExist = await userManager.FindByNameAsync(request.UserName);
                if (userExist != null)
                {
                    throw new Exception("Tên đăng nhập đã tồn tại !");
                }

                if (request.PositionId != null)
                {
                    var position = await _context.Positions.FindAsync(request.PositionId);
                    if (position == null)
                    {
                        throw new Exception("PositionId không tìm thấy !");
                    }
                }

                var user = new ApplicationUser
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    Fullname = request.FullName,
                    PhoneNumber = request.PhoneNumber,
                    BirthDay = request.BirthDay,
                };
                //để tạo một người dùng mới (user) với mật khẩu được cung cấp (request.Password
                var result = await userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, request.SelectedRole);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
                else
                {
                    throw new Exception("Tạo tài khoản thất bại !");
                }

                var entity = new Employee
                {
                    PositionId = request.PositionId,
                    ApplicationUserId = user.Id,
                    CitizenIdentificationNumber = request.CitizenIdentificationNumber,
                    CreatedDateCIN = request.CreatedDateCIN,
                    PlaceForCIN = request.PlaceForCIN,
                    Address = request.Address,
                    District = request.District,
                    Province = request.Province,
                };

                var rs =  _context.Employees.Add(entity).Entity;
                await _context.SaveChangesAsync(cancellationToken);

                return (rs.Id.ToString());
           
        }
    }

}
