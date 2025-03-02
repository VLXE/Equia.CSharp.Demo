
// we intentionally block the thread until the calculation is complete for the sake of the sample
using Equia.CSharp.BatchFlashSample;

var calculator = new BatchFlashSample();
calculator.ExecuteAsync().Wait();
