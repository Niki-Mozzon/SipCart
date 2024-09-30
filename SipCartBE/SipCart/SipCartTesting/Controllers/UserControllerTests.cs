namespace IntegrationTests.Controllers
{
    //[TestFixture()]
    //[Category("ExcludeOnDeploy")]
    //public class UserControllerTests : ContainerizedTests
    //{
    //    private CustomWebApplicationFactory _factory;
    //    private HttpClient _client;


    //    [OneTimeSetUp]
    //    public async Task OneTimeSetUp()
    //    {
    //        MsSqlContainer msSqlContainer = await CreateContainerAsync();
    //        _factory = new CustomWebApplicationFactory(msSqlContainer);
    //        _client = _factory.CreateClient();
    //    }

    //    [OneTimeTearDown]
    //    public async Task OneTimeTearDown()
    //    {
    //        await DisposeContainerAsync();
    //        if (_factory != null)
    //        {
    //            await _factory.DisposeAsync();
    //        }
    //        _client?.Dispose();
    //    }


    //    [Test()]
    //    [Order(-1)]
    //    public void ContainerHealthCheckTest()
    //    {
    //        Assert.That(_msSqlContainer!.State, Is.EqualTo(TestcontainersStates.Running));
    //    }


    //    [Test(Description =
    //        "WHEN get all users" +
    //        "THEN get 3 users")]
    //    [Order(1)]
    //    public async Task GetAllUsersTest()
    //    {
    //        HttpResponseMessage response = await _client.GetAsync("user/all");
    //        string responseString = await response.Content.ReadAsStringAsync();
    //        Console.WriteLine(responseString);
    //        GetUserOutput[]? result = JsonConvert.DeserializeObject<GetUserOutput[]>(responseString) ?? null;
    //        Assert.Multiple(() =>
    //        {
    //            Assert.That(response.IsSuccessStatusCode);
    //            Assert.That(result, Is.InstanceOf(typeof(GetUserOutput[])));
    //            Assert.That(result, Is.Not.Null);
    //            Assert.That(result, Has.Length.EqualTo(3));
    //        });
    //    }

    //    [Test(Description =
    //        "GIVEN a valid id" +
    //        "THEN get the user with that id")]
    //    [Order(2)]
    //    public async Task GetUserByIdTest()
    //    {
    //        int id = 1;
    //        HttpResponseMessage response = await _client.GetAsync($"user/get/{id}");
    //        string responseString = await response.Content.ReadAsStringAsync();
    //        GetUserOutput? result = JsonConvert.DeserializeObject<GetUserOutput>(responseString) ?? null;

    //        Assert.Multiple(() =>
    //        {
    //            Assert.That(response, Is.Not.Null);
    //            Assert.That(result, Is.InstanceOf(typeof(GetUserOutput)));
    //            Assert.That(result, Is.Not.Null);
    //            Assert.That(result!.UserId, Is.EqualTo(id));
    //        });
    //    }

    //    [Test(Description =
    //        "GIVEN a valid id" +
    //        "WHEN verify the user with that id" +
    //        "THEN return the user verified")]
    //    [Order(3)]
    //    public async Task VerifyUserTest()
    //    {
    //        int id = 2;
    //        HttpRequestMessage request = new(HttpMethod.Patch, $"user/verify/{id}");
    //        HttpResponseMessage response = await _client.SendAsync(request);
    //        string responseString = await response.Content.ReadAsStringAsync();
    //        GetUserOutput? result = JsonConvert.DeserializeObject<GetUserOutput>(responseString) ?? null;

    //        Assert.Multiple(() =>
    //        {
    //            Assert.That(result, Is.InstanceOf(typeof(GetUserOutput)));
    //            Assert.That(result!.Verified, Is.EqualTo(true));
    //        });
    //    }


    //    [Test(Description =
    //        "GIVEN a user" +
    //        "WHEN added" +
    //        "THEN get the user from response")]
    //    [Order(4)]
    //    public async Task AddUserTest()
    //    {
    //        CreateUserInput input = new()
    //        {
    //            Name = "John",
    //            Age = 23,
    //            Email = "John@email.com",
    //            Password = "pass123"
    //        };
    //        string json = JsonConvert.SerializeObject(input);
    //        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
    //        HttpResponseMessage response = await _client.PostAsync("user/add", content);
    //        string responseString = await response.Content.ReadAsStringAsync();
    //        Console.WriteLine(responseString);
    //        CreateUserOutput result = JsonConvert.DeserializeObject<CreateUserOutput>(responseString);
    //        Assert.Multiple(() =>
    //        {
    //            Assert.That(response.IsSuccessStatusCode);
    //            Assert.That(result, Is.InstanceOf(typeof(CreateUserOutput)));
    //            Assert.That(result, Is.Not.Null);
    //            Assert.That(result!.Email, Is.EqualTo(input.Email));
    //        });
    //    }

    //    [Test(Description =
    //        "GIVEN an underage user" +
    //        "WHEN added" +
    //        "THEN get exception")]
    //    [Order(5)]
    //    public async Task AddUnderageUserTest()
    //    {
    //        CreateUserInput input = new()
    //        {
    //            Name = "John",
    //            Age = 17,
    //            Email = "John@email.com",
    //            Password = "pass123"
    //        };
    //        string json = JsonConvert.SerializeObject(input);
    //        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
    //        HttpResponseMessage response = await _client.PostAsync("user/add", content);
    //        Assert.That(response.IsSuccessStatusCode, Is.Not.True);
    //    }

    //    [Test(Description =
    //        "GIVEN a valid id" +
    //        "WHEN ban the user with that id" +
    //        "THEN return the user banned")]
    //    [Order(6)]
    //    public async Task BanUserTest()
    //    {
    //        int id = 3;
    //        HttpRequestMessage request = new(HttpMethod.Patch, $"user/ban/{id}");
    //        HttpResponseMessage response = await _client.SendAsync(request);
    //        string responseString = await response.Content.ReadAsStringAsync();
    //        GetUserOutput? result = JsonConvert.DeserializeObject<GetUserOutput>(responseString) ?? null;

    //        Assert.Multiple(() =>
    //        {
    //            Assert.That(response.IsSuccessStatusCode, Is.True);
    //            Assert.That(result, Is.InstanceOf(typeof(GetUserOutput)));
    //            Assert.That(result!.IsBanned, Is.Not.Null);
    //        });
    //    }

    //    [Test(Description =
    //        "GIVEN a banned user" +
    //        "WHEN added" +
    //        "THEN get exception")]
    //    [Order(7)]
    //    public async Task AddBannedUserTest()
    //    {
    //        CreateUserInput input = new()
    //        {
    //            Name = "John",
    //            Age = 17,
    //            Email = "testuser3@example.com",
    //            Password = "pass123"
    //        };
    //        string json = JsonConvert.SerializeObject(input);
    //        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
    //        HttpResponseMessage response = await _client.PostAsync("user/add", content);
    //        Assert.That(response.IsSuccessStatusCode, Is.Not.True);
    //    }

    //    [Test(Description =
    //      "GIVEN a user with an already registered mail" +
    //      "WHEN added" +
    //      "THEN get UserAlreadyRegisteredException")]
    //    [Order(7)]
    //    public async Task AddAlreadyRegisteredUserTest()
    //    {
    //        CreateUserInput input = new()
    //        {
    //            Name = "John",
    //            Age = 17,
    //            Email = "testuser2@example.com",
    //            Password = "pass123"
    //        };
    //        string json = JsonConvert.SerializeObject(input);
    //        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
    //        HttpResponseMessage response = await _client.PostAsync("user/add", content);
    //        Assert.That(response.IsSuccessStatusCode, Is.Not.True);
    //    }


    //}
}