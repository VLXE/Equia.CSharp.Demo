using Equia.Api.Shared.Client;
using Equia.Api.Shared.Fluids.FluidParts;
using Equia.Api.Shared.Utility;
using Equia.CSharp.Shared;

namespace Equia.CSharp.RequestFluidSample
{
  internal static class RequestFluidSample
  {
    public static async Task RunRequestFluidSampleAsync()
    {
      try
      {
        var client = CreateClient();
        var input = client.GetFluidInput();
        input.FluidId = new Guid("Fluid id here");

        var result = await client.CallGetFluidAsync(input);

        if (result.Success && result.Fluid is not null)
          PrintFluidInfo(result.Fluid);
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

    static void PrintExceptionInfo(ApiExceptionInfo exceptionInfo)
    {
      PrintLine($"Date: {exceptionInfo.Date}");
      PrintLine($"Message Type: {exceptionInfo.MessageType}");
      PrintLine($"Message: {exceptionInfo.Message}");
      PrintLine(string.Empty);
      PrintLine($"Stack Trace: {exceptionInfo.StackTrace}");
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