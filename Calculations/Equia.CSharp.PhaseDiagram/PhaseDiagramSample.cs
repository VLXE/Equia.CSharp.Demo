using Equia.Api.Shared.ApiInput;
using Equia.Api.Shared.ApiOutput.Phasediagram;
using Equia.Api.Shared.Calculations.Phasediagram;
using Equia.Api.Shared.Client;
using Equia.Api.Shared.Utility;
using Equia.CSharp.Shared;
using Equia.CSharp.Shared.Fluids;

namespace Equia.CSharp.PhaseDiagramSample
{
  internal static class PhaseDiagramSample
  {
    public static async Task RunPhaseDiagramSampleAsync()
    {
      try
      {
        var client = CreateClient();
        var input = CreateInput(client);

        var result = await client.CallPhasediagramStandardAsync(input);

        if (result.Success && result.Curve is not null)
        {
          PrintPhaseDiagramResult(result.Curve);
        }
        else if (result.ExceptionInfo is not null)
          PrintExceptionInfo(result.ExceptionInfo);
      }
      catch (Exception ex)
      {
        PrintLine($"Message: {ex.Message}");
        PrintLine($"Stack Trace: {ex.StackTrace}");
      }
    }

    static ApiEquiaClient CreateClient()
    {
      return new ApiEquiaClient(new HttpClient(), SharedSettings.ApiUrl, SharedSettings.UserId, SharedSettings.AccessSecret);
    }

    static ApiPhasediagramCalculationInput CreateInput(ApiEquiaClient client)
    {
      var input = client.GetPhasediagamStandardInput();
      input.Fluid = Fluid_nHexane_Ethylene_HDPE7.Create();
      input.VLLE = true;
      input.Components = new List<ApiCalculationComposition> {
                new ApiCalculationComposition { Mass = 0.78 },
                new ApiCalculationComposition { Mass = 0.02 },
                new ApiCalculationComposition { Mass = 0.2 } };
      return input;
    }

    static void PrintExceptionInfo(ApiExceptionInfo exceptionInfo)
    {
      PrintLine($"Date: {exceptionInfo.Date}");
      PrintLine($"Message Type: {exceptionInfo.MessageType}");
      PrintLine($"Message: {exceptionInfo.Message}");
      PrintLine();
      PrintLine($"Stack Trace: {exceptionInfo.StackTrace}");
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
