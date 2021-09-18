#pragma warning disable CS1591
using Newtonsoft.Json;

namespace Discord.API
{
    internal class Role
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("color")]
        public uint Color { get; set; }
        [JsonProperty("hoist")]
        public bool Hoist { get; set; }
        [JsonProperty("mentionable")]
        public bool Mentionable { get; set; }
        [JsonProperty("position")]
        public int Position { get; set; }
        [JsonProperty("permissions"), Int53]
        public string Permissions { get; set; }
        [JsonProperty("managed")]
        public bool Managed { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("unicode_emoji")]
        public string UnicodeEmoji { get; set; }
        [JsonProperty("tags")]
        public Optional<RoleTags> Tags { get; set; }
    }
}
