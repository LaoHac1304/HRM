﻿using System.ComponentModel.DataAnnotations.Schema;
using hrOT.Domain.IdentityModel;

namespace hrOT.Domain.Entities;

public class Employee : BaseAuditableEntity
{
    [ForeignKey("ApplicationUser")]
    public string ApplicationUserId { get; set; }

    [ForeignKey("Position")]
    public Guid PositionId { get; set; }

    // Bằng cấp
    //public string? Diploma { get; set; }

    // Thẻ căn cước
    public string? CitizenIdentificationNumber { get; set; }

    public DateTime? CreatedDateCIN { get; set; }
    public string? PlaceForCIN { get; set; }
    public string? PhotoCIOnTheFront { get; set; }
    public string? PhotoCIOnTheBack { get; set; }

    //CV
    public string? CVPath { get; set; }

    //Địa chỉ
    public string? Address { get; set; }

    public string? District { get; set; }
    public string? Province { get; set; }

    // Relationship
    public IList<Family> Families { get; private set; }

    public IList<Experience> Experiences { get; private set; }

    public IList<OvertimeLog> OvertimeLogs { get; private set; }
    public IList<LeaveLog> LeaveLogs { get; private set; }

    public IList<EmployeeContract> EmployeeContracts { get; private set; }

    //public IList<InterviewProcess> InterviewProcesses { get; private set; }
    public IList<Skill_Employee> Skill_Employees { get; private set; }

    public IList<TimeAttendanceLog> TimeAttendanceLogs { get; private set; }

    public IList<Degree> Degrees { get; private set; }
    public IList<BankAccount> BankAccounts { get; private set; }
    public virtual ApplicationUser ApplicationUser { get; set; }
    public virtual Position Position { get; set; }
}