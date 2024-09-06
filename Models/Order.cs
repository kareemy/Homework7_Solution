using System.ComponentModel.DataAnnotations;

namespace Homework7_Solution.Models;

public class Order
{
    public int OrderID {get; set;}

    [Display(Name = "Customer Name")]
    public string CustomerName {get; set;} = string.Empty;

    [Display(Name = "Order Date")]
    [DataType(DataType.Date)]
    public DateTime OrderDate {get; set;}

    [Display(Name = "Products in Order")]
    public List<ProductOrder>? Products {get; set;} = default!;
}

public class ProductOrder
{
    public int OrderID {get; set;}
    public int ProductID {get; set;}
    public Order Order {get; set;} = default!;
    public Product Product {get; set;} = default!;
}