using AltV.Net;
using AltV.Net.Elements.Entities;

namespace AltV.Icarus.Chat;

public static class ChatExtensions
{
    public static void SendChatMessage( this IPlayer player, string message )
    {
        player.Emit( ChatModule.EventName, null, message );
    }
    
    public static void SendChatMessageToAll( this IPlayer sender, string message )
    {
        Alt.EmitAllClients( ChatModule.EventName, sender.Name, message );
    }
    
    public static void SendChatMessageToAll( string message )
    {
        Alt.EmitAllClients( ChatModule.EventName, null, message );
    }
}