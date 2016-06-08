# Tecniche di tracking markerless per sistemi di manutenzione ed assemblaggio in Realtà Aumentata - tesi magistrale in Informatica

Presto sarà aggiunto anche il codice del progetto

______________________________________________________

In questo lavoro di tesi è stato implementato un sistema di realtà aumentata utilizzabile per guidare l’utente nell’assemblaggio delle componenti di una scheda madre per computer. La soluzione proposta, utilizzabile in modalità hand held display oppure attraverso un HMD quale il Google Cardboard, sfrutta la videocamera dello smartphone come periferica di acquisizione; le potenzialità di calcolo del dispositivo sono, invece, utilizzate per effettuare il tracking della scena. 

Il rinoscimento delle diverse componenti vviene con tecniche di tracciamento che non utilizzano marker (sistemi marker-less) capaci di sfruttare le caratteristiche naturali degli oggetti come features. Dopo aver testato diverse alternative software commerciali e non, si è deciso di implementare i prototipi AR realizzati con il framework Vuforia; in seguito ad un'attenta valutazione, si è scelto di utilizzare, per lo stesso target, innumerevoli "scansioni" dello stesso, da più angolazioni e punti di vista. Gli esperimenti condotti mostrano come la soluzione implementata sia più efficiente rispetto a quella che utilizza una singola "scansione": i tempi di tracciamento del bersaglio sono più che raddoppiati e le aumentazioni mostrate risultano, anche in condizioni sfavorevoli, sempre perfettamente allineate rispetto alla scena osservata. 

I sistemi AR realizzati sono stati confrontati con una soluzione di manutenzione tradizionale, un manuale cartaceo. Dai test condotti su un campione di 18 soggetti è emerso come i risultati misurati per il sistema AR hend held display siano migliori rispetto a quelli calcolati per le soluzioni HMD AR e il manuale cartaceo, sia in termini di errori commessi durante la procedura di manutenzione, sia per quanto riguarda i tempi di completamento dei task proposti. Dai risultati del NASA-TLX risulta, inoltre, come il carico di lavoro soggettivo percepito dagli utenti che indossano un HMD sia maggiore.  
