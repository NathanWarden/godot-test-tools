using Godot;
using GodotTestTools;

public class RunTests : Node
{
	async public override void _Ready()
	{
		TestRunner testRunner = new TestRunner();
		int passed = 0;
		int failed = 0;

		await testRunner.Run((TestResult testResult) =>
		{
			if (testResult.result == TestResult.Result.Failed)
			{
				failed++;
				GD.Print(testResult.classType.Name + "." + testResult.testMethod.Name + "\n" + testResult.exception.Message);
			}
			else
			{
				passed++;
			}
		});

		GD.Print("Success: " + passed);
		GD.Print("Failed: " + failed);

		/*
		// You can also collect results once all tests have been run
		TestResult[] testResults = testRunner.TestResults;
		foreach (TestResult testResult in testResults)
		{
			if (testResult.result == TestResult.Result.Failed)
			{
				GD.Print(testResult.classType.Name + "." + testResult.testMethod.Name + "\n" + testResult.exception.Message);
			}
		}
		*/
	}
}