using DotNet.Testcontainers.Containers;
using IntegrationTests.Setup;
using Newtonsoft.Json;
using NUnit.Framework.Internal;
using SipCartApi.Dtos.Input;
using SipCartApi.Dtos.Output;
using SipCartCore.Entities;
using System.Net;
using System.Text;
using Testcontainers.MsSql;

namespace IntegrationTests.Controllers
{
    [TestFixture()]
    [Category("ExcludeOnDeploy")]
    public class OrderControllerTests : ContainerizedTests
    {
        private readonly string controllerRoute = "order";
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            MsSqlContainer msSqlContainer = await CreateContainerAsync();
            _factory = new CustomWebApplicationFactory(msSqlContainer);
            _client = _factory.CreateClient();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await DisposeContainerAsync();
            if (_factory != null)
            {
                await _factory.DisposeAsync();
            }
            _client?.Dispose();
        }


        [Test()]
        [Order(-1)]
        public void ContainerHealthCheckTest()
        {
            Assert.That(_msSqlContainer!.State, Is.EqualTo(TestcontainersStates.Running));
        }


        [Test(Description =
            "GIVEN an checkout input" +
            "WHEN checks out" +
            "THEN get right order")]
        [Order(1)]
        public async Task CheckOutTest()
        {

            string couponCode = "ABC"; // 50% discount
            CheckoutInput input = new CheckoutInput()
            {
                CouponCode = couponCode, // 50% discount
                Products = new Dictionary<int, int>()
            };
            input.Products.Add(1, 2);   //1 = 1.50 * 2 => 3.00
            input.Products.Add(2, 2);   //2 = 1.99 * 2 => 3.98
            decimal expectedFullPrice = 6.98m;
            decimal expectedTotal = 3.49m;
            decimal expectedPercentageReduction = 50.0m;
            HttpResponseMessage response = await _client.PostAsync(controllerRoute + "/checkout", new StringContent(JsonConvert.SerializeObject(input), encoding: Encoding.UTF8, "application/json"));
            string responseString = await response.Content.ReadAsStringAsync();
            OrderOutput result = JsonConvert.DeserializeObject<OrderOutput>(responseString) ?? new OrderOutput();
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessStatusCode);
                Assert.That(result, Is.InstanceOf(typeof(OrderOutput)));
                Assert.That(result, Is.Not.Null);
                Assert.That(result.TotalPrice, Is.EqualTo(expectedTotal));
                Assert.That(result.FullPrice, Is.EqualTo(expectedFullPrice));
                Assert.That(result.PercentageReduction, Is.EqualTo(expectedPercentageReduction));
                Assert.That(result.CouponCode, Is.EqualTo(couponCode));
                Assert.That(result.Id, Is.Null);
                Assert.That(result.Products, Has.Count.EqualTo(2));
            });
        }


        [Test(Description =
                    "GIVEN an order input" +
                    "WHEN purchase" +
                    "THEN get order id")]
        [Order(2)]
        public async Task PurchaseTest()
        {
            OrderInput input = new OrderInput()
            {
                TotalPrice = 10,
                CouponCode = null,
                PaymentMethod = ePaymentMethod.CASH
            };
            HttpResponseMessage response = await _client.PostAsync(controllerRoute + "/purchase", new StringContent(JsonConvert.SerializeObject(input), encoding: Encoding.UTF8, "application/json"));
            string responseString = await response.Content.ReadAsStringAsync();
            int result = JsonConvert.DeserializeObject<int>(responseString);
            Assert.Multiple(() =>
                {
                    Assert.That(response.IsSuccessStatusCode);
                    Assert.That(result, Is.TypeOf(typeof(int)));
                    Assert.That(result, Is.Not.EqualTo(0));

                });
        }

        [Test(Description =
                    "GIVEN an order input with total greater than 10€ and cash as payment method " +
                    "WHEN purchase" +
                    "THEN return 400")]
        [Order(3)]
        public async Task PurchaseFails()
        {
            OrderInput input = new OrderInput()
            {
                TotalPrice = 12,
                CouponCode = null,
                PaymentMethod = ePaymentMethod.CASH
            };
            HttpResponseMessage response = await _client.PostAsync(controllerRoute + "/purchase", new StringContent(JsonConvert.SerializeObject(input), encoding: Encoding.UTF8, "application/json"));
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessStatusCode, Is.Not.True);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            });
        }


        //If ran alone will always fail, because it relies on the first order added in the test #2
        [Test(Description =
            "WHEN get all orders" +
            "THEN return the order created previously")]
        [Order(4)]
        public async Task GetAllOrdersTest()
        {
            HttpResponseMessage response = await _client.GetAsync(controllerRoute + "/all");
            string responseString = await response.Content.ReadAsStringAsync();
            List<Order> result = JsonConvert.DeserializeObject<List<Order>>(responseString) ?? new List<Order>();
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessStatusCode);
                Assert.That(result, Is.InstanceOf(typeof(List<Order>)));
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.EqualTo(1));
            });
        }
    }
}






