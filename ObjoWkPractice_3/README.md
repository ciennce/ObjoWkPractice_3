# ObjoWkPractice_3 – Vending Machine

Konsolen-Übung zur Objektorientierung: Ein Getränkeautomat, in den man Artikel einlegen,
Geld einwerfen und Artikel kaufen kann.

## Starten

In Visual Studio `ObjoWkPractice_3` als Startprojekt setzen und F5, oder:

```
dotnet run --project ObjoWkPractice_3
```

## Bedienung

```
ADD ITEM       [1]   Name, Preis (Cent), Volumen (ml), Id eingeben
BUY ITEM       [2]   Id wählen – Geld wird abgezogen, Artikel verschwindet aus dem Automaten
SHOW ALLITEMS  [3]   Alle Artikel mit Id, Name, Preis, Volumen
ADD CASH       [4]   Betrag in Cent einwerfen
EXIT           [5]   Programm beenden
```

## Struktur

| Datei | Zweck |
|---|---|
| `Program.cs` | Einstieg, Hauptschleife mit `switch` über die Menüauswahl |
| `Vendingmachine.cs` | Artikelliste (`List<VendingItem>`), Suchen/Hinzufügen/Löschen, Kauf, Menü-Ein-/Ausgabe |
| `Softdrinks.cs` | Ein Getränk: `Price`, `Volume`, `Name`, `CanBePaid(Cash)` |
| `Cash.cs` | Guthaben des Kunden (`Amount` in Cent) |

## Konzepte, die hier geübt werden

- Klassen mit Konstruktor und Properties
- Eine `List<T>` per Schleife oder `FirstOrDefault` nach einer Id durchsuchen (`GetItemById`)
- Objekte an Methoden übergeben und darin verändern (`Cash` ist ein Referenztyp)
- Menüsteuerung mit `while` + `switch`

## Bekannte Schwächen / offene Punkte

- `Convert.ToInt32(Console.ReadLine())` wirft bei Nicht-Zahlen eine `FormatException` → `int.TryParse` verwenden
- `Vendingmachine` enthält auch Menü-Ein-/Ausgabe (`UserInput`, statisches `AddItem`) – gehört in `Program`
- `Cash.Amount` hat einen öffentlichen Setter; besser `Insert`/`Deduct` mit Prüfungen
- `Softdrinks.CanBePaid` wird nicht benutzt
- `Environment.Exit(0)` beendet hart; `return` aus `Main` wäre sauberer

Eine überarbeitete Version, die diese Punkte umsetzt, liegt im Nachbarprojekt
[`../Refactored`](../Refactored/README.md).
