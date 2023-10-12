using Equia.Api.Shared.Client;
using Equia.Api.Shared.Fluids.FluidParts;
using Equia.CSharp.Shared;

namespace Equia.CSharp.RequestFluidSample
{
  /// <summary>
  /// Example of how a fluid can be retrived from the cloud database
  /// </summary>
  static class GetFluidSample
  {
    public static async Task ExecuteAsync()
    {
      try
      {
        var client = CreateClient();
        var input = client.GetFluidInput();
        input.FluidId = new Guid("Fluid id here");

        var result = await client.CallGetFluidAsync(input);

        if (result.Success && result.Fluid is not null)
          PrintFluidInfo(result.Fluid);
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

    static void PrintLine(string input) => Console.WriteLine(input);

    static void PrintFluidInfo(ApiFluid fluid)
    {
      PrintLine($"Fluid: {fluid.Name}");
      PrintLine($"Comment: {fluid.Comment}");
      PrintLine($"EoS: {fluid.Eos}");
      PrintLine($"Solvent Cp: {fluid.SolventCp}");
      PrintLine($"Polymer Cp: {fluid.PolymerCp}");

      PrintLine($"Property reference point: {fluid.PropertyReferencePoint}");

      PrintLine($"No standard components: {fluid.Standards.Count}");
      PrintLine($"No polymers: {fluid.Polymers.Count}");
    }

  }
}