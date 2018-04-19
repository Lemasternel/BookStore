using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookStore.Test.WebAPI
{
    [TestClass]
    public class BookWebAPITest
    {
        [TestMethod]
        public async Task ConnectionTest()
        {
            var client = new HttpClient();
            try
            {
                var result = await client.GetAsync("http://localhost:50026/publisher/all");
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Assert.Fail("Connection Error");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Connection Error", ex);
            }
        }
    }
}
