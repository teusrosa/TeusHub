using System.Collections.ObjectModel;
using System.Text.Json;
using GameHub.Models;
using Microsoft.Maui.Storage;

namespace GameHub.Services;
//aba de favoritos
public class FavoritesService
{
    private const string PrefKey = "favorites_v1";

    public ObservableCollection<ItemJogo> Items { get; } = new();

    public FavoritesService() => Load();

    public bool IsFavorite(ItemJogo item) => Items.Any(i => i.FavoriteKey == item.FavoriteKey);

    public bool Toggle(ItemJogo item)
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
            Items.Add(new ItemFavorito(item));
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
            var records = Items.Select(i => i as ItemFavorito ?? new ItemFavorito(i)).Select(f => f.ToRecord()).ToList();
            Preferences.Default.Set(PrefKey, JsonSerializer.Serialize(records));
        }
        catch { }
    }

    private void Load()
    {
        try
        {
            var json = Preferences.Default.Get(PrefKey, "");
            if (string.IsNullOrWhiteSpace(json)) return;

            var records = JsonSerializer.Deserialize<List<RegistroFavorito>>(json) ?? new();
            foreach (var r in records)
                Items.Add(new ItemFavorito(r));
        }
        catch { }
    }
}
