using Equia.Api.Shared.Fluids.FluidParts;

namespace Equia.CSharp.Shared.Fluids
{
  /// <summary>
  /// Demo fluid 1
  /// Example of fluid with two solvents and one monodisperse polymer with 7 pseudo components
  /// </summary>
  public class DemoFluid1_nHexane_Ethylene_HDPE7
  {
    public static ApiFluid GetFluid()
    {
      var fluid = new ApiFluid
      {
        Name = "nHexane + Ethylene + HDPE(7)",
        Comment = "Used by API C# demo",
        Eos = "PC-SAFT",
        PropertyReferencePoint = "Original",
        SolventCp = "DIPPR",
        PolymerCp = "Polynomial"
      };

      var nHexane = new ApiFluidStandardComponent
      {
        Name = "n-Hexane",
        IsAlkane = true,
        MolarMass = 86.17536,

        AcentricFactor = 0.301261,
        CriticalPressure = 30.25,
        CriticalTemperature = 507.6,

        PcSaftdm = 0.03548,
        PcSaftSigma0 = 3.7983,
        PcSaftEpsilon = 236.77,

        IdealGasEnthalpyOfFormation = -1937.212679,

        PcSaftCpIgDipprC0 = 1.21148319,
        PcSaftCpIgDipprC1 = 4.088175553,
        PcSaftCpIgDipprC2 = 1694.6,
        PcSaftCpIgDipprC3 = 2.749045667,
        PcSaftCpIgDipprC4 = 761.6,
        PcSaftCpIgDipprC5 = 200,
        PcSaftCpIgDipprC6 = 1500,
      };

      var ethylene = new ApiFluidStandardComponent
      {
        Name = "Ethylene",
        IsAlkane = true,
        MolarMass = 28.05316,

        AcentricFactor = 0.087,
        CriticalPressure = 50.41,
        CriticalTemperature = 282.34,

        PcSaftdm = 0.0567914,
        PcSaftSigma0 = 3.445,
        PcSaftEpsilon = 176.47,

        IdealGasEnthalpyOfFormation = 1871.80339,

        PcSaftCpIgDipprC0 = 1.189883778,
        PcSaftCpIgDipprC1 = 3.37894198,
        PcSaftCpIgDipprC2 = 1596,
        PcSaftCpIgDipprC3 = 1.964128105,
        PcSaftCpIgDipprC4 = 740.8,
        PcSaftCpIgDipprC5 = 60,
        PcSaftCpIgDipprC6 = 1500

      };

      var hdpe7 = new ApiFluidPolymerComponent
      {
        ShortName = "HDPE",
      };
      var block = new ApiFluidPolymerBlock
      {
        BlockMassfraction = 1,
        BlockName = "HDPE",
        MonomerMolarMass = 28.054,
        MonomerName = "Ethylene",
        PcSaftdm = 0.0263,
        PcSaftSigma0 = 4.0217,
        PcSaftEpsilon = 252.0,
        SleC = 0.4,
        SleHu = 8220.0,
        SleDensityAmorphous = 0.853,
        SleDensityCrystalline = 1.004,

        IdealGasEnthalpyOfFormation = -1613.2549,

        PcSaftCpIgPolyC0 = 0.81694,
        PcSaftCpIgPolyC1 = -0.00030569,
        PcSaftCpIgPolyC2 = 0.000015706,
        PcSaftCpIgPolyC3 = -2.1058E-08,
        PcSaftCpIgPolyC4 = 8.5078E-12,
        PcSaftCpIgPolyC5 = 0,
        PcSaftCpIgPolyC6 = 200,
        PcSaftCpIgPolyC7 = 1000
      };

      hdpe7.Blocks.Add(block);

      hdpe7.PseudoComponents.Add(new ApiFluidPseudoComponent { Name = "HDPE(17.3)", MeltingTemperature = 415.82, MolarMass = 17300, Massfraction = 0.00498 });
      hdpe7.PseudoComponents.Add(new ApiFluidPseudoComponent { Name = "HDPE(25.6)", MeltingTemperature = 416.38, MolarMass = 25600, Massfraction = 0.03067 });
      hdpe7.PseudoComponents.Add(new ApiFluidPseudoComponent { Name = "HDPE(36)", MeltingTemperature = 416.72, MolarMass = 36000, Massfraction = 0.23902 });
      hdpe7.PseudoComponents.Add(new ApiFluidPseudoComponent { Name = "HDPE(50)", MeltingTemperature = 416.95, MolarMass = 50000, Massfraction = 0.45515 });
      hdpe7.PseudoComponents.Add(new ApiFluidPseudoComponent { Name = "HDPE(69.2)", MeltingTemperature = 417.12, MolarMass = 69200, Massfraction = 0.23902 });
      hdpe7.PseudoComponents.Add(new ApiFluidPseudoComponent { Name = "HDPE(97.6)", MeltingTemperature = 417.24, MolarMass = 97600, Massfraction = 0.03067 });
      hdpe7.PseudoComponents.Add(new ApiFluidPseudoComponent { Name = "HDPE(144)", MeltingTemperature = 417.34, MolarMass = 144000, Massfraction = 0.0005 });

      fluid.Standards.Add(nHexane);
      fluid.Standards.Add(ethylene);
      fluid.Polymers.Add(hdpe7);

      return fluid;
    }
  }
}