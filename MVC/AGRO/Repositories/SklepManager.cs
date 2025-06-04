using GEOsklep.Data;
using GEOsklep.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace GEOsklep.Repositories
{
    public class SklepManager

    {
        private GEOsklepContext _context;

        public SklepManager(GEOsklepContext context)
        {
            _context = context;
        }

        //public SklepManager AddArtykul(Artykul artykul, ArtykulBasket artykulbasket)
        //{
        //    _context.Basket.Add(artykulbasket);             //dodane
        //    _context.Artykuls.Add(artykul);          //zmieniony add na remove
        //    _context.SaveChanges();
        //    return this;
        //}

        public SklepManager AddArtykul(ArtykulBasket artykulbasket)
        {
            _context.Basket.Add(artykulbasket);             //dodane
            _context.SaveChanges();
            return this;
        }

        //public SklepManager RemoveArtykul(int id)
        //{
        //    var elementTodelete = GetArtykul(id);
        //    if (elementTodelete != null)
        //    {
        //        _context.Artykuls.Remove(elementTodelete);
        //        _context.SaveChanges();
        //    }
        //    return this;
        //}

        //public SklepManager RemoveArtykul(int id)
        //{
        //    var elementTodelete = GetArtykul(id);
        //    var elementToBuy = GetArtykulToBasket(elementTodelete);
        //    if (elementTodelete != null)
        //    {
        //        //_context.Artykuls.Remove(elementTodelete);
        //        //_context.Basket.Add(elementToBuy);
        //        _context.Artykuls.Find(id).quantity = elementTodelete.quantity -1;
        //        Console.WriteLine(elementToBuy);
        //        //bool is_he = false;
        //        //for(int i =0; i<=_context.Basket.Count(); i++)
        //        //{
        //            //if (_context.Basket.Contains(elementToBuy.name))
        //            //{
        //            //    is_he = true;
        //            //}

        //        //}

        //        bool is_he = _context.Basket.Any(p => p.name == elementTodelete.name );
        //        if (is_he == false)
        //        {
        //            _context.Basket.Add(elementToBuy);
        //        }
        //        else
        //        {
        //            _context.Basket.Find(id).quantity = elementToBuy.quantity + 1;
        //        }
        //            _context.SaveChanges();
        //    }

        //        return this;
        //}

        public SklepManager RemoveArtykul(int id)
        {
            var artykul = GetArtykul(id);

            if (artykul == null || artykul.quantity <= 0)
            {
                return this; // Produkt nie istnieje lub brak na stanie
            }

            // Zmniejsz ilość w sklepie
            artykul.quantity--;

            if (artykul.quantity == 0)
            {
                _context.Artykuls.Remove(artykul); // Usuń produkt, bo ilość = 0
            }
            else
            {
                _context.Artykuls.Update(artykul); // Zaktualizuj nową ilość
            }

            // Sprawdź, czy produkt już jest w koszyku
            var basketItem = _context.Basket.SingleOrDefault(p => p.name == artykul.name);

            if (basketItem == null)
            {
                // Jeśli nie ma, dodaj nowy z ceną
                basketItem = new ArtykulBasket
                {
                    name = artykul.name,
                    quantity = 1,
                    price = artykul.price // Przepisujemy cenę ze sklepu
                };
                _context.Basket.Add(basketItem);
            }
            else
            {
                // Jeśli jest, zwiększ ilość (cena zostaje niezmieniona)
                basketItem.quantity++;
                _context.Basket.Update(basketItem);
            }

            _context.SaveChanges();
            return this;
        }

        public SklepManager UpdateArtykul(Artykul artykul)
        {
            _context.Artykuls.Update(artykul);
            _context.SaveChanges();
            return this;
        }

        public SklepManager ChangeName(int id, string newName)
        {
           
            return this;
        }

        public Artykul GetArtykul(int id)
        {
            var artykul = _context.Artykuls.SingleOrDefault(x => x.id == id);
            return artykul;
        }

        //public ArtykulBasket GetArtykulToBasket(int id)
        //{
        //    var artykulToBasket = _context.Basket.SingleOrDefault(x => x.id == id);
        //    return artykulToBasket;
        //}


        public ArtykulBasket GetArtykulToBasket(Artykul artykul)
        {
            var artykulToBasket = _context.Basket.SingleOrDefault(p => p.name == artykul.name);
            return artykulToBasket;
        }

        public List<Artykul> GetArtykuls()
        {
            var artykuls = _context.Artykuls.ToList();
            return artykuls;
        }

        public List<ArtykulBasket> GetArtykulsBasket()
        {
            var artykulsbasket = _context.Basket.ToList();
            return artykulsbasket;
        }


    }
}
