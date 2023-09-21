﻿using Equia.Api.Shared.ApiInput;
using Equia.Api.Shared.ApiOutput.Point;
using Equia.Api.Shared.Calculations.CloudPoint;
using Equia.Api.Shared.Client;
using Equia.Api.Shared.Utility;
using Equia.CSharp.Shared;
using Equia.CSharp.Shared.Fluids;

namespace Equia.CSharp.CloudPointSample
{
  internal static class CloudPointSample
  {
    public static async Task RunCloudPointSampleAsync()
    {
      try
      {
        var client = CreateClient();
        var input = CreateInput(client);

        var result = await client.CallCloudPointAsync(input);

        if (result.Success && result.Point is not null)
        {
          PrintCalculationResult(result.Point);
        }
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

    static ApiCloudPointCalculationInput CreateInput(ApiEquiaClient client)
    {
      var input = client.GetCloudPointInput();
      input.Fluid = Fluid_nHexane_Ethylene_HDPE7.Create();
      input.Pressure = 25;
      input.PointType = "Fixed Pressure";
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

    static void PrintCalculationResult(ApiOutputCalculationResultPoint result)
    {
      PrintLine();
      PrintValue("Property");
      foreach (var phase in result.Phases)
        PrintValue(phase.PhaseLabel);
      PrintLine();

      PrintValue($"Temperature [{result.Temperature.Units}]");
      PrintValue(result.Temperature.Value);
      PrintLine();
      PrintValue($"Pressure [{result.Pressure.Units}]");
      PrintValue(result.Pressure.Value);
      PrintLine();

      PrintComposition(result);
      PrintProperties(result);
      PrintPolymerMoments(result);
      PrintPolymerDistributions(result);
    }

    static void PrintComposition(ApiOutputCalculationResultPoint result)
    {
      PrintLine();
      PrintLine("Components");
      var firstPhase = result.Phases[0];
      foreach (var compIndex in Enumerable.Range(0, firstPhase.Composition.Composition.Components.Count))
      {
        PrintValue($"{firstPhase.Composition.Composition.Components[compIndex].Name} [{firstPhase.Composition.CompositionUnits}]");
        foreach (var phase in result.Phases)
          PrintValue(phase.Composition.Composition.Components[compIndex].Value);
        PrintLine();
      }
    }

    static void PrintProperties(ApiOutputCalculationResultPoint result)
    {
      var firstPhase = result.Phases[0];
      PrintLine();
      PrintValue($"Phase Fraction [Mole]");
      foreach (var phase in result.Phases)
        PrintValue(phase.MolePercent.Value);
      PrintLine();
      PrintValue($"Phase Fraction [Weight]");
      foreach (var phase in result.Phases)
        PrintValue(phase.WeightPercent.Value);
      PrintLine();
      PrintValue($"Compressibility [-]");
      foreach (var phase in result.Phases)
        PrintValue(phase.Compressibility.Value);
      PrintLine();
      PrintValue($"Density [{firstPhase.Density.Units}]");
      foreach (var phase in result.Phases)
        PrintValue(phase.Density.Value);
      PrintLine();
      PrintValue($"Molar Volumne [{firstPhase.Volume.Units}]");
      foreach (var phase in result.Phases)
        PrintValue(phase.Volume.Value);
      PrintLine();
      PrintValue($"Enthalpy [{firstPhase.Enthalpy.Units}]");
      foreach (var phase in result.Phases)
        PrintValue(phase.Enthalpy.Value);
      PrintLine();
      PrintValue($"Entropy [{firstPhase.Entropy.Units}]");
      foreach (var phase in result.Phases)
        PrintValue(phase.Entropy.Value);
      PrintLine();
      PrintValue($"Cp [{firstPhase.Cp.Units}]");
      foreach (var phase in result.Phases)
        PrintValue(phase.Cp.Value);
      PrintLine();
      PrintValue($"Cv [{firstPhase.Cv.Units}]");
      foreach (var phase in result.Phases)
        PrintValue(phase.Cv.Value);
      PrintLine();
      PrintValue($"JTCoefficient [{firstPhase.JTCoefficient.Units}]");
      foreach (var phase in result.Phases)
        PrintValue(phase.JTCoefficient.Value);
      PrintLine();
      PrintValue($"Velocity of sound [{firstPhase.SpeedOfSound.Units}]");
      foreach (var phase in result.Phases)
        PrintValue(phase.SpeedOfSound.Value);
      PrintLine();
      PrintValue($"Molecular Weight [{firstPhase.MolecularWeight.Units}]");
      foreach (var phase in result.Phases)
        PrintValue(phase.MolecularWeight.Value);
      PrintLine();
    }

    static void PrintPolymerMoments(ApiOutputCalculationResultPoint result)
    {
      var first_phase_moments = result.Phases[0].PolymerMoments;
      foreach (var momentIndex in Enumerable.Range(0, first_phase_moments.Polymers.Count))
      {
        PrintValue($"Mn ({first_phase_moments.Polymers[momentIndex].PolymerName}) [{first_phase_moments.MomentUnits}]");
        foreach (var phase in result.Phases)
          PrintValue(phase.PolymerMoments.Polymers[momentIndex].Mn);
        PrintLine();

        PrintValue($"Mw ({first_phase_moments.Polymers[momentIndex].PolymerName}) [{first_phase_moments.MomentUnits}]");
        foreach (var phase in result.Phases)
          PrintValue(phase.PolymerMoments.Polymers[momentIndex].Mw);
        PrintLine();

        PrintValue($"Mz ({first_phase_moments.Polymers[momentIndex].PolymerName}) [{first_phase_moments.MomentUnits}]");
        foreach (var phase in result.Phases)
          PrintValue(phase.PolymerMoments.Polymers[momentIndex].Mz);
        PrintLine();
      }
    }

    static void PrintPolymerDistributions(ApiOutputCalculationResultPoint result)
    {
      var firstPhase = result.Phases[0];
      // find components with distribution (polymers)
      foreach (var compIndex in Enumerable.Range(0, firstPhase.Composition.Composition.Components.Count))
      {
        var component = firstPhase.Composition.Composition.Components[compIndex];
        if (component.Distribution is null || !component.Distribution.Any())
          continue;

        // just print the name of the polymer on top of each phase column
        PrintValue(string.Empty);
        foreach (var phaseIndex in Enumerable.Range(0, result.Phases.Count))
          PrintValue(component.Name);

        // now print the actual distribution values for each phase
        foreach (var distIndex in Enumerable.Range(0, component.Distribution.Count))
        {
          PrintLine();
          PrintValue(string.Empty);
          foreach (var phaseIndex in Enumerable.Range(0, result.Phases.Count))
          {
            var distributions = result.Phases[phaseIndex].Composition.Composition.Components[compIndex].Distribution;
            if (distributions is null)
            {
              PrintValue(string.Empty);
              continue;
            }

            var distribution = distributions[distIndex];
            PrintValue(distribution.Value);
          }
        }
      }
    }
  }
}