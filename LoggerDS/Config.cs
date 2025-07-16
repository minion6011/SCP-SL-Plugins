using System.ComponentModel;

namespace LoggerDS;

public class Config
{
    public bool debug { get; set; } = false;

    [Description("Impostare 'none' se non si ha un DS WebHook")]
    public string DSWebHookUrl { get; set; } = "none";

    [Description("WebHook Setting")]
    public float RequestCooldown { get; set; } = 1;

    [Description("WebHook Colors")]
    public string ColorRed { get; set; } = "#fc0303";
    public string ColorGreen { get; set; } = "#1ed64f";
    public string ColorYellow { get; set; } = "#f5bb0c";
    public string ColorBlue { get; set; } = "#186cf2";
}