using System.Collections.Generic;

namespace PetPlugin;

public class Config
{
    public Dictionary<string, List<string>> PetLists { get; set; } = new Dictionary<string, List<string>> {
        ["Vip"] = new List<string> { "Ghost", "SCP173", "Mimic", "Toilet", "Amongus"},
        ["Booster"] = new List<string> { "Booster" },
    };
    public Dictionary<string, List<string>> UserList { get; set; } = new Dictionary<string, List<string>>
    {
        ["Vip"] = new List<string> { "1234@steam" },
        ["Booster"] = new List<string> { "1234@steam" },
    };

}