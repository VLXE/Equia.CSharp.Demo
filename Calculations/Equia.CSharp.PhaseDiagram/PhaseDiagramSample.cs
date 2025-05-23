using Equia.Api.Shared.ApiInput;
using Equia.Api.Shared.ApiOutput.Phasediagram;
using Equia.Api.Shared.Calculations.Phasediagram;
using Equia.Api.Shared.Client;
using Equia.CSharp.Shared;
using Equia.CSharp.Shared.Fluids;

namespace Equia.CSharp.PhaseDiagramSample
{
  /// <summary>
  /// Calculates entire phasediagram.
  /// VLE and LLE line is always included. But here we also ask for SLE, SLVE and VLLE part.
  /// We skip the spinodal curve
  /// </summary>
  static class PhaseDiagramSample
  {
    public static async Task ExecuteAsync()
    {
      try
      {
        var client = CreateClient();
        var input = CreateInput(client);

        var result = await client.CallPhasediagramFixedCompositionAsync(input);

        if (result.Success && result.Curve is not null)
          PrintPhaseDiagramResult(result.Curve);
        else
          HandleExceptions.PrintExceptionInfo(result.ExceptionInfo);
      }
      catch (Exception ex)
      {
        PrintLine($"Message: {ex.Message}");
        PrintLine($"Stack Trace: {ex.StackTrace}");
      }
    }

    static ApiEquiaClient CreateClient()
    {
      return new ApiEquiaClient(new HttpClient(), SharedSettings.ApiUrl, SharedSettings.AccessKey);
    }

    static ApiPhasediagramFixedCompositionInput CreateInput(ApiEquiaClient client)
    {
      var input = client.GetPhasediagramFixedCompositionInput();
      input.Fluid = DemoFluid1_nHexane_Ethylene_HDPE7.GetFluid();
      input.SLE = true;
      input.SLVE = true;
      input.VLLE = true;
      input.Spinodal = false;
      input.Units = "C(In,Massfraction);C(Out,Massfraction);T(In,Celsius);T(Out,Celsius);P(In,Bar);P(Out,Bar);H(In,kJ/Kg);H(Out,kJ/Kg);S(In,kJ/(Kg Kelvin));S(Out,kJ/(Kg Kelvin));Cp(In,kJ/(Kg Kelvin));Cp(Out,kJ/(Kg Kelvin));Viscosity(In,centiPoise);Viscosity(Out,centiPoise);Surfacetension(In,N/m);Surfacetension(Out,N/m)";
      input.Components = [
                new() { Amount = 0.78 },
                new() { Amount = 0.02 },
                new() { Amount = 0.2 } ];
      return input;
    }

    static void PrintValue(double input) => PrintValue(input.ToString());
    static void PrintValue(string input) => Console.Write(input.PadRight(25));
    static void PrintLine(string input = "") => Console.WriteLine(input);

    static void PrintPhaseDiagramResult(ApiOutputPhasediagram result)
    {
      PrintLinePoints("Phase Envelope", result.TemperatureUnits, result.PressureUnits, result.Phaseenvelope);
      PrintLinePoints("VLLE", result.TemperatureUnits, result.PressureUnits, result.Vlle);
      PrintLinePoints("SLE", result.TemperatureUnits, result.PressureUnits, result.SLE);
      PrintLinePoints("SLVE", result.TemperatureUnits, result.PressureUnits, result.Slve);
    }

    static void PrintLinePoints(string title, string temperatureUnit, string pressureUnit, IEnumerable<ApiOutputPhasediagramPoint> points)
    {
      if (!points.Any())
        return;

      PrintLine();
      PrintLine(title);
      PrintValue("Label");
      PrintValue($"Temperature [{temperatureUnit}]");
      PrintValue($"Pressure [{pressureUnit}]");
      PrintLine();
      foreach (var point in points)
      {
        if (point.Label != null)
          PrintValue(point.Label);
        else
          PrintValue(string.Empty);
        PrintValue(point.Temperature);
        PrintValue(point.Pressure);
        PrintLine();
      }
      PrintLine();
    }
  }
}