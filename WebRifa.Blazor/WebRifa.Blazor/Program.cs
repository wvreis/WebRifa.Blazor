using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebRifa.Blazor.BlazorServices;
using WebRifa.Blazor.Client.Pages;
using WebRifa.Blazor.Components;
using WebRifa.Blazor.Components.Account;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Entities.ReceiptEntities;
using WebRifa.Blazor.Core.Entities.TicketEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Services;
using WebRifa.Blazor.Data;
using WebRifa.Blazor.Exceptions;
using WebRifa.Blazor.Repositories;
using WebRifa.Blazor.Services;
using WebRifa.Blazor.Services.UserServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
builder.Services.AddTransient<ICustomUserIdProvider, CustomUserIdProvider>();

builder.Services.AddScoped<IBuyerBlazorService, BuyerBlazorService>();
builder.Services.AddScoped<IRaffleBlazorService, RaffleBlazorService>();

builder.Services.AddScoped<IBuyerRepository, BuyerRepository>();
builder.Services.AddScoped<IRaffleRepository, RaffleRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();
builder.Services.AddScoped<IBuyerTicketReceiptRepository, BuyerTicketReceiptRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IBuyerService, BuyerService>();
builder.Services.AddScoped<IRaffleService, RaffleService>();
builder.Services.AddScoped<ITicketService, TicketService>();    
builder.Services.AddScoped<IRaffleCoreService,  RaffleCoreService>();
builder.Services.AddScoped<IDrawRepository, DrawRepository>();
builder.Services.AddScoped<IReceiptService, ReceiptService>();

builder.Services.AddAuthentication(options => {
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

builder.Services.AddSingleton(autoMapper => new MapperConfiguration(cfg => {
    cfg.CreateMap<Buyer, BuyerDto>().ReverseMap();
    cfg.CreateMap<Raffle, RaffleDto>().ReverseMap();
    cfg.CreateMap<Ticket, TicketDto>().ReverseMap();
    cfg.CreateMap<BuyerTicketReceipt, BuyerTicketReceiptDto>().ReverseMap();
    cfg.CreateMap<Receipt, ReceiptDto>()
        .ForMember(
            m => m.BuyerId,
            map => map.MapFrom(prop =>
                prop.BuyerTicketReceipt != null ?
                prop.BuyerTicketReceipt.FirstOrDefault()!.BuyerId :
                new Guid()))
        .ForMember(
            m => m.RaffleId,
            map => map.MapFrom(prop =>
                prop.Tickets.FirstOrDefault()!.RaffleId))
        .ForMember(
            m => m.BuyerName,
            map => map.MapFrom(prop => 
                prop.BuyerTicketReceipt != null ?
                prop.BuyerTicketReceipt.FirstOrDefault()!.Buyer!.Name :
                string.Empty))
        .ForMember(
            m => m.RaffleDescription,
            map => map.MapFrom(prop =>
                prop.Tickets.FirstOrDefault()!.Raffle != null ?
                prop.Tickets.FirstOrDefault()!.Raffle!.Description :
                string.Empty))
        .ForMember(
            m => m.TicketsNumbers,
            map => map.MapFrom(prop =>
                prop.BuyerTicketReceipt
                    .Select(btr => btr.Ticket)
                    .Select(t => t!.Number)
                    .ToList())) 
        .ReverseMap();
})
    .CreateMapper());

builder.Services.AddControllers().AddNewtonsoftJson(opt => {
    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerGenConfig());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddScoped(s => {
    try {
        var uriHelper = s.GetRequiredService<NavigationManager>();
        var httpContext = s.GetRequiredService<IHttpContextAccessor>();
        var httpClient = new HttpClient {
            BaseAddress = new Uri(uriHelper.BaseUri)
        };

        var cookieValue = 
            httpContext?.HttpContext?.Request.Headers
                .FirstOrDefault(x => x.Key == "Cookie").Value
                .ToString();

        httpClient.DefaultRequestHeaders.Add("Cookie", cookieValue);

        return httpClient;
    }
    catch {
        return new();
    }
    // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();

static Action<SwaggerGenOptions> SwaggerGenConfig()
{
    return c => {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "APILicitaSystem", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
            Description =
                "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
    };
}