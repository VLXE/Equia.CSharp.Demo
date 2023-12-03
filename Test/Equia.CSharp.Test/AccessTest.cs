using NUnit.Framework;
using Equia.Api.Shared.ApiInput;
using Equia.Api.Shared.Client;
using Equia.CSharp.Shared;
using Equia.CSharp.Shared.Fluids;

namespace Equia.CSharp.Test
{
  [TestFixture]
  class AccessTest
  {
    /// <summary>
    /// Call API with a invalid access key. 
    /// HTTP 401 is then returned.
    /// </summary>
    [Test]
    public void WrongUserId()
    {
      var client = new ApiEquiaClient(new HttpClient(), SharedSettings.ApiUrl, "Does not work");

      var input = client.GetFlashInput();
      input.Fluid = DemoFluid1_nHexane_Ethylene_HDPE7.GetFluid();
      input.Temperature = 490;
      input.Pressure = 30;
      input.FlashType = "Fixed Temperature/Pressure";
      input.Components = new List<ApiCalculationComposition> {
                new() { Mass = 0.78 },
                new() { Mass = 0.02 },
                new() { Mass = 0.20 },
      };

      var exception = Assert.ThrowsAsync<HttpRequestException>(async () => await client.CallFlashAsync(input));

      Assert.That("Response status code does not indicate success: 401 (Unauthorized).", Is.EqualTo(exception?.Message));
    }

  }
}