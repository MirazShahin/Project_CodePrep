using CodePrep.API.Middlewares;
using CodePrep.Application.AI.Interfaces;
using CodePrep.Application.AI.Services;
using CodePrep.Application.Dashboard.Interfaces;
using CodePrep.Application.Dashboard.Services;
using CodePrep.Application.Interfaces;
using CodePrep.Application.Interview.Interfaces;
using CodePrep.Application.Interview.Services;
using CodePrep.Application.Learning.Interfaces;
using CodePrep.Application.Learning.Services;
using CodePrep.Application.Problem.Interfaces;
using CodePrep.Application.Problem.Services;
using CodePrep.Application.Services;
using CodePrep.Application.Validators;
using CodePrep.Infrastructure.AI;
using CodePrep.Infrastructure.Persistence;
using CodePrep.Infrastructure.Repositories;
using CodePrep.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using CodePrep.Application.Topics.Interfaces; 

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. CORS Policy Configuration (ADDED)
// ==========================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "http://localhost:3000",
                "https://localhost:65067", 
                "http://localhost:65067"
              )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(CodePrep.Application.Mapping.QuestionMapping).Assembly);
builder.Services.AddSingleton(config);

builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CodePrep API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token like this: Bearer {your token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionService, QuestionService>();

builder.Services.AddScoped<ITopicRepository, TopicRepository>();
builder.Services.AddScoped<ITopicService, TopicService>();

builder.Services.AddScoped<ITestCaseRepository, TestCaseRepository>();
builder.Services.AddScoped<ITestCaseService, TestCaseService>();

builder.Services.AddScoped<ISubmissionRepository, SubmissionRepository>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();

builder.Services.AddScoped<ILearningRepository, LearningRepository>();
builder.Services.AddScoped<ILearningService, LearningService>();

builder.Services.AddScoped<IInterviewRepository, InterviewRepository>();
builder.Services.AddScoped<IInterviewService, InterviewService>();

builder.Services.AddScoped<IProblemRepository, ProblemRepository>();
builder.Services.AddScoped<IProblemService, ProblemService>();

// Dashboard Dependencies
builder.Services.AddScoped<IUserProblemRepository, UserProblemRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IResourceLinkRepository, ResourceLinkRepository>();
// Add HttpClient for Codeforces
builder.Services.AddHttpClient<ICodeforcesService, CodeforcesService>();
builder.Services.Configure<GeminiOptions>(
    builder.Configuration.GetSection(GeminiOptions.SectionName));

// HttpClient & AI Provider Setup
builder.Services.AddHttpClient<IAIProvider, GeminiProvider>();
builder.Services.AddScoped<AIService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CodePrep API v1");
    });
}

// Custom Exception Middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

// ==========================================
// 2. Enable CORS Middleware (ADDED HERE)
// ==========================================
// Routing/Middleware সিরিয়ালে UseCors সবসময় Authentication-এর আগে থাকতে হবে
app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();