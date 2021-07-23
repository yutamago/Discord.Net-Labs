using Discord.SlashCommands.Builders;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Discord.SlashCommands
{
    /// <summary>
    /// Contains the information of a Slash command module
    /// </summary>
    public class SlashModuleInfo
    {
        /// <summary>
        /// Command service this module belongs to
        /// </summary>
        public SlashCommandService CommandService { get; }
        public string Name { get; }
        /// <summary>
        /// Get the name of this module that will be displayed on Discord
        /// </summary>
        /// <remarks>
        /// This value may be missing if the commands are registered as standalone
        /// </remarks>
        public string SlashGroupName { get; }
        public bool IsSlashGroup => !string.IsNullOrEmpty(SlashGroupName);
        /// <summary>
        /// Description of this module
        /// </summary>
        /// <remarks>
        /// This value may be missing if the commands are registered as standalone
        /// </remarks>
        public string Description { get; }
        /// <summary>
        /// Check if the Application Command for this module can be executed by default
        /// </summary>
        public bool DefaultPermission { get; }
        public IReadOnlyList<SlashModuleInfo> SubModules { get; }
        /// <summary>
        /// Get the information list of the Commands that belong to this module
        /// </summary>
        public IReadOnlyList<SlashCommandInfo> Commands { get; }
        /// <summary>
        /// Get the information list of the Interactions that belong to this module
        /// </summary>
        public IReadOnlyCollection<SlashInteractionInfo> Interactions { get; }
        public SlashModuleInfo Parent { get; }
        public bool IsSubModule => Parent != null;
        /// <summary>
        /// Get a list of the attributes of this module
        /// </summary>
        public IReadOnlyList<Attribute> Attributes { get; }

        internal SlashModuleInfo (SlashModuleBuilder builder, SlashCommandService commandService = null,  SlashModuleInfo parent = null)
        {
            CommandService = commandService ?? builder.CommandService;

            Name = builder.Name;
            SlashGroupName = builder.SlashGroupName;
            Description = builder.Description;
            Parent = parent;
            DefaultPermission = builder.DefaultPermission;
            Commands = BuildCommands(builder).ToImmutableArray();
            Interactions = BuildInteractions(builder).ToImmutableArray();
            SubModules = BuildSubModules(builder).ToImmutableArray();;
            Attributes = BuildAttributes(builder).ToImmutableArray();

            if (IsSlashGroup)
            {
                Preconditions.SlashCommandName(SlashGroupName, nameof(SlashGroupName));
                Preconditions.SlashCommandDescription(Description, nameof(Description));
            }
        }

        private IEnumerable<SlashModuleInfo> BuildSubModules(SlashModuleBuilder builder, SlashCommandService commandService = null)
        {
            var result = new List<SlashModuleInfo>();

            foreach (Builders.SlashModuleBuilder moduleBuilder in builder.SubModules)
                result.Add(moduleBuilder.Build(commandService, this));

            return result;
        }

        private IEnumerable<SlashCommandInfo> BuildCommands (SlashModuleBuilder builder)
        {
            var result = new List<SlashCommandInfo>();

            foreach (Builders.SlashCommandBuilder commandBuilder in builder.Commands)
                result.Add(commandBuilder.Build(this, CommandService));

            return result;
        }

        private IEnumerable<SlashInteractionInfo> BuildInteractions(SlashModuleBuilder builder)
        {
            var result = new List<SlashInteractionInfo>();

            foreach (var interactionBuilder in builder.Interactions)
                result.Add(interactionBuilder.Build(this, CommandService));

            return result;
        }

        private IEnumerable<Attribute> BuildAttributes (SlashModuleBuilder builder)
        {
            var result = new List<Attribute>();
            var currentParent = builder;

            while (currentParent != null)
            {
                result.AddRange(currentParent.Attributes);
                currentParent = currentParent.Parent;
            }

            return result;
        }
    }
}
