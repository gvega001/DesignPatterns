using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public enum Color
    {
        Red, Green, Blue, Black, Orange
    }

    public enum Size
    {
        Small, Medium, Large
    }

    public class Product
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }

        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }

            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var product in products)
            {
                if (product.Size == size)
                {
                    yield return product;
                }
            }
        }
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var product in products)
            {
                if (product.Color == color)
                {
                    yield return product;
                }
            }
        }

    }

    public class Program
    {
        static void Main(string[] args)
        {

            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var roseBush = new Product("House", Color.Blue, Size.Medium);

            Product[] products = {apple, tree, roseBush};

            var pf = new ProductFilter();

            Console.WriteLine("Green products (old):");
            foreach (var product in pf.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($" -{product.Name} is green");
            }
        }
    }
}
