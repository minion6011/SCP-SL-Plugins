using System.ComponentModel;

namespace Buddy;

public class Config
{
    [Description("Plugin Config")]
    public bool ResetBuddiesEveryRound { get; set; } = true;
    public bool JoinMsg { get; set; } = true;
    public bool SendHints { get; set; } = true;
    public bool DisallowGuardScientistCombo { get; set; } = true;
    public ushort MinPlayers { get; set; } = 10;
    public bool ForceExactRole { get; set; } = false;

    [Description("Messages Text")]
    public string ServerJoinMsg { get; set; } = "Vuoi un Buddy? Scrivi .buddy per chiedere a qualcuno di diventare il tuo Buddy!";

    public string ErrMinPlayerMsg { get; set; } = "Errore: Ci devono essere almeno $min players online per poter usare questo comando";

    public string ErrNoPlayerMsg { get; set; } = "Errore: Specifica un player, esempio: .buddy <nome_player>";

    public string ErrNoPlayerFoundMsg { get; set; } = "Errore: Non è stato possibile trovare un player con quel nome";

    public string RequestAcceptMsg { get; set; } = "$name ha accettato di diventare il tuo Buddy!";
    public string RequestAcceptHint { get; set; } = "$name ha accettato di diventare il tuo Buddy, premi ò e scrivi .bremove per rimuoverlo";

    public string RequestSentMsg { get; set; } = "$name ti ha chiesto di diventare il suo Buddy";
    public string RequestSentHint { get; set; } = "$name ti ha chiesto di diventare il suo Buddy, premi ò e scrivi .baccept per accettare";

    public string RequestSent { get; set; } = "Ho chiesto a $name di diventare il tuo Buddy!";

    public string NoRequestMsg { get; set; } = "Non hai richieste di Buddy in sospeso";

    public string ErrorMsg { get; set; } = "Si è verificato un errore sconosciuto";

    public string SuccessMsg { get; set; } = "Ora sei il Buddy di $name!";
    public string JoinedMsg { get; set; } = "Il tuo Buddy è $name";

    public string SuccessUnMsg { get; set; } = "Non sei più il Buddy di nessuno";

    [Description("Messages Options")]
    public string MsgColor { get; set; } = "yellow";

    public float HintDuration { get; set; } = 5f;
}