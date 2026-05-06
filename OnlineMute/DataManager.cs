using LabApi.Features.Console;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace OnlineMute
{
    public static class DataStorage
    {
        private static string dictPath = Path.Combine(Plugin.Singleton.MuteDBPath, "mutes.json");
        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public static void Save(Dictionary<string, float> data)
        {
            string json = JsonSerializer.Serialize(data, _options);
            File.WriteAllText(dictPath, json);
        }
        public static Dictionary<string, float> Load()
        {
            if (!File.Exists(dictPath))
                return new Dictionary<string, float>();
            string json = File.ReadAllText(dictPath);
            return JsonSerializer.Deserialize<Dictionary<string, float>>(json);
        }
    }
}
