using System;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.CommandsNext;
using Booker.Services;

namespace Booker
{
    public class BookerBot
    {
        //! Make sure to implement a shutdown routine, or else the bot will timeout and crash.
        // TODO: Redo this to support config files, as well as more complex features.
        public async Task RunAsync()
        {

            #region INITIALIZE

            var configUtil = new ConfigService();
            var config = configUtil.GetConfig();

            //* Here we create a initialize DiscordConfiguration into a variable aptly named DiscordConfig.
            DiscordConfiguration DiscordConfig = new()
            {
                Token = config.Token,
                TokenType = TokenType.Bot

            };

            //* Create a new DiscordShardedClient, so we can both connect to the bot account, and not have to worry about manually writing any sharding code.
            var bot = new DiscordShardedClient(DiscordConfig);

            //* Setup CommandsNext to also work with shards.
            await bot.UseCommandsNextAsync(new CommandsNextConfiguration()
            {
                EnableMentionPrefix = true,
                StringPrefixes = new[] { config.DefaultPrefix }
            });

            await bot.StartAsync();
            await Task.Delay(-1);

            #endregion
        }
    }
}