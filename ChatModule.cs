using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AltV.Icarus.Chat;

public static class ChatModule
{
    internal static string EventName = "chat:message";
    
    public static IServiceCollection RegisterChatModule( this IServiceCollection services, string eventName = "chat:message" )
    {
        EventName = eventName;

        services.AddSingleton<IChat, Chat>( );
        
        return services;
    }
    
    public static IServiceProvider InitializeChatModule( this IServiceProvider serviceProvider )
    {
        var chatManager = serviceProvider.GetService<IChat>( );

        var logger = serviceProvider.GetService<ILogger<Logger>>( );

        if( chatManager is null || logger is null )
            throw new NullReferenceException( "Failed to initialize Chat" );

        logger.LogInformation( "ChatModule initialized!" );
        return serviceProvider;
    }

    private class Logger { }
}