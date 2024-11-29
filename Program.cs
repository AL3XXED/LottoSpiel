using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;
using System;
using System.Globalization;

namespace Lotto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Teil1

            //Lottozahlen
            //Erstellen Sie ein int[] lottozahlen mit der Länge 49.
            //Schreiben Sie dann einen Code der dieses Array automatisch mit den Zahlen 1 - 49 befüllt.

            //Teil2

            //Ziehung der Lottozahlen
            //Aus dem Array unserer Lottozahlen sollen nun sechs Zahlen zufällig gezogen werden.
            //Diese sechs Zahlen müssen in einem neuen Array abgelegt werden. 
            //Verwenden Sie auch wieder Random für die Zufällige Ziehung.
            //Bei den gezogenen Zahlen darf es zu keiner Dopplung kommen.

            //Teil3

            //User-Eingabe und Gewinnausgabe
            //Der User soll in der Lage sein, sechs Zahlen einzugeben.
            //Diese werden in einem Array abgelegt.
            //Danach soll überprüft werden, wieviele Zahlen der User im Vergleich zu den gezogenen Lottozahlen richtig getippt hat.
            //Geben Sie in der Konsole aus, wieviele Richtige getippt wurden.

            //Sollten Sie in der vorherigen Aufgabe zu keiner Lösung gekommen sein, schreiben Sie von Hand ein Array mit gezogenen Zahlen.



            Random zufall = new Random();
            double gewinn = zufall.Next(25000000, 125000000) + zufall.NextDouble();
            string formattedGewinn = gewinn.ToString("N2", CultureInfo.GetCultureInfo("de-DE"));
            //CultureInfo.GetCultureInfo("de-DE") legt die deutsche Kultur fest, N" steht für Zahlenformat mit Tausendertrenzeichen & 2 gibt die Dezimalstellen an.

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("-~-~-~- Willkommen zum XXL-Lotto -~-~-~-");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($" Jackpot: {formattedGewinn} Millionen Euro \n"); //F3 gibt die Nachkommastellen an
            

            int[] lottozahlen = new int[49];                 // Erstellen eines Arrays mit 49 Elementen für die Lottozahlen
            for (int i = 0; i < lottozahlen.Length; i++)     // Befüllen des Arrays mit Zahlen von 1 bis 49
            {
                lottozahlen[i] = i + 1;                      // Setze jedes Element auf den Wert i + 1 (also 1 bis 49
            }
            int[] gezogenezahlen = new int[6];
            bool[] gezogen = new bool[49];                  // Boolean-Array zur Verfolgung, ob eine Zahl bereits gezogen wurd

            for (int i = 0; i < gezogenezahlen.Length; i++) //Ziehen von sechs verschiedenen Lottozahlen
            {
                int zahl;
                do
                {
                    zahl = zufall.Next(0, 49);
                }
                while (gezogen[zahl - 1]);                  //Wiederholt solange bis bis die sechs Plätze im Array voll sind
                gezogenezahlen[i] = zahl;
                gezogen[zahl - 1] = true;                    //Markiere die bereits gezogenen Zahlen, um Duplikate zu vermeiden
            }
            
            int superzahl = zufall.Next(0, 10);
            int[] tippzahl = new int[6];
            Console.WriteLine("Bitte geben Sie sechs verschiedene Zahlen zwischen 1 und 49 ein:");

            for (int i = 0; i < tippzahl.Length; i++)       // Schleife zur Eingabe der sechs Zahlen durch den Benutzer
            {
                int eingabe;
                do                                          // Fordere den Benutzer auf, eine Zahl einzugeben
                {
                    Console.WriteLine($"Zahl {i + 1}: ");
                    eingabe = Convert.ToInt32(Console.ReadLine()); // Lese die Eingabe und konvertiere sie in einen Integer
                }
                while (eingabe < 1 || eingabe > 49 || Array.Exists(tippzahl, z => z == eingabe));
                tippzahl[i] = eingabe;                               // Überprüfung der richtigen Tipps des Benutzers
            }
            //Array.Exists nimmt zwei Parameter: das Array(benutzerZahlen) und einen Predicate(eine Bedingung), der definiert, was gesucht wird.
            //Der Predicate hier ist eine Lambda-Funktion z => z == eingabe, die für jedes Element z im Array prüft, ob es gleich der Benutzereingabe(eingabe) ist.
            //Wenn mindestens ein Element im Array mit der Benutzereingabe übereinstimmt, gibt Array.Exists true zurück; andernfalls gibt es false zurück.

            int tippsuper;
            do
            {
                Console.WriteLine("Bitte geben Sie ihre Superzahl (1 bis 9) ein: ");
                tippsuper = Convert.ToInt32(Console.ReadLine());
            }
            while (tippsuper < 0 || tippsuper > 9);
            int richtigeTipps = 0;
            foreach (int zahl in tippzahl)                           // Durchlaufe alle vom Benutzer eingegebenen Zahlen
            {
                if (Array.Exists(gezogenezahlen, z => z == zahl))
                {
                    richtigeTipps++;
                }
            }

            Console.Clear();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("-~-~-~- Willkommen zum XXL-Lotto -~-~-~-");
            Console.WriteLine("----------------------------------------");
            Array.Sort(gezogenezahlen, gezogen);
            Console.WriteLine($"Die heutigen Zufallszahlen: {string.Join(" - ", gezogenezahlen)} Superzahl: {superzahl}");
            Console.WriteLine($"Ihre getippten Zahlen :   {string.Join("- ", tippzahl)}       Superzahl: {tippsuper}");
            Thread.Sleep(500) ;
            if (richtigeTipps == 6 && tippsuper == superzahl)
            {
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Herzlichen Glückwunsch! Sie haben den JACKPOT geknackt!");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine($"    Jackpot: {formattedGewinn} Millionen Euro \n");
            }
            if (tippsuper == superzahl)
            {
                Console.WriteLine("\nZusätzlich ist Ihre Superzahl richtig! Großartig <3 \n\n");
                Console.WriteLine($"\nSie haben {richtigeTipps} richtige !\n\n");
            }
            else
            {
                Console.WriteLine($"\nSie haben {richtigeTipps} von 6 richtige.\n\n");
            }

            //Die Methode string.Join in C# wird verwendet, um die Elemente eines Arrays oder einer Sammlung zu einem
            //einzigen String zu verbinden, wobei ein spezifizierter Separator zwischen den einzelnen Elementen eingefügt wird
            Console.WriteLine("******************************************!  Glücksspiel kann Süchtig machen !******************************************");
            Console.WriteLine("Anonyme Hotlines wie die der BZgA (Bundeszentrale für gesundheitliche Aufklärung) stehen rund um die Uhr zur Verfügung. Hier können Betroffene und Angehörige Hilfe und Informationen erhalten: Telefonnummer: 0800 1 37 27 00");
            Console.ReadKey();
        }
    }
}
