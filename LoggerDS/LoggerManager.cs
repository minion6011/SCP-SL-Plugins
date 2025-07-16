using MEC;
using System;
using System.Net.Http;
using System.Text;

namespace LoggerDS
{
    
    public class RequestManager
    {
        public static float CooldownTime { get; private set; } = 0;
        private static readonly HttpClient client = new HttpClient();

        public static void SendRequest(string title, string description, string colorHex) 
        {
            if (Plugin.Singleton.Config.DSWebHookUrl == "none") {
                return;
            }
            float unixTime = (float)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            if (unixTime > CooldownTime)
            {
                SendWebHook(title, description, colorHex);
                CooldownTime = unixTime + Plugin.Singleton.Config.RequestCooldown;
            }
            else
            {
                Timing.CallDelayed(unixTime - CooldownTime, () => {
                    SendWebHook(title, description, colorHex);
                });
                CooldownTime = CooldownTime + Plugin.Singleton.Config.RequestCooldown;
            }
            if (Plugin.Singleton.Config.debug) {
                LabApi.Features.Console.Logger.Info($"Colldown Time: {CooldownTime}");
            }
        }
        private async static void SendWebHook(string title, string description, string colorHex)
        {
            string timestampMsg = $"<t:{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}:D><t:{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}:T> (<t:{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}:R>)";
            int colorInt = int.Parse(colorHex.Replace("#", ""), System.Globalization.NumberStyles.HexNumber);

            string json = $@"
            {{
                ""embeds"": [
                    {{
                        ""title"": ""{EscapeJson(title)}"",
                        ""description"": ""{EscapeJson(timestampMsg + "\n" + description)}"",
                        ""color"": {colorInt}
                    }}
                ]
            }}";

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(Plugin.Singleton.Config.DSWebHookUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();
            if (Plugin.Singleton.Config.debug) { 
                LabApi.Features.Console.Logger.Info($"Json: \n{json}");
                LabApi.Features.Console.Logger.Info($"Response Code: \n{response.StatusCode.ToString()}");
            }
        }

        public static string EscapeJson(string value)
        {
            return value
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\r", "")
                .Replace("\n", "\\n");
        }
    }
}