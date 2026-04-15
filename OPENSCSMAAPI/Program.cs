using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using OPENSCSMAAPI.WSCSMServiceAccess;
using OPENSCSMAPI.Execptions;
using OPENSCSMAPI.GraphQL;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddHttpClient("PowerShellWorker", client =>
{
    client.BaseAddress = new Uri("http://localhost:5001");
    client.Timeout = TimeSpan.FromSeconds(60);
});

builder.Services.AddScoped<ISCSMService, SCSMService>();
builder.Services.AddScoped<CatalogsItemsQuery>();
builder.Services.AddScoped<CatalogssrItemsQuery>();

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<CatalogsItemsQuery>()
    .AddFiltering();

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OPEN SCSM-API ", Version = "v1" });
    c.TagActionsBy(api => new[] { api.GroupName });
    c.DocInclusionPredicate((name, api) => true);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(); 


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OPEN SCSM API v1");
        c.RoutePrefix = string.Empty;  
    });
}

app.UseHttpsRedirection();

app.MapControllers();

// Mapping Incident Requets Catalogs

app.MapGet("/Catalogs/troubleTicketUrgency", async ([FromServices] CatalogsItemsQuery query) =>
{
	var data = await query.GetTroubleTicketUrgency();
	return Results.Ok(data);
})
.WithName("GetTroubleTicketUrgency")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/troubleTicketImpacts", async ([FromServices] CatalogsItemsQuery query) =>
{
	var data = await query.GetTroubleTicketImpact();
	return Results.Ok(data);
})
.WithName("GetTroubleTicketImpact")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/incidentSources", async ([FromServices] CatalogsItemsQuery query) =>
{
	var data = await query.GetIncidentSource();
	return Results.Ok(data);
})
.WithName("GetIncidentSource")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/IncidentStatus", async ([FromServices] CatalogsItemsQuery query) =>
{
	var data = await query.GetIncidentStatus();
	return Results.Ok(data);
})
.WithName("GetIncidentStatus")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/IncidentClassification", async ([FromServices] CatalogsItemsQuery query) =>
{
	var data = await query.GetIncidentClassification();
	return Results.Ok(data);
})
.WithName("GetIncidentClassification")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/IncidentTier", async ([FromServices] CatalogsItemsQuery query) =>
{
	var data = await query.GetIncidentTier();
	return Results.Ok(data);
})
.WithName("GetIncidentTier")
.WithGroupName("SCSM Catalogs");

// Mapping Service Requets Catalogs  

app.MapGet("/Catalogs/SR/ServiceRequestArea", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestAreaEnum();
	return Results.Ok(data);
})
.WithName("GetServiceRequestAreaEnum")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestArea_Content", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestAreaEnum_Content();
	return Results.Ok(data);
})
.WithName("GetServiceRequestAreaEnum_Content")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestArea_Directory", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestAreaEnum_Directory();
	return Results.Ok(data);
})
.WithName("GetServiceRequestAreaEnum_Directory")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestArea_Facilities", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestAreaEnum_Facilities();
	return Results.Ok(data);
})
.WithName("GetServiceRequestAreaEnum_Facilities")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestArea_File", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestAreaEnum_File();
	return Results.Ok(data);
})
.WithName("GetServiceRequestAreaEnum_File")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestArea_Hardware", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestAreaEnum_Hardware();
	return Results.Ok(data);
})
.WithName("GetServiceRequestAreaEnum_Hardware")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestArea_Infrastructure", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestAreaEnum_Infrastructure();
	return Results.Ok(data);
})
.WithName("GetServiceRequestAreaEnum_Infrastructure")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestArea_Messaging", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestAreaEnum_Messaging();
	return Results.Ok(data);
})
.WithName("GetServiceRequestAreaEnum_Messaging")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestArea_Operations", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestAreaEnum_Operations();
	return Results.Ok(data);
})
.WithName("GetServiceRequestAreaEnum_Operations")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestArea_Other", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestAreaEnum_Other();
	return Results.Ok(data);
})
.WithName("GetServiceRequestAreaEnum_Other")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestArea_Security", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestAreaEnum_Security();
	return Results.Ok(data);
})
.WithName("GetServiceRequestAreaEnum_Security")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestArea_Software", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestAreaEnum_Software();
	return Results.Ok(data);
})
.WithName("GetServiceRequestAreaEnum_Software")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestImplementationResults", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestImplementationResultsEnum();
	return Results.Ok(data);
})
.WithName("GetServiceRequestImplementationResultsEnum")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestPriority", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestPriorityEnum();
	return Results.Ok(data);
})
.WithName("GetServiceRequestPriorityEnum")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestSource", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestSourceEnum();
	return Results.Ok(data);
})
.WithName("GetServiceRequestSourceEnum")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestStatus", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestStatusEnum();
	return Results.Ok(data);
})
.WithName("GetServiceRequestStatusEnum")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestSupportGroup", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestSupportGroupEnum();
	return Results.Ok(data);
})
.WithName("GetServiceRequestSupportGroupEnum")
.WithGroupName("SCSM Catalogs");

app.MapGet("/Catalogs/SR/ServiceRequestUrgency", async ([FromServices] CatalogssrItemsQuery query) =>
{
	var data = await query.GetServiceRequestUrgencyEnum();
	return Results.Ok(data);
})
.WithName("GetServiceRequestUrgencyEnum")
.WithGroupName("SCSM Catalogs");

app.Run();
