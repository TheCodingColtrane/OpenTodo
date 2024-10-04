using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenTodo.Auth;
using OpenTodo.Data;
using OpenTodo.DTOs;
using OpenTodo.Models;
using OpenTodo.Repositories;
using OpenTodo.Services;
using OpenTodo.Utils;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var jwtOptions = builder.Configuration
    .GetSection("JwtOptions")
    .Get<JwtOptions>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<OpenTodoContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("OpenTodo")));
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TaskRepository>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<BoardRepository>();
builder.Services.AddScoped<BoardService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<Seed>();
builder.Services.AddSingleton(jwtOptions);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // Check if the token is in the Authorization cookie
            var token = context.Request.Cookies["Authorization"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }

            return Task.CompletedTask;
        }
    };

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions?.Issuer,
        ValidAudience = jwtOptions?.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.SigningKey))

    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend",
                        policy =>
                            {
                                policy.WithOrigins("https://localhost:5173", "https://localhost:5174")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();

                            });
});
builder.Services.AddAuthorization();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<OpenTodoContext>();


    if (!db.Users.Any())
    {
        var seed = services.GetRequiredService<Seed>();
        await seed.Start();
    }

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Frontend");
app.UseAuthentication();
app.UseAuthorization();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");




app.MapGet("/user", async (UserService userService) =>
{
    try
    {
        var users = await userService.GetUsers();
        return Results.Ok(users);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.Problem(ex.Message, statusCode: 500);
    }
});

app.MapGet("/user/search/{term}/", async (string term, UserService userService) =>
{
    try
    {
        var users = await userService.SearchUserByTerm(term);
        if (users is not null) return Results.Ok(users);
        return Results.NotFound(users);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.Problem(ex.Message, statusCode: 500);
    }

}).RequireAuthorization();


app.MapGet("/user/{id}", [Authorize] async (int id, UserService userService, HttpContext context) =>
{
    try
    {
        var rawId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!string.IsNullOrEmpty(rawId)) id = int.Parse(rawId);
        var user = await userService.GetUser(id);
        if (user is not null) return Results.Ok(user);
        return Results.NotFound(user);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.Problem(ex.Message, statusCode: 500);
    }
});

app.MapPost("/user", async (UserSchema user, UserService userService, HttpContext respMsg) =>
{
    try
    {
        Auth auth = new();
        if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.PasswordHash))
        {
            return Results.BadRequest("Some fields were not provided");
        }
        var createdUser = await userService.Create(user);
        var jwt = auth.GenerateToken(user);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(30),
            IsEssential = true
        };
        // respMsg.Response.Cookies.Append("Authorization", jwt, cookieOptions);
        return Results.Ok(new { success = createdUser });
    }
    catch (Exception)
    {

        throw;
    }
});

app.MapGet("/user/username/{username}", async (string username, UserService userService) =>
{
    var isTaken = await userService.GetUsernameAvailability(username);
    return Results.Ok(new { isTaken });
});

app.MapGet("/task/category/{categoryCode}/{boardCode}/{pageCode?}", async (string categoryCode, string boardCode, string? pageCode, TaskService taskService) =>
{
    var Task = await taskService.GetAllTasksByCategory(categoryCode, boardCode, pageCode);
    if (Task is not null) return Results.Ok(Task);
    return Results.Ok(Task);
});

app.MapGet("/task", async (TaskService taskService) =>
{
    try
    {
        var Task = await taskService.GetAllTasks();
        if (Task is not null) return Results.Ok(Task);
        return Results.NotFound(Task);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.Problem(ex.Message, statusCode: 500);
    }

}).AllowAnonymous();

app.MapGet("/task/{code}", async (string code, TaskService TaskService) =>
{
    try
    {
        var todo = await TaskService.GetTasksById(code);
        if (todo is not null) return Results.Ok(todo);
        return Results.NotFound(todo);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.Problem(ex.Message, statusCode: 500);
    }

});

