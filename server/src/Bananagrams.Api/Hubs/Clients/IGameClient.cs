using Bananagrams.Api.ViewModels.GameUserGameAnagrams;

namespace Bananagrams.Api.Hubs.Clients;

public interface IGameClient
{
    Task SendUpdate(GameUserGameAnagramResponseViewModel GameUserGameAnagramResponse);
}