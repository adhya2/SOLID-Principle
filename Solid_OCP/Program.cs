using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid_OCP
{
    class Product
    {
        public string name { get; set; }
        public int price { get; set; }

        public Product(string name, int price)
        {
            this.name = name;
            this.price = price;
        }
    }

    class ShoppingCart
    {
        List<Product> p = new List<Product>();
        public void addProduct(Product pr)
        {
            p.Add(pr);
        }
        public List<Product> getProduct()
        {
            return p;
        }
        public double calculateTotal()
        {
            double total = 0;
            foreach(Product pr in p)
            {
                total += pr.price;
            }
            return total;
        }
    }

    class ShoppingCartPrinter {
        ShoppingCart c1;
        public ShoppingCartPrinter(ShoppingCart c)
        {
            this.c1 = c;
        }
        public void printInvoice()
        {
            Console.WriteLine("The Invoice of the Shopping Cart is Shown Below :-");
            foreach (var cp in c1.getProduct())
            {
                Console.WriteLine($"{cp.name} and it's cost is {cp.price}");
            }
            Console.WriteLine("\nThe Total price of the cart is " + c1.calculateTotal());
        }
    }


    abstract class Persistence
    {
        public abstract void save(ShoppingCart c);
    }

    class SQLPersistence : Persistence {
        public override void save(ShoppingCart c)
        {
            Console.WriteLine("Shopping Cart data stored in SQL db.....\n");
        }
    }

    class MongoPersistence : Persistence {
        public override void save(ShoppingCart c)
        {
            Console.WriteLine("Shopping Cart Data saved to db.....\n");
        }
    }

    class FilePersistence : Persistence
    {
        public override void save(ShoppingCart c)
        {
            Console.WriteLine("Shopping Cart Data saved to db......\n");
        }
    }


    class Program
    {
        public static void Main(string[] args)
        {
            Product p1 = new Product("Laptop", 50000);
            Product p2 = new Product("Mouse", 2000);
            Product p3 = new Product("CPU", 20000);
            Product p4 = new Product("Computer Monitor", 50000);

            ShoppingCart c = new ShoppingCart();
            c.addProduct(p1);
            c.addProduct(p2);
            c.addProduct(p3);
            c.addProduct(p4);
            double t = c.calculateTotal();
            Console.WriteLine("From ShoppingCart class the total directly is " + t);
            Console.WriteLine();

            ShoppingCartPrinter printer = new ShoppingCartPrinter(c);
            printer.printInvoice();
            Console.WriteLine();

            Persistence db = new SQLPersistence();
            db.save(c);

            Persistence mongo = new MongoPersistence();
            mongo.save(c);

            Persistence file = new FilePersistence();
            file.save(c);

        }
    }
}