app.MapGet("/task/{code}/user", async (string code, TaskService taskService, HttpContext ctx) =>
{
    try
    {
        var auth = new Auth();
        var user = auth.GetUserContext(ctx);
        if (user is null) return Results.Unauthorized();
        var Task = await taskService.GetAllTasksByUserId(code, user.Id);
        if (Task is not null) return Results.Ok(Task);
        return Results.NotFound(Task);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.Problem(ex.Message, statusCode: 500);
    }

}).RequireAuthorization();



app.MapPost("/task", async (TaskDTO task, TaskService taskService, HttpContext ctx) =>
{
    var auth = new Auth();
    var user = auth.GetUserContext(ctx);
    if (task.Category == 0 || string.IsNullOrEmpty(task.Title))
    {
        return Results.BadRequest("Some fields were not provided");
    }
    var createdTask = await taskService.Create(task, user);
    if (createdTask is not "") return Results.Ok(new { success = true, code = createdTask });
    return Results.Ok(new { success = false });


}).RequireAuthorization();

app.MapPut("/task", async (TaskDTO task, TaskService taskService, HttpContext ctx) =>
{
    var auth = new Auth();
    var user = auth.GetUserContext(ctx);
    if (task.Category == 0 || string.IsNullOrEmpty(task.Title))
    {
        return Results.BadRequest("Some fields were not provided");
    }
    var updatedTask = await taskService.Update(task, user);
    if (updatedTask) return Results.Ok(new { success = true });
    return Results.Ok(new { success = false });


}).RequireAuthorization();

app.MapDelete("/task/{code}", async (string code, TaskService taskService) =>
{
    var deletedTask = await taskService.Delete(code);
    if (deletedTask) return Results.Ok(new { success = true });
    return Results.Ok(new { success = false });
}).RequireAuthorization();

app.MapPost("/auth", async (UserSchema user, AuthService authService, HttpContext ctx) =>
{
    if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.PasswordHash)) return Results.BadRequest(error: "Username or Password lacking.");
    var currentUser = await authService.Login(user.Username, user.PasswordHash);
    if (currentUser.Id == 0) return Results.Unauthorized();
    Auth auth = new();
    var jwt = auth.GenerateJwtToken(currentUser);
    var cookieOptions = new CookieOptions
    {
        HttpOnly = true,
        Expires = DateTime.UtcNow.AddDays(30),
        IsEssential = true
    };
    StringBuilder sb = new();
    sb.Append(currentUser.FirstName);
    sb.Append(currentUser.LastName);
    ctx.Response.Cookies.Append("Authorization", jwt, cookieOptions);
    ctx.Response.Cookies.Append("sid", new HashID().GenerateHash(currentUser.Id), new CookieOptions() { Expires = DateTime.UtcNow.AddDays(30), IsEssential = true });
    ctx.Response.Cookies.Append("username", user.Username, new CookieOptions() { Expires = DateTime.UtcNow.AddDays(30), IsEssential = true });
    ctx.Response.Cookies.Append("name", sb.ToString(), new CookieOptions() { Expires = DateTime.UtcNow.AddDays(30), IsEssential = true });


    return Results.Ok(new { success = true });
});





app.MapGet("/board", async (BoardService boardService) =>
{
    try
    {
        var Task = await boardService.GetAllBoards();
        if (Task is not null) return Results.Ok(Task);
        return Results.NotFound(Task);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.Problem(ex.Message, statusCode: 500);
    }

}).AllowAnonymous();

app.MapGet("/board/{code}", async (string code, BoardService boardService) =>
{
    try
    {
        var Task = await boardService.GetBoardsById(code);
        if (Task is not null) return Results.Ok(Task);
        return Results.NotFound(Task);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.Problem(ex.Message, statusCode: 500);
    }

}).AllowAnonymous();

app.MapGet("/board/user-board", async (BoardService boardService, HttpContext ctx) =>
{
    try
    {
        var auth = new Auth();
        var user = auth.GetUserContext(ctx);
        var Task = await boardService.GetBoardsByUserId(user.Id);
        if (Task is not null) return Results.Ok(Task);
        return Results.NotFound(Task);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.Problem(ex.Message, statusCode: 500);
    }

}).AllowAnonymous();


