[![Clicca per guardare il video](https://img.youtube.com/vi/jz3wdhnOrpw/0.jpg)](https://www.youtube.com/watch?v=jz3wdhnOrpw)


# Calcolatore dell'Imposta sul Reddito e Generatore di Codice Fiscale
Questo programma è un'applicazione console che consente agli utenti di calcolare l'imposta sul reddito in base ai loro dati personali e generare il codice fiscale italiano corrispondente. È stato sviluppato in C# e utilizza la libreria Newtonsoft.Json per la gestione dei dati dei comuni italiani da un file JSON esterno.

# Funzionalità Principali
Inserimento Dati Personali: L'utente può inserire il proprio nome, cognome, data di nascita, sesso e comune di residenza. Vengono effettuate diverse verifiche per garantire l'accuratezza dei dati inseriti.

## Calcolo dell'Imposta: Il programma calcola l'imposta annuale in base al reddito dichiarato, seguendo le aliquote fiscali italiane. L'imposta è mostrata all'utente insieme ai dettagli del calcolo.

## Generazione del Codice Fiscale: Dopo aver confermato i dati personali, il programma genera il codice fiscale italiano dell'utente in base ai dati inseriti, inclusi nome, cognome, data di nascita, sesso e comune di residenza.

## Gestione dei Comuni: Il programma utilizza un elenco di comuni italiani da un file JSON esterno ("codici_comuni.json") per verificare la validità del comune di residenza inserito dall'utente.

## Menu Interattivo: L'utente può scegliere se calcolare una nuova imposta o uscire dal programma dopo aver visualizzato i risultati.

# Istruzioni per l'Utilizzo
Avvia il programma.

Segui le istruzioni per inserire i tuoi dati personali, assicurandoti che siano corretti.

Il programma calcolerà l'imposta sul reddito e mostrerà il risultato.

Dopo aver confermato i dati, verrà generato il tuo codice fiscale italiano.

Puoi scegliere di calcolare una nuova imposta o uscire dal programma.

# Requisiti
È necessario avere installato un ambiente di sviluppo C# (ad esempio, Visual Studio) per eseguire il programma.

Il file JSON "codici_comuni.json" deve essere presente nella stessa directory del programma. Assicurati che il file contenga i dati dei comuni italiani in un formato corretto.

# Esempio di Utilizzo

***** INSERISCI DATI PERSONALI *****
Nome: Mario
Cognome: Rossi
Data di Nascita (gg/mm/aaaa): 15/03/1985
Sesso (M/F): M
Comune di Residenza: Roma

Il tuo codice fiscale è RSSMRA85C15H501L, confermi?: [S]i/ [N]o
S

Reddito Annuale: 35000

***** CALCOLO DELL'IMPOSTA DA VERSARE: *****
Contribuente: Mario Rossi
nato il 15/03/1985
residente a Roma
codice fiscale: RSSMRA85C15H501L
Reddito dichiarato: Euro 35000
IMPOSTA DOVUTA: Euro 8700

Cosa desideri fare ora?:
1. Calcola nuova imposta
2. Esci
Scelta: 2
