using System.Collections.Generic;

namespace Discord.ApplicationCommands
{
    internal class ApplicationCommandMap<T> where T : class, IExecutableInfo
    {
        private readonly char[] _seperators = { ' ', '\n', '\r', ',' };

        private readonly ApplicationCommandMapNode<T> _root = new ApplicationCommandMapNode<T>(null);

        public IReadOnlyCollection<char> Seperators => _seperators;

        public ApplicationCommandMap (char[] seperators = null)
        {
            if (seperators != null)
                foreach (var seperator in seperators)
                    _seperators[_seperators.Length] = seperator;
        }

        public void AddCommand(T command, bool ignoreGroupNames = false)
        {
            if (ignoreGroupNames)
                AddCommand(command);
            else
                AddCommandToRoot(command);
        }

        public void AddCommand (T command)
        {
            string[] key = ParseCommandName(command);

            _root.AddCommand(key, 0, command);
        }

        public void AddCommandToRoot(T command)
        {
            string[] key = new string[] { command.Name };
            _root.AddCommand(key, 0, command);
        }

        public void RemoveCommand (T command)
        {
            string[] key = ParseCommandName(command);

            _root.RemoveCommand(key, 0);
        }

        public SearchResult<T> GetCommand (string input) =>
            GetCommand(input.Split(_seperators));

        public SearchResult<T> GetCommand (string[] input) =>
            _root.GetCommand(input, 0);

        private string[] ParseCommandName (T command)
        {
            var keywords = new List<string>() { command.Name };

            var currentParent = command.Module;

            while (currentParent != null)
            {
                if(!string.IsNullOrEmpty(currentParent.SlashGroupName))
                    keywords.Add(currentParent.SlashGroupName);
                currentParent = currentParent.Parent;
            }

            keywords.Reverse();

            return keywords.ToArray();
        }
    }
}
