using System.Threading.Tasks;

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Booker.Modules
{
    public class TestModule : BaseCommandModule
    {
        [Command("test")]
        public async Task TestCommand(CommandContext ctx)
        {
            await ctx.RespondAsync("Test these nuts.");
        }
    }
}