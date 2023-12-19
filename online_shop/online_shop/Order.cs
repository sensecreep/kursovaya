using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//класс "заказ"
public class Order
{
    public List<Goods> goodsInOrder = new(); //список товаров в заказе
    int _id; //айди заказа
    public int ID { get { return _id; } set { _id = value; } }
    public void AddOrder(Goods goods) //добавление товаров
    {
        goodsInOrder.Add(goods);
    }
    public void PrintOrder() //показ товаров в заказе
    {
        foreach (Goods good in goodsInOrder)
        {
            Console.WriteLine($"'{good.Album}' - {good.Band}, {good.Price} руб.");
        }
    }
    public double TotalSum() //подсчет суммы всего заказа
    {
        double total = 0;
        foreach (Goods good in goodsInOrder)
        {
            total += good.Price;
        }
        return total;
    }
    public int CountProducts() //подсчет продуктов в заказе
    {
        return goodsInOrder.Count;
    }
    public void Cancel() //сброс товаров в заказе после его отмены
    {
        goodsInOrder.Clear();
    }
}