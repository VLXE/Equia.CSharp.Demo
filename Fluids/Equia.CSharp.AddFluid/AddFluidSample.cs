using Equia.Api.Shared.Client;
using Equia.Api.Shared.Fluids.AddFluid;
using Equia.CSharp.Shared;
using Equia.CSharp.Shared.Fluids;

namespace Equia.CSharp.AddFluid
{
  /// <summary>
  /// Example of how to add a fluid to the cloud database
  /// Note that the id returned is needed if the fluid is to be retrived or used in API calculations
  /// If lost it can be found in the cloud client
  /// </summary>
  static class AddFluidSample
  {
    public static async Task ExecuteAsync()
    {
      try
      {
        var client = CreateClient();
        var input = CreateInput(client);

        var result = await client.CallAddFluidAsync(input);

        if (result.Success)
          PrintCalculationResult(result.FluidId.Value);
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

    static ApiAddFluidInput CreateInput(ApiEquiaClient client)
    {
      var input = client.AddFluidInput();
      input.Fluid = DemoFluid1_nHexane_Ethylene_HDPE7.Create();

      return input;
    }

    static void PrintValue(string input) => Console.Write(input.PadRight(25));
    static void PrintLine(string input = "") => Console.WriteLine(input);

    static void PrintCalculationResult(Guid fluidId)
    {
      PrintLine();
      PrintValue($"Fluid added. Id: '{fluidId.ToString().ToUpper()}'");
    }

  }
}