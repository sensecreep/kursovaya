using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Shop
{
    DataBase _data = new("DataBase.xlsx");
    public static List<Customer> customers = new();
    public static List<Goods> goods = new();
    public static List<Record> records = new();
    public static List<CD> cds = new();
    public void AddCustomer()
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
    public void RemoveCustomer(string phoneNum)
    {
        foreach (Customer customer in customers)
        {
            if (customer.PhoneNumber == phoneNum)
            {
                //Console.WriteLine($"{customer.Name}, {customer.Surname}, {customer.PhoneNumber}");
                customers.RemoveAt(customers.IndexOf(customer));
                break;
            }
        }
    }
    public Customer SearchCustomer(string phoneNum)
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
    public void PrintCustomers()
    {
        foreach (Customer customer in customers)
        {
            Console.WriteLine($"ФИ: {customer.Name} {customer.Surname}, номер телефона - {customer.PhoneNumber}");
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
    public Goods SearchGoods(int num)
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
    public void SavingInBD()
    {
        _data.SaveCustomersDB(customers);
    }
}
