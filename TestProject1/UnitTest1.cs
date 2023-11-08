namespace MyProject.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductClass;
using ClientClass;
using OrderClass;
using Holder;
using FluentAssertions;
using Moq;
using ProductClassInterface;
using System.Diagnostics;
using System.Text.Json;

[TestClass]
public class UnitTest1
{
    private const string newOrderStatus = "Nowe";
    private const string productStatus = "available";
    private const string nameOfProduct = "Car";
    private const int idOfProduct = 1;

    [DataTestMethod]
    [DataRow(1, 5.5)]
    [DataRow(2, 15.5)]
    [DataRow(3, 25.5)]
    [DataRow(4, 35.5)]
    public void checkPrice(int id, double Price)
    {
        Product product1 = new Product { Id = 1, Price = 5.5 };
        Product product2 = new Product { Id = 2, Price = 15.5 };
        Product product3 = new Product { Id = 3, Price = 25.5 };
        Product product4 = new Product { Id = 4, Price = 35.5 };
        ProductHolder productHolder = new ProductHolder();
        productHolder.add(product1);
        productHolder.add(product2);
        productHolder.add(product3);
        productHolder.add(product4);

        double result = productHolder.showPrice(id);

        Assert.AreEqual(Price, result);

    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "no product with this Id exists")]
    public void checkPriceWithBadId()
    {
        ProductHolder productHolder = new ProductHolder();

        double result = productHolder.showPrice(1);

        Assert.IsTrue(false);
    }

    [TestMethod]
    public void addOrder()
    {

        List<Product> list = new List<Product>();
        Product product1 = new Product { Id = idOfProduct };
        list.Add(product1);
        ProductHolder productHolder = new ProductHolder();
        productHolder.add(product1);
        int ClientId = 21;
        Client client = new Client{ Id = ClientId };
        ClientHolder clientHolder = new ClientHolder();
        clientHolder.add(client);
        int idOfOrder = 1;

        Order order = new Order(idOfOrder, ClientId, list, clientHolder,productHolder);

        Assert.AreEqual(newOrderStatus,order.status);
        Assert.AreEqual(ClientId, order.ClientId);
        Assert.AreEqual(idOfOrder, order.Id);
        Assert.AreEqual(idOfProduct, order.Items[0].Id);
    }

    [TestMethod]
    public void addOrderButWithFluentAssertions()
    {

        List<Product> list = new List<Product>();
        Product product1 = new Product { Id = idOfProduct };
        list.Add(product1);
        ProductHolder productHolder = new ProductHolder();
        productHolder.add(product1);
        int ClientId = 21;
        Client client = new Client { Id = ClientId };
        ClientHolder clientHolder = new ClientHolder();
        clientHolder.add(client);
        int idOfOrder = 1;
        Order result = new Order();
        result.Items = list;
        result.status = newOrderStatus;
        result.ClientId = ClientId;
        result.Id = idOfOrder;

        Order order = new Order(idOfOrder, ClientId, list, clientHolder, productHolder);

        order.status.Should().Be(newOrderStatus);
        order.ClientId.Should().Be(ClientId);
        order.Id.Should().Be(idOfOrder);
        order.Items[0].Id.Should().Be(idOfOrder);
    }

    [TestMethod]
    public void addProduct()
    {
        Product product1 = new Product { Id = idOfProduct , Name = nameOfProduct, Price = 5.5, status = productStatus};
        var product = new Mock<IProduct>();
        product.Setup(x => x.Id).Returns(idOfProduct);
        product.Setup(x => x.Price).Returns(5.5);
        product.Setup(x => x.status).Returns(productStatus);
        product.Setup(x => x.Name).Returns(nameOfProduct);
        ProductHolder productHolder = new ProductHolder();
        productHolder.add(product1);

        Boolean result = productHolder.containsObject(product.Object);

        Assert.IsTrue(result);

    }

    [TestMethod]
    public void changeStatus()
    {
        string status = "cancelled";
        var product = new Mock<IProduct>();

        product.Object.changeStatus(status);

        product.Verify(x => x.changeStatus(status), Times.Once);
    }

    public static IEnumerable<object[]> AdditionData
    {
        get
        {
            return new[]
            {
            new object[] { new Product { Id = 1, Price = 5.5 } },
            new object[] { new Product { Id = 2, Price = 15.5 } }
        };
        }
    }

    [TestMethod]
    [DynamicData(nameof(AdditionData))]
    public void ShowPricesMoreEfficient(Product product)
    {

        ProductHolder productHolder = new ProductHolder();
        productHolder.add(product);

        double result = productHolder.showPrice(product.Id);

        Assert.AreEqual(product.Price, result);
    }
    public static IEnumerable<object[]> AdditionData2
    {
        get
        {
            string json = File.ReadAllText("products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(json);

            foreach (var product in products)
            {
                yield return new object[] { product };
            }
        }
    }
    [TestMethod]
    [DynamicData(nameof(AdditionData2))]
    public void ShowPricesWithJson(Product product)
    {

        ProductHolder productHolder = new ProductHolder();
        productHolder.add(product);

        double result = productHolder.showPrice(product.Id);

        Assert.AreEqual(product.Price, result);
    }

    public TestContext TestContext { get; set; }

    [TestMethod]
    public void ShowPricesWithTestContext()
    {
        Product product1 = new Product { Id = 1, Price = 5.5 };
        ProductHolder productHolder = new ProductHolder();
        productHolder.add(product1);
        TestContext.WriteLine(@"Checking if show price with given id");

        double result = productHolder.showPrice(product1.Id);

        Assert.AreEqual(product1.Price, result);

    }

    [TestMethod]
    public void TestAdd()
    {
        Product product1 = new Product { Id = 1, Price = 5.5 };
        ProductHolder productHolder = new ProductHolder();


        productHolder.add(product1);


        Product result = productHolder.read(1);
        result.Price.Should().Be(5.5);
        result.Id.Should().Be(1);

    }
    [TestMethod]
    [ExpectedException(typeof(Exception), "no product with this Id exists")]
    public void TestRemove()
    {
        Product product1 = new Product { Id = 1, Price = 5.5 };
        ProductHolder productHolder = new ProductHolder();
        productHolder.add(product1);

        productHolder.remove(product1);

        Product result = productHolder.read(1);
    }
    [TestMethod]
    public void TestUpdate()
    {
        Product product1 = new Product { Id = 1, Price = 5.5 };
        Product product2 = new Product { Id = 2, Price = 6.5 };
        ProductHolder productHolder = new ProductHolder();

        productHolder.add(product1);
        productHolder.update(product2, 1);

        Product result = productHolder.read(2);
        result.Price.Should().Be(6.5);
        result.Id.Should().Be(2);

    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "no object with this id found")]
    public void TestUpdateFailed()
    {
        Product product1 = new Product { Id = 1, Price = 5.5 };
        Product product2 = new Product { Id = 2, Price = 6.5 };
        ProductHolder productHolder = new ProductHolder();

        productHolder.add(product1);
        productHolder.update(product2, 41);

    }

}
