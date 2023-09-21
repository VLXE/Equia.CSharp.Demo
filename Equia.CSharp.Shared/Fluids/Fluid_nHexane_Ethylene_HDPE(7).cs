using Equia.Api.Shared.Fluids.FluidParts;

namespace Equia.CSharp.Shared.Fluids
{
  public class Fluid_nHexane_Ethylene_HDPE7
  {
    public static ApiFluid Create()
    {
      var fluid = new ApiFluid
      {
        Name = "nHexane + Ethylene + HDPE(7) - API C#",
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
        PcSaftEpsilon = 236.77
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
        PcSaftEpsilon = 176.47
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
        SleDensityCrystalline = 1.004
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

      fluid.Kij.Add(new ApiFluidKij { IndexI = 0, IndexJ = 1, Kija = 0.0001 });
      fluid.Kij.Add(new ApiFluidKij { IndexI = 0, IndexJ = 2, Kija = 0.0002 });
      fluid.Kij.Add(new ApiFluidKij { IndexI = 1, IndexJ = 2, Kija = 0.0003 });

      return fluid;
    }
  }
}