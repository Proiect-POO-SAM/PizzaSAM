# Proiect 1 - Gestionarea Comenzilor de Pizza 🍕

## Descriere  
Un sistem software pentru gestionarea comenzilor de pizza al unei pizzerii, dezvoltat în C# (aplicație consolă). Aplicația permite administrarea meniului, gestionarea comenzilor, procesarea plăților și generarea de rapoarte.

---

## Funcționalități  

### Pentru Clienți  
- **Autentificare și înregistrare**: Creare cont nou sau login.  
- **Vizualizare meniu**: Sortimente de pizza disponibile.  
- **Plasare comenzi**: Alegere sortimente, metodă de livrare, calcul preț total (include reduceri pentru clienți fideli și costuri de livrare).  

### Pentru Administratori  
- **Gestionare meniu**:  
  - Adăugare/ștergere/modificare sortimente.  
  - Gestionare ingrediente (vizualizare, modificare preț, adăugare/ștergere).  
- **Vizualizare comenzi**:  
  - Istoric comenzi per client.  
  - Comenzi finalizate într-o anumită zi.  
- **Raportare și statistici**:  
  - Cele mai populare sortimente.  
  - Total venituri într-o perioadă.  

---

## Cerințe Tehnice  

### Nota maximă 8  
- Model OO bine definit, utilizând POO (încapsulare, moștenire, polimorfism).  
- Salvarea stării aplicației în fișiere.  
- Tratarea erorilor (ex: fișiere lipsă, date invalide).  
- Utilizare GitHub pentru versionare și colaborare.  

### Nota maximă 10  
- Izolarea funcționalităților externe (clase wrapper).  
- Gestionare dependențe cu .NET Core GenericHost.  
- Logging cu `ILogger` pentru erori și evenimente.  

### Puncte Bonus  
- Stocare în baza de date SQL.  
- Notificări (email/SMS).  
- Teste unitare pentru clasele principale.  

---

## Obiective  
Crearea unui sistem extensibil, robust și ușor de întreținut, capabil să gestioneze comenzile de pizza ale unei pizzerii într-un mod eficient.

