using ConversationalWeed.Game.Abstractions;
using ConversationalWeed.Game.Pictures.Abstractions;
using ConversationalWeed.Models;
using ConversationalWeed.Resources;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConversationalWeed.Services.CommandModules
{
    [Name("Game")]
    public class GameModule : ModuleBase<SocketCommandContext>
    {
        private static readonly Color GREEN = new Color(34, 139, 34);

        private static readonly Color NORMAL_TIER = new Color(220, 220, 220);
        private static readonly Color COOL_TIER = new Color(100, 150, 225);
        private static readonly Color EPIC_TIER = new Color(210, 44, 230);
        private static readonly Color LEGENDARY_TIER = new Color(255, 174, 57);

        [Group("Play"), Name("Play"), Alias("p")]
        [RequireContext(ContextType.Guild)]
        public class Play : Weed
        {
            public Play(IWeedHub gameHub,
                IImageGenerator imageGenerator,
                DiscordSocketClient discord,
                CommandService service)
                : base(gameHub, imageGenerator, discord, service)
            {
            }

            [Command("oneplant"), Alias("1p")]
            [Summary("Para jugar una carta de oneplant de tu mano")]
            public Task PlayOnePlant(SocketGuildUser targetPlayer, int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Weed1,
                    TargetPlayerId = targetPlayer.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("oneplant"), Alias("1p")]
            [Summary("Para jugar una carta de oneplant de tu mano")]
            public Task PlayOnePlant(int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Weed1,
                    TargetPlayerId = Context.User.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("twoplant"), Alias("2p")]
            [Summary("Para jugar una carta de twoplant de tu mano")]
            public Task PlayTwoPlant(SocketGuildUser targetPlayer, int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Weed2,
                    TargetPlayerId = targetPlayer.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("twoplant"), Alias("2p")]
            [Summary("Para jugar una carta de twoplant de tu mano")]
            public Task PlayTwoPlant(int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Weed2,
                    TargetPlayerId = Context.User.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("threeplant"), Alias("3p")]
            [Summary("Para jugar una carta de threeplant de tu mano")]
            public Task PlayThreePlant(SocketGuildUser targetPlayer, int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Weed3,
                    TargetPlayerId = targetPlayer.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("threeplant"), Alias("3p")]
            [Summary("Para jugar una carta de threeplant de tu mano")]
            public Task PlayThreePlant(int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Weed3,
                    TargetPlayerId = Context.User.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("fourplant"), Alias("4p")]
            [Summary("Para jugar una carta de threeplant de tu mano")]
            public Task PlayFourPlant(SocketGuildUser targetPlayer, int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Weed4,
                    TargetPlayerId = targetPlayer.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("fourplant"), Alias("4p")]
            [Summary("Para jugar una carta de fourplant de tu mano")]
            public Task PlayFourPlant(int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Weed4,
                    TargetPlayerId = Context.User.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("sixplant"), Alias("6p")]
            [Summary("Para jugar una carta de sixplant de tu mano")]
            public Task PlaySixPlant(SocketGuildUser targetPlayer, int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Weed6,
                    TargetPlayerId = targetPlayer.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("sixplant"), Alias("6p")]
            [Summary("Para jugar una carta de sixplant de tu mano")]
            public Task PlaySixPlant(int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Weed6,
                    TargetPlayerId = Context.User.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("weedkiller"), Alias("wk")]
            [Summary("Para jugar una carta de weedkiller de tu mano")]
            public Task PlayWeedKiller(SocketGuildUser targetPlayer, int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.WeedKiller,
                    TargetPlayerId = targetPlayer.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("weedkiller"), Alias("wk")]
            [Summary("Para jugar una carta de weedkiller de tu mano")]
            public Task PlayWeedKiller(int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.WeedKiller,
                    TargetPlayerId = Context.User.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("dandelion"), Alias("dd")]
            [Summary("Para jugar una carta de dandelion de tu mano")]
            public Task PlayDandelion(SocketGuildUser targetPlayer, int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Dandileon,
                    TargetPlayerId = targetPlayer.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("dandelion"), Alias("dd")]
            [Summary("Para jugar una carta de dandelion de tu mano")]
            public Task PlayDandelion(int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Dandileon,
                    TargetPlayerId = Context.User.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("dog"), Alias("d")]
            [Summary("Para jugar una carta de dog de tu mano")]
            public Task PlayDog(SocketGuildUser targetPlayer, int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Dog,
                    TargetPlayerId = targetPlayer.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("dog"), Alias("d")]
            [Summary("Para jugar una carta de dog de tu mano")]
            public Task PlayDog(int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Dog,
                    TargetPlayerId = Context.User.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("busted"), Alias("b")]
            [Summary("Para jugar una carta de busted de tu mano")]
            public Task PlayBusted(SocketGuildUser targetPlayer, int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Busted,
                    TargetPlayerId = targetPlayer.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("busted"), Alias("b")]
            [Summary("Para jugar una carta de busted de tu mano")]
            public Task PlayBusted(int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Busted,
                    TargetPlayerId = Context.User.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("monzon"), Alias("m")]
            [Summary("Para jugar una carta de monzon de tu mano")]
            public Task PlayMonzon(SocketGuildUser targetPlayer, int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Monzon,
                    TargetPlayerId = targetPlayer.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("monzon"), Alias("m")]
            [Summary("Para jugar una carta de monzon de tu mano")]
            public Task PlayMonzon(int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Monzon,
                    TargetPlayerId = Context.User.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("hippie"), Alias("h")]
            [Summary("Para jugar una carta de hippie de tu mano")]
            public Task PlayHippie(SocketGuildUser targetPlayer, int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Hippie,
                    TargetPlayerId = targetPlayer.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("hippie"), Alias("h")]
            [Summary("Para jugar una carta de hippie de tu mano")]
            public Task PlayHippie(int targetField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Hippie,
                    TargetPlayerId = Context.User.Id,
                    TagetPlayerFieldId = targetField
                };
                return ReplyPlayCard(request);
            }

            [Command("stealer"), Alias("s")]
            [Summary("Para jugar una carta de stealer de tu mano")]
            public Task PlayStealer(SocketGuildUser targetPlayer, int targetField, int playerField)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Stealer,
                    TargetPlayerId = targetPlayer.Id,
                    TagetPlayerFieldId = targetField,
                    BeneficiaryPlayerFieldId = playerField,
                };
                return ReplyPlayCard(request);
            }

            [Command("potzilla"), Alias("pz")]
            [Summary("Para jugar una carta de potzilla de tu mano")]
            public Task PlayPotzilla(SocketGuildUser targetPlayer)
            {
                PlayCardRequest request = new PlayCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = CardType.Potzilla,
                    TargetPlayerId = targetPlayer.Id,
                };
                return ReplyPlayCard(request);
            }

            [Command("discard"), Alias("d")]
            [Summary("Para descartar una carta de tu mano si no puedes realizar ningun movimiento")]
            public Task Discard(string cardName)
            {
                CardType cardType = GetCardType(cardName);
                if(cardType == CardType.Undefined)
                {
                    return SendError("Nombre de carta invalido", new List<string>());
                }
                DiscardCardRequest request = new DiscardCardRequest
                {
                    PlayerId = Context.User.Id,
                    CardType = cardType,
                };
                return ReplyDiscardCard(request);
            }
        }

        [Group("Weed"), Name("Weed"), Alias("w")]
        [RequireContext(ContextType.Guild)]
        public class Weed : ModuleBase
        {
            protected readonly IWeedHub _gameHub;
            protected readonly IImageGenerator _imageGenerator;
            protected readonly DiscordSocketClient _discord;
            protected readonly CommandService _service;

            public Weed(IWeedHub gameHub,
                IImageGenerator imageGenerator,
                DiscordSocketClient discord,
                CommandService service)
            {
                _gameHub = gameHub;
                _imageGenerator = imageGenerator;
                _discord = discord;
                _service = service;
            }
            [Command("help"), Alias("h")]
            [Summary("Para obtener la lista de comandos")]
            public async Task Help(int page = 1)
            {
                string prefix = "!";

                Emoji helpEmoji = new Emoji("🆘");
                string help = $"{helpEmoji} {Literals.HelpTitle}\n\n";
                string basicComands = "Comandos básicos: \n\n";
                string playCommands = "Comandos de juego: \n\n";
                foreach (ModuleInfo module in _service.Modules)
                {
                    string moduleText = string.Empty;
                    foreach (CommandInfo cmd in module.Commands)
                    {
                        PreconditionResult result = await cmd.CheckPreconditionsAsync(Context);
                        if (result.IsSuccess)
                        {
                            Emoji summaryEmoji = new Emoji("📄");
                            Emoji commandEmoji = new Emoji("🛠");
                            string parameters = string.Empty;
                            foreach (ParameterInfo parameter in cmd.Parameters)
                            {
                                parameters += "[" + parameter.Name + "] ";
                            }
                            string title = $"{commandEmoji} {prefix}{module.Group} {cmd.Name} {parameters}\n";
                            string description = $"{summaryEmoji} {cmd.Summary}\n";
                            if (module.Group == "Play")
                            {
                                moduleText += title;
                            }
                            else
                            {
                                moduleText += title + description;
                            }
                        }
                    }
                    if (moduleText != string.Empty)
                    {
                        if (module.Group == "Weed")
                        {
                            basicComands += moduleText;

                        }
                        if (module.Group == "Play")
                        {
                            playCommands += moduleText;

                        }
                    }
                }
                switch (page)
                {
                    case 1:
                        help += basicComands + "\nPágina 1";
                        await ReplyAsync(help);
                        break;
                    case 2:
                        help += playCommands + "\nPágina 2";
                        await ReplyAsync(help);
                        break;
                    default:
                        break;
                }


            }

            [Command("stats"), Alias("st")]
            [Summary("Para obtner las estadísticas del jugador")]
            public Task Stats()
            {
                return ReplyPlayerStats(Context.User.Id, Context.User.Username);
            }

            [Command("stats"), Alias("st")]
            [Summary("Para obtner las estadísticas de un jugador en concreto")]
            public Task Stats(SocketGuildUser targetPlayer)
            {
                return ReplyPlayerStats(targetPlayer.Id, targetPlayer.Username);
            }

            [Command("start"), Alias("s")]
            [Summary("Para solicitar un nuevo juego de weed, los demas jugadores del canal pueden unirse! (Tipos de juego: 'competitive' | 'og') ")]
            public async Task Start(string gameType = "competitive")
            {
                GameType type = GameType.Competitive;
                string textMessage = "Quien quiere juegar una partida competitiva de Weed?";

                switch (gameType)
                {
                    default:
                        await Context.Channel.SendMessageAsync($"No existe ningun tipo de juego llamado {gameType}... Se empezará por defecto una partida competitiva.");
                        break;
                    case "competitive":
                        break;
                    case "og":
                        textMessage = "Quien quiere jugar una partida OG de Weed? (las partidas OG no se computaran en tus stats)";
                        type = GameType.Classic;
                        break;
                }

                var YourEmoji = new Emoji("👌");
                IUserMessage message = await Context.Channel.SendMessageAsync(textMessage);
                await message.AddReactionAsync(YourEmoji);
                Thread.Sleep(1000);

                IUserMessage timeWarning = await Context.Channel.SendMessageAsync("15 segundos para unirse! ⌛️");
                Thread.Sleep(10000);
                await timeWarning.DeleteAsync();
                IUserMessage count5 = await Context.Channel.SendMessageAsync("⏰ 5 ⏰");
                Thread.Sleep(1000);
                await count5.DeleteAsync();
                IUserMessage count4 = await Context.Channel.SendMessageAsync("⏰ 4 ⏰");
                Thread.Sleep(1000);
                await count4.DeleteAsync();
                IUserMessage count3 = await Context.Channel.SendMessageAsync("⏰ 3 ⏰");
                Thread.Sleep(1000);
                await count3.DeleteAsync();
                IUserMessage count2 = await Context.Channel.SendMessageAsync("⏰ 2 ⏰");
                Thread.Sleep(1000);
                await count2.DeleteAsync();
                IUserMessage count1 = await Context.Channel.SendMessageAsync("⏰ 1 ⏰");
                Thread.Sleep(1000);
                await count1.DeleteAsync();
                IAsyncEnumerable<IReadOnlyCollection<IUser>> userReactions = message.GetReactionUsersAsync(YourEmoji, 8);
                IReadOnlyCollection<IUser> users = await userReactions.FirstAsync();

                StartMatchRequest request = new StartMatchRequest
                {
                    Type = type,
                    Players = users.Where(x => !x.IsBot).Select(u => new PlayerRequest
                    {
                        Id = u.Id,
                        Name = u.Username,
                    }).ToList(),
                };
                await ReplyStartGame(request);
            }

            [Command("shop")]
            [Summary("Weed Shop!")]
            public async Task Shop()
            {
                ulong playerId = Context.User.Id;
                string playerName = Context.User.Username;
                PlayerStats playerStats = await _gameHub.GetStats(playerId, playerName);
                IList<CardSkin> skins = await _gameHub.GetPurchasableSkins(playerId);
                await ReplyWeedShop(playerStats, skins);
            }

            [Command("buy")]
            [Summary("Para comprar una skin de la tienda")]
            public async Task Buy(string skinName)
            {
                ulong playerId = Context.User.Id;
                ValidationResult<CardSkin> result = await _gameHub.BuySkin(playerId, skinName);
                if (result.Success)
                {
                    await ReplyAsync("Gracias por tu compra!");
                }
                else
                {
                    await SendError("Compra no realizada", result.InvalidReasons);
                }
            }

            [Command("use")]
            [Summary("Para usar una skin de tu inventario")]
            public async Task Use(string skinName)
            {
                ulong playerId = Context.User.Id;
                string playerName = Context.User.Username;
                ValidationResult<CardSkin> result = await _gameHub.SetCurrentSkin(playerId, playerName, skinName);
                if (result.Success)
                {
                    await ReplyAsync("Ahora estas usando " + result.Result.Name);
                }
                else
                {
                    await SendError("Imposible usar esta skin", result.InvalidReasons);
                }
            }

            [Command("exit")]
            [Summary("Para terminar la partida actual")]
            public Task Exit()
            {
                GameExitRequest request = new GameExitRequest
                {
                    PlayerId = Context.User.Id,
                };
                ValidationResult<MatchResult> result = _gameHub.GameExit(request);
                if (result.Success)
                {
                    ReplyAsync("Partida repentinamiente cerrada por:" + Context.User.Mention);
                    return SendGameBoard(result.Result);
                }
                else
                {
                    return SendError("No se ha podido terminar la partida", result.InvalidReasons);
                }
            }

            #region Private Methods

            protected async Task ReplyPlayCard(PlayCardRequest request)
            {
                ValidationResult<MatchResult> result = await _gameHub.PlayCardAsync(request);
                if (result.Success)
                {
                    await SendCardPlayed(request.PlayerId, request.CardType, result.Result);
                    await SendHandToCurrentPlayer(result.Result);
                    await SendGameBoard(result.Result);
                }
                else
                {
                    await SendError(Literals.PlayCardFailed, result.InvalidReasons);
                }
            }

            protected Task SendCardPlayed(ulong playerId, CardType cardType, MatchResult result)
            {
                SocketUser user = _discord.GetUser(playerId);
                Player currentPlayer = result.Players.First(x => x.Id == playerId);
                string message = "El jugador " + user.Mention + " juega de su mano:";
                MemoryStream cardPlayed = _imageGenerator.GenerateCardPlayed(cardType, currentPlayer.CurrentCardSkin);
                string fileName = $"{user.Username}Play_{DateTime.UtcNow:hh:mm:ss}.png";
                return Context.Channel.SendFileAsync(cardPlayed, fileName, message);
            }

            protected Task SendHandToCurrentPlayer(MatchResult gameResult)
            {
                Player player = gameResult.CurrentPlayer;
                if (player.Hand.Count > 0)
                {
                    return SendPlayerHand(gameResult, player);
                }
                else
                {
                    return Task.CompletedTask;
                }
            }

            protected Task SendPlayerHand(MatchResult gameResult, Player player)
            {
                MemoryStream playerHand = _imageGenerator.GeneratePlayerHand(player);
                SocketUser user = _discord.GetUser(player.Id);
                string messageInfo = Literals.HandInfoMessage;
                if (gameResult.IsCurrentPlayerBrick)
                {
                    messageInfo = Literals.HandBrickedMessage;
                }
                string fileName = $"{player.Name}Hand_Round{gameResult.Round}_{DateTime.UtcNow:hh:mm:ss}.png";
                return user.SendFileAsync(playerHand, fileName, messageInfo);
            }

            protected async Task ReplyDiscardCard(DiscardCardRequest request)
            {
                ValidationResult<MatchResult> result = await _gameHub.DiscardCardAsync(request);
                if (result.Success)
                {
                    await SendHandToCurrentPlayer(result.Result);
                    await SendGameBoard(result.Result);
                }
                else
                {
                    await SendError(Literals.PlayCardFailed, result.InvalidReasons);
                }
            }

            protected async Task ReplyStartGame(StartMatchRequest request)
            {
                ValidationResult<MatchResult> validationResult = await _gameHub.StartMatch(request);
                if (validationResult.Success)
                {
                    var playerIds = validationResult.Result.Players.Select(p => p.Id).ToList();
                    await SendGameStartMessage(playerIds);
                    await SendInitialHand(validationResult.Result);
                    await SendGameBoard(validationResult.Result);
                }
                else
                {
                    await SendError("Vaya... No se ha podido iniciar la partida... ", validationResult.InvalidReasons);
                }
            }

            protected async Task ReplyPlayerStats(ulong playerId, string playerName)
            {
                PlayerStats playerStats = await _gameHub.GetStats(playerId, playerName);
                IList<CardSkin> skins = await _gameHub.GetPlayerSkins(playerId);
                await ReplyPlayerStats(playerStats);
                await ReplyWeedSkinSection(skins, "💎 Inventario 💎", GREEN, true);
            }

            protected async Task ReplyWeedShop(PlayerStats playerStats, IList<CardSkin> skins)
            {
                SocketUser currentPlayerSocketUser = _discord.GetUser(playerStats.PlayerId);
                string title = $"💎 Bienvenido a la weed shop {currentPlayerSocketUser.Mention} ! 💎";
                var builder = new EmbedBuilder()
                {
                    Color = GREEN,
                    Description = title,
                };
                builder.AddField(x =>
                {
                    x.Name = "Usa el comando !weed buy para comprar";
                    x.Value = "💰 Weed coins: $ " + playerStats.WeedCoins; ;
                    x.IsInline = true;
                });
                await ReplyAsync("", false, builder.Build());

                var normalSkins = skins.Where(x => x.Tier == SkinTier.Normal).ToList();
                await ReplyWeedSkinSection(normalSkins, "Skins comunes 🤏🏼", NORMAL_TIER);

                var coolSkins = skins.Where(x => x.Tier == SkinTier.Cool).ToList();
                await ReplyWeedSkinSection(coolSkins, "Skins raras 🖖🏼", COOL_TIER);

                var epicSkins = skins.Where(x => x.Tier == SkinTier.Epic).ToList();
                await ReplyWeedSkinSection(epicSkins, "Skins epicas! 💪🏼", EPIC_TIER);

                var legendarySkins = skins.Where(x => x.Tier == SkinTier.Legendary).ToList();
                await ReplyWeedSkinSection(legendarySkins, "Skins LEGENDARIAS!!! 🙌🏾", LEGENDARY_TIER);
            }

            protected async Task ReplyWeedSkinSection(IList<CardSkin> skins, string title, Color color, bool showTier = false)
            {
                var builder = new EmbedBuilder()
                {
                    Color = color,
                    Description = title,
                };
                foreach(var skin in skins)
                {
                    string value = showTier ? $"{skin.Tier}" : $"$ {skin.Cost}";
                    builder.AddField(x =>
                    {
                        x.Name = skin.Name;
                        x.Value = value;
                        x.IsInline = false;
                    });
                }
                if(skins.Count == 0)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "👌🏼";
                        x.Value = "Ya tienes todas las skins de este tier, de puta madre loco!";
                        x.IsInline = false;
                    });
                }
                await ReplyAsync("", false, builder.Build());
            }

            protected async Task ReplyPlayerStats(PlayerStats playerStats)
            {
                SocketUser currentPlayerSocketUser = _discord.GetUser(playerStats.PlayerId);
                var builder = new EmbedBuilder()
                {
                    Color = GREEN,
                    Description = "📊 Estadisticas de Weed del jugador " + currentPlayerSocketUser.Mention,
                };
                builder.AddField(x =>
                {
                    x.Name = "💰 Weed coins: ";
                    x.Value = playerStats.WeedCoins;
                    x.IsInline = false;
                });
                builder.AddField(x =>
                {
                    x.Name = "📌 Skin actual: ";
                    x.Value = playerStats.CurrentSkin;
                    x.IsInline = false;
                });
                builder.AddField(x =>
                {
                    x.Name = "🏆 Victorias: ";
                    x.Value = playerStats.TotalWins;
                    x.IsInline = true;
                });
                builder.AddField(x =>
                {
                    x.Name = "🧮 Partidas jugadas: ";
                    x.Value = playerStats.TotalMatches;
                    x.IsInline = true;
                });
                builder.AddField(x =>
                {
                    x.Name = "💪🏽 Ratio de victorias: ";
                    x.Value = (playerStats.WinRatio * 100).ToString("N2") + " %";
                    x.IsInline = true;
                });
                builder.AddField(x =>
                {
                    x.Name = "💢 Puntos totales: ";
                    x.Value = $"{playerStats.TotalPoints} ({playerStats.PointsAverage:N2} Avg.) ";
                    x.IsInline = true;
                });
                builder.AddField(x =>
                {
                    x.Name = "🚬 Puntos fumados: ";
                    x.Value = $"{playerStats.TotalSmokedPoints} ({playerStats.SmokedPointsAverage:N2} Avg.) ";
                    x.IsInline = true;
                });
                builder.AddField(x =>
                {
                    x.Name = "🌾 Puntos cosechados: ";
                    x.Value = $"{playerStats.TotalWeedPoints} ({playerStats.WeedPointsAverage:N2} Avg.) ";
                    x.IsInline = true;
                });
                await ReplyAsync("", false, builder.Build());
            }

            protected async Task SendGameBoard(MatchResult matchResult)
            {
                var builder = new EmbedBuilder()
                {
                    Color = GREEN,
                    Description = string.Format(Literals.GameBoardInfoTitle, matchResult.Round),
                };
                SocketUser currentPlayerSocketUser = _discord.GetUser(matchResult.CurrentPlayer.Id);
                IList<Player> players = matchResult.Players.OrderByDescending(p => p.Points).ToList();
                int bestPoints = players.First().Points;
                IList<Player> technicalTiePlayers = players.Where(p => p.Points == bestPoints).ToList();
                if (matchResult.GameOver)
                {
                    List<ulong> technicalTiePlayerIds = technicalTiePlayers.Select(p => p.Id).ToList();
                    IList<SocketUser> discordUsers = GetSocketUsers(technicalTiePlayerIds);
                    if (technicalTiePlayers.Count > 1)
                    {
                        string technicalTieLiteral = Literals.TechnicalTieLiteral;
                        foreach (SocketUser tUser in discordUsers)
                        {
                            technicalTieLiteral += " " + tUser.Mention + " ";
                        }
                        builder.AddField(x =>
                        {
                            x.Name = Literals.GameOverLiteral;
                            x.Value = technicalTieLiteral;
                            x.IsInline = false;
                        });
                    }
                    else
                    {
                        builder.AddField(x =>
                        {
                            x.Name = Literals.GameOverLiteral;
                            x.Value = string.Format(Literals.WinnerLiteral, discordUsers.First().Mention);
                            x.IsInline = false;
                        });
                    }
                }
                else
                {
                    builder.AddField(x =>
                    {
                        x.Name = Literals.CurrentPlayerLabel;
                        x.Value = currentPlayerSocketUser.Mention;
                        x.IsInline = false;
                    });
                    builder.AddField(x =>
                    {
                        x.Name = Literals.RoundsLeftLabel;
                        x.Value = matchResult.RoundsLeft;
                        x.IsInline = false;
                    });
                }

                foreach (Player player in players)
                {
                    builder.AddField(x =>
                    {
                        x.Name = player.Name;
                        x.Value = string.Format(Literals.PointsLabel, player.Points);
                        x.IsInline = false;
                    });
                }

                string fileName = $"Board_Round{matchResult.Round}_{matchResult.CurrentPlayer.Name}Turn_{DateTime.UtcNow:hh:mm:ss}.png";
                MemoryStream board = _imageGenerator.GenerateGameBoard(matchResult.Players);
                await Context.Channel.SendFileAsync(board, fileName, string.Empty, false, builder.Build());
            }

            protected Task SendInitialHand(MatchResult gameResult)
            {
                foreach (Player player in gameResult.Players)
                {
                    if (player.Hand.Count > 0)
                    {
                        SendPlayerHand(gameResult, player);
                    }
                }
                return Task.CompletedTask;
            }

            protected Task SendGameStartMessage(IList<ulong> playerIds)
            {
                var builder = new EmbedBuilder()
                {
                    Color = GREEN,
                    Description = Literals.NewGame,
                };
                IList<SocketUser> socketUsers = GetSocketUsers(playerIds);
                foreach (SocketUser user in socketUsers)
                {
                    builder.AddField(x =>
                    {
                        x.Name = user.Username;
                        x.Value = user.Mention;
                        x.IsInline = true;
                    });
                }
                return ReplyAsync("", false, builder.Build());
            }

            protected Task SendError(string generalError, IList<string> invalidReasons)
            {
                var builder = new EmbedBuilder()
                {
                    Color = GREEN,
                    Description = generalError,
                };
                int i = 1;
                foreach (string invalidReason in invalidReasons)
                {
                    builder.AddField(x =>
                    {
                        x.Name = "❌ Error ";
                        x.Value = invalidReason;
                    });
                    i++;
                }
                return ReplyAsync("", false, builder.Build());
            }

            protected IList<SocketUser> GetSocketUsers(IList<ulong> userIds)
            {
                IList<SocketUser> users = new List<SocketUser>();
                foreach (ulong userId in userIds)
                {
                    SocketUser user = _discord.GetUser(userId);
                    users.Add(user);
                }
                return users;
            }

            protected CardType GetCardType(string cardName)
            {
                CardType cardType = cardName switch
                {
                    "oneplant" => CardType.Weed1,
                    "twoplant" => CardType.Weed2,
                    "threeplant" => CardType.Weed3,
                    "fourplant" => CardType.Weed4,
                    "sixplant" => CardType.Weed6,
                    "dandelion" => CardType.Dandileon,
                    "dog" => CardType.Dog,
                    "hippie" => CardType.Hippie,
                    "monzon" => CardType.Monzon,
                    "potzilla" => CardType.Potzilla,
                    "stealer" => CardType.Stealer,
                    "busted" => CardType.Busted,
                    "weedkiller" => CardType.WeedKiller,
                    _ => CardType.Undefined,
                };
                return cardType;
            }

            #endregion
        }

    }
}
