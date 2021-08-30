using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord
{
    /// <summary>
    ///     Represents an interface used to specify classes that they are a vaild dataype of a <see cref="IDiscordInteraction"/> class.
    /// </summary>
    public interface IDiscordInteractionData : IEntity<ulong>
    {
        /// <summary>
        ///     Gets the name of the invoked command if this interaction data is for an application command; otherwise <see langword="null"/>.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets the custom id of the message component if this interaction data is for a message component; otherwise <see langword="null"/>
        /// </summary>
        string CustomId { get; }

        /// <summary>
        ///     Gets the type of the invoked command if this interaction data is for an application command.
        /// </summary>
        ApplicationCommandType Type { get; }
    }
}
