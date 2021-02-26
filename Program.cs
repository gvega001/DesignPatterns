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

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specification);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Color == color;
        }
    }
    public class BetterFilter: IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specification)
        {
            foreach (var item in items)
            {
                if (specification.IsSatisfied(item))
                {
                    yield return item;
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

            var pf = new BetterFilter();
            Console.WriteLine("Green products (new):");
            foreach (var product in pf.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($" - {product.Name} is green");
            }
        }
    }
}
