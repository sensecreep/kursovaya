using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//класс магазина
public class Shop
{
    DataBase _data = new("DataBase.xlsx"); //экземпляр бд с переданным в конструктор путем файла
    public static List<Customer> customers = new(); //список пользователей
    public static List<Goods> goods = new(); //список товаров
    public static List<Record> records = new(); //список пластинок
    public static List<CD> cds = new(); //список компакт-дисков
    public void AddCustomer() //добавление пользователя
    {
        Customer customer = new Customer();
        Console.WriteLine("Введите имя пользователя: ");
        customer.Name = Console.ReadLine();
        Console.WriteLine("Введите фамилию пользователя: ");
        customer.Surname = Console.ReadLine();
        Console.WriteLine("Введите номер телефона пользователя: ");
        customer.PhoneNumber = Console.ReadLine();
        foreach (Customer customerr  in customers)
        {
            if (customerr.PhoneNumber == customer.PhoneNumber)
            {
                throw new Exception("Пользователь с таким номером уже существует, невозможно создать нового");
            }
        }
        customers.Add(customer);
    }
    public void RemoveCustomer(string phoneNum) //удаление пользователя
    {
        foreach (Customer customer in customers)
        {
            if (customer.PhoneNumber == phoneNum)
            {
                customers.RemoveAt(customers.IndexOf(customer));
                break;
            }
        }
    }
    public Customer SearchCustomer(string phoneNum) //поиск пользователя
    {
        foreach (Customer customer in customers)
        {
            if (customer.PhoneNumber == phoneNum)
            {
                return customer;
            }
        }
        return null;
    }
    public void PrintCustomers() //показ всех пользователей
    {
        foreach (Customer customer in customers)
        {
            Console.WriteLine($"ФИ: {customer.Name} {customer.Surname}, номер телефона - {customer.PhoneNumber}");
        }
    }
    public void AddGoods<T>(T product) where T : Goods //добавление товаров в списки
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
    public void PrintGoods(int num) //показ товаров по категориям
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
    public Goods SearchGoods(int num) //поиск товаров
    {
        foreach (Goods product in goods)
        {
            if (product.ID == num)
            {
                return product;
            }
        }
        return null;
    }
    public void SavingInBD() //передача данных в бд для сохранения
    {
        _data.SaveCustomersDB(customers);
    }
    public void ReadingBD() //принятие данных из бд
    {
        customers = _data.ReadCustomersDB();
    }
}
