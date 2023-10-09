using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegistrationModule.Interfaces;
using RegistrationModule.Services;

namespace RegistrationModule;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();


        builder.Services.AddTransient<IAuthService, AuthService>();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif


		return builder.Build();
	}
}
