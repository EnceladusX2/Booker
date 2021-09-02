using System;
using System.Threading;

namespace Booker
{
	class Program
	{
		//* Entry point of the bot.
		static void Main(string[] args)
		{
			var Bot = new BookerBot();
			Bot.RunAsync().GetAwaiter().GetResult();
		}
	}
}
