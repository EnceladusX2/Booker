using System;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace Booker
{
    public class BookerBot
    {
        // TODO: Redo this to support config files, as well as more complex features.
        public async Task RunAsync()
        {
            //* Create a new DiscordShardedClient, so we can both connect to the bot account, and not have to worry about manually writing any sharding code.
            var bot = new DiscordShardedClient(new DiscordConfiguration
            {
                //! Fix this, the bot token shouldn't be visible in the code.
                Token = "token",
                TokenType = TokenType.Bot
            });

            //* Setup CommandsNext to also work with shards.
            await bot.UseCommandsNextAsync(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "b!" }
            });

            await bot.StartAsync();
            await Task.Delay(-1);
        }
    }
}