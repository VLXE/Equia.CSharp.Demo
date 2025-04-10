using Equia.Api.Shared.ApiInput;
using Equia.Api.Shared.Calculations.BatchFlash;
using Equia.Api.Shared.Calculations.Flash;
using Equia.Api.Shared.Client;
using Equia.CSharp.Shared;
using Equia.CSharp.Shared.Fluids;

namespace Equia.CSharp.BatchFlashSample
{
  /// <summary>
  /// Example of a batch flash calculation at fixed temperature/pressure
  /// </summary>
  class BatchFlashSample : SharedBase
  {
    public async Task ExecuteAsync()
    {
      try
      {
        var client = CreateClient();
        var input = CreateInput(client);

        var result = await client.CallBatchFlashAsync(input);

        if (result.Success && result.Points is not null)
        {
          foreach (var point in result.Points)
          {
            PrintCalculationResult(point);
            PrintLine();
          }
        }
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

    static ApiBatchFlashCalculationInput CreateInput(ApiEquiaClient client)
    {
      var input = client.GetBatchFlashInput();
      input.Fluid = DemoFluid1_nHexane_Ethylene_HDPE7.GetFluid();
      input.Units = "C(In,Massfraction);C(Out,Massfraction);T(In,Celsius);T(Out,Celsius);P(In,Bar);P(Out,Bar);H(In,kJ/Kg);H(Out,kJ/Kg);S(In,kJ/(Kg Kelvin));S(Out,kJ/(Kg Kelvin));Cp(In,kJ/(Kg Kelvin));Cp(Out,kJ/(Kg Kelvin));Viscosity(In,centiPoise);Viscosity(Out,centiPoise);Surfacetension(In,N/m);Surfacetension(Out,N/m)";
      input.FlashType = "Fixed Temperature/Pressure";
      input.Points.Add(new ApiBatchFlashCalculationItem
      {
        Temperature = 200, //In Celcius
        Pressure = 25, //In Bar
        Components = [
                new() { Amount = 0.78 },
                new() { Amount = 0.02 },
                new() { Amount = 0.20 },
         ]
      });
      input.Points.Add(new ApiBatchFlashCalculationItem
      {
        Temperature = 225, //In Celcius
        Pressure = 35, //In Bar
        Components = [
                new() { Amount = 0.88 },
                new() { Amount = 0.02 },
                new() { Amount = 0.10 },
         ]
      });

      return input;
    }

  }
}