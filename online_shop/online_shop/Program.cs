using System.Drawing;
using System.Xml.Linq;

namespace online_shop
{
    internal class Program
    {
        static void Continue()
        {
            Console.WriteLine("\nНажмите любую кнопку для продолжения");
            string a = Console.ReadLine();
            Console.Clear();
        }

        public const int accountWork = 1;
        public const int showCustomers = 2;
        public const int showGoods = 3;
        public const int registration = 1;
        public const int editing = 2;
        public const int remove = 3;
        static void Main(string[] args)
        {
            Shop shop = new();

            List<Goods> goods = new List<Goods>();

            List<Record> records = new()
            {
                new Record("OK Computer", "Radiohead", 6000, 50, "nie"),
                new Record("Ultraviolence", "Lana Del Rey", 5500, 70, "nie"),
                new Record("Diamond Eyes", "Deftones", 5500, 50, "nie")
            };

            List<CD> cds = new()
            {
                new CD("OK Computer", "Radiohead", 6000, 65),
                new CD("Ultraviolence", "Lana Del Rey", 5500, 30),
                new CD("Diamond Eyes", "Deftones", 5500, 7)
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
                Console.WriteLine("Для работы над аккаунтом нажмите 1, показать всех пользователей - 2, показать все товары - 3");
                int number = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (number)
                {
                    case accountWork:
                        int number2 = 0;
                        while (number2 != 4)
                        {
                            Console.WriteLine("Чтобы зарегистрироваться нажмите 1, чтобы отредактировать свой аккаунт - 2, чтобы удалить аккаунт - 3, чтобы вернуться в основное меню - 4");
                            number2 = int.Parse(Console.ReadLine());
                            switch (number2)
                            {
                                case registration:
                                    try
                                    {
                                        Customer customer = new Customer();
                                        customer.Registration();
                                        Console.WriteLine("\nПользователь успешно добавлен!");
                                        Continue();
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case remove:
                                    shop.RemoveCustomer(Console.ReadLine());
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
                        }
                        else
                        {
                            Console.WriteLine("Ошибка! Недопустимое значение");
                            Continue();
                        }
                        break;
                }
            }
        }
    }
}

public class Customer
{
    string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            if (int.TryParse(value, out int num))
            {
                throw new Exception("Имя не может быть числом");
            }
            else
            {
                _name = value;
            }
        }
    }
    string _surname;
    public string Surname
    {
        get { return _surname; }
        set
        {
            if (int.TryParse(value, out int num))
            {
                throw new Exception("Фамилия не может быть числом");
            }
            else
            {
                _surname = value;
            }
        }
    }
    string _phoneNumber;
    public string PhoneNumber
    {
        get { return _phoneNumber; }
        set
        {
            if (value[0] == '+' && value[1] == '7')
            {

                value = '8' + value.Substring(2);
            }
            if (value[0] != '8') //еще про кол-во цифр дописать надо
            {
                throw new Exception("Номер не соответсвует стандарту: должен начинать с +7 или 8");
            }
            if (int.TryParse(value, out int num))
            {
                _phoneNumber = value;
            }
            else
            {
                throw new Exception("В номере не могут содержаться не числовые символы");
            }
        }
    }

    public void Registration()
    {
        Console.WriteLine("Введите имя пользователя: ");
        Name = Console.ReadLine();
        Console.WriteLine("Введите фамилию пользователя: ");
        Surname = Console.ReadLine();
        Console.WriteLine("Введите номер телефона пользователя: ");
        PhoneNumber = Console.ReadLine();
        Shop.AddCustomer(this);
    }
}

abstract public class Goods
{
    public string Album { get; }
    public string Band { get; }
    public double Price { get; }
    int _amount;
    public int AmountInStock
    {
        get { return _amount; }
        set { if  (value < 0)
            {
                _amount = 0;
            } }
    }
    public Goods(string album, string band, double price, int amountInStock)
    {
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
    string Color { get; }
    public Record(string album, string band, double price, int amountInStock, string color) : base(album, band, price, amountInStock)
    {
        Color = color;
    }
    public override string Info()
    {
        return $"{Album}, {Band}, {Price}, {AmountInStock}, {Color}";
    }
}

public class CD : Goods
{
    public CD(string album, string band, double price, int amountInStock) : base(album, band, price, amountInStock) { }
    public override string Info()
    {
        return $"{Album}, {Band}, {Price}, {AmountInStock}";
    }
    //int _id;
}

public class Order : Customer
{
    int _id;
    int _orderDate;
}

public class Shop
{
    public static List<Customer> customers = new();
    public static List<Goods> goods = new();
    public static List<Record> records = new();
    public static List<CD> cds = new();
    static public void AddCustomer(Customer customer)
    {
        customers.Add(customer);
    }
    public void RemoveCustomer(string phoneNum)
    {
        foreach (Customer customer in customers)
        {
            if (customer.PhoneNumber == phoneNum)
            {
                Console.WriteLine($"{customer.Name}, {customer.Surname}, {customer.PhoneNumber}");
                Console.WriteLine(customers.IndexOf(customer));
                customers.RemoveAt(customers.IndexOf(customer));
            }
        }
    }
    public void PrintCustomers()
    {
        foreach (Customer customer in customers)
        {
            Console.WriteLine($"{customer.Name}, {customer.Surname}, {customer.PhoneNumber}");
        }
    }
    public void AddGoods<T>(T product) where T : Goods
    {
        if (product is Record record) //добавление пластинки в список всех товаров и список пластинок
        {
            records.Add(record);
            goods.Add(product);
        }
        else if (product is CD cd) //добавление компакт-диска в список всех товаров и список компакт-дисков
        {
            cds.Add(cd);
            goods.Add(product);
        }
    }
    public void PrintGoods(int num)
    {
        if (num == 1)
        {
            foreach (Goods good in goods)
            {
                if (good is Record record)
                {
                    Console.WriteLine($"Vinyl: {record.Info()}");
                }
                else if (good is CD cd)
                {
                    Console.WriteLine($"CD: {cd.Info()}");
                }
            }
        }
        if (num == 2)
        {
            foreach (Record record in records)
            {
                Console.WriteLine($"Vinyl: {record.Info()}");
            }
        }
        if (num == 3)
        {
            foreach (CD cd in cds)
            {
                Console.WriteLine($"CD: {cd.Info()}");
            }
        }
    }
}

public class DataBase
{

}
