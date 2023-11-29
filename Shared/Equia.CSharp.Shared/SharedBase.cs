using Equia.Api.Shared.ApiOutput.Point;
using Equia.Api.Shared.Client;

namespace Equia.CSharp.Shared
{
  /// <summary>
  /// Code shared between the samples
  /// </summary>
  public class SharedBase
  {
    static void PrintValue(double input) => PrintValue(input.ToString("E6").PadLeft(22));
    static void PrintValue(string input) => Console.Write(input.PadRight(30));
    static void PrintValuePadLeft(string input) => Console.Write(input.PadLeft(22));
    protected static void PrintLine(string input = "") => Console.WriteLine(input);

    protected static ApiEquiaClient CreateClient()
    {
      return new ApiEquiaClient(new HttpClient(), SharedSettings.ApiUrl, SharedSettings.AccessKey);
    }


    /// <summary>
    /// Write result to console.
    /// Is shared between several calculations since output format is the same in VLXE
    /// </summary>
    /// <param name="result"></param>
    protected static void PrintCalculationResult(ApiOutputCalculationResultPoint result)
    {
      PrintLine();
      PrintValue("Property");
      foreach (var phase in result.Phases)
        PrintValuePadLeft(phase.PhaseLabel);
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
      var firstPhase = result.Phases[0].PolymerMoments;
      for (var momentIndex = 0; momentIndex < firstPhase.Polymers.Count; momentIndex++)
      {
        PrintValue($"Mn ({firstPhase.Polymers[momentIndex].PolymerName}) [{firstPhase.MomentUnits}]");
        foreach (var phase in result.Phases)
          PrintValue(phase.PolymerMoments.Polymers[momentIndex].Mn);
        PrintLine();

        PrintValue($"Mw ({firstPhase.Polymers[momentIndex].PolymerName}) [{firstPhase.MomentUnits}]");
        foreach (var phase in result.Phases)
          PrintValue(phase.PolymerMoments.Polymers[momentIndex].Mw);
        PrintLine();

        PrintValue($"Mz ({firstPhase.Polymers[momentIndex].PolymerName}) [{firstPhase.MomentUnits}]");
        foreach (var phase in result.Phases)
          PrintValue(phase.PolymerMoments.Polymers[momentIndex].Mz);
        PrintLine();
      }
    }

    static void PrintPolymerDistributions(ApiOutputCalculationResultPoint result)
    {
      var firstPhase = result.Phases[0];
      for (int compIndex = 0; compIndex < firstPhase.Composition.Composition.Components.Count; compIndex++)
      {
        var component = firstPhase.Composition.Composition.Components[compIndex];
        if (component.Distribution is null || !component.Distribution.Any())
          continue;

        PrintValue(string.Empty);
        for (int phaseIndex = 0; phaseIndex < result.Phases.Count; phaseIndex++)
          PrintValuePadLeft(component.Name);

        for (int distIndex = 0; distIndex < component.Distribution.Count; distIndex++)
        {
          PrintLine();
          for (int phaseIndex = 0; phaseIndex < result.Phases.Count; phaseIndex++)
          {
            var distributions = result.Phases[phaseIndex].Composition.Composition.Components[compIndex].Distribution;
            if (distributions is null)
            {
              PrintValue(string.Empty);
              continue;
            }

            var distribution = distributions[distIndex];
            if (phaseIndex == 0)
              PrintValue(distribution.Name);
            PrintValue(distribution.Value);
          }
        }
      }
    }

  }
}