app.MapGet("/board/{code}/task", async (string code, BoardService boardService) =>
{
    try
    {
        var Task = await boardService.GetAllTasksByBoardId(code);
        if (Task is not null) return Results.Ok(Task);
        return Results.NotFound(Task);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.Problem(ex.Message, statusCode: 500);
    }

}).AllowAnonymous();



app.MapGet("/board/{code}/task/search/{term}/", async (string code, string term, BoardService BoardService) =>
{
    try
    {
        var Task = await BoardService.SearchTasksByTerm(code, term);
        if (Task is not null) return Results.Ok(Task);
        return Results.NotFound(Task);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.Problem(ex.Message, statusCode: 500);
    }

}).RequireAuthorization();

app.MapPost("/board", async (BoardSchema board, BoardService boardService, HttpContext ctx) =>
{
    var auth = new Auth();
    var user = auth.GetUserContext(ctx);
    board.UserId = user.Id;
    if (string.IsNullOrEmpty(board.Name)) return Results.BadRequest("Name field was not provided");
    var boardCode = await boardService.Create(board);
    if (boardCode is not "") return Results.Ok(new { success = true, code = boardCode });
    return Results.Ok(new { success = false, code = "" });


}).RequireAuthorization();

app.MapPut("/board", async (BoardDTO board, BoardService boardService) =>
{
    if (string.IsNullOrEmpty(board.Name) || string.IsNullOrEmpty(board.Code)) return Results.BadRequest("One or more fields were not provided");
    var updatedBoard = await boardService.Update(board);
    if (updatedBoard) return Results.Ok(new { success = true });
    return Results.Ok(new { success = false });


}).RequireAuthorization();

app.MapDelete("/board/{code}", async (string code, BoardService boardService) =>
{
    var deletedBoard = await boardService.Delete(code);
    if (deletedBoard) return Results.Ok(new { success = true });
    return Results.Ok(new { success = false });
}).RequireAuthorization();

app.MapGet("/category", async (CategoryService categoryService) =>
{
    try
    {
        var categories = await categoryService.GetAllCategories();
        if (categories is not null) return Results.Ok(categories);
        return Results.NotFound(categories);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.Problem(ex.Message, statusCode: 500);
    }

}).AllowAnonymous();

app.MapGet("/category/{code}", async (string code, CategoryService categoryService) =>
{
    try
    {
        var category = await categoryService.GetCategoriesById(code);
        if (category is not null) return Results.Ok(category);
        return Results.NotFound(category);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.Problem(ex.Message, statusCode: 500);
    }

});

app.MapGet("/category/board/{code}", async (string code, CategoryService categoryService, HttpContext ctx) =>
{
    try
    {
        var auth = new Auth();
        var user = auth.GetUserContext(ctx);
        if (user is null) return Results.Unauthorized();
        var categories = await categoryService.GetAllCategoriesByBoard(code);
        if (categories is not null) return Results.Ok(categories);
        return Results.NotFound(categories);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.Problem(ex.Message, statusCode: 500);
    }

}).RequireAuthorization();



app.MapPost("/category", async (CategoryDTO category, CategoryService categoryService, HttpContext ctx) =>
{
    if (string.IsNullOrEmpty(category.Name) || category.Code == "") return Results.BadRequest("One or more fields were not provided");
    var createdCategory = await categoryService.Create(category);
    if (createdCategory is not "") return Results.Ok(new { success = true, code = createdCategory });
    return Results.Ok(new { success = false, code = "" });


}).RequireAuthorization();

app.MapPut("/category", async (CategoryDTO category, CategoryService categoryService) =>
{
    if (string.IsNullOrEmpty(category.Name) || category.Code == "") return Results.BadRequest("One or more fields were not provided");
    var updatedCategory = await categoryService.Update(category);
    if (updatedCategory) return Results.Ok(new { success = true });
    return Results.Ok(new { success = false });


}).RequireAuthorization();

app.MapDelete("/category/{code}", async (string code, CategoryService categoryService) =>
{
    var deletedCategory = await categoryService.Delete(code);
    if (deletedCategory) return Results.Ok(new { success = true });
    return Results.Ok(new { success = false });
}).RequireAuthorization();

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}