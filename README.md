# [SCP-575](/SCP575)
Un plugin che aggiunge un SCP ambientale chiamato SCP-575 <br>
Questo SCP spegenerà le luci in una stanza a caso della facility in qui ci sono dei player e danneggerà coloro che non hanno una torcia o lanterna i mano <br>
Dai config si può impostare:
- Ogni quanto può spawnare SCP-575 in secondi (`loopTime`)
- La possibilità di spawn di SCP-575 (`spawnChance`)
- Delay tra il messagio di che avvisa lo spawn e lo spawn di SCP-575 (`spawnDelay`)
- Durata *dell'attacco* di SCP-575 (`duration575`)
- Danni al secondo causati da SCP-575 (`damagePerSecond`)
- Se impostato come vero SCP-575 può spawnare in Surface (`SurfaceEnabled`)
- Messaggio/Ragione della morte causata da SCP-575 (`killMsg575`)
- Messaggio che avvisa lo spawn di SCP-575 (`warnHint575`)
- Durata del messaggio che avvisa lo spawn di SCP-575 (`hintDuration`)
# [Buddy](/Buddy)
Un plugin che aggiunge la possibilità di giocare in coppia con un amico.
<br>
Aggiunge i comandi:

- `.buddy <nome-amico>` Per invitare qualcuno a diventare il proprio Buddy
- `.baccept` Accetta la richiesta di Buddy
- `.bremove` Rimuove il Buddy attuale

Dai config si può impostare:

- Resetta i buddy ogni round (`ResetBuddiesEveryRound`)
- Mostra un messaggio quando un giocatore si connette (`JoinMsg`)
- Invia suggerimenti/hint ai giocatori (`SendHints`)
- Il numero minimo di player per usare il comando (`MinPlayers`)
- Impedisce che Guardia e Scienziato diventino buddy (`DisallowGuardScientistCombo`)
- Forza il ruolo esatto per il buddy (`ForceExactRole`)
- Testi usati nel plugin [Hint e User console] (`ErrMinPlayerMsg`, `ServerJoinMsg`, `ErrNoPlayerMsg`, `ErrNoPlayerFoundMsg`, `RequestAcceptMsg`, `RequestAcceptHint`, `RequestSentMsg`, `RequestSentHint`, `RequestSent`, `NoRequestMsg`, `ErrorMsg`, `SuccessMsg`, `JoinedMsg`, `SuccessUnMsg`)
- Colore dei messaggi normali (`MsgColor`)
- Colore dei messaggi di errore (`ErrorColor`)
- Durata in secondi degli hint a schermo (`HintDuration`)

