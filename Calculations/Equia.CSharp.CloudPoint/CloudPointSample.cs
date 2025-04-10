using Equia.Api.Shared.ApiInput;
using Equia.Api.Shared.Calculations.CloudPoint;
using Equia.Api.Shared.Client;
using Equia.CSharp.Shared;
using Equia.CSharp.Shared.Fluids;

namespace Equia.CSharp.CloudPointSample
{
  /// <summary>
  /// Calculate a cloud point at fixed pressure.
  /// Note that VLXE does not distinguish between bubble and cloud point. It is the same calculation.
  /// </summary>
  class CloudPointSample : SharedBase
  {
    public async Task ExecuteAsync()
    {
      try
      {
        var client = CreateClient();
        var input = CreateInput(client);

        var result = await client.CallCloudPointAsync(input);

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

    static ApiCloudPointCalculationInput CreateInput(ApiEquiaClient client)
    {
      var input = client.GetCloudPointInput();
      input.Fluid = DemoFluid1_nHexane_Ethylene_HDPE7.GetFluid();
      input.Pressure = 25;
      input.Units = "C(In,Massfraction);C(Out,Massfraction);T(In,Celsius);T(Out,Celsius);P(In,Bar);P(Out,Bar);H(In,kJ/Kg);H(Out,kJ/Kg);S(In,kJ/(Kg Kelvin));S(Out,kJ/(Kg Kelvin));Cp(In,kJ/(Kg Kelvin));Cp(Out,kJ/(Kg Kelvin));Viscosity(In,centiPoise);Viscosity(Out,centiPoise);Surfacetension(In,N/m);Surfacetension(Out,N/m)";
      input.PointType = "Fixed Pressure";
      input.Components = new List<ApiCalculationComposition> {
                new() { Amount = 0.78 },
                new() { Amount = 0.02 },
                new() { Amount = 0.2 } };
      return input;
    }

  }
}