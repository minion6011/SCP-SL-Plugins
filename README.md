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

<br>

> [!IMPORTANT]  
> I file `.dll` che sono situati all'interno delle cartelle dei plugin sono i plugin compilati
