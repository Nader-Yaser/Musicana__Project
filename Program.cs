using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Musicana.Api.Data;
using Musicana.Api.DependencyInjection;
using Musicana.Api.Repositories;
using Musicana.Api.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.HttpOverrides;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters
            .Add(new JsonStringEnumConverter());

        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.MusicianServices();
builder.Services.SongServices();
builder.Services.Song_MusicianService();
builder.Services.InstrumentServices();
builder.Services.Musician_InstrumentServices();
builder.Services.AlbumServices();
builder.Services.PlaylistServices();
builder.Services.FavouriteServices();


builder.Services.AddHttpContextAccessor();

var isDevelopment = builder.Environment.IsDevelopment();

builder.Services.AddDbContext<MusicanaDbContext>(options =>
{
    if (isDevelopment)
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    else
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
});

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                    ForwardedHeaders.XForwardedProto |
                    ForwardedHeaders.XForwardedHost,
    KnownNetworks = { },
    KnownProxies = { }
});
app.UseCors("AllowAll");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MusicanaDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();