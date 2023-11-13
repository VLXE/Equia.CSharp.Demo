using Equia.Api.Shared.Utility;

namespace Equia.CSharp.Shared
{
  /// <summary>
  /// Shared code for handling exceptions
  /// </summary>
  public class HandleExceptions
  {
    public static void PrintExceptionInfo(ApiExceptionInfo? exceptionInfo)
    {
      if(exceptionInfo == null) 
      {
        Console.WriteLine("Unknown error. Please report to VLXE.");
        return;
      }

      PrintLine($"Date: {exceptionInfo.Date}");
      PrintLine($"Message Type: {exceptionInfo.MessageType}");
      PrintLine($"Message: {exceptionInfo.Message}");
      PrintLine();
      PrintLine($"Stack Trace: {exceptionInfo.StackTrace}");
    }

    static void PrintLine(string input = "") => Console.WriteLine(input);

  }
}