# GameHub (.NET MAUI)

App .NET MAUI com 3 APIs de jogos + 1 API que as relaciona.

| API | Jogo | Uso no app |
|---|---|---|
| GTA V Data Dumps | GTA V | lista de veículos (`vehicles.json`) |
| Fortnite-API | Fortnite | busca de skins/cosméticos |
| CSGO-API (ByMykel) | CS2 | skins de armas (`skins.json`) |
| **RAWG** (API de ligação) | os três | nota, gêneros, plataformas, descrição |

## Como abrir
1. Visual Studio 2022/2026 com a carga de trabalho **Desenvolvimento para dispositivos móveis com .NET (MAUI)**.
2. Abra `GameHub.sln`.
3. Crie uma chave grátis em https://rawg.io/apidocs e cole em `GameHub/Constants.cs` (`AppConfig.RawgApiKey`).
   Sem a chave, tudo funciona, exceto a tela "Sobre o jogo".
4. Escolha **Windows Machine** ou um emulador Android e execute (F5).

> O projeto usa `net10.0`. Se o seu Visual Studio for mais antigo, troque `net10.0` por `net9.0`/`net8.0`
> no `GameHub.csproj` e a versão do pacote `Microsoft.Extensions.Logging.Debug` (9.0.0 / 8.0.0).

## Os 3 modos de navegação
| Modo | Onde está | Como funciona |
|---|---|---|
| **Menu lateral** | `AppShell.xaml` | `FlyoutItem`: Início, Jogos, Sobre + `MenuItem` "Meus favoritos" |
| **Rodapé** | `AppShell.xaml` | `FlyoutItem "Jogos"` com 4 `ShellContent` = abas GTA V, Fortnite, CS2, Favoritos |
| **Pilha** | `ShellNavigationService` | `GoToAsync("itemdetail")` → `GoToAsync("gameinfo")` → `GoToAsync("..")` |

Relação entre eles: Início (menu) → aba do jogo (rodapé) → item → detalhe (pilha) → info RAWG (pilha)
→ volta para a aba do jogo (rodapé) ou para o Início (menu). Os favoritos são compartilhados pelos 3 jogos.

## Telas
Início, GTA V, Fortnite, CS2, Favoritos, Detalhe do item, Info do jogo (RAWG), Sobre.

## POO
- **Herança/abstração:** `GameItem` → `VehicleItem`, `CosmeticItem`, `SkinItem`, `FavoriteItem`;
  `CatalogServiceBase` → `CachedFileCatalogService<T>` → `GtaCatalogService`, `Cs2CatalogService`; `CatalogPage` → `GtaPage`...
- **Polimorfismo:** as telas só conhecem `GameItem`.
- **Interfaces:** `ICatalogService`, `IGameInfoService`, `INavigationService`.
- **Encapsulamento / MVVM:** `BaseViewModel`, `CatalogViewModel` (+ `GtaViewModel`, `FortniteViewModel`, `Cs2ViewModel`).
- **Injeção de dependência:** `MauiProgram.cs`.

## Observações
- `vehicles.json` e `skins.json` são grandes: são baixados uma vez e ficam em cache no aparelho.
- Na lista do CS2 são mostrados no máximo 300 resultados por busca.
- No Windows a barra de abas aparece no topo; no Android, no rodapé.
