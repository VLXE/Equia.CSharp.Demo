
// we intentionally block the thread until the calculation is complete for the sake of the sample
using Equia.CSharp.Shared;

var executer = new GetStatusSample();
executer.ExecuteAsync().Wait();
