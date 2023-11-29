using Equia.Api.Shared.ApiInput;
using Equia.Api.Shared.Calculations.SlePoint;
using Equia.Api.Shared.Client;
using Equia.CSharp.Shared;
using Equia.CSharp.Shared.Fluids;

namespace Equia.CSharp.Slepoint
{
  /// <summary>
  /// Example of finding a SLE point at fixed pressure.
  /// Routing will find the temperature where the first solid is formed
  /// </summary>
  class SlepointSample : SharedBase
  {
    public async Task ExecuteAsync()
    {
      try
      {
        var client = CreateClient();
        var input = CreateInput(client);

        var result = await client.CallSlePointAsync(input);

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

    static ApiSlePointCalculationInput CreateInput(ApiEquiaClient client)
    {
      var input = client.GetSlePointInput();
      input.Fluid = DemoFluid1_nHexane_Ethylene_HDPE7.Create();
      input.Pressure = 30;
      input.PointType = "Fixed Pressure";
      input.Components = new List<ApiCalculationComposition> {
                new() { Mass = 0.78 },
                new() { Mass = 0.02 },
                new() { Mass = 0.20 },
      };

      return input;
    }

  }
}