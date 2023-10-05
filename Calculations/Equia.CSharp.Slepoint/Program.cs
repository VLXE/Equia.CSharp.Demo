
// we intentionally block the thread until the calculation is complete for the sake of the sample
using Equia.CSharp.Slepoint;

var calculator = new SlepointSample();
calculator.ExecuteAsync().Wait();

