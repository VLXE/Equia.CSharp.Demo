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

    static ApiSlePointFixedPressureInput CreateInput(ApiEquiaClient client)
    {
      var input = client.GetSlePointFixedPressureInput();
      input.Fluid = DemoFluid1_nHexane_Ethylene_HDPE7.GetFluid();
      input.Pressure = 30;
      input.Units = "C(In,Massfraction);C(Out,Massfraction);T(In,Celsius);T(Out,Celsius);P(In,Bar);P(Out,Bar);H(In,kJ/Kg);H(Out,kJ/Kg);S(In,kJ/(Kg Kelvin));S(Out,kJ/(Kg Kelvin));Cp(In,kJ/(Kg Kelvin));Cp(Out,kJ/(Kg Kelvin));Viscosity(In,centiPoise);Viscosity(Out,centiPoise);Surfacetension(In,N/m);Surfacetension(Out,N/m)";
      input.Components = [
                new() { Amount = 0.78 },
                new() { Amount = 0.02 },
                new() { Amount = 0.20 },
      ];

      return input;
    }

  }
}