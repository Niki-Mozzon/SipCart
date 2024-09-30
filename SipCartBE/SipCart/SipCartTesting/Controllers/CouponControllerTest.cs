using DotNet.Testcontainers.Containers;
using IntegrationTests.Setup;
using Newtonsoft.Json;
using NUnit.Framework.Internal;
using SipCartCore.Entities;
using System.Net;
using Testcontainers.MsSql;

namespace IntegrationTests.Controllers
{
    [TestFixture()]
    [Category("ExcludeOnDeploy")]
    public class CouponControllerTests : ContainerizedTests
    {
        private readonly string controllerRoute = "coupon";
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
            "WHEN get all coupons" +
            "THEN get 2 coupons")]
        [Order(1)]
        public async Task GetAllCouponsTest()
        {
            HttpResponseMessage response = await _client.GetAsync(controllerRoute + "/all");
            string responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            List<Coupon>? result = JsonConvert.DeserializeObject<List<Coupon>>(responseString) ?? null;
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessStatusCode);
                Assert.That(result, Is.InstanceOf(typeof(List<Coupon>)));
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.EqualTo(2));
            });
        }


        [Test(Description =
            "GIVEN a valid id" +
            "THEN get the coupon with that id")]
        [Order(2)]
        public async Task GetCouponByIdTest()
        {
            string code = "ABC";
            HttpResponseMessage response = await _client.GetAsync(controllerRoute + "/coupon?code=" + code);
            string responseString = await response.Content.ReadAsStringAsync();
            Coupon? result = JsonConvert.DeserializeObject<Coupon>(responseString) ?? null;
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessStatusCode);
                Assert.That(result, Is.InstanceOf(typeof(Coupon)));
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Code, Is.EqualTo(code));
            });
        }


        [Test(Description =
            "GIVEN an invalid id" +
            "THEN get 404")]
        [Order(3)]
        public async Task Get404WhenCouponByCodeNotFound()
        {
            string code = "aaa";
            HttpResponseMessage response = await _client.GetAsync(controllerRoute + "/coupon?code=" + code);
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessStatusCode, Is.Not.True);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            });
        }
    }
}