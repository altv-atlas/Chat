using AltV.Net.Elements.Entities;

namespace AltV.Icarus.Chat;

public delegate void ChatMessageDelegate( IPlayer player, string message );
public delegate Task ChatMessageAsyncDelegate( IPlayer player, string message );

public interface IChat
{
    event ChatMessageDelegate? OnChatMessage;
    event ChatMessageAsyncDelegate? OnChatMessageAsync;
}