using CodePrep.Domain.Common; 
using CodePrep.Domain.Enums;
namespace CodePrep.Domain.Entities;
public class User : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string Role { get; set; } = "Student";
    //public UserRole Role { get; set; } = UserRole.Student;

    public bool IsActive { get; set; } = true;

    public string? ProfileImageUrl { get; set; }

    public DateTime? LastLogin { get; set; }
    public ICollection<Submission> Submissions { get; set; }
    = new List<Submission>();
    public ICollection<UserProblem> UserProblems { get; set; }
    = new List<UserProblem>();
}