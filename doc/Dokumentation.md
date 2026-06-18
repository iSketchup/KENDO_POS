# Dokumentation POS-Projekt
**Projektgruppe:** Daniel Walser, Luis Kaufmann, Luis Birnbaumer

  

**Klasse:** 3AHIF

  

**Jahr:** 2025/26

  
  

- Projektitel: Kendo (Shader Renderer)

- Projetkidee: Programm um Shader zu rendern via Code Editor.

  

Betreuer: Lukas Diem, Christof Bauer, David Bechtold

## 1. Inhaltsverzeichnis
- [Kurzbeschreibung](#kurzbeschreibung)
- [Systemanforderungen (Must-Haves / Nice-Haves)](#must-haves)
- [2. Projektzeitplan](#2-projektzeitplan)
  - [2.1 Luis Kaufmann](#luis-kaufmann)
  - [2.2 Luis Birnbaumer](#luis-birnbaumer)
  - [2.3 Daniel Walser](#daniel-walser)
- [3. Lastenheft](#3-lastenheft)
  - [3.1 Kurzbeschreibung](#31-kurzbeschreibung)
  - [3.2 Skizzen](#32-skizzen)
- [4. Pflichtenheft](#4-pflichtenheft)
  - [4.1 UML-Diagramme](#41-uml)
  - [4.2 Umsetzungsdetails](#42-umsetzungsdetail)
  - [4.3 Ergebnisse & Interpretation](#43-ergebnisse-intepretation)
- [5. Anleitung](#5-anleitung)
  - [5.1 Installationsanleitung](#51-installationsanleitung)
  - [5.2 Bedienungsanleitung](#52-bedienungsanleitung)
- [6. Bekannte Bugs & Probleme](#6-bekannte-bugs-probleme)
- [7. Erweiterungsmöglichkeiten](#7-erweiterungsmöglichkeiten)



## 1.1 Kurzbeschreibung:
Programm um Shader zu rendern via Code Editor. Man kann aber auch Bilder hinzufügen und diese bearbeiten.
Zusätzlich kann man auch Shader kommentieren und liken.
Der Besitzer eines Shaders kann Tags zu den Shadern verweisen.


## 1.2 Must-Haves
- Shaders​

- Kommentarsystem​

- Tagsystem​

- Code Editor ​

- Userhandling​


## 1.3 Nice-Haves

- Likes​

- Follows​

- Tags​

- Private Shaders --- Publishing System​

- User Settings​
 
- Per profile view ​

- Guest mode


## 1.4 Very-unlikely-to-have

- shader profile pics
- user profile pics



## 2. Projektzeitplan

### Luis Kaufmann
| Datum | Commit-Nachricht | Bearbeiter |
| :--- | :--- | :--- |
| 24.05.2026 | added a button for submitting the current code | Kaufmann |
| 24.05.2026 | hot keys | Kaufmann |
| 24.05.2026 | Shaders now reload on button press | Kaufmann |
| 24.05.2026 | Merge remote-tracking branch 'origin/main' | Kaufmann |
| 25.05.2026 | made Texture work | Kaufmann |
| 25.05.2026 | added a sample Texture | Kaufmann |
| 25.05.2026 | tuff ass minion | Kaufmann |
| 25.05.2026 | added sampling from multiple textures | Kaufmann |
| 25.05.2026 | jiggle physics | Kaufmann |
| 27.05.2026 | wip refacctoring | Kaufmann |
| 27.05.2026 | fix for comments | Kaufmann |
| 27.05.2026 | clean mvvm structure + hot reloads | Kaufmann |
| 28.05.2026 | added a proper TextEditor -> Avalonia Edit with highlighting | Kaufmann |
| 28.05.2026 | added Assets | Kaufmann |
| 28.05.2026 | Renamed for unified Syntax | Kaufmann |
| 28.05.2026 | Merge remote-tracking branch 'origin/main' | Kaufmann |
| 28.05.2026 | restyling | Kaufmann |
| 31.05.2026 | Merge remote-tracking branch 'origin/main' | Kaufmann |
| 31.05.2026 | changed styling to be system dependend | Kaufmann |
| 31.05.2026 | implemented App Context | Kaufmann |
| 31.05.2026 | init Texture dropables | Kaufmann |
| 31.05.2026 | added a shaderRepository + a quick test | Kaufmann |
| 31.05.2026 | init FrontPage | Kaufmann |
| 31.05.2026 | restructuring to enable an ez front and shader page switch | Kaufmann |
| 31.05.2026 | added a fake repo | Kaufmann |
| 01.06.2026 | shaders now are ac pulled from the repo | Kaufmann |
| 01.06.2026 | added a logger | Kaufmann |
| 01.06.2026 | UI Update for switching fake load status | Kaufmann |
| 01.06.2026 | added a switch logic for fake laod status | Kaufmann |
| 01.06.2026 | Log added | Kaufmann |
| 01.06.2026 | Merge remote-tracking branch 'origin/main' | Kaufmann |
| 01.06.2026 | failsafed for repos status switching if uri isnt reached | Kaufmann |
| 01.06.2026 | logging | Kaufmann |
| 03.06.2026 | init user view | Kaufmann |
| 03.06.2026 | Context is now loaded out of main | Kaufmann |
| 03.06.2026 | Merge remote-tracking branch 'origin/main' | Kaufmann |
| 03.06.2026 | bugfix it now shows | Kaufmann |
| 03.06.2026 | bugfix | Kaufmann |
| 03.06.2026 | bugfix when reloading fake | Kaufmann |
| 03.06.2026 | Added a Main Menu | Kaufmann |
| 03.06.2026 | cleanup | Kaufmann |
| 03.06.2026 | wip | Kaufmann |
| 03.06.2026 | context updating | Kaufmann |
| 03.06.2026 | wip | Kaufmann |
| 03.06.2026 | bugfix | Kaufmann |
| 05.06.2026 | Working Navigation from everywhere | Kaufmann |
| 05.06.2026 | multi-binding for shader on the FrontPage | Kaufmann |
| 05.06.2026 | Scrollable main page | Kaufmann |
| 05.06.2026 | debug info | Kaufmann |
| 05.06.2026 | wiindows compatibility | Kaufmann |
| 05.06.2026 | scaling is now taken into acount | Kaufmann |
| 06.06.2026 | reworked polling logic | Kaufmann |
| 08.06.2026 | textures are now loaded from the Shaders TesUris List -> they can be loaded from the db | Kaufmann |
| 08.06.2026 | icon added | Kaufmann |
| 09.06.2026 | https implementation | Kaufmann |
| 09.06.2026 | wip | Kaufmann |
| 09.06.2026 | temporary fix ignores https untrustwothynes | Kaufmann |
| 09.06.2026 | wip | Kaufmann |
| 09.06.2026 | naming | Kaufmann |
| 09.06.2026 | adjusted shader structur for jason deseriallization | Kaufmann |
| 09.06.2026 | wow names | Kaufmann |
| 10.06.2026 | File input | Kaufmann |
| 10.06.2026 | wip | Kaufmann |
| 10.06.2026 | Merge remote-tracking branch 'origin/main' | Kaufmann |
| 10.06.2026 | Dropslots now directly coralate with the texture so switchting them switches the texture | Kaufmann |
| 11.06.2026 | refactoring | Kaufmann |
| 11.06.2026 | textures loaded now display their name | Kaufmann |
| 11.06.2026 | bugfixe dropslot naming | Kaufmann |
| 11.06.2026 | Merge remote-tracking branch 'origin/main' | Kaufmann |
| 11.06.2026 | fixed a debug button | Kaufmann |
| 14.06.2026 | holy wip | Kaufmann |
| 14.06.2026 | wip | Kaufmann |
| 14.06.2026 | pics are encoded and saved in the db loading will start soon | Kaufmann |
| 15.06.2026 | made Shaders load correctly from the server | Kaufmann |
| 15.06.2026 | postable shaders hurray | Kaufmann |
| 15.06.2026 | deleted test files | Kaufmann |
| 16.06.2026 | styling | Kaufmann |
| 16.06.2026 | Restyling | Kaufmann |
| 16.06.2026 | fixt a problem with loading fake shaders | Kaufmann |
| 16.06.2026 | added new shader functionality | Kaufmann |
| 16.06.2026 | Merge remote-tracking branch 'origin/main' | Kaufmann |
| 16.06.2026 | Styling | Kaufmann |
| 17.06.2026 | fixt userhandling files not finding .env variables | Kaufmann |
| 17.06.2026 | Going to Front page now refreshes everything | Kaufmann |
| 17.06.2026 | Styling | Kaufmann |
| 17.06.2026 | Merge remote-tracking branch 'origin/main' | Kaufmann |
| 17.06.2026 | wip | Kaufmann |
| 17.06.2026 | Merge remote-tracking branch 'origin/main' | Kaufmann |
| 17.06.2026 | fix god? | Kaufmann |
| 17.06.2026 | revert | Kaufmann |
| 18.06.2026 | cleanup | Kaufmann |
| 18.06.2026 | added a url picker | Kaufmann |
| 18.06.2026 | Styling | Kaufmann |
| 18.06.2026 | Styling | Kaufmann |
| 18.06.2026 | styling | Kaufmann |
| 18.06.2026 | Merge remote-tracking branch 'origin/main' | Kaufmann |


### Luis Birnbaumer
| Datum | Commit-Nachricht | Bearbeiter |
| :--- | :--- | :--- |
| 27.05.2026 | WIP displaying the comments section | Luis BIRNBAUMER |
| 27.05.2026 | Comment section now working | Luis BIRNBAUMER |
| 28.05.2026 | Creation of Comments works | Luis BIRNBAUMER |
| 31.05.2026 | Added tags and updated UI | Luis BIRNBAUMER |
| 01.06.2026 | fixed scrollviewer for taglist | Luis BIRNBAUMER |
| 03.06.2026 | WIP FilterSelection created | Luis BIRNBAUMER |
| 11.06.2026 | Created Like Button and Display in ShaderPage | Luis BIRNBAUMER |
| 13.06.2026 | Tag Delete Finally works | Luis BIRNBAUMER |
| 15.06.2026 | Created a Tagview + viewmodel | Luis BIRNBAUMER |
| 15.06.2026 | added theme | Luis BIRNBAUMER |
| 16.06.2026 | created repositroies for comment, likes, tags and created tagview | Luis BIRNBAUMER |
| 16.06.2026 | fixed crash when pressing add before typing in the textbox | Luis BIRNBAUMER |
| 17.06.2026 | fixed the comment post and added tag post | Luis BIRNBAUMER |
| 17.06.2026 | fixed like amount not updating | Luis BIRNBAUMER |
| 17.06.2026 | deleted "seba" being the base user | Luis BIRNBAUMER |
| 17.06.2026 | Fixed GetShaderByFilters with the ShaderAuthor parameter | Luis BIRNBAUMER |
| 17.06.2026 | Implemented repository for likes | Luis BIRNBAUMER |
| 18.06.2026 | fixed filter view ui | Luis BIRNBAUMER |
| 18.06.2026 | doesnt crash when deleting a tag | Luis BIRNBAUMER |
| 18.06.2026 | Merge remote-tracking branch 'origin/main' | Luis BIRNBAUMER |


### Daniel Walser
| Datum | Commit-Nachricht | Bearbeiter |
| :--- | :--- | :--- |
| 21.05.2026 | Hier sind mal die Klassen für vom UML (Teil 1). | Daniel WALSER |
| 24.05.2026 | Hier ist die API-Klasse. | Daniel WALSER |
| 24.05.2026 | Die User Klasse hat ein Password Attribut. Userhandling: AddUser, ValidateLogin. | Daniel WALSER |
| 24.05.2026 | User Klasse angepasst auf Attribute der DBUser. Für den APIService wurde die Route auf "user/" geändert. Userhandling: Die Attribute wurden hier auch angepasst. | Daniel WALSER |
| 24.05.2026 | Merge branch 'main' of https://github.com/iSketchup/KENDO_POS | Daniel WALSER |
| 24.05.2026 | ... Anpassung | Daniel WALSER |
| 25.05.2026 | Das Post wurde ermöglicht. User Property wird im Json geändert. | Daniel WALSER |
| 25.05.2026 | Merge branch 'main' of https://github.com/iSketchup/KENDO_POS | Daniel WALSER |
| 27.05.2026 | Hashing beim erstellen von Usern ermöglicht. Versuch: Login ermöglichen. | Daniel WALSER |
| 28.05.2026 | ... Nuget für Hash | Daniel WALSER |
| 28.05.2026 | Merge branch 'main' of https://github.com/iSketchup/KENDO_POS | Daniel WALSER |
| 28.05.2026 | Versuch 2.Teil: Login ermöglichen. | Daniel WALSER |
| 31.05.2026 | 3. Teil Login implementierung: user wird beim validieren zurückgegeben. Eine User Instanz wird für das holen von Daten erstellt (POST). | Daniel WALSER |
| 31.05.2026 | 4. Teil: Login sollte jetz möglich sein, vorher hat er nichts vom Server geholt. SQL-Injection sollte nichtmehr möglich sein. | Daniel WALSER |
| 02.06.2026 | Login Update | Daniel WALSER |
| 02.06.2026 | merge problem | Daniel WALSER |
| 03.06.2026 | Login GUI erstellt. Logik für die GUI wurde auch erstellt (es fehlt noch das weiterleiten auf eine Seite nach erfolgreichen Login). | Daniel WALSER |
| 03.06.2026 | Login: es wurde für den appContext der User gesetzt. | Daniel WALSER |
| 05.06.2026 | Login-GUI: Jetzt funktioniert das Login korrekt. | Daniel WALSER |
| 05.06.2026 | Login: Es wird ein boolscher Wert für das Login verwendet. HTTPS bzw. SSL wurde auch hinzugefügt. | Daniel WALSER |
| 07.06.2026 | Jetzt kann man vorerst im Testprogramm einen User löschen bzw. die Userdaten abändern. | Daniel WALSER |
| 08.06.2026 | Neue Methode um User nach dem Namen zu suchen. Neues Register Window. Button zum abmelden wurde erzeugt. Adduser gibt jetzt ein bool aus. Im MainWindow wurde noch auf Änderungen angepasst. | Daniel WALSER |
| 10.06.2026 | Versuch darauf, dass die App sich nicht automatisch schließt. | Daniel WALSER |
| 10.06.2026 | Das löschen von Usern wurde ermöglicht. | Daniel WALSER |
| 10.06.2026 | Fehlerbehandlung bei den diversen Userhandling-Methoden. | Daniel WALSER |
| 11.06.2026 | Bearbeitung der Userdaten wurden ermöglicht. Jetzt kann man das Login nicht mehr via unteren Buttons umgehen. | Daniel WALSER |
| 11.06.2026 | Merge branch 'main' of https://github.com/iSketchup/KENDO_POS | Daniel WALSER |
| 11.06.2026 | Jetzt kann man auch nur den Usernamen ohne Passwort ändern. Das löschen von Usern wurde auch angepasst. | Daniel WALSER |
| 11.06.2026 | Aussehen von Windows angepasst. | Daniel WALSER |
| 15.06.2026 | API-KEY wird jetzt für alle Endpunkte geladen und verwendet als Authentifizierungsmittel. | Daniel WALSER |
| 15.06.2026 | Fehlerbehebung: Es gab einen Fehler beim Registrieren. | Daniel WALSER |
| 16.06.2026 | Kleiner Fix: Temporäre User sind nicht mehr vorhanden. | Daniel WALSER |
| 16.06.2026 | xUnitTest wurde für das Userhandling erstellt. | Daniel WALSER |
| 16.06.2026 | ADmin wurde hinzugefügt. Fenster wurden angepasst auf Admin. User Klasse wurde auf die DB angepasst. Kommen wird noch eine Adminseite. | Daniel WALSER |
| 16.06.2026 | 2. Teil vom Admin: Jetzt gibt es mal das Dashboard fpr den ADmin. | Daniel WALSER |
| 16.06.2026 | Sorry hier ist noch das Adminhandling | Daniel WALSER |
| 16.06.2026 | Hier kommt hoffentlich der finale Teil vom Admin Teil. Änderungen: - Neuer Endpunkt in ApiService.cs - Admin Dashboard wurde finalisiert (hoffentlich) - ToString wurde erstellt (User.cs) - Registrierungswindow und Loginwindow führen jetzt entweder zum Dashboard oder zum Hauptmenü - Unittest angepasst. | Daniel WALSER |
| 16.06.2026 | Bug Fix. | Daniel WALSER |
| 17.06.2026 | anpassung | Daniel WALSER |
| 17.06.2026 | Merge branch 'main' of https://github.com/iSketchup/KENDO_POS | Daniel WALSER |
| 17.06.2026 | Bug fix. | Daniel WALSER |
| 17.06.2026 | Merge branch 'main' of https://github.com/iSketchup/KENDO_POS | Daniel WALSER |
| 17.06.2026 | Man kann als admin via button auf das dashboard gehen. Aber auch nur als admin! | Daniel WALSER |
| 17.06.2026 | Merge branch 'main' of https://github.com/iSketchup/KENDO_POS | Daniel WALSER |
| 17.06.2026 | Jetzt ist es möglich sich mit dem Server zu verbinden, wenn man sich einloggen möchte und einen user anlegen möchte. | Daniel WALSER |
| 17.06.2026 | Bug FIx: Passwort wurde nicht vom Json versendet. Jetzt wird die ID ans appContext vergeben. | Daniel WALSER |
| 17.06.2026 | Dokumentation für unser Projekt | Daniel WALSER |
| 18.06.2026 | Admin kann man im Frontend nicht mehr erstellen. | Daniel WALSER |
| 18.06.2026 | Userdaten werden entfernt nach Abmelden und Löschen. Absenden kann man im Login und Register per Enter-Taste | Daniel WALSER |
| 18.06.2026 | Merge branch 'main' of https://github.com/iSketchup/KENDO_POS | Daniel WALSER |
| 18.06.2026 | Benutzerfreundlichkeit für das Login/Registriern sowie für die Serververbindung ist vorhanden. Jetzt wird auch verhindert, dass falsche Userdaten an den Server verschickt werden. | Daniel WALSER |
| 18.06.2026 | Merge branch 'main' of https://github.com/iSketchup/KENDO_POS | Daniel WALSER |
| 18.06.2026 | UserId sollte jetzt weitergeleitet werden. | Daniel WALSER |
| 18.06.2026 | Dashboard: Admin kann sich nicht selbst löschen. | Daniel WALSER |
| 18.06.2026 | Merge branch 'main' of https://github.com/iSketchup/KENDO_POS | Daniel WALSER |
| 18.06.2026 | Merge | Daniel WALSER |



## 3 Lastenheft
### 3.1 Kurzbeschreibung


### 3.2 Skizzen

# TODO


## 4. Pflichtenheft



### 4.1 UML 

# TODO


### 4.2 Umsetzungsdetail

# TODO

### 4.3 Ergebnisse, Intepretation

Login und Registrieren funktioniert
Verbindung zum Server funktioniert
Löschen und Bearbeiten von Usern geht
Kommentare und Likes sowie Tags gehen auch
Shader kann man veröffentlichen und erstellen
Bilder können den Shader hinzugefügt werden

## 5. Anleitung
### 5.1 Installationsanleitung

1. Requirements auf der Server-Seite installieren.

2. Testdatenbank anlegen, wenn vorhanden (DB wird sowieso durch den Server erzeugt.)

3. Server starten

4. Auf den Bin Folder drücken

5. As.exe ausführen

### 5.2 Bedienungsanleitung

1. Verbindet man sich mit dem Server per IP-Adresse und Port des Servers.

2. Loggt man sich ein oder man Registriert als User.
    - Registrieren erreicht man durch **you dont have an account?**

3. Beim Button mit dem Account Name (User Name) kann man folgendes auswählen:
    - Löschen eines Users
    - Abmelden 
    - Bearbeiten eines Users
    - Dashboard (geht aber nur als Admin)

4. per Frontpage kommt man ins Hauptmenü

5. Per New Shader kann man einen eigenen Shader erstellen

6. Im Hauptmenü kann man auf die shader klicken und bearbeiten

7. Bei den Shadern kann man folgendes machen:
    - Herz: liken
    - auf dem Kommentarfeld kommentare schreiben
    - neben dem Add button kann man Tags reinschreiben und hinzufügen
    - Beim Plus namens **Texture0** kann man ein eigenes Bild hinzufügen
    - mit save kann man den Shader speichern

8. Im Hauptmenü kann man noch weiteres machen:
    - Nach likes filtern 
    - Nach dem Usernamen filtern
    - Nach dem Shadernamen filtern

# TODO


## 6 Bekannte Bugs, Probleme

  
  # TODO


## 7 Erweiterungsmöglichkeiten

  
# TODO

Repo: https://github.com/iSketchup/AS.git