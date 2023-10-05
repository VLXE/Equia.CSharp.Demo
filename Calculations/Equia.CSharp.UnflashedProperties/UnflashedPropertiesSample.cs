using Equia.Api.Shared.ApiInput;
using Equia.Api.Shared.ApiOutput.Property;
using Equia.Api.Shared.Calculations.UnflashedProperties;
using Equia.Api.Shared.Client;
using Equia.CSharp.Shared;
using Equia.CSharp.Shared.Fluids;

namespace Equia.CSharp.UnflashedProperties
{
  /// <summary>
  /// Example of obtaining unflashed properties
  /// Unflashed means that all properties are from the EoS  directly. No stability analysis is performed
  /// </summary>
  static class UnflashedPropertiesSample
  {
    public static async Task ExecuteAsync()
    {
      try
      {
        var client = CreateClient();
        var input = CreateInput(client);

        var result = await client.CallUnFlashedPropertiesAsync(input);

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

    static ApiUnflashedPropertyCalculationInput CreateInput(ApiEquiaClient client)
    {
      var input = client.GetUnFlashedPropertyInput();
      input.Fluid = DemoFluid1_nHexane_Ethylene_HDPE7.Create();
      input.Temperature = 490;
      input.Pressure = 30;
      input.VolumeType = "Auto";
      input.CalculationType = "Fixed Temperature/Pressure";
      input.Components = new List<ApiCalculationComposition> {
                new ApiCalculationComposition { Mass = 0.78 },
                new ApiCalculationComposition { Mass = 0.02 },
                new ApiCalculationComposition { Mass = 0.20 },
      };

      return input;
    }

    static void PrintValue(double input) => PrintValue(input.ToString());
    static void PrintValue(string input) => Console.Write(input.PadRight(25));
    static void PrintLine(string input = "") => Console.WriteLine(input);

    static void PrintCalculationResult(ApiOutputUnflashedPropertyResult result)
    {
      PrintLine();
      PrintValue("Property");
      PrintLine();

      PrintValue($"Temperature [{result.Temperature.Units}]");
      PrintValue(result.Temperature.Value);
      PrintLine();
      PrintValue($"Pressure [{result.Pressure.Units}]");
      PrintValue(result.Pressure.Value);
      PrintLine();
      PrintValue($"Volume [{result.Volume.Units}]");
      PrintValue(result.Volume.Value);
      PrintLine();

    }

  }
}