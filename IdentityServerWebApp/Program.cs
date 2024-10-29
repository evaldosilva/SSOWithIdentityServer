using IdentityServer4.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Identity Servers
builder.Services.AddIdentityServer()
            .AddInMemoryClients(
            [
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "api1" }
                }
            ])
            .AddInMemoryApiScopes(
            [
                new ApiScope("api1", "My API")
            ])
            .AddDeveloperSigningCredential();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use Identity Servers
app.UseIdentityServer();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
