using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine($"'{good.Album}' - {good.Band}, {good.Price} руб.");
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
    public int CountProducts()
    {
        return goodsInOrder.Count;
    }
}