using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Customer
{
    List<Order> orders = new();
    public void AddNewOrder(Order order)
    {
        orders.Add(order);
    }
    public void ShowOrders()
    {
        foreach (Order order in orders)
        {
            Console.WriteLine($"ID: {order.ID}, сумма: {order.TotalSum()} руб.");
        }
    }

    string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            if (int.TryParse(value, out int num))
            {
                throw new Exception("Ошибка! Имя не может быть числом");
            }
            else if (value == String.Empty)
            {
                throw new Exception("Ошибка! Имя не может отсутствовать");
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
                throw new Exception("Ошибка! Фамилия не может быть числом");
            }
            else if (value == String.Empty)
            {
                throw new Exception("Ошибка! Фамилия не может отсутствовать");
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
            if (value == String.Empty)
            {
                throw new Exception("Ошибка! Номер не может отсутствовать");
            }
            if (value[0] == '+' && value[1] == '7')
            {

                value = '8' + value.Substring(2);
            }
            if (value[0] != '8') //еще про кол-во цифр дописать надо
            {
                throw new Exception("Ошибка! Номер не соответсвует стандарту: должен начинать с +7 или 8");
            }
            if (int.TryParse(value, out int num))
            {
                _phoneNumber = value;
            }
            else
            {
                throw new Exception("Ошибка! В номере не могут содержаться не числовые символы");
            }
        }
    }
}