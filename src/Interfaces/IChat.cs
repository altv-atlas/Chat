using AltV.Net.Elements.Entities;

namespace AltV.Atlas.Chat.Interfaces;

/// <summary>
/// The chat delegate
/// </summary>
public delegate void ChatMessageDelegate( IPlayer player, string message );

/// <summary>
/// The async chat delegate
/// </summary>
public delegate Task ChatMessageAsyncDelegate( IPlayer player, string message );

/// <summary>
/// Basic interface for chat feature
/// </summary>
public interface IChat
{
    /// <summary>
    /// Chat event, is triggered whenever a new message has been received from any client
    /// </summary>
    event ChatMessageDelegate? OnChatMessage;
    
    /// <summary>
    /// Chat async event, is triggered whenever a new message has been received from any client
    /// </summary>
    event ChatMessageAsyncDelegate? OnChatMessageAsync;
}