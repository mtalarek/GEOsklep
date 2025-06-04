using System;
using System.Collections.Generic;
using System.Linq;

namespace SklepConsole
{
    class Program
    {
        public class Produkt
        {
            public int Id { get; set; }
            public string Nazwa { get; set; }
            public decimal Cena { get; set; }
            public int Ilosc { get; set; }
        }

        static List<Produkt> DostepneProdukty = new List<Produkt>()
        {
            new Produkt { Id = 1, Nazwa = "Tachimetr", Cena = 30000.00m, Ilosc = 10 },
            new Produkt { Id = 2, Nazwa = "Statyw", Cena = 500.00m, Ilosc = 8 },
            new Produkt { Id = 3, Nazwa = "Pryzmat", Cena = 1200.00m, Ilosc = 5 },
            new Produkt { Id = 4, Nazwa = "Tyczka", Cena = 200.00m, Ilosc = 5 },
            new Produkt { Id = 5, Nazwa = "Kontroler", Cena = 20000.00m, Ilosc = 8 },
            new Produkt { Id = 6, Nazwa = "GPS", Cena = 15000.00m, Ilosc = 2 },
        };

        static List<Produkt> Koszyk = new List<Produkt>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== MENU SKLEPU ===");
                Console.WriteLine("0. Wyjście");
                Console.WriteLine("1. Wyświetl dostępne produkty");
                Console.WriteLine("2. Dodaj produkt do koszyka");
                Console.WriteLine("3. Wyświetl koszyk");
                Console.WriteLine("4. Usuń 1 sztukę produktu z koszyka");
                Console.WriteLine("5. Finalizuj zakupy");
                Console.Write("Wybierz opcję: ");
                string wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "0":
                        Console.WriteLine("Dziękujemy za zakupy! Do zobaczenia.");
                        return;
                    case "1":
                        PokazProdukty();
                        break;
                    case "2":
                        DodajDoKoszyka();
                        break;
                    case "3":
                        PokazKoszyk();
                        break;
                    case "4":
                        UsunZKoszyka();
                        break;
                    case "5":
                        FinalizujZakupy();
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                        break;
                }
            }
        }

        static void PokazProdukty()
        {
            Console.WriteLine("\n--- Dostępne Produkty ---");
            foreach (var produkt in DostepneProdukty)
            {
                Console.WriteLine($"ID: {produkt.Id} | {produkt.Nazwa} | Cena: {produkt.Cena} zł | Ilość: {produkt.Ilosc}");
            }
        }

        static void DodajDoKoszyka()
        {
            Console.Write("Podaj ID produktu do dodania: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Nieprawidłowy ID.");
                return;
            }

            var produkt = DostepneProdukty.FirstOrDefault(p => p.Id == id);
            if (produkt == null)
            {
                Console.WriteLine("Nie znaleziono produktu.");
                return;
            }

            if (produkt.Ilosc <= 0)
            {
                Console.WriteLine("Produkt niedostępny.");
                return;
            }

            produkt.Ilosc--;

            var wKoszyku = Koszyk.FirstOrDefault(p => p.Id == id);
            if (wKoszyku != null)
            {
                wKoszyku.Ilosc++;
            }
            else
            {
                Koszyk.Add(new Produkt
                {
                    Id = produkt.Id,
                    Nazwa = produkt.Nazwa,
                    Cena = produkt.Cena,
                    Ilosc = 1
                });
            }

            Console.WriteLine($"Dodano {produkt.Nazwa} do koszyka.");
        }

        static void PokazKoszyk()
        {
            Console.WriteLine("\n--- Twój Koszyk ---");

            if (Koszyk.Count == 0)
            {
                Console.WriteLine("Koszyk jest pusty.");
                return;
            }

            decimal suma = 0;

            foreach (var produkt in Koszyk)
            {
                decimal wartosc = produkt.Cena * produkt.Ilosc;
                suma += wartosc;
                Console.WriteLine($"ID: {produkt.Id} | {produkt.Nazwa} | Cena: {produkt.Cena} zł | Ilość: {produkt.Ilosc} | Wartość: {wartosc} zł");
            }

            Console.WriteLine($"Suma: {suma} zł");
        }

        static void UsunZKoszyka()
        {
            Console.Write("Podaj ID produktu do usunięcia z koszyka: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Nieprawidłowy ID.");
                return;
            }

            var produktWKoszyku = Koszyk.FirstOrDefault(p => p.Id == id);
            if (produktWKoszyku == null)
            {
                Console.WriteLine("Produkt nie znajduje się w koszyku.");
                return;
            }

            produktWKoszyku.Ilosc--;

            if (produktWKoszyku.Ilosc == 0)
            {
                Koszyk.Remove(produktWKoszyku);
            }

            var produktWDostepnych = DostepneProdukty.FirstOrDefault(p => p.Id == id);
            if (produktWDostepnych != null)
            {
                produktWDostepnych.Ilosc++;
            }

            Console.WriteLine($"Usunięto jedną sztukę {produktWKoszyku.Nazwa} z koszyka.");
        }
        static void FinalizujZakupy()
        {
            if (Koszyk.Count == 0)
            {
                Console.WriteLine("Koszyk jest pusty. Nie można sfinalizować zakupu.");
                return;
            }

            Console.WriteLine("\n--- FINALIZACJA ZAKUPÓW ---");
            decimal suma = 0;

            foreach (var produkt in Koszyk)
            {
                decimal wartosc = produkt.Cena * produkt.Ilosc;
                suma += wartosc;
                Console.WriteLine($"{produkt.Nazwa} x{produkt.Ilosc} = {wartosc} zł");
            }

            Console.WriteLine($"Suma do zapłaty: {suma} zł");

            Koszyk.Clear();
            Console.WriteLine("Dziękujemy za zakupy! Koszyk został opróżniony.");
        }
    }
}