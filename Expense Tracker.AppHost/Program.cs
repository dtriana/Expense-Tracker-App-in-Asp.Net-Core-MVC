var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("sqldata");

builder.AddProject<Projects.Expense_Tracker>("expense-tracker")
    .WithReference(sql)
    .WaitFor(sql);

builder.Build().Run();
