using Equia.CSharp.Fluid;

// we intentionally block the thread until the calculation is complete for the sake of the sample
FluidSample.ExecuteAsync().Wait();
