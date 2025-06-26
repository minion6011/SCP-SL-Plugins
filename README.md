# To Do List ✔️

- [ ] Abilità di SCP-999

<hr>

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
L'SCP custom utlizza [ProjectMER](https://github.com/Michal78900/ProjectMER) per posizionare sopra il giocatore scelto la schematica di SCP-999
<br>
SCP-999 dispone come abilità:
- Curare i giocatori nel raggio scelto nei config
- Rallentare gli SCP nel raggio scelto nei config
- Alla morte droppa una carta `O5` Gialla
<br>
Dai config si può impostare:

- Minimo di Player prima che lui possa spawnare (`min_player`)
- Il raggio in cui SCP-999 usa la sua abilità (`ability_radius`)
- Se al suo spawn e alla sua morte viene fatto il messaggio dal C.A.S.S.I.E. (`cassie_msg`)
- Hint che viene inviato dopo che è spawnato (`hint_msg`)
- Durata dell'hint (`hint_duration`)

# [Plugin Pet](/PetPlugin)
Un plugin che aggiunge il comando `.pet` per avere dei pet in gioco per alcuni utenti (impostabili nei config)
- `.pet add <nome-pet>` per spawnare un pet
- `.pet remove` per rimuovere il pet spawnato

Il plugin utlizza [ProjectMER](https://github.com/Michal78900/ProjectMER)

Dai config si può impostare
- Il nome del gruppo che avrà degli specifici pet `"gruppo1" : ["pet1", "pet2"]` (`PetLists`)
- Gli steam id che possono usare i pet di uno specifico gruppo `"gruppo1": ["1234@steam", "1234@steam"]` (`UserList`)

# [ScpQuit](/ScpQuit)
Un plugin che permette agli SCP di venire sostituiti da un giocatore 
`.scpquit` eseguito dal SCP che vuole essere sostituito
`.scpclaim` eseguito dal giocatore che vuole diventare SCP (lo possono fare più giocatori e ne verrà scelta una a caso)

Dai config si può impostare
- Il tempo massimo dopo l'inizio del round per diventare SCP in minuti (`MaxTimeReq_Min`)
- Il tempo dopo il quale vengono dati i risultati della richiesta di sostituzione e il tempo del broadcast in cui viene chiesto di usare il comando per sostiutire il giocatore SCP in secondi (`MaxTimeClaim_Sec`)
- Durata del broadcast contenente il risultato della richiesta (`TimeBrodcastResult_Sec`)

# [Torri Custom](/TorriCustom)
Un plugin che permette di decoraare la torre dei tutorial e aggiungere una torre custom accessibile in surface
<br>
All'interno della torre si trova una telecamera che può essere usata da 079 e un Pedestal con la possibilità di trovare vari oggetti

Il plugin utlizza [ProjectMER](https://github.com/Michal78900/ProjectMER)

**Guida all'installazione**
- Inserire le schematiche nella cartella `Schematics` di ProjectMER
- Inserire la mappa nella cartella `Maps` di ProjectMER
- Modificare i config di ProjectMER e aggiungere `load:MappaTorri` nella sezione `on_round_started`
- Aggiungere il plugin utilizzato per teletrasportare i giocatori

Dai config si può impostare
- Il raggio di distanza dalla prima porta prima che il giocatore venga teletrasportato (`Door1Radius`)
- Il raggio di distanza dalla seconda porta prima che il giocatore venga teletrasportato (`Door2Radius`)


# [The Spy](/TheSpy)
Un plugin che aggiunge il ruolo della spia NTF e della spia Chaos
Dai config si può impostare
- Il motivo che verrà dato alla morte del giocatore (`DamageReason`)
- Hint che verrà mostrato al giocatore quando diventerà spia (`SpyHint`)
- Durata dell'hint (`SpyHintDuration`)
- Scudo che verrà dato alla spia allo spawn (`SpyShield`)
- Numero minimo di persone in una wave prima dello spawn della spia (`MinWaveSize`)

<br>

> [!IMPORTANT]  
> I file `.dll` che sono situati all'interno delle cartelle dei plugin sono i plugin compilati
