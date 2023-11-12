using AltV.Net.Elements.Entities;

namespace AltV.Icarus.Chat;

public delegate void ChatMessageDelegate( IPlayer player, string message );
public delegate Task ChatMessageAsyncDelegate( IPlayer player, string message );

public interface IChatManager
{
    event ChatMessageDelegate? OnChatMessage;
    event ChatMessageAsyncDelegate? OnChatMessageAsync;

    void SendMessageToPlayer( IPlayer player, string message );
    void SendMessageToAll( IPlayer sender, string message );
    void SendMessageToAll( string message );
}