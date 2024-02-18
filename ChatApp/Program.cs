using ChatApp.DBM;
using ChatApp.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=SignalRTests}/{action=Index}/{id?}");

//adjust rooms by user's joined rooms
Pool.OnHubAdded += Pool_OnHubAdded;

Pool.AddNewHub("Room1");
Pool.AddNewHub("Room2");

void Pool_OnHubAdded(string hubName)
{
	app.MapHub<RoomHub>(hubName);
}

app.Run();