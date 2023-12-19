using Spire.Xls;
using Spire.Xls.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//класс работающий с файлом excel
public class DataBase
{
    string path; //путь файла
    static Workbook workbook = new(); //создание экземпляра excel книги
    public DataBase(string path) //конструктор, принимающий путь файла и проверяющий его (файл) на существование 
    {
        if (File.Exists(path))
        {
            workbook.LoadFromFile(path);
        }
        this.path = path;
    }

    static int rowCustomers = 1; //счетчик занятых рядов
    public void SaveCustomersDB(List<Customer> customers) //сохранение данных пользователей в файл
    {
        Worksheet worksheet = workbook.Worksheets[0];
        foreach (Customer customer in customers)
        {
            int column = 1;
            worksheet.Range[rowCustomers, column].Value = customer.Name;
            column++;
            worksheet.Range[rowCustomers, column].Value = customer.Surname;
            column++;
            worksheet.Range[rowCustomers, column].Value = customer.PhoneNumber;
            rowCustomers++;
        }
        workbook.SaveToFile(path);
    }
    public List<Customer> ReadCustomersDB() //чтение данных пользователей в файле для возможности дальнейшей работы с ними
    {
        List<Customer> customers = new();
        Worksheet worksheet = workbook.Worksheets[0];
        int row = 1;
        while (worksheet.Range[row, 1].Value != String.Empty)
        {
            Customer customer = new();
            customer.Name = worksheet.Range[row, 1].Value;
            customer.Surname = worksheet.Range[row, 2].Value;
            customer.PhoneNumber = worksheet.Range[row, 3].Value;
            customers.Add(customer);
            row++;
        }
        return customers;
    }
}