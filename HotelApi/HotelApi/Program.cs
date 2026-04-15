using HotelApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Dodanie obsługi klasycznych kontrolerów
builder.Services.AddControllers();

// Rejestracja naszych serwisów jako Singleton 
builder.Services.AddSingleton<IRoomService, RoomService>();
builder.Services.AddSingleton<IReservationService, ReservationService>();

// Konfiguracja Swaggera
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Konfiguracja potoku żądań HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Zmapowanie ścieżek do naszych kontrolerów
app.MapControllers();

app.Run();