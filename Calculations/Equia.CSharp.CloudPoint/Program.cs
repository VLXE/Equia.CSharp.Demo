
// we intentionally block the thread until the calculation is complete for the sake of the sample
using Equia.CSharp.CloudPointSample;

var calculator = new CloudPointSample();
calculator.ExecuteAsync().Wait();
