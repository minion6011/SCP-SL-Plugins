using System.ComponentModel;

namespace DNTAudio;

public class Config
{
    [Description("Settings header, Sarà in CAPS")]
    public string SHeader { get; set; } = "Impostazioni Audio";

    public string CassieSName { get; set; }  = "Muta il C.A.S.S.I.E.";
    public string CassieSHint { get; set; } = "Muta tutti i CASSIE delle Wave e degli altri plugin";
    public int CassieID { get; set; } = 1;

    public string AudioPlayerSName { get; set; } = "Muta gli Audio Custom";
    public string AudioPlayerSHint { get; set; } = "Muta tutti gli audio del plugin AudioPlayer";
    public int AudioPlayerID { get; set; } = 2;

}