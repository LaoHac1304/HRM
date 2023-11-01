using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Auth.Queries;

public class FindUserByUsernameQuery : IRequest<Employee>
{
    public Guid Id { get; set; }

    public class FindUserByUsernameQueryHandler : IRequestHandler<FindUserByUsernameQuery, Employee>
    {
        private readonly IApplicationDbContext _context;

        public FindUserByUsernameQueryHandler(IIdentityService identityService, UserManager<ApplicationUser> userManager, IApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
        }

        public async Task<Employee> Handle(FindUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Thực hiện logic để tìm người dùng dựa trên username
                // Ví dụ:
                var Id = request.Id;

                // Thực hiện truy vấn từ CSDL hoặc nơi lưu trữ người dùng
                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == Id);
                if (employee == null) { throw new Exception("Không tìm thấy Employee"); }
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi ở FindByUsernameQuery: {ex}");
            }
        }
    }
}