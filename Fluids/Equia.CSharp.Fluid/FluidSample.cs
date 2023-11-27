using Equia.Api.Shared.Client;
using Equia.Api.Shared.Fluids.AddFluid;
using Equia.Api.Shared.Fluids.FluidParts;
using Equia.CSharp.Shared;
using Equia.CSharp.Shared.Fluids;

namespace Equia.CSharp.Fluid
{
  /// <summary>
  /// Example of how to add a fluid to the cloud database
  /// Note that the id returned is needed if the fluid is to be retrived or used in API calculations
  /// If lost it can be found in the cloud client
  /// </summary>
  static class FluidSample
  {
    public static async Task ExecuteAsync()
    {
      try
      {
        var client = CreateClient();

        //First we add the fluid to the server
        var inputAdd = CreateAddInput(client);

        var resultAdd = await client.CallAddFluidAsync(inputAdd);

        if (resultAdd.Success)
          PrintFluidResult(resultAdd.FluidId.Value);
        else
        {
          HandleExceptions.PrintExceptionInfo(resultAdd.ExceptionInfo);
          FinishUp();
          return;
        }
        if(await GetFluid(client, resultAdd))
          await DeleteFluid(client, resultAdd);

        FinishUp();
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

    private static void FinishUp()
    {
      Console.WriteLine(string.Empty);
      Console.WriteLine("Press any key to close");
      Console.ReadKey();
    }

    static async Task<bool> GetFluid(ApiEquiaClient client, ApiAddFluidOutput resultAdd)
    {
      var inputGet = client.GetFluidInput();
      inputGet.FluidId = resultAdd.FluidId.Value;

      var resultGet = await client.CallGetFluidAsync(inputGet);

      if (resultGet.Success && resultGet.Fluid is not null)
      {
        PrintFluidInfo(resultGet.Fluid);
        return true;
      }
      HandleExceptions.PrintExceptionInfo(resultGet.ExceptionInfo);
      return false;
    }

    static async Task DeleteFluid(ApiEquiaClient client, ApiAddFluidOutput resultAdd)
    {
      var inputDelete = client.DeleteFluidInput();
      inputDelete.FluidId = resultAdd.FluidId.Value;

      var resultDelete = await client.CallDeleteFluidAsync(inputDelete);

      PrintLine();
      if (resultDelete.Success)
        Console.WriteLine("Fluid deleted on server.");
      else
        HandleExceptions.PrintExceptionInfo(resultDelete.ExceptionInfo);
    }

    static ApiEquiaClient CreateClient()
    {
      return new ApiEquiaClient(new HttpClient(), SharedSettings.ApiUrl, SharedSettings.AccessKey);
    }

    static ApiAddFluidInput CreateAddInput(ApiEquiaClient client)
    {
      var input = client.AddFluidInput();
      input.Fluid = DemoFluid1_nHexane_Ethylene_HDPE7.Create();
      input.Fluid.Name = "FluidSample"; //Fluid names must be unique.
      return input;
    }

    static void PrintValue(string input) => Console.Write(input.PadRight(25));
    static void PrintLine(string input = "") => Console.WriteLine(input);

    static void PrintFluidResult(Guid fluidId)
    {
      PrintLine();
      PrintLine($"Fluid added: '{fluidId.ToString().ToUpper()}'");
    }
    static void PrintFluidInfo(ApiFluid fluid)
    {
      PrintLine();
      PrintLine("Fluid obtained from server.");
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