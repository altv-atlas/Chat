using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Args;
using AltV.Net.Elements.Entities;
using Microsoft.Extensions.Logging;

namespace AltV.Icarus.Chat;

internal class ChatManager : IChatManager
{
    private readonly ILogger<ChatManager> _logger;
    public event ChatMessageDelegate? OnChatMessage;
    public event ChatMessageAsyncDelegate? OnChatMessageAsync;

    public ChatManager( ILogger<ChatManager> logger )
    {
        _logger = logger;
        _logger.LogInformation( "Chat Manager initialized!" );
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

        action( player, arg.GetString( ) );
    }

    /// <summary>
    /// Processes an incoming message
    /// </summary>
    /// <param name="player">the player who sent it</param>
    /// <param name="message">the message that the player sent</param>
    private void OnChat( IPlayer player, string message )
    {
        message = message.Trim( );
        
        if( string.IsNullOrEmpty( message ) )
            return;
        
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

    public void SendMessageToPlayer( IPlayer player, string message )
    {
        player.Emit( ChatModule.EventName, message );
    }

    public void SendMessageToAll( IPlayer sender, string message )
    {
        SendMessageToAll( $"{sender.Name}: {message}" );
    }
    
    public void SendMessageToAll( string message )
    {
        Alt.EmitAllClients( ChatModule.EventName, message );
    }
}