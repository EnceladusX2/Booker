using System;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Serilog;
using Booker.Services;

namespace Booker
{
	public class BookerBot
	{
		//? Gonna keep it real, this is a slight mess.
		public readonly EventId BotEventId = new EventId(10, "Booker");
		public DiscordShardedClient Bot { get; set; }
		public IReadOnlyDictionary<int, CommandsNextExtension> Commands { get; set; }

		public async Task RunAsync()
		{

			#region LOGGER_INITIALIZE

			//? Maybe move this into it's own service?
			Log.Logger = new LoggerConfiguration()
				.WriteTo.Console()
				.CreateLogger();

			var LogFactory = new LoggerFactory().AddSerilog();

			#endregion

			#region INITIALIZE

			var configUtil = new ConfigService();
			var config = configUtil.GetConfig();

			//* Here we create a initialize DiscordConfiguration into a variable aptly named DiscordConfig.
			DiscordConfiguration DiscordConfig = new()
			{
				Token = config.Token,
				TokenType = TokenType.Bot,
				LoggerFactory = LogFactory
			};

			//* Create a new DiscordShardedClient, so we can both connect to the bot account, and not have to worry about manually writing any sharding code.
			Bot = new DiscordShardedClient(DiscordConfig);

			this.Bot.Ready += this.Bot_Ready;
			this.Bot.GuildAvailable += this.Bot_GuildAvailable;
			this.Bot.ClientErrored += this.Bot_ClientError;

			//* Setup CommandsNext to also work with shards.
			Commands = await Bot.UseCommandsNextAsync(new CommandsNextConfiguration()
			{
				EnableMentionPrefix = true,
				StringPrefixes = new[] { config.DefaultPrefix },
				EnableDms = false,
				EnableDefaultHelp = true
			});

			Commands.RegisterCommands(Assembly.GetExecutingAssembly());

			await Bot.StartAsync();
			await Task.Delay(-1);

			#endregion
		}

		#region BOT_SHUTDOWN

		//! Implement the rest of this function.
		public async Task StopAsync()
		{
			await this.Bot.StopAsync();
		}

		#endregion

		#region EVENT_HANDLING

		private Task Bot_Ready(DiscordClient sender, ReadyEventArgs e)
		{
			sender.Logger.LogInformation(BotEventId, "Bot is ready to process events.");

			return Task.CompletedTask;
		}

		private Task Bot_GuildAvailable(DiscordClient sender, GuildCreateEventArgs e)
		{
			sender.Logger.LogInformation(BotEventId, $"Guild available: {e.Guild.Name}");

			return Task.CompletedTask;
		}

		private Task Bot_ClientError(DiscordClient sender, ClientErrorEventArgs e)
		{
			sender.Logger.LogError(BotEventId, e.Exception, "Exception occured");

			return Task.CompletedTask;
		}

		#endregion
	}
}