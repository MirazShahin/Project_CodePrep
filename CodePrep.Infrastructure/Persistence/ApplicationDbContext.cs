using CodePrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodePrep.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<TestCase> TestCases => Set<TestCase>();
    public DbSet<QuestionTopic> QuestionTopics => Set<QuestionTopic>();
    public DbSet<Submission> Submissions => Set<Submission>();
    public DbSet<LearningContent> LearningContents => Set<LearningContent>();
    public DbSet<InterviewQuestion> InterviewQuestions => Set<InterviewQuestion>();
    public DbSet<Problem> Problems => Set<Problem>();
    public DbSet<UserProblem> UserProblems => Set<UserProblem>();

    // Newly Added DbSets
    public DbSet<ResourceLink> ResourceLinks => Set<ResourceLink>();
    public DbSet<UserTopicProgress> UserTopicProgresses => Set<UserTopicProgress>();
    public DbSet<LearningCategory> LearningCategories => Set<LearningCategory>();

    public DbSet<LearningTopic> LearningTopics => Set<LearningTopic>();

    public DbSet<LearningResource> LearningResources => Set<LearningResource>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Many-to-Many Configuration for Question and Topic
        modelBuilder.Entity<QuestionTopic>()
            .HasOne(qt => qt.Question)
            .WithMany(q => q.QuestionTopics)
            .HasForeignKey(qt => qt.QuestionId);

        modelBuilder.Entity<QuestionTopic>()
            .HasOne(qt => qt.Topic)
            .WithMany(t => t.QuestionTopics)
            .HasForeignKey(qt => qt.TopicId);

        // TestCase Relationship
        modelBuilder.Entity<TestCase>()
            .HasOne(tc => tc.Question)
            .WithMany(q => q.TestCases)
            .HasForeignKey(tc => tc.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Submission Relationships
        modelBuilder.Entity<Submission>()
            .HasOne(s => s.User)
            .WithMany(u => u.Submissions)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Submission>()
            .HasOne(s => s.Question)
            .WithMany(q => q.Submissions)
            .HasForeignKey(s => s.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        // LearningContent Relationship
        modelBuilder.Entity<LearningContent>()
            .HasOne(l => l.Topic)
            .WithMany(t => t.LearningContents)
            .HasForeignKey(l => l.TopicId)
            .OnDelete(DeleteBehavior.Restrict);

        // InterviewQuestion Relationship
        modelBuilder.Entity<InterviewQuestion>()
            .HasOne(iq => iq.Topic)
            .WithMany()
            .HasForeignKey(iq => iq.TopicId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Problem>()
            .HasOne(x => x.Topic)
            .WithMany(t => t.Problems)
            .HasForeignKey(x => x.TopicId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserProblem>()
            .HasOne(x => x.User)
            .WithMany(x => x.UserProblems)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserProblem>()
            .HasOne(x => x.Problem)
            .WithMany(x => x.UserProblems)
            .HasForeignKey(x => x.ProblemId)
            .OnDelete(DeleteBehavior.Cascade);


        // Topic and ResourceLink Relationship
        modelBuilder.Entity<ResourceLink>()
            .HasOne(r => r.Topic)
            .WithMany(t => t.Resources)
            .HasForeignKey(r => r.TopicId)
            .OnDelete(DeleteBehavior.Cascade);

        // UserTopicProgress Composite Key / Relationship Configuration
        modelBuilder.Entity<UserTopicProgress>()
            .HasOne(ut => ut.Topic)
            .WithMany()
            .HasForeignKey(ut => ut.TopicId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}