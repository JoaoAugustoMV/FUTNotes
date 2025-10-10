// See https://aka.ms/new-console-template for more information

using FootNotes.IAM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddDbContext<IAMContext>(options =>
    options.UseNpgsql("Server=localhost;Database=FootNote;Trusted_Connection=True;"));

//var provider = services.BuildServiceProvider();
//var context = provider.GetRequiredService<UserDbContext>();
//context.Database.Migrate();
