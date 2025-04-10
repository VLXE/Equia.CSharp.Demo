using Equia.Api.Shared.ApiInput;
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

    static ApiFlashCalculationInput CreateInput(ApiEquiaClient client)
    {
      var input = client.GetFlashInput();
      input.Fluid = DemoFluid1_nHexane_Ethylene_HDPE7.GetFluid();
      input.Temperature = 200; //In Celcius
      input.Pressure = 25; //In Bar
      input.Units = "C(In,Massfraction);C(Out,Massfraction);T(In,Celsius);T(Out,Celsius);P(In,Bar);P(Out,Bar);H(In,kJ/Kg);H(Out,kJ/Kg);S(In,kJ/(Kg Kelvin));S(Out,kJ/(Kg Kelvin));Cp(In,kJ/(Kg Kelvin));Cp(Out,kJ/(Kg Kelvin));Viscosity(In,centiPoise);Viscosity(Out,centiPoise);Surfacetension(In,N/m);Surfacetension(Out,N/m)";
      input.FlashType = "Fixed Temperature/Pressure";
      input.Components = new List<ApiCalculationComposition> {
                new() { Amount = 0.78 },
                new() { Amount = 0.02 },
                new() { Amount = 0.20 },
      };

      return input;
    }

  }
}