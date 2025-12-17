var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ProjectManagement>("projectmanagement");

builder.Build().Run();
