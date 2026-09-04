# Refactored – verbesserte Kopie des Vending-Machine-Programms

Eigenes Projekt neben `ObjoWkPractice_3`, damit sich die Klassen nicht mit dem Original beißen.
Starten: in Visual Studio `Refactored` als Startprojekt setzen, oder

```
dotnet run --project Refactored
```

## Struktur

```
Refactored/
├── Program.cs              Einstieg: baut Objekte zusammen und startet die UI
├── Domain/                 Fachlogik – KEIN Console.* hier
│   ├── Product.cs          abstrakte Basis: Id, Name, Preis
│   ├── Softdrink.cs        konkretes Produkt mit Volumen
│   ├── Wallet.cs           Guthaben mit Insert/Deduct + Prüfungen
│   ├── VendingMachine.cs   Bestand + Buy(...) → PurchaseResult
│   └── PurchaseResult.cs   Ergebnis eines Kaufs (enum + record)
└── Ui/                     alles, was mit der Konsole redet
    ├── ConsoleUi.cs        Menü-Schleife, Ausgabe, Übersetzung Result → Text
    ├── ConsoleInput.cs     TryParse-basierte Eingabehelfer
    └── MenuOption.cs       enum statt Magic Numbers
```

## Was sich gegenüber dem Original geändert hat

| Original | Refactored | Warum |
|---|---|---|
| `Cash.Amount` mit public Setter, `cash.Amount -= ...` von außen | `Wallet.Insert/Deduct/CanAfford`, Setter privat | Das Objekt schützt seine eigenen Regeln (keine Negativbeträge, nicht überziehen). |
| `VendingItem`-Wrapper klebt `Id` an `Softdrinks` | `Product` hat die `Id` selbst | Ein Produkt hat eine Identität – kein Hilfsobjekt nötig. |
| `Softdrinks` (Plural, fest verdrahtet) | `Product` (abstrakt) → `Softdrink` | Polymorphismus: weitere Produktarten sind ein neuer Subtyp, kein Umbau. |
| `BuyItem` liest Konsole, prüft, bucht, druckt | `Buy(id, wallet)` gibt `PurchaseResult` zurück; UI druckt | Domäne ist ohne Konsole testbar; Text und Logik getrennt. |
| `UserInput` + statisches `AddItem` in `Vendingmachine` | `ConsoleUi` | Ein Automat rendert kein Menü (Single Responsibility). |
| `using static Vendingmachine` | normale Aufrufe über `_machine.` | Herkunft jeder Methode ist sichtbar. |
| `Convert.ToInt32(Console.ReadLine())` überall | `ConsoleInput.ReadInt` mit `TryParse` und Bereich | Kein Absturz bei Tippfehlern, eine Stelle statt sieben. |
| `case 5: Environment.Exit(0); break;` | `running = false` | Prozess nicht hart abwürgen; `break` war unerreichbar. |
| `switch (x)` mit 1–5 | `switch (MenuOption)` | Lesbare Namen statt Magic Numbers. |
| `Vendingmachine.Id`, leerer Konstruktor, `CanBePaid` ungenutzt | entfernt bzw. genutzt | Toter Code weg. |
| `ShowAllitems` bei leerer Liste stumm | Hinweis „(keine Produkte)“ | Benutzer weiß, was los ist. |
| Doppelte Id möglich | `AddProduct` wirft `InvalidOperationException` | Eindeutige Ids sind eine Domänenregel. |
