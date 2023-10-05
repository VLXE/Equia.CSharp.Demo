using Equia.Api.Shared.ApiInput;
using Equia.Api.Shared.ApiOutput.Point;
using Equia.Api.Shared.Calculations.Flash;
using Equia.Api.Shared.Client;
using Equia.CSharp.Shared;
using Equia.CSharp.Shared.Fluids;

namespace Equia.CSharp.FlashSample
{
  /// <summary>
  /// Example of a flash calculation at fixed temperature/pressure
  /// </summary>
  class FlashSample : SharedBase
  {
    public async Task ExecuteAsync()
    {
      try
      {
        var client = CreateClient();
        var input = CreateInput(client);

        var result = await client.CallFlashAsync(input);

        if (result.Success && result.Point is not null)
          PrintCalculationResult(result.Point);
        else
          HandleExceptions.PrintExceptionInfo(result.ExceptionInfo);

        Console.WriteLine(string.Empty);
        Console.WriteLine("Press any key to close");
        Console.ReadKey();
      }
      catch (Exception ex)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        PrintLine(string.Empty);
        PrintLine($"Message: {ex.Message}");
        PrintLine($"Stack Trace: {ex.StackTrace}");
        Console.ResetColor();
      }
    }

    static ApiEquiaClient CreateClient()
    {
      return new ApiEquiaClient(new HttpClient(), SharedSettings.ApiUrl, SharedSettings.UserId, SharedSettings.AccessSecret);
    }

    static ApiFlashCalculationInput CreateInput(ApiEquiaClient client)
    {
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

      return input;
    }

  }
}