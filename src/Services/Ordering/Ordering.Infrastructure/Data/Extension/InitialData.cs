namespace Ordering.Infrastructure.Data.Extension;

public class InitialData
{
    public static IEnumerable<Customer> Customers = [
        Customer.Create(CustomerId.Of(new Guid("12345-id-customer1")), "amrane", "aa.aa@aa.aa"),
        Customer.Create(CustomerId.Of(new Guid("12345-id-customer2")), "amazigh", "aaz.aaz@aaz.aaz")
    ];

    public static IEnumerable<Product> Products = [
        Product.Create(ProductId.Of(new Guid("12345-id-product1")), "IPhone15", 500),
        Product.Create(ProductId.Of(new Guid("12345-id-product2")), "GalaxyS24", 600),
        Product.Create(ProductId.Of(new Guid("12345-id-product3")), "Earpods", 200),
        Product.Create(ProductId.Of(new Guid("12345-id-product4")), "JBLcasqueX2", 99),
        Product.Create(ProductId.Of(new Guid("12345-id-product5")), "IncassableTitane", 50)
    ];

    public static IEnumerable<Order> Orders
    {
        get
        {
            var address1 = Address.Of("amrane", "djebarri", "aa.aa@aa.aa", "18 Av Terrasse", "France", "Juvisy", "91260");
            var address2 = Address.Of("amazigh", "djebarri", "aaz.aaz@aaz.aaz", "13 b rue du petit dadas", "France", "Montpellier", "24150");

            var payment1 = Payment.Of("VISA", "1234 1234 1234 1234 1234", "10/28", "001", 1);
            var payment2 = Payment.Of("VISA", "4321 4321 4321 4321 4321", "10/29", "002", 1);

            var order1 = Order.Create(
                OrderId.Of(new Guid("12345-id-order1")),
                CustomerId.Of(new Guid("12345-id-customer1")),
                OrderName.Of("ORD_1"),
                address1,
                address1,
                payment1
                );

            var order2 = Order.Create(
                OrderId.Of(new Guid("12345-id-order2")),
                CustomerId.Of(new Guid("12345-id-customer2")),
                OrderName.Of("ORD_2"),
                address2,
                address2,
                payment2
                );

            order1.Add(ProductId.Of(new Guid("12345-id-product1")), 2, 500);
            order1.Add(ProductId.Of(new Guid("12345-id-product2")), 1, 600);

            order1.Add(ProductId.Of(new Guid("12345-id-product3")), 2, 200);
            order1.Add(ProductId.Of(new Guid("12345-id-product4")), 1, 99);

            return [order1, order2];
        }
    }
}
