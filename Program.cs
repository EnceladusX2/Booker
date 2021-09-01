using System;

namespace Booker
{
    class Program
    {
        //* Entry point of the bot, as well as where we'll send the needed data for the bot to function.
        static void Main(string[] args)
        {
            var Bot = new BookerBot();
            Bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