> [!NOTE]
> Il plugin è la versione di [Buddy](https://github.com/PintTheDragon/Buddy) aggiornata e scritta in LabApi

# [Assistenza Staff](/AssistenzaStaff)
Un plugin che aggiunge il comando `.assistenza` che permette di chiamare lo staff (chiunque abbia accesso alla R.A. verrà pingato)
<br>
Il comando non può essere utilizzato dallo staff
<br>
Dai config si può impostare:

- Durata del broadcast che verrà fatto allo staff (`BroadcastDuration`)

# [SCP-999](/SCP999)
Un plugin che aggiunge SCP-999
<br>
Questo SCP custom utlizza [ProjectMER](https://github.com/Michal78900/ProjectMER) per posizionare sopra il giocatore scelto la schematica di SCP-999
<br>
SCP-999 dispone come abilità:
- Curare i giocatori nel raggio scelto nei config
- Rallentare gli SCP nel raggio scelto nei config
- Alla morte droppa una carta `O5` Gialla
- Prendendo il tasto abilità da l'effetto della rainbow candy, di Invigorated e cura i giocatori nelle vicinanze
<br>
Dai config si può impostare:

- Minimo di Player prima che lui possa spawnare (`MinPlayer`)
- Il raggio in cui SCP-999 usa la sua abilità (`AbilityRadius`)
- Se al suo spawn e alla sua morte viene fatto il messaggio dal C.A.S.S.I.E. (`CassieMsg`)
- Hint che viene inviato dopo che è spawnato (`HintMsg`)
- Durata dell'hint (`HintDuration`)

**Config Abilità Speciale (Tasto Default: `F`)**
- Durata dell'hint del cooldown dell'abilità (`HintCooldownDuration`)
- Collodown per usare l'abilità (`KeyAbilityCooldown`)
- HP curati dall'abilità (`KeyAbilityHp`)
- Raggio dell'abilità (`KeyAbilityRadius`)
- Intesità degli effetti (`KeyAbilityIntesity`)
- Durata degli effetti (`KeyAbilityDuration`)

# [Plugin Pet](/PetPlugin)
Un plugin che aggiunge il comando `.pet` per avere dei pet in gioco per alcuni utenti (impostabili nei config)
- `.pet add <nome-pet>` per spawnare un pet
- `.pet remove` per rimuovere il pet spawnato

Il plugin utlizza [ProjectMER](https://github.com/Michal78900/ProjectMER)

Dai config si può impostare:
- Il nome del gruppo che avrà degli specifici pet `"gruppo1" : ["pet1", "pet2"]` (`PetLists`)
- Gli steam id che possono usare i pet di uno specifico gruppo `"gruppo1": ["1234@steam", "1234@steam"]` (`UserList`)

# [ScpQuit](/ScpQuit)
Un plugin che permette agli SCP di venire sostituiti da un giocatore 
`.scpquit` eseguito dal SCP che vuole essere sostituito
`.scpclaim` eseguito dal giocatore che vuole diventare SCP (lo possono fare più giocatori e ne verrà scelta una a caso)

Dai config si può impostare:
- Il tempo massimo dopo l'inizio del round per diventare SCP in minuti (`MaxTimeReq_Min`)
- Il tempo dopo il quale vengono dati i risultati della richiesta di sostituzione e il tempo del broadcast in cui viene chiesto di usare il comando per sostiutire il giocatore SCP in secondi (`MaxTimeClaim_Sec`)
- Durata del broadcast contenente il risultato della richiesta (`TimeBrodcastResult_Sec`)

# [Torri Custom](/TorriCustom) (REWORK IN PROGRESS)
Un plugin che permette di decorare la torre dei tutorial e aggiungere una torre custom accessibile in surface
<br>
All'interno della torre si trova una telecamera che può essere usata da 079 e un Pedestal con la possibilità di trovare vari oggetti

Il plugin utlizza [ProjectMER](https://github.com/Michal78900/ProjectMER)

**Guida all'installazione**
- Inserire le schematiche nella cartella `Schematics` di ProjectMER
- Inserire la mappa nella cartella `Maps` di ProjectMER
- Modificare i config di ProjectMER e aggiungere `load:MappaTorri` nella sezione `on_round_started`
- Aggiungere il plugin utilizzato per teletrasportare i giocatori

Dai config si può impostare:
- Il raggio di distanza dalla prima porta prima che il giocatore venga teletrasportato (`Door1Radius`)
- Il raggio di distanza dalla seconda porta prima che il giocatore venga teletrasportato (`Door2Radius`)


# [The Spy](/TheSpy)
Un plugin che aggiunge il ruolo della spia NTF e della spia Chaos
Dai config si può impostare:
- Il motivo che verrà dato alla morte del giocatore (`DamageReason`)
- Hint che verrà mostrato al giocatore quando diventerà spia (`SpyHint`)
- Durata dell'hint (`SpyHintDuration`)
- Scudo che verrà dato alla spia allo spawn (`SpyShield`)
- Numero minimo di persone in una wave prima dello spawn della spia (`MinWaveSize`)
- Lista di custom info di eventuali ruoli custom di altri plugin da ignorare come giogatori per il controllo della fine del round; per esempio SCP-999 (`exclued_infos`)

# [Custom Zombie](/CustomZombie)
Un plugin che aggiunge una lista di zombi custom all'interno del gioco
Lista dei ruoli custom:

| Common Names  | Common Abilities                   |
| :------------ | :--------------------------------- |
| Il nano       | Sei più piccolo                    |
| Velocista     | Sei più veloce                     |
| Tank          | Sei più lento ma hai più vita      |
| Kamikaze      | Abilità: Detona il giocare         |
| Il Cagatore   | Abilità: Crea una pozza di Tantrum |
| Il Lanciatore | Abilità: Ottiene una granata       |

| Uncommon Names     | Uncommon Abilities                             |
| :----------------- | :--------------------------------------------- |
| Urlatore           | Abilità: Assorda i nemici vicini               |
| Light Eater        | Abilità: Spegne le luce nella stanza           |
| Battalion’s Backup | Abilità: Cura il 20% degli HP dei 049-2 vicini |

| Rare Names | Rare Abilities                                |
| :--------- | :-------------------------------------------- |
| Supporter  | Abilità: Cura tutti gli SCP del 10% degli HP  |
| Ruttatore  | Abilità: Acceca e rallenta i giocatori vicini |

| Epic Names    | Epic Abilities                                                    |
| :------------ | :---------------------------------------------------------------- |
| Figlio di 106 | Abilità on Hit: 25% di TP nella Pocket Dimension                  |
| Slender       | Abilità: Si tippa da un giocatore casuale si acceca e si rallenta |
| Texiano       | Spawna con una revolver, ottiene munizioni mangiando i corpi      |

| Leggendary Names | Leggendary Abilities                                                          |
| :--------------- | :---------------------------------------------------------------------------- |
| Femboy           | Abilità: Fa droppare gli item in mano ai giocatori vicini e si cura di 100 HP |

Ci sono numerosi config all'interno del plugin

# [Heavy SCP-3114](/SCP3114)
Un plugin che permette di far spawnare SCP-3114 all'interno della cella di SCP-127 (che sarà bloccata) e dopo un tot. di secondi scelto dai config verrà liberato.
<br>

Dai config si può impostare:
- Minimo di player prima che SCP-3114 possa spawnare (`MinPlayer`)
- Hint che verrà dato a SCP-3114 allo spawn (`SCP3114Hint`)
- Durata dell'hint che sarà dato allo spawn (`HintDuration`)
- Tempo dopo il quale la porta della cella di SCP-127 (dove spawna SCP-3114) verrà aperta (`DoorOpenTime`)

# [Logger Discord](/LoggerDS)
Un plugin che permette di avere i log di ciò che accade in game all'interno di una chat di discord utilizzando un discord WebHook.
<br>

Dai config si può impostare:
<br>
- Se inviare all'interno della console log di debug come Cooldown delle richieste, status code delle richieste ecc... (`debug`)
- Url del WebHook di Discord che verrà utilizzato per mandare i log (`DSWebHookUrl`)
- Cooldown che bisogna aspettare per ogni richiesta prima di essere inviata (`RequestCooldown`)
- Colore rosso in Hex degli embed (`ColorRed`)
- Colore verde in Hex degli embed (`ColorGreen`)
- Colore giallo in Hex degli embed (`ColorYellow`)
- Colore blue in Hex degli embed (`ColorBlue`)

> [!IMPORTANT]  
> I file `.dll` che sono situati all'interno delle cartelle dei plugin sono i plugin compilati
