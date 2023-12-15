using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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