﻿using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Employees.Commands.Update
{
    public record UpdateEmployeeRoleEmployee : IRequest<string>
    {
        public Guid EmployeeId { get; set; }
        public Guid PositionId { get; set; }
        public string? CitizenIdentificationNumber { get; set; }
        public DateTime? CreatedDateCIN { get; set; }
        public string? PlaceForCIN { get; set; }
        public DateTime BirthDay { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        //Địa chỉ
        public string District { get; set; }
        public string Province { get; set; }
    }

    public class UpdateEmployeeRoleEmployeeHandler : IRequestHandler<UpdateEmployeeRoleEmployee, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHostingEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateEmployeeRoleEmployeeHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this.userManager = userManager;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> Handle(UpdateEmployeeRoleEmployee request, CancellationToken cancellationToken)
        {
            //var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
            var employeeId = request.EmployeeId;
            var entity = await _context.Employees
                .Include(e => e.ApplicationUser)
                .FirstOrDefaultAsync(e => e.Id == employeeId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException("Nhân viên này không tồn tại");
            }
            else if (entity.IsDeleted == true)
            {
                throw new NotFoundException("Nhân viên này đã bị xóa");
            }
            entity.CitizenIdentificationNumber = request.CitizenIdentificationNumber;
            entity.CreatedDateCIN = request.CreatedDateCIN;
            entity.PlaceForCIN = request.PlaceForCIN;
            entity.District = request.District;
            entity.Province = request.Province;
            entity.Address = request.Address;
            entity.PositionId = request.PositionId;

            if (entity.ApplicationUser != null)
            {
                entity.ApplicationUser.Fullname = request.Fullname;

                entity.ApplicationUser.BirthDay = request.BirthDay;
                entity.ApplicationUser.PhoneNumber = request.PhoneNumber;
            }

            _context.Employees.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            var user = await userManager.FindByIdAsync(entity.ApplicationUser.Id);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                await userManager.RemoveFromRolesAsync(user, userRoles);
            }

            return ("Cập nhật thành công");

        }
    }
}