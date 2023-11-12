using AltV.Icarus.Chat.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AltV.Icarus.Chat;

public static class ChatModule
{
    internal static string EventName = "chat:message";
    
    /// <summary>
    /// Registers the chat module
    /// </summary>
    /// <param name="services">A service collection</param>
    /// <param name="eventName">Optional: The event that is used to send/receive chat messages from client-side. By default this is "chat:message".</param>
    /// <returns></returns>
    public static IServiceCollection RegisterChatModule( this IServiceCollection services, string eventName = "chat:message" )
    {
        EventName = eventName;

        services.AddSingleton<IChat, Chat>( );
        
        return services;
    }
    
    /// <summary>
    /// Initializes the chat module to receive basic chat events from clients.
    /// </summary>
    /// <param name="serviceProvider">A service provider</param>
    /// <returns>The service provider</returns>
    /// <exception cref="NullReferenceException">Thrown when the chat module failed to initialize.</exception>
    public static IServiceProvider InitializeChatModule( this IServiceProvider serviceProvider )
    {
        var chatManager = serviceProvider.GetService<IChat>( );

        var logger = serviceProvider.GetService<ILogger<Logger>>( );

        if( chatManager is null || logger is null )
            throw new NullReferenceException( "Failed to initialize ChatModule" );

        logger.LogInformation( "ChatModule initialized!" );
        return serviceProvider;
    }

    private class Logger { }
}