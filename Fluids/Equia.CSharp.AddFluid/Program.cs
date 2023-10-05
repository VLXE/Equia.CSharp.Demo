using Equia.CSharp.AddFluid;

// we intentionally block the thread until the calculation is complete for the sake of the sample
AddFluidSample.ExecuteAsync().Wait();
