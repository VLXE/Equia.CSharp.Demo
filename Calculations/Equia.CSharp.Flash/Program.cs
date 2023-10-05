
// we intentionally block the thread until the calculation is complete for the sake of the sample
using Equia.CSharp.FlashSample;

var calculator = new FlashSample();
calculator.ExecuteAsync().Wait();
