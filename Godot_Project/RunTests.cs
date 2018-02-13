using Godot;
using GodotTestTools;

public class RunTests : Node
{
	[Export] bool enabled;


	async public override void _Ready()
	{
		if (!enabled) return;

		TestRunner testRunner = new TestRunner();
		int passed = 0;
		int failed = 0;

		GD.Print("\nTest Results:\n");

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
	}
}