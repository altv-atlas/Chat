using AltV.Atlas.Chat.Interfaces;
using AltV.Net;
using AltV.Net.Elements.Args;
using AltV.Net.Elements.Entities;
using Microsoft.Extensions.Logging;

namespace AltV.Atlas.Chat;

internal class Chat : IChat
{
    private readonly ILogger<Chat> _logger;
    public event ChatMessageDelegate? OnChatMessage;
    public event ChatMessageAsyncDelegate? OnChatMessageAsync;

    public Chat( ILogger<Chat> logger )
    {
        _logger = logger;
        _logger.LogDebug( "Chat initialized!" );
        Alt.OnClient<IPlayer, string>( ChatModule.EventName, OnChat, OnChatParser );
    }
    
    /// <summary>
    /// Parses the message before actually processing it. In case of failure it will not be processed in the next step.
    /// </summary>
    /// <param name="player">the player who sent it</param>
    /// <param name="mValueArray">data that was received</param>
    /// <param name="action">the action to trigger processing of the command</param>
    private void OnChatParser( IPlayer player, MValueConst[ ] mValueArray, Action<IPlayer, string> action )
    {
        if( mValueArray.Length != 1 )
            return;

        var arg = mValueArray[ 0 ];

        if( arg.type != MValueConst.Type.String )
            return;

        var message = arg.GetString( ).Trim();
        
        if( string.IsNullOrEmpty( message ) || message.StartsWith( ChatModule.CommandPrefix ) )
            return;
        
        action( player, message );
    }

    /// <summary>
    /// Processes an incoming message
    /// </summary>
    /// <param name="player">the player who sent it</param>
    /// <param name="message">the message that the player sent</param>
    private void OnChat( IPlayer player, string message )
    {
        _logger.LogTrace( "New message from {PlayerName}: {Message}", player.Name, message );
        
        if( OnChatMessageAsync != null )
        {
            foreach( var d in OnChatMessageAsync.GetInvocationList( ).Cast<ChatMessageAsyncDelegate>( ) )
            {
                d.Invoke( player, message ).ConfigureAwait( false );
            }
        }
        
        OnChatMessage?.Invoke( player, message );
    }
}