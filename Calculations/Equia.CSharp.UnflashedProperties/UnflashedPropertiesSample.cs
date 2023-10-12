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
      return new ApiEquiaClient(new HttpClient(), SharedSettings.ApiUrl, SharedSettings.AccessKey);
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
      PrintLine();

      var residual = result.Residual;
      var ideal = result.Ideal;
      var sum = result.Sum;

      Console.WriteLine(GetLine("Name", "Residual", "Ideal", "Sum"));
      Console.WriteLine(GetLine($"Volume [{sum.Volume.Units}]", residual.Volume.Value, ideal.Volume.Value, sum.Volume.Value));
      Console.WriteLine(GetLine($"Enthalpy [{sum.Enthalpy.Units}]", residual.Enthalpy.Value, ideal.Enthalpy.Value, sum.Enthalpy.Value));
      Console.WriteLine(GetLine($"Entropy [{sum.Entropy.Units}]", residual.Entropy.Value, ideal.Entropy.Value, sum.Entropy.Value));
      Console.WriteLine(GetLine($"Cp [{sum.Cp.Units}]", residual.Cp.Value, ideal.Cp.Value, sum.Cp.Value));
      Console.WriteLine(GetLine($"Cv [{sum.Cv.Units}]", residual.Cv.Value, ideal.Cv.Value, sum.Cv.Value));
      Console.WriteLine(GetLine($"GibbsEnergy [{sum.GibbsEnergy.Units}]", residual.GibbsEnergy.Value, ideal.GibbsEnergy.Value, sum.GibbsEnergy.Value));
      Console.WriteLine(GetLine($"InternalEnergy [{sum.InternalEnergy.Units}]", residual.InternalEnergy.Value, ideal.InternalEnergy.Value, sum.InternalEnergy.Value));
      Console.WriteLine(GetLine($"HelmholtzEnergy [{sum.HelmholtzEnergy.Units}]", residual.HelmholtzEnergy.Value, ideal.HelmholtzEnergy.Value, sum.HelmholtzEnergy.Value));
    }

    static string GetLine(string name, string residual, string ideal, string sum)
    {
      return name.PadRight(25) + residual.PadRight(25) + ideal.PadRight(25) + sum.PadRight(25);
    }

    static string GetLine(string name, double residual, double ideal, double sum)
    {
      return GetLine(name, residual.ToString(), ideal.ToString(), sum.ToString());
    }
  }
}