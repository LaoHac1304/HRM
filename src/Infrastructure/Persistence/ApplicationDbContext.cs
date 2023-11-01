using System.Reflection;
using Duende.IdentityServer.EntityFramework.Options;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using hrOT.Domain.IdentityModel;

//using hrOT.Infrastructure.Identity;
using hrOT.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace hrOT.Infrastructure.Persistence;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options, operationalStoreOptions)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    public DbSet<Allowance> Allowances => Set<Allowance>();
    public DbSet<AllowanceType> AllowanceTypes => Set<AllowanceType>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<DetailTaxIncome> DetailTaxIncomes => Set<DetailTaxIncome>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<EmployeeContract> EmployeeContracts => Set<EmployeeContract>();
    public DbSet<Exchange> Exchanges => Set<Exchange>();
    public DbSet<Experience> Experiences => Set<Experience>();

    public DbSet<LeaveLog> LeaveLogs => Set<LeaveLog>();

    public DbSet<OvertimeLog> OvertimeLogs => Set<OvertimeLog>();

    public DbSet<PaySlip> PaySlips => Set<PaySlip>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Skill_Employee> Skill_Employees => Set<Skill_Employee>();

    public DbSet<TaxInCome> TaxInComes => Set<TaxInCome>();
    public DbSet<TimeAttendanceLog> TimeAttendanceLogs => Set<TimeAttendanceLog>();
    public DbSet<Family> Families => Set<Family>();
    public DbSet<Degree> Degrees => Set<Degree>();
    public DbSet<Bank> Banks => Set<Bank>();
    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();

    public DbSet<AnnualWorkingDay> AnnualWorkingDays => Set<AnnualWorkingDay>();
    public DbSet<LeaveType> LeaveTypes => Set<LeaveType>();


    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    //    base.OnModelCreating(builder);
    //}


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<AppIdentityRole>()
    .HasData(
        new AppIdentityRole
        {
            Id = "37dde3f4-d0c9-4477-97d6-ff29f677dccc",
            Name = "Manager",
            NormalizedName = "MANAGER",
            ConcurrencyStamp = null,
            Description = "Quản lí hệ thống"
        },
        new AppIdentityRole
        {
            Id = "b9cf3487-3d04-4cbf-85b7-e33360566485",
            Name = "Employee",
            NormalizedName = "EMPLOYEE",
            ConcurrencyStamp = null,
            Description = "Nhân viên công ty"
        }
    );

        builder.Entity<ApplicationUser>()
    .HasData(
        new ApplicationUser
        {
            Id = "871a809a-b3fa-495b-9cc2-c5d738a866cf",
            Fullname = "string",
            BirthDay = DateTime.Parse("2002-06-20 08:30:56"),
            Image = null,
            UserName = "lewis",
            NormalizedUserName = "LEWIS",
            Email = "tekato2002@gmail.com",
            NormalizedEmail = "TEKATO2002@GMAIL.COM",
            EmailConfirmed = false,
            PasswordHash = "AQAAAAIAAYagAAAAELBNBVEaKLPiH0GMl0YJtU00Ss5zZODHsIRzLlxZlgsxD1ZOy8YBBpvTdyxPsp2+AQ==",
            SecurityStamp = "FHSBRSP7Q6SW6JWBVKCFBC6LKFR4MAR7",
            ConcurrencyStamp = "445607b7-57f3-4092-9129-c8becc104929",
            PhoneNumber = "0899248435",
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnd = null,
            LockoutEnabled = true,
            AccessFailedCount = 0
        }
    );

        builder.Entity<ApplicationUser>()
            .HasData(
            new ApplicationUser
            {
                Id = "fe30e976-2640-4d35-8334-88e7c3b1eac1",
                Fullname = "Lewis",
                Image = "TESTIMAGE",
                UserName = "admin",
                BirthDay = DateTime.Parse("9/9/9999"),
                NormalizedUserName = "test",
                Email = "test@gmail.com",
                NormalizedEmail = "test@gmail.com",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEFNwXlIXp0mbDE5k1gIQdlbAczn8BwINQnF5S0qULxDK/6luT/bumpD+HFOXM0k59A==",
                SecurityStamp = "VEPOTJNXQCZMK3J7R27HMLXD64T72GU6",
                ConcurrencyStamp = "40495f9c-e853-41e8-8c5b-6b3c93d3791b",
                PhoneNumber = "123456789",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnd = DateTimeOffset.Parse("9/9/9999 12:00:00 AM +07:00"),
                LockoutEnabled = true,
                AccessFailedCount = 0
            }
        );

        builder.Entity<IdentityUserRole<string>>()
            .HasData(
            new IdentityUserRole<string>
            {
                UserId = "871a809a-b3fa-495b-9cc2-c5d738a866cf",
                RoleId = "37dde3f4-d0c9-4477-97d6-ff29f677dccc"
            }
    );

        builder.Entity<AllowanceType>().HasData(
            new AllowanceType
            {
                Id = new Guid("123e4567-e89b-12d3-a456-426655440000"),
                Name = "Trợ cấp thâm niên",
                IsPayTax = true,
                Description = "Trợ cấp được cấp cho nhân viên dựa trên thâm niên làm việc trong công ty.",
                Eligibility_Criteria = "Nhân viên có thâm niên làm việc từ 5 năm trở lên.",
                Requirements = "Không yêu cầu.",
                Created = DateTime.Parse("2023-06-28 10:00:00"),
                CreatedBy = "Admin",
                LastModified = DateTime.Parse("2023-06-28 10:30:00"),
                LastModifiedBy = "Admin",
                IsDeleted = false
            },
            new AllowanceType
            {
                Id = new Guid("223e4567-e89b-12d3-a456-426655440000"),
                Name = "Trợ cấp đi lại",
                IsPayTax = false,
                Description = "Trợ cấp được cấp cho nhân viên để chi trả các chi phí đi lại.",
                Eligibility_Criteria = "Áp dụng cho tất cả nhân viên công ty.",
                Requirements = "Không yêu cầu.",
                Created = DateTime.Parse("2023-06-28 11:00:00"),
                CreatedBy = "Admin",
                LastModified = DateTime.Parse("2023-06-28 11:30:00"),
                LastModifiedBy = "Admin",
                IsDeleted = false
            },
            new AllowanceType
            {
                Id = new Guid("323e4567-e89b-12d3-a456-426655440000"),
                Name = "Trợ cấp ăn trưa",
                IsPayTax = true,
                Description = "Trợ cấp được cấp cho nhân viên để trang trải chi phí ăn trưa hàng ngày.",
                Eligibility_Criteria = "Áp dụng cho tất cả nhân viên công ty.",
                Requirements = "Không yêu cầu.",
                Created = DateTime.Parse("2023-06-28 12:00:00"),
                CreatedBy = "Admin",
                LastModified = DateTime.Parse("2023-06-28 12:30:00"),
                LastModifiedBy = "Admin",
                IsDeleted = false
            },
            new AllowanceType
            {
                Id = new Guid("423e4567-e89b-12d3-a456-426655440000"),
                Name = "Trợ cấp làm thêm giờ",
                IsPayTax = true,
                Description = "Trợ cấp được cấp cho nhân viên khi làm thêm giờ làm việc.",
                Eligibility_Criteria = "Áp dụng cho nhân viên làm thêm giờ.",
                Requirements = "Yêu cầu có xác nhận của quản lý.",
                Created = DateTime.Parse("2023-06-28 13:00:00"),
                CreatedBy = "Admin",
                LastModified = DateTime.Parse("2023-06-28 13:30:00"),
                LastModifiedBy = "Admin",
                IsDeleted = false
            },
            new AllowanceType
            {
                Id = new Guid("523e4567-e89b-12d3-a456-426655440000"),
                Name = "Trợ cấp sức khỏe",
                IsPayTax = true,
                Description = "Trợ cấp được cấp cho nhân viên để hỗ trợ chi phí chăm sóc sức khỏe.",
                Eligibility_Criteria = "Áp dụng cho tất cả nhân viên công ty.",
                Requirements = "Không yêu cầu.",
                Created = DateTime.Parse("2023-06-28 14:00:00"),
                CreatedBy = "Admin",
                LastModified = DateTime.Parse("2023-06-28 14:30:00"),
                LastModifiedBy = "Admin",
                IsDeleted = false
            }
);

        builder.Entity<Department>().HasData(
    new Department
    {
        Id = new Guid("123e4567-e89b-12d3-a456-426655440000"),
        Name = "Bộ phận Phát triển",
        Description = "Bộ phận chịu trách nhiệm phát triển các ứng dụng và giải pháp công nghệ mới.",
        Created = DateTime.Parse("2023-06-28 10:00:00"),
        CreatedBy = "Admin",
        LastModified = DateTime.Parse("2023-06-28 10:30:00"),
        LastModifiedBy = "Admin",
        IsDeleted = false
    },
    new Department
    {
        Id = new Guid("223e4567-e89b-12d3-a456-426655440000"),
        Name = "Bộ phận Hỗ trợ kỹ thuật",
        Description = "Bộ phận cung cấp hỗ trợ kỹ thuật và giải quyết sự cố cho khách hàng.",
        Created = DateTime.Parse("2023-06-28 11:00:00"),
        CreatedBy = "Admin",
        LastModified = DateTime.Parse("2023-06-28 11:30:00"),
        LastModifiedBy = "Admin",
        IsDeleted = false
    },
    new Department
    {
        Id = new Guid("323e4567-e89b-12d3-a456-426655440000"),
        Name = "Bộ phận Quản lý Dự án",
        Description = "Bộ phận quản lý và điều phối các dự án công nghệ trong công ty.",
        Created = DateTime.Parse("2023-06-28 12:00:00"),
        CreatedBy = "Admin",
        LastModified = DateTime.Parse("2023-06-28 12:30:00"),
        LastModifiedBy = "Admin",
        IsDeleted = false
    },
    new Department
    {
        Id = new Guid("423e4567-e89b-12d3-a456-426655440000"),
        Name = "Bộ phận Kỹ thuật Mạng",
        Description = "Bộ phận chịu trách nhiệm quản lý và duy trì hệ thống mạng trong công ty.",
        Created = DateTime.Parse("2023-06-28 13:00:00"),
        CreatedBy = "Admin",
        LastModified = DateTime.Parse("2023-06-28 13:30:00"),
        LastModifiedBy = "Admin",
        IsDeleted = false
    },
    new Department
    {
        Id = new Guid("523e4567-e89b-12d3-a456-426655440000"),
        Name = "Bộ phận Phân tích Dữ liệu",
        Description = "Bộ phận thực hiện phân tích và xử lý dữ liệu để đưa ra thông tin và thông báo phù hợp.",
        Created = DateTime.Parse("2023-06-28 14:00:00"),
        CreatedBy = "Admin",
        LastModified = DateTime.Parse("2023-06-28 14:30:00"),
        LastModifiedBy = "Admin",
        IsDeleted = false
    }
);

        builder.Entity<Position>().HasData(
    new Position
    {
        Id = new Guid("123e4567-e89b-12d3-a456-426655440001"),
        DepartmentId = new Guid("123e4567-e89b-12d3-a456-426655440000"),
        Name = "Quản lý Dự án",
        Created = DateTime.Parse("2023-06-28 10:00:00"),
        CreatedBy = "Admin",
        LastModified = DateTime.Parse("2023-06-28 10:30:00"),
        LastModifiedBy = "Admin",
        IsDeleted = false
    },
    new Position
    {
        Id = new Guid("223e4567-e89b-12d3-a456-426655440002"),
        DepartmentId = new Guid("223e4567-e89b-12d3-a456-426655440000"),
        Name = "Kỹ sư Phần mềm",
        Created = DateTime.Parse("2023-06-28 11:00:00"),
        CreatedBy = "Admin",
        LastModified = DateTime.Parse("2023-06-28 11:30:00"),
        LastModifiedBy = "Admin",
        IsDeleted = false
    },
    new Position
    {
        Id = new Guid("323e4567-e89b-12d3-a456-426655440003"),
        DepartmentId = new Guid("323e4567-e89b-12d3-a456-426655440000"),
        Name = "Chuyên viên Hỗ trợ kỹ thuật",
        Created = DateTime.Parse("2023-06-28 12:00:00"),
        CreatedBy = "Admin",
        LastModified = DateTime.Parse("2023-06-28 12:30:00"),
        LastModifiedBy = "Admin",
        IsDeleted = false
    },
    new Position
    {
        Id = new Guid("423e4567-e89b-12d3-a456-426655440004"),
        DepartmentId = new Guid("423e4567-e89b-12d3-a456-426655440000"),
        Name = "Chuyên gia Bảo mật",
        Created = DateTime.Parse("2023-06-28 13:00:00"),
        CreatedBy = "Admin",
        LastModified = DateTime.Parse("2023-06-28 13:30:00"),
        LastModifiedBy = "Admin",
        IsDeleted = false
    },
    new Position
    {
        Id = new Guid("523e4567-e89b-12d3-a456-426655440005"),
        DepartmentId = new Guid("523e4567-e89b-12d3-a456-426655440000"),
        Name = "Trưởng Nhóm Phân tích Dữ liệu",
        Created = DateTime.Parse("2023-06-28 14:00:00"),
        CreatedBy = "Admin",
        LastModified = DateTime.Parse("2023-06-28 14:30:00"),
        LastModifiedBy = "Admin",
        IsDeleted = false
    }
);

        builder.Entity<Employee>().HasData(
    new Employee
    {
        Id = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        ApplicationUserId = "fe30e976-2640-4d35-8334-88e7c3b1eac1",
        PositionId = new Guid("123e4567-e89b-12d3-a456-426655440001"),
        Created = DateTime.Parse("2023-06-28 15:00:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    }
);

        builder.Entity<EmployeeContract>().HasData(
    new EmployeeContract
    {
        Id = new Guid("123e4567-e89b-12d3-a456-426655440020"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        File = null,
        StartDate = DateTime.Parse("2023-06-28 16:00:00"),
        EndDate = DateTime.Parse("2023-12-31 23:59:59"),
        Job = "Nhân viên",
        Salary = 5000000,
        CustomSalary = null,
        Status = EmployeeContractStatus.Effective,
        SalaryType = SalaryType.Net,
        ContractType = ContractType.Open_Ended,
        Created = DateTime.Parse("2023-06-28 16:00:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    }
);

        builder.Entity<Allowance>().HasData(
    new Allowance
    {
        Id = new Guid("123e4567-e89b-12d3-a456-426655440030"),
        EmployeeContractId = new Guid("123e4567-e89b-12d3-a456-426655440020"),
        AllowanceTypeId = new Guid("123e4567-e89b-12d3-a456-426655440000"),
        Amount = 500000,
        Created = DateTime.Parse("2023-06-28 17:00:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Allowance
    {
        Id = new Guid("223e4567-e89b-12d3-a456-426655440031"),
        EmployeeContractId = new Guid("123e4567-e89b-12d3-a456-426655440020"),
        AllowanceTypeId = new Guid("223e4567-e89b-12d3-a456-426655440000"),
        Amount = 1000000,
        Created = DateTime.Parse("2023-06-28 17:30:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Allowance
    {
        Id = new Guid("323e4567-e89b-12d3-a456-426655440032"),
        EmployeeContractId = new Guid("123e4567-e89b-12d3-a456-426655440020"),
        AllowanceTypeId = new Guid("323e4567-e89b-12d3-a456-426655440000"),
        Amount = 2000000,
        Created = DateTime.Parse("2023-06-28 18:00:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Allowance
    {
        Id = new Guid("423e4567-e89b-12d3-a456-426655440033"),
        EmployeeContractId = new Guid("123e4567-e89b-12d3-a456-426655440020"),
        AllowanceTypeId = new Guid("423e4567-e89b-12d3-a456-426655440000"),
        Amount = 3000000,
        Created = DateTime.Parse("2023-06-28 18:30:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Allowance
    {
        Id = new Guid("523e4567-e89b-12d3-a456-426655440034"),
        EmployeeContractId = new Guid("123e4567-e89b-12d3-a456-426655440020"),
        AllowanceTypeId = new Guid("523e4567-e89b-12d3-a456-426655440000"),
        Amount = 4000000,
        Created = DateTime.Parse("2023-06-28 19:00:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    }
);

        builder.Entity<Bank>().HasData(
    new Bank
    {
        Id = new Guid("123e4567-e89b-12d3-a456-426655440001"),
        BankName = "Ngân hàng TMCP Á Châu",
        Description = "Asia Commercial Joint Stock Bank",
        Created = DateTime.Parse("2023-06-28 10:00:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Bank
    {
        Id = new Guid("223e4567-e89b-12d3-a456-426655440002"),
        BankName = "Ngân hàng TMCP Tiên Phong",
        Description = "Tien Phong Commercial Joint Stock Bank",
        Created = DateTime.Parse("2023-06-28 11:00:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Bank
    {
        Id = new Guid("323e4567-e89b-12d3-a456-426655440003"),
        BankName = "Ngân hàng TMCP Đông Á",
        Description = "Dong A Commercial Joint Stock Bank",
        Created = DateTime.Parse("2023-06-28 12:00:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Bank
    {
        Id = new Guid("423e4567-e89b-12d3-a456-426655440004"),
        BankName = "Ngân Hàng TMCP Đông Nam Á",
        Description = "Southeast Asia Commercial Joint Stock Bank",
        Created = DateTime.Parse("2023-06-28 13:00:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Bank
    {
        Id = new Guid("523e4567-e89b-12d3-a456-426655440005"),
        BankName = "Ngân hàng TMCP An Bình",
        Description = "An Binh Commercial Joint Stock Bank",
        Created = DateTime.Parse("2023-06-28 14:00:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Bank
    {
        Id = new Guid("623e4567-e89b-12d3-a456-426655440006"),
        BankName = "Ngân hàng TMCP Bắc Á",
        Description = "Bac A Commercial Joint Stock Bank",
        Created = DateTime.Parse("2023-06-28 15:00:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Bank
    {
        Id = new Guid("723e4567-e89b-12d3-a456-426655440007"),
        BankName = "Ngân hàng TMCP Bản Việt",
        Description = "Vietcapital Commercial Joint Stock Bank",
        Created = DateTime.Parse("2023-06-28 16:00:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Bank
    {
        Id = new Guid("823e4567-e89b-12d3-a456-426655440008"),
        BankName = "Ngân hàng TMCP Hàng hải Việt Nam",
        Description = "Vietnam Maritime Joint – Stock Commercial Bank",
        Created = DateTime.Parse("2023-06-28 17:00:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Bank
    {
        Id = new Guid("923e4567-e89b-12d3-a456-426655440009"),
        BankName = "Ngân hàng TMCP Hàng hóa",
        Description = "Vietnam Bank for Industry and Trade",
        Created = DateTime.Parse("2023-06-28 18:00:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Bank
    {
        Id = new Guid("a23e4567-e89b-12d3-a456-426655440010"),
        BankName = "Ngân hàng TMCP Nam Á",
        Description = "Nam A Commercial Joint Stock Bank",
        Created = DateTime.Parse("2023-06-28 19:00:00"),
        CreatedBy = "Admin",
        IsDeleted = false
    }
);

        builder.Entity<BankAccount>().HasData(
    new BankAccount
    {
        Id = new Guid("223e4567-e89b-12d3-a456-426655440001"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        BankId = new Guid("123e4567-e89b-12d3-a456-426655440001"),
        BankAccountNumber = "0123456789",
        BankAccountName = "John Doe",
        Selected = true,
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new BankAccount
    {
        Id = new Guid("223e4567-e89b-12d3-a456-426655440002"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        BankId = new Guid("223e4567-e89b-12d3-a456-426655440002"),
        BankAccountNumber = "9876543210",
        BankAccountName = "Jane Smith",
        Selected = false,
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new BankAccount
    {
        Id = new Guid("223e4567-e89b-12d3-a456-426655440003"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        BankId = new Guid("323e4567-e89b-12d3-a456-426655440003"),
        BankAccountNumber = "5555555555",
        BankAccountName = "Robert Johnson",
        Selected = false,
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    }
);

        builder.Entity<Degree>().HasData(
    new Degree
    {
        Id = new Guid("223e4567-e89b-12d3-a456-426655440011"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        Name = "Bằng Cử nhân Công nghệ thông tin",
        Status = DegreeStatus.Valid,
        Type = TypeOfDegree.Master,
        Photo = "test",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Degree
    {
        Id = new Guid("223e4567-e89b-12d3-a456-426655440012"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        Name = "Thạc sĩ Khoa học Máy tính",
        Status = DegreeStatus.Valid,
        Type = TypeOfDegree.Master,
        Photo = "test",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Degree
    {
        Id = new Guid("223e4567-e89b-12d3-a456-426655440013"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        Name = "Tiến sĩ Công nghệ Thông tin",
        Status = DegreeStatus.Valid,
        Type = TypeOfDegree.Master,
        Photo = "test",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Degree
    {
        Id = new Guid("223e4567-e89b-12d3-a456-426655440014"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        Name = "Bằng Cử nhân Khoa học Máy tính",
        Status = DegreeStatus.Valid,
        Type = TypeOfDegree.Master,
        Photo = "test",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Degree
    {
        Id = new Guid("223e4567-e89b-12d3-a456-426655440015"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        Name = "Tiến sĩ Khoa học Máy tính ứng dụng",
        Status = DegreeStatus.Valid,
        Type = TypeOfDegree.Master,
        Photo = "test",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    }
);

        builder.Entity<Experience>().HasData(
    new Experience
    {
        Id = new Guid("323e4567-e89b-12d3-a456-426655440016"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        NameProject = "Dự án Quản lý hệ thống CRM",
        TeamSize = 5,
        StartDate = new DateTime(2020, 1, 1),
        EndDate = new DateTime(2021, 6, 30),
        Description = "Dự án Quản lý hệ thống CRM cho khách hàng lớn",
        TechStack = ".NET, SQL Server, Angular",
        Status = true,
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Experience
    {
        Id = new Guid("323e4567-e89b-12d3-a456-426655440017"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        NameProject = "Dự án Phát triển ứng dụng di động",
        TeamSize = 8,
        StartDate = new DateTime(2019, 5, 1),
        EndDate = new DateTime(2020, 12, 31),
        Description = "Dự án Phát triển ứng dụng di động trên nền tảng iOS và Android",
        TechStack = "Swift, Kotlin, Firebase",
        Status = true,
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Experience
    {
        Id = new Guid("323e4567-e89b-12d3-a456-426655440018"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        NameProject = "Dự án Xây dựng hệ thống quản lý dữ liệu",
        TeamSize = 4,
        StartDate = new DateTime(2018, 3, 1),
        EndDate = new DateTime(2019, 12, 31),
        Description = "Dự án Xây dựng hệ thống quản lý dữ liệu cho khách hàng trong lĩnh vực tài chính",
        TechStack = "Java, Spring Boot, MySQL",
        Status = true,
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Experience
    {
        Id = new Guid("323e4567-e89b-12d3-a456-426655440019"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        NameProject = "Dự án Migrating hệ thống cũ",
        TeamSize = 3,
        StartDate = new DateTime(2017, 6, 1),
        EndDate = new DateTime(2018, 12, 31),
        Description = "Dự án Migrating hệ thống cũ sang môi trường mới",
        TechStack = "ASP.NET, SQL Server, Azure",
        Status = true,
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Experience
    {
        Id = new Guid("323e4567-e89b-12d3-a456-426655440020"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        NameProject = "Dự án Phát triển ứng dụng web",
        TeamSize = 6,
        StartDate = new DateTime(2016, 2, 1),
        EndDate = new DateTime(2017, 5, 31),
        Description = "Dự án Phát triển ứng dụng web sử dụng công nghệ mới",
        TechStack = "React, Node.js, MongoDB",
        Status = true,
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    }
);

        builder.Entity<Family>().HasData(
    new Family
    {
        Id = new Guid("423e4567-e89b-12d3-a456-426655440021"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        Name = "Nguyễn Văn A",
        DateOfBirth = new DateTime(1980, 1, 1),
        Relationship = Relationship.Parent,
        IsDependent = true,
        CitizenIdentificationNumber = "123456789",
        CreatedDateCI = new DateTime(2020, 5, 10),
        PlaceForCI = "Hà Nội",
        PhotoCIOnTheFront = "front.jpg",
        PhotoCIOnTheBack = "back.jpg",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Family
    {
        Id = new Guid("423e4567-e89b-12d3-a456-426655440022"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        Name = "Nguyễn Thị B",
        DateOfBirth = new DateTime(1985, 2, 10),
        Relationship = Relationship.Parent,
        IsDependent = true,
        CitizenIdentificationNumber = "987654321",
        CreatedDateCI = new DateTime(2019, 8, 20),
        PlaceForCI = "Hồ Chí Minh",
        PhotoCIOnTheFront = "front.jpg",
        PhotoCIOnTheBack = "back.jpg",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Family
    {
        Id = new Guid("423e4567-e89b-12d3-a456-426655440023"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        Name = "Nguyễn Minh C",
        DateOfBirth = new DateTime(2010, 7, 15),
        Relationship = Relationship.Child,
        IsDependent = true,
        CitizenIdentificationNumber = "654321789",
        CreatedDateCI = new DateTime(2022, 1, 5),
        PlaceForCI = "Đà Nẵng",
        PhotoCIOnTheFront = "front.jpg",
        PhotoCIOnTheBack = "back.jpg",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Family
    {
        Id = new Guid("423e4567-e89b-12d3-a456-426655440024"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        Name = "Nguyễn Đức D",
        DateOfBirth = new DateTime(2015, 12, 25),
        Relationship = Relationship.Child,
        IsDependent = true,
        CitizenIdentificationNumber = "321789654",
        CreatedDateCI = new DateTime(2023, 3, 10),
        PlaceForCI = "Hải Phòng",
        PhotoCIOnTheFront = "front.jpg",
        PhotoCIOnTheBack = "back.jpg",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Family
    {
        Id = new Guid("423e4567-e89b-12d3-a456-426655440025"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        Name = "Nguyễn Thị E",
        DateOfBirth = new DateTime(1990, 6, 18),
        Relationship = Relationship.Sibling,
        IsDependent = false,
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    }
);

        builder.Entity<OvertimeLog>().HasData(
    new OvertimeLog
    {
        Id = new Guid("523e4567-e89b-12d3-a456-426655440031"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        StartDate = new DateTime(2023, 6, 1),
        EndDate = new DateTime(2023, 6, 1),
        TotalHours = 2.5,
        Coefficients = 1.5,
        Status = OvertimeLogStatus.Approved,
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new OvertimeLog
    {
        Id = new Guid("523e4567-e89b-12d3-a456-426655440032"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        StartDate = new DateTime(2023, 6, 2),
        EndDate = new DateTime(2023, 6, 2),
        TotalHours = 3.0,
        Coefficients = 1.5,
        Status = OvertimeLogStatus.Approved,
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new OvertimeLog
    {
        Id = new Guid("523e4567-e89b-12d3-a456-426655440033"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        StartDate = new DateTime(2023, 6, 3),
        EndDate = new DateTime(2023, 6, 3),
        TotalHours = 1.5,
        Coefficients = 1.5,
        Status = OvertimeLogStatus.Approved,
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new OvertimeLog
    {
        Id = new Guid("523e4567-e89b-12d3-a456-426655440034"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        StartDate = new DateTime(2023, 6, 4),
        EndDate = new DateTime(2023, 6, 4),
        TotalHours = 4.0,
        Coefficients = 2.0,
        Status = OvertimeLogStatus.Approved,
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new OvertimeLog
    {
        Id = new Guid("523e4567-e89b-12d3-a456-426655440035"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        StartDate = new DateTime(2023, 6, 5),
        EndDate = new DateTime(2023, 6, 5),
        TotalHours = 2.0,
        Coefficients = 2.0,
        Status = OvertimeLogStatus.Approved,
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    }
);

        builder.Entity<LeaveLog>().HasData(
    new LeaveLog
    {
        Id = new Guid("623e4567-e89b-12d3-a456-426655440041"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        StartDate = new DateTime(2023, 6, 1),
        EndDate = new DateTime(2023, 6, 1),
        LeaveHours = 8.0,
        Reason = "Nghỉ ốm",
        Status = LeaveLogStatus.Approved,
        LeaveTypeId = new Guid("1611aee4-0a90-464d-927c-462f51d19120"),
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new LeaveLog
    {
        Id = new Guid("623e4567-e89b-12d3-a456-426655440042"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        StartDate = new DateTime(2023, 6, 2),
        EndDate = new DateTime(2023, 6, 2),
        LeaveHours = 4.0,
        Reason = "Nghỉ phép",
        Status = LeaveLogStatus.Approved,
        LeaveTypeId = new Guid("1611aee4-0a90-464d-927c-462f51d19120\r\n"),
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new LeaveLog
    {
        Id = new Guid("623e4567-e89b-12d3-a456-426655440043"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        StartDate = new DateTime(2023, 6, 3),
        EndDate = new DateTime(2023, 6, 3),
        LeaveHours = 8.0,
        Reason = "Nghỉ ốm",
        Status = LeaveLogStatus.Approved,
        LeaveTypeId = new Guid("0d87bff5-e9ca-4e8a-af18-6cc2e804e57d"),
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new LeaveLog
    {
        Id = new Guid("623e4567-e89b-12d3-a456-426655440044"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        StartDate = new DateTime(2023, 6, 4),
        EndDate = new DateTime(2023, 6, 4),
        LeaveHours = 4.0,
        Reason = "Nghỉ phép",
        Status = LeaveLogStatus.Approved,
        LeaveTypeId = new Guid("18167791-8248-4924-a1c6-90858852f905"),
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new LeaveLog
    {
        Id = new Guid("623e4567-e89b-12d3-a456-426655440045"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        StartDate = new DateTime(2023, 6, 5),
        EndDate = new DateTime(2023, 6, 5),
        LeaveHours = 8.0,
        Reason = "Nghỉ ốm",
        Status = LeaveLogStatus.Approved,
        LeaveTypeId = new Guid("51c55016-58c4-49d3-b0b2-2d7e0a9cd7d7"),
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    }
);

        builder.Entity<LeaveType>().HasData(
    new LeaveType
    {
        Id = new Guid("1611aee4-0a90-464d-927c-462f51d19120"),
        Name = "Nghỉ thai sản",
        Description = "Nghỉ mà nhân viên nữ được nghỉ trong thời kỳ mang thai và sau sinh.",
        Benefit = 10,
        IsReward = true
    },
    new LeaveType
    {
        Id = new Guid("0d87bff5-e9ca-4e8a-af18-6cc2e804e57d"),
        Name = "Nghỉ phép cha",
        Description = "Nghỉ mà nhân viên nam được nghỉ để hỗ trợ đối tác trong thời kỳ mang thai và sau sinh.",
        Benefit = 10,
        IsReward = true
    },
    new LeaveType
    {
        Id = new Guid("18167791-8248-4924-a1c6-90858852f905"),
        Name = "Nghỉ phép hàng năm",
        Description = "Nghỉ dành cho nhân viên để nghỉ ngơi, thư giãn và có thời gian cá nhân.",
        Benefit = 2,
        IsReward = false
    },
    new LeaveType
    {
        Id = new Guid("51c55016-58c4-49d3-b0b2-2d7e0a9cd7d7"),
        Name = "Nghỉ ốm",
        Description = "Nghỉ khi nhân viên bị ốm hoặc cần chăm sóc y tế."
,
        Benefit = 2,
        IsReward = false
    }
);

        builder.Entity<Skill>().HasData(
    new Skill
    {
        Id = new Guid("723e4567-e89b-12d3-a456-426655440051"),
        SkillName = "Lập trình Java",
        Skill_Description = "Mô tả kỹ năng lập trình Java",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Skill
    {
        Id = new Guid("723e4567-e89b-12d3-a456-426655440052"),
        SkillName = "Phân tích yêu cầu",
        Skill_Description = "Mô tả kỹ năng phân tích yêu cầu",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Skill
    {
        Id = new Guid("723e4567-e89b-12d3-a456-426655440053"),
        SkillName = "Thử nghiệm phần mềm",
        Skill_Description = "Mô tả kỹ năng thử nghiệm phần mềm",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Skill
    {
        Id = new Guid("723e4567-e89b-12d3-a456-426655440054"),
        SkillName = "Quản lý dự án",
        Skill_Description = "Mô tả kỹ năng quản lý dự án",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Skill
    {
        Id = new Guid("723e4567-e89b-12d3-a456-426655440055"),
        SkillName = "Công nghệ web",
        Skill_Description = "Mô tả kỹ năng công nghệ web",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    }
);

        //
        builder.Entity<Skill_Employee>().HasData(
    new Skill_Employee
    {
        Id = new Guid("823e4567-e89b-12d3-a456-426655440061"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        SkillId = new Guid("723e4567-e89b-12d3-a456-426655440051"),
        Level = "Chuyên gia",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Skill_Employee
    {
        Id = new Guid("823e4567-e89b-12d3-a456-426655440062"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        SkillId = new Guid("723e4567-e89b-12d3-a456-426655440052"),
        Level = "Trung bình",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Skill_Employee
    {
        Id = new Guid("823e4567-e89b-12d3-a456-426655440063"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        SkillId = new Guid("723e4567-e89b-12d3-a456-426655440053"),
        Level = "Trung bình",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Skill_Employee
    {
        Id = new Guid("823e4567-e89b-12d3-a456-426655440064"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        SkillId = new Guid("723e4567-e89b-12d3-a456-426655440054"),
        Level = "Chuyên gia",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    },
    new Skill_Employee
    {
        Id = new Guid("823e4567-e89b-12d3-a456-426655440065"),
        EmployeeId = new Guid("123e4567-e89b-12d3-a456-426655440010"),
        SkillId = new Guid("723e4567-e89b-12d3-a456-426655440055"),
        Level = "Trung bình",
        Created = DateTime.Now,
        CreatedBy = "Admin",
        IsDeleted = false
    }
);

        builder.Entity<TaxInCome>()
       .HasData(
           new TaxInCome

           {
               Id = Guid.Parse("a279788d-0fa2-4d9e-9e8e-5d689e853972"),
               Muc_chiu_thue = 5000000,
               Thue_suat = 0.05,
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           },

           new TaxInCome

           {
               Id = Guid.Parse("2d6a8e64-6130-456b-9c9d-95a1bc0a11fd"),
               Muc_chiu_thue = 10000000,
               Thue_suat = 0.1,
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           },

           new TaxInCome

           {
               Id = Guid.Parse("e582dd24-ec47-4c64-b0a7-6f8647b488a7"),
               Muc_chiu_thue = 18000000,
               Thue_suat = 0.15,
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           },

           new TaxInCome

           {
               Id = Guid.Parse("c0b17a9e-ee6f-4fe0-8e6f-33d5c63640c8"),
               Muc_chiu_thue = 32000000,
               Thue_suat = 0.2,
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           },

           new TaxInCome

           {
               Id = Guid.Parse("f0f3e78c-67c9-4e5e-a9fc-c8d2e7c1e5f5"),
               Muc_chiu_thue = 52000000,
               Thue_suat = 0.25,
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           },

           new TaxInCome

           {
               Id = Guid.Parse("78a65c98-2d7a-4c57-98f0-81f5a870a915"),
               Muc_chiu_thue = 80000000,
               Thue_suat = 0.3,
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           },

           new TaxInCome

           {
               Id = Guid.Parse("4ae0e892-5369-4ef1-9e37-5d4e0a9a3e2e"),
               Muc_chiu_thue = double.MaxValue,
               Thue_suat = 0.35,
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           });

        builder.Entity<Exchange>()
        .HasData(
            new Exchange
            {
                Id = Guid.Parse("d9f3a521-36e4-4a5c-9a89-0c733e92e85b"),
                Muc_Quy_Doi = 4750000,
                Giam_Tru = 0,
                Thue_Suat = 0.95,
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            },
            new Exchange
            {
                Id = Guid.Parse("3a1d42d9-9ee7-4a4c-b283-6e8f9126e44a"),
                Muc_Quy_Doi = 9250000,
                Giam_Tru = 250000,
                Thue_Suat = 0.9,
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            },
            new Exchange
            {
                Id = Guid.Parse("08d5ef29-44a5-47d0-9a85-28d9d0dc30f3"),
                Muc_Quy_Doi = 16050000,
                Giam_Tru = 750000,
                Thue_Suat = 0.85,
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            },
            new Exchange
            {
                Id = Guid.Parse("9218741c-99f6-40a2-87f4-d4baf4c9e15d"),
                Muc_Quy_Doi = 27250000,
                Giam_Tru = 1650000,
                Thue_Suat = 0.8,
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            },
            new Exchange
            {
                Id = Guid.Parse("e28a08ad-2b30-4df5-bc95-684d56ad8a56"),
                Muc_Quy_Doi = 42250000,
                Giam_Tru = 3250000,
                Thue_Suat = 0.75,
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            },
            new Exchange
            {
                Id = Guid.Parse("5ef5f6be-ef53-4af2-8b18-78fc6b8a295f"),
                Muc_Quy_Doi = 61850000,
                Giam_Tru = 5850000,
                Thue_Suat = 0.7,
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            },
            new Exchange
            {
                Id = Guid.Parse("7f1b1d11-3070-4b4b-96db-801d448a8920"),
                Muc_Quy_Doi = double.MaxValue,
                Giam_Tru = 9850000,
                IsDeleted = false,
                Thue_Suat = 0.65,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            });
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}