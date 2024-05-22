using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Xml.Linq;

internal class Program
{
    //обработчик события
    public void HandleMes(string msg)
    {
        Console.WriteLine(msg);
    }

    public delegate void MyDelegate(string msg); //делегат принял в себя объект и аргумент
    public event MyDelegate onMsg; //евент уведомляет другие объекты о наступлении действий


    //лямбда


    static void Main(string[] args)
    {
        //делегаты события анонимные методы и linq

        //делегат
        C c = new C();
        c.TakeMsg("asasas");
        //c.onMsg += HandleMes("ddd");

        //анонимные методы, те же самые делегаты, но без названий
        MyDelegate del = delegate (string msg)
        {
            Console.WriteLine(msg);
        };
        del("Hello from Delegate");

        //лямбда
        MyDelegate del1 = (string msg) =>
        {
            Console.WriteLine(msg);
        };
        del1("Hello from Delegate");

        //линка
        List<Person> list = new List<Person>();
        var result = from x in list //query syntax
                     select x.Name; //вытащили данные

        var result2 = from x in list
                      where x.Age > 18 //where дает условие
                      select x; //вытащили

        var result3 = from x in list
                      group x by x.Name; //сгруппируй по имени, сортировка

        var result4 = list.Where(x => x.Age > 18).ToList(); //сгруппируй по возрасту, сортировка

        var result5 = list.Select(x => x.Age * 2).ToList(); //умножили возраст всех на 2

        //OrderBy / OrderByDEscending: Сортирует элементы в последовательности данных по возрастанию/убиванию
        //GroupBy: Группирует элементы по определенному ключу
        //Distinct: Удаляет дублирующиеся элементы из последовательности данных
        //Reverse: Разворачивает порядок элементов в последовательности
        //First/FirstOrDefault: Возвращает первый элемент из последовательности. 
        // First - выбросит исключение, если последовательность пуста, а FirstOrDefault - вернет значение по умолчанию
        //Avarage - дает среднюю сумму всех чисел

        //Task1
        List<Student> students = new List<Student>
        {
            new Student {Name = "Alice", Grades = new List<int> {5,3,4 } },
            new Student {Name = "Bob", Grades = new List<int> {4,4,4 } },
            new Student {Name = "Dasha", Grades = new List<int> {2,3,4 } }
        };
        var sortedList = students.OrderByDescending(Student => Student.Grades.Average());
        Console.WriteLine("Отсортиорванный список: ");
        foreach (var student in sortedList)
        {
            Console.WriteLine($"{student.Name}: {student.Grades.Average()}");
        }

  

        //Task2
        List<Zakaz> zakazs = new List<Zakaz>();
        {
            new Zakaz
            {
                Name = "Alice",
                Id = 1,
                Products = new List<OrderProduct> {
                    new OrderProduct { ProductName = "Banan", Price = 12 },
                    new OrderProduct { ProductName = "Orange", Price = 15 }
                }
            };
            new Zakaz
            {
                Name = "Alina",
                Id = 2,
                Products = new List<OrderProduct> {
                 new OrderProduct {ProductName = "Arbuz", Price = 1200 },
                 new OrderProduct {ProductName = "MAnka", Price = 150}
                }
            };
            
        };

        var sum1 = zakazs.Select(x => new
        {
            OrderId = x.Id,
            TotalSum = x.Products.Sum(p => p.Price),
        });
        Console.WriteLine("Общая сумма по каждому заказу: ");
        foreach (var item in sum1)
        {
            Console.WriteLine($"{item.OrderId}: {item.TotalSum}");
        }

        

    }
}

//Task2
public class OrderProduct
{
    public string? ProductName { get; set; }
    public int Price { get; set; }

}

public class Zakaz
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<OrderProduct>? Products { get; set; }
}

//Task1
public class Student
{
    public string? Name{ get; set; }
    public List<int>? Grades { get; set; }
}



public class Person
{
    public string? Name { get; set; }
    public int Age { get; set; }
}

public class C
{
    public delegate void MyDelegate(string msg); //делегат принял в себя объект и аргумент
    public event MyDelegate? onMsg; //евент уведомляет другие объекты о наступлении действий

    //создать метод где вызывается событие
    public void TakeMsg(string msg)
    {
        onMsg?.Invoke(msg); //инвок позволяет вызывать делегат
    }
}

