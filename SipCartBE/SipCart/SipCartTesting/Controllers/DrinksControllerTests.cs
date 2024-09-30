using DotNet.Testcontainers.Containers;
using IntegrationTests.Setup;
using Newtonsoft.Json;
using NUnit.Framework.Internal;
using SipCartCore.Entities;
using System.Net;
using System.Text;
using Testcontainers.MsSql;

namespace IntegrationTests.Controllers
{
    [TestFixture()]
    [Category("ExcludeOnDeploy")]
    public class DrinksControllerTests : ContainerizedTests
    {
        private readonly string controllerRoute = "drinks";
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
            "WHEN get all drinks" +
            "THEN get 4 drinks")]
        [Order(1)]
        public async Task GetAllDrinksTest()
        {
            HttpResponseMessage response = await _client.GetAsync(controllerRoute + "/all");
            string responseString = await response.Content.ReadAsStringAsync();
            List<Drink>? result = JsonConvert.DeserializeObject<List<Drink>>(responseString) ?? null;
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessStatusCode);
                Assert.That(result, Is.InstanceOf(typeof(List<Drink>)));
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.EqualTo(4));
            });
        }


        [Test(Description =
            "GIVEN a valid id" +
            "THEN get the drink with that id")]
        [Order(2)]
        public async Task GetDrinkByIdTest()
        {
            int id = 1;
            HttpResponseMessage response = await _client.GetAsync(controllerRoute + "/drink/" + id);
            string responseString = await response.Content.ReadAsStringAsync();
            Drink? result = JsonConvert.DeserializeObject<Drink>(responseString) ?? null;
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessStatusCode);
                Assert.That(result, Is.InstanceOf(typeof(Drink)));
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(id));
            });
        }


        [Test(Description =
            "GIVEN an invalid id" +
            "THEN get 404")]
        [Order(3)]
        public async Task Get404WhenDrinkByIdNotFound()
        {
            int id = -1;
            HttpResponseMessage response = await _client.GetAsync(controllerRoute + "/drink/" + id);
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessStatusCode, Is.Not.True);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            });
        }


        [Test(Description =
            "GIVEN a valid list of ids" +
            "THEN get the drinks with those ids")]
        [Order(4)]
        public async Task GetMultipleDrinksByIdTest()
        {
            List<int> ids = new List<int> { 1, 2, 3 };
            HttpResponseMessage response = await _client.PostAsync(controllerRoute + "/drinks", new StringContent(JsonConvert.SerializeObject(ids), encoding: Encoding.UTF8, "application/json"));
            string responseString = await response.Content.ReadAsStringAsync();
            List<Drink>? result = JsonConvert.DeserializeObject<List<Drink>>(responseString) ?? null;
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessStatusCode);
                Assert.That(result, Is.InstanceOf(typeof(List<Drink>)));
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.EqualTo(3));
            });
        }
    }
}