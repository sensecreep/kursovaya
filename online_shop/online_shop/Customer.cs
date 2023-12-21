using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//класс покупателя
public class Customer
{
    List<Order> orders = new(); //список заказов
    public void AddNewOrder(Order order) //добавление заказа
    {
        orders.Add(order);
    }
    public void ShowOrders() //показать заказы
    {
        if (orders.Count > 0)
        {
            foreach (Order order in orders)
            {
                Console.WriteLine($"ID: {order.ID}, сумма: {order.TotalSum()} руб.");
            }
        }
        else { Console.WriteLine("Заказы отсутсвуют"); }
    }

    string _name; //имя
    public string Name //свойство имени
    {
        get { return _name; }
        set
        {
            if (double.TryParse(value, out double num))
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
    string _surname; //фамилия
    public string Surname
    {
        get { return _surname; }
        set
        {
            if (double.TryParse(value, out double num))
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
    string _phoneNumber; //номер телефона 
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
            if (value[0] != '8')
            {
                throw new Exception("Ошибка! Номер не соответсвует стандарту: должен начинать с +7 или 8");
            }
            if (value.Length != 11)
            {
                throw new Exception("Ошибка! Номер не соответсвует стандарту: должен содержать 11 цифр");
            }
            if (long.TryParse(value, out long num))
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