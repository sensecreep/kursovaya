using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//абстрактный класс, содержащий общие характеристики всех товаров в магазине
abstract public class Goods
{
    public int ID { get; } //индивидуальный код товара для однозначной идентификации при заказе
    public string Album { get; } //название альбома 
    public string Band { get; } //название группы-исполнителя
    public double Price { get; } //цена
    int _amount;
    public int AmountInStock //кол-во единиц товара, оставшихся в наличии
    {
        get { return _amount; }
        set
        {
            if (value < 0)
            {
                _amount = 0;
            }
            else
            {
                _amount = value;
            }
        }
    }
    public Goods(int id, string album, string band, double price, int amountInStock) //конструктор
    {
        ID = id;
        Album = album;
        Band = band;
        Price = price;
        AmountInStock = amountInStock;
    }
    public abstract string Info(); //метод для вывода информации о товаре
    public int InStock() //ф-ция, считающая и запоминающее оставшееся кол-во товаров после их покупки
    {
        return AmountInStock -= 1;
    }
}
public class Record : Goods //класс-наследник для виниловых пластинок
{
    public string Color { get; } //цвет пластинки
    public Record(int id, string album, string band, double price, int amountInStock, string color) : base(id, album, band, price, amountInStock) //конструктор
    {
        Color = color;
    }
    public override string Info() //переопределение метода в классе-наследнике
    {
        return $"(ID: {ID}) '{Album}' - {Band}, {Price} руб., в наличии {AmountInStock} шт., цвет: {Color}";
    }
}

public class CD : Goods //класс-наследник для компакт-дисков
{
    public CD(int id, string album, string band, double price, int amountInStock) : base(id, album, band, price, amountInStock) { }
    public override string Info() 
    {
        return $"(ID: {ID}) '{Album}' - {Band}, {Price} руб., в наличии {AmountInStock} шт.";
    }
}