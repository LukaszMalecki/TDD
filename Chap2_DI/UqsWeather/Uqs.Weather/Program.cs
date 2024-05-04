using AdamTibi.OpenWeather;
using Uqs.Weather;
using Uqs.Weather.Wrappers;

var builder = WebApplication.CreateBuilder(args);

/*Console.WriteLine($"Service count: {builder.Services.Count}");
foreach( var item in builder.Services.Where(x => x.ServiceType.Name.Contains("Log")).ToArray() )
{
    Console.WriteLine(item);
}*/

// Add services to the container.
builder.Services.AddSingleton<IClient>(_ => 
{
    bool isLoad = bool.Parse(builder.Configuration["LoadTest:IsActive"]);
    if(isLoad)
        return new ClientStub();
    string apiKey = builder.Configuration["OpenWeather:Key"];
    HttpClient httpClient = new HttpClient();
    return new ClientThreeOne(apiKey, httpClient);
});
builder.Services.AddSingleton<INowWrapper>(_ =>
{
    return new NowWrapper();
});
builder.Services.AddTransient<IRandomWrapper>(_ =>
{
    return new RandomWrapper();
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
