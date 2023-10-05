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
    [Test]
    public void WrongUserId()
    {
      SharedSettings.AccessSecret = "Does not work";
      SharedSettings.UserId = Guid.NewGuid().ToString();
      var client = new ApiEquiaClient(new HttpClient(), SharedSettings.ApiUrl, SharedSettings.UserId, SharedSettings.AccessSecret);

      var input = client.GetFlashInput();
      input.Fluid = DemoFluid1_nHexane_Ethylene_HDPE7.Create();
      input.Temperature = 490;
      input.Pressure = 30;
      input.FlashType = "Fixed Temperature/Pressure";
      input.Components = new List<ApiCalculationComposition> {
                new ApiCalculationComposition { Mass = 0.78 },
                new ApiCalculationComposition { Mass = 0.02 },
                new ApiCalculationComposition { Mass = 0.20 },
      };

      var exception = Assert.ThrowsAsync<HttpRequestException>(async () => await client.CallFlashAsync(input));

      Assert.AreEqual("Response status code does not indicate success: 401 (Unauthorized).", exception?.Message);
    }

  }
}