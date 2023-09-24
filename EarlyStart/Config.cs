using Exiled.API.Interfaces;
using System.ComponentModel;

namespace EarlyStart
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        [Description("How many seconds that should be waited ")]
        public float Seconds { get; set; } = 30f;
    }
}