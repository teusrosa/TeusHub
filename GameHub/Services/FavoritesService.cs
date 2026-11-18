using System.Collections.ObjectModel;
using System.Text.Json;
using GameHub.Models;
using Microsoft.Maui.Storage;

namespace GameHub.Services;

/// <summary>Favoritos compartilhados entre os 3 jogos (e entre menu lateral, rodapé e pilha).</summary>
public class FavoritesService
{
    private const string PrefKey = "favorites_v1";

    public ObservableCollection<GameItem> Items { get; } = new();

    public FavoritesService() => Load();

    public bool IsFavorite(GameItem item) => Items.Any(i => i.FavoriteKey == item.FavoriteKey);

    /// <returns>true se o item passou a ser favorito; false se foi removido.</returns>
    public bool Toggle(GameItem item)
    {
        var existing = Items.FirstOrDefault(i => i.FavoriteKey == item.FavoriteKey);
        bool added;
        if (existing is not null)
        {
            Items.Remove(existing);
            added = false;
        }
        else
        {
            Items.Add(new FavoriteItem(item));
            added = true;
        }

        Save();
        return added;
    }

    public void Clear()
    {
        Items.Clear();
        Save();
    }

    private void Save()
    {
        try
        {
            var records = Items.Select(i => i as FavoriteItem ?? new FavoriteItem(i)).Select(f => f.ToRecord()).ToList();
            Preferences.Default.Set(PrefKey, JsonSerializer.Serialize(records));
        }
        catch { /* falha ao gravar não deve derrubar o app */ }
    }

    private void Load()
    {
        try
        {
            var json = Preferences.Default.Get(PrefKey, "");
            if (string.IsNullOrWhiteSpace(json)) return;

            var records = JsonSerializer.Deserialize<List<FavoriteRecord>>(json) ?? new();
            foreach (var r in records)
                Items.Add(new FavoriteItem(r));
        }
        catch { /* dados antigos/inválidos: começa vazio */ }
    }
}
