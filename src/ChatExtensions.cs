using AltV.Net;
using AltV.Net.Elements.Entities;

namespace AltV.Atlas.Chat;

/// <summary>
/// Contains some extension methods for the chat module
/// </summary>
public static class ChatExtensions
{
    /// <summary>
    /// Send a chat message to the specified player
    /// </summary>
    /// <param name="player">The player to send the message to</param>
    /// <param name="message">The message to send</param>
    public static void SendChatMessage( this IPlayer player, string message )
    {
        player.Emit( ChatModule.EventName, null, message );
    }
    
    /// <summary>
    /// Sends the specified message to all players
    /// </summary>
    /// <param name="sender">The player who sends the message</param>
    /// <param name="message">The message to send</param>
    public static void SendChatMessageToAll( this IPlayer sender, string message )
    {
        Alt.EmitAllClients( ChatModule.EventName, sender.Name, message );
    }
    
    /// <summary>
    /// Sends the specified message to all players
    /// </summary>
    /// <param name="message">The message to send</param>
    public static void SendChatMessageToAll( string message )
    {
        Alt.EmitAllClients( ChatModule.EventName, null, message );
    }
}