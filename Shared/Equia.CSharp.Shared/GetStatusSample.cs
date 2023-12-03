using Equia.Api.Shared.Calculations.Properties;
using Equia.Api.Shared.Client;

namespace Equia.CSharp.Shared
{
  /// <summary>
  /// Call getstatus to verify the backbone is running.
  /// </summary>
  class GetStatusSample : SharedBase
  {
    public async Task ExecuteAsync()
    {
      try
      {
        var client = CreateClient();
        var input = CreateInput(client);

        var result = await client.CallGetStatus(input);

        if (result.Success)
        {
          Console.WriteLine("Success");
          Console.WriteLine($"Backbonename: {result.Name}");
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

    static ApiStatusInput CreateInput(ApiEquiaClient client)
    {
      var input = client.GetStatusInput();
      return input;
    }

  }
}