using Equia.Api.Shared.Client;
using Equia.Api.Shared.Fluids.AddFluid;
using Equia.Api.Shared.Utility;
using Equia.CSharp.Shared;
using Equia.CSharp.Shared.Fluids;

namespace Equia.CSharp.AddFluid
{
  internal static class FluidSample
  {
    public static async Task RunFluidSampleAsync()
    {
      try
      {
        var client = CreateClient();
        var input = CreateInput(client);

        var result = await client.CallAddFluidAsync(input);

        if (result.Success)
          PrintCalculationResult(result.FluidId.Value);
        else if (result.ExceptionInfo is not null)
          PrintExceptionInfo(result.ExceptionInfo);

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

    static ApiAddFluidInput CreateInput(ApiEquiaClient client)
    {
      var input = client.AddFluidInput();
      input.Fluid = Fluid_nHexane_Ethylene_HDPE7.Create();
      input.UserId = new Guid(SharedSettings.UserId);

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

    static void PrintValue(string input) => Console.Write(input.PadRight(25));
    static void PrintLine(string input = "") => Console.WriteLine(input);

    static void PrintCalculationResult(Guid fluidId)
    {
      PrintLine();
      PrintValue($"Fluid added. Id: '{fluidId.ToString().ToUpper()}'");
    }

  }
}