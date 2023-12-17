using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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