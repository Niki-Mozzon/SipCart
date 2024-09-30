using SipCartCore.Entities;
using SipCartCore.Exceptions;
using SipCartCore.Services.Interfaces;
using SipCartTesting.Setup;

namespace SipCartCore.Services.Tests
{
    [TestFixture()]
    public class CouponServiceTests : InMemoryTestSuite
    {
        private ICouponService _sut;


        [OneTimeSetUp]
        public async Task SetUp()
        {
            await GetContextAsync();
            if (_appContext.Coupons.Count() == 0)
            {
                _appContext.Coupons.Add(new Coupon { Code = "Test1", PercentageReduction = 20 });
                _appContext.Coupons.Add(new Coupon { Code = "Test2", PercentageReduction = 50 });
                await _appContext.SaveChangesAsync();
            }
            _sut = new CouponService(_appContext);
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await CleanUpAsync();
        }



        [Test(Description =
            "WHEN get all coupons" +
            "THEN return all coupons"
            )]
        [Order(1)]
        public async Task GetAllCouponsAsyncTest()
        {
            IEnumerable<Coupon> res = await _sut.GetAllCouponsAsync();
            Assert.That(res.Count(), Is.EqualTo(2));
        }

        [Test(Description =
            "GIVEN an valid id" +
            "WHEN validating coupon" +
            "THEN return coupon"
            )]
        [Order(2)]
        public async Task GetCouponByCodeAsyncTest()
        {
            string code = "Test1";
            Coupon res = await _sut.GetCouponByCodeAsync(code);
            Assert.That(res.Code, Is.EqualTo(code));
        }

        [Test(Description =
            "GIVEN an invalid id" +
            "WHEN validating coupon" +
            "THEN throw exception"
            )]
        [Order(3)]
        public async Task GetCouponByCodeAsyncTest_ThrowsCouponNotFoundException()
        {
            string code = "aaa";
            Assert.ThrowsAsync<CouponNotFoundException>(async () => await _sut.GetCouponByCodeAsync(code));
        }

    }
}