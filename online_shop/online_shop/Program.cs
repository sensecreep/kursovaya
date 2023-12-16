using System.Collections.Immutable;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace online_shop
{
    internal class Program
    {
        static void Continue()
        {
            Console.WriteLine("\nНажмите Enter для продолжения");
            string a = Console.ReadLine();
            Console.Clear();
        }

        public const int accountWork = 1;
        public const int showCustomers = 2;
        public const int showGoods = 3;
        public const int order = 4; //добавить ф-цию на случай если товары кончились
        public const int registration = 1;
        public const int remove = 2;

        static void Main(string[] args)
        {
            Shop shop = new();

            List<Goods> goods = new List<Goods>();

            List<Record> records = new()
            {
                new Record(55, "OK Computer", "Radiohead", 6000, 50, "синий"),
                new Record(27, "Ultraviolence", "Lana Del Rey", 5500, 70, "прозрачный"),
                new Record(33, "Diamond Eyes", "Deftones", 5500, 50, "черный")
            };

            List<CD> cds = new()
            {
                new CD(34,"In Rainbows", "Radiohead", 750, 65),
                new CD(67,"Puberty 2", "Mitski", 1200, 30),
                new CD(90,"The Head On The Door", "The Cure", 830, 7)
            };

            foreach (Record record in records)
            {
                goods.Add(record);
            }
            foreach (CD cd in cds)
            {
                goods.Add(cd);
            }
            foreach (Goods good in goods)
            {
                shop.AddGoods(good);
            }

            while (true)
            {
                Console.WriteLine("Для работы над аккаунтом нажмите 1, показать всех пользователей - 2, показать все товары - 3, сделать заказ - 4");
                int number = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (number)
                {
                    case accountWork:
                        int number2 = 0;
                        while (number2 != 3)
                        {
                            Console.WriteLine("Чтобы зарегистрироваться нажмите 1, чтобы удалить аккаунт - 2, чтобы вернуться в основное меню - 3");
                            number2 = int.Parse(Console.ReadLine());
                            switch (number2)
                            {
                                case registration:
                                    try
                                    {
                                        shop.AddCustomer();
                                        Console.WriteLine("\nПользователь успешно добавлен!");
                                        Continue();
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        Continue();
                                    }
                                    break;
                                case remove:
                                    Console.Clear();
                                    Console.WriteLine("Для удаления введите номер телефона, что привязан к Вашему аккаунту:");
                                    string num = Console.ReadLine();
                                    if (shop.SearchCustomer(num) == null)
                                    {
                                        Console.WriteLine("\nТакого пользователя нет, невозможно удалить аккаунт");
                                        Continue();
                                        break;
                                    }
                                    shop.RemoveCustomer(num);
                                    Console.WriteLine("Пользователь успешно удален");
                                    Continue();
                                    break;
                            }
                        }
                        Continue();
                        break;
                    case showCustomers:
                        shop.PrintCustomers();
                        Continue();
                        break;
                    case showGoods:
                        Console.WriteLine("Если вы хотите посмотреть весь список товаров нажмите 1, только виниловые пластинки - 2, только компакт-диски - 3");
                        int chooseGoods = int.Parse(Console.ReadLine());
                        if (chooseGoods >0 && chooseGoods < 4)
                        {
                            shop.PrintGoods(chooseGoods);
                            Continue();
                        }
                        else
                        {
                            Console.WriteLine("Ошибка! Недопустимое значение");
                            Continue();
                        }
                        break;
                    case order:
                        string stop = "none";
                        Order newOrder = new();

                        Random random = new Random();
                        newOrder.ID = random.Next(1, 100);
                        Console.WriteLine("Чтобы оформить заказа введите номер телефона, который привязан к Вашему аккаунту: ");
                        string numCustomer = Console.ReadLine();
                        if (shop.SearchCustomer(numCustomer) == null)
                        {
                            Console.WriteLine("\nТакого пользователя нет, невозможно оформить заказ");
                            Continue();
                            break;
                        }

                        while (stop!="S")
                        {
                            Console.WriteLine("Список товаров:\n");
                            shop.PrintGoods(1);
                            Console.WriteLine("\nВведите ID желаемого товара:");
                            int id_chosed = int.Parse(Console.ReadLine());
                            foreach (Goods product in goods)
                            {
                                if (product.ID == id_chosed)
                                {
                                    newOrder.AddOrder(shop.SearchGoods(id_chosed));
                                    product.InStock();
                                }
                            }

                            Console.WriteLine("\nВаш заказ: ");
                            newOrder.PrintOrder();
                            Console.WriteLine($"Итоговая стоимость: {newOrder.TotalSum()}");

                            Console.WriteLine("\nДля продолжения покупок нажмите Enter, для завершения заказа - 'S'");
                            stop = Console.ReadLine().ToUpper();
                            Console.Clear();
                        }

                        (shop.SearchCustomer(numCustomer)).AddNewOrder(newOrder);
                        (shop.SearchCustomer(numCustomer)).ShowOrders();
                        break;
                }
            }
        }
    }
}


abstract public class Goods
{
    public int ID { get; }
    public string Album { get; }
    public string Band { get; }
    public double Price { get; }
    int _amount;
    public int AmountInStock
    {
        get { return _amount; }
        set
        {
            if ( value < 0 )
            {
                _amount = 0;
            }
            else
            {
                _amount = value;
            }
        }
    }
    public Goods(int id, string album, string band, double price, int amountInStock)
    {
        ID = id;
        Album = album;
        Band = band;
        Price = price;
        AmountInStock = amountInStock;
    }
    public abstract string Info();
    public int InStock() //кол-во после покупки
    {
        return AmountInStock -= 1;
    }
}
public class Record : Goods
{
    public string Color { get; }
    public Record(int id, string album, string band, double price, int amountInStock, string color) : base(id, album, band, price, amountInStock)
    {
        Color = color;
    }
    public override string Info()
    {
        return $"(ID: {ID}) '{Album}' - {Band}, {Price} руб., в наличии {AmountInStock} шт., цвет: {Color}";
    }
}

public class CD : Goods
{
    public CD(int id, string album, string band, double price, int amountInStock) : base(id, album, band, price, amountInStock) { }
    public override string Info()
    {
        return $"(ID: {ID}) '{Album}' - {Band}, {Price} руб., в наличии {AmountInStock} шт.";
    }
}

public class Order
{
    public List<Goods> goodsInOrder = new();
    int _id;
    public int ID { get { return _id; } set { _id = value; } }
    public void AddOrder(Goods goods)
    {
        goodsInOrder.Add(goods);
    }
    public void PrintOrder()
    {
        foreach (Goods good in goodsInOrder)
        {
            Console.WriteLine($"{good.Album}, {good.Band}, {good.Price}, {good.AmountInStock}");
        }
    }
    public double TotalSum()
    {
        double total = 0;
        foreach (Goods good in goodsInOrder)
        {
            total += good.Price;
        }
        return total;
    }
}



public class DataBase
{

}
