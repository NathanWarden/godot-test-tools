# Godot Test Tools For Mono
A test runner implementation for the NUnit Framework


## Setup
The current setup for using the test runner is quite easy.

**1. Copy the "GodotTestTools" folder**

First, you can use it in your own project by either copy-pasting the GodotTestTools folder into your own project, or if your more sofisticated you can use this as a subrepository.

**2. Add the GodotTestTools.csproj to your Godot project**

In MonoDevelop:

1. Open your Godot game project's .sln file
1. Right click on the solution and choose "Add > Add Existing Project..." and then find and add the "GodotTestTools.csproj" project
1. In you game project, right click on "References" and select "Edit References..."
1. Click on the "Projects" tab and then click the checkbox for GodotTestTools

*(Note: I don't typically use Visual Studio, if someone wants to provide instructions for VS, feel free to contribute)*

**3. Call the test runner from your code**

Once you begin to write your code that will use the test runner, such as the examples below, make sure that the method you use has the `async` keyword.

It can currently be used in one of three ways:

1. Run the test runner and collect the results at the end
1. Run the test runner and get live results via a callback
1. A combination of 1 and 2

Example of option 1

``` C#
async public override void _Ready()
{
	TestRunner testRunner = new TestRunner();
	int passed = 0;
	int failed = 0;

	await testRunner.Run();

	TestResult[] testResults = testRunner.TestResults;
	foreach (TestResult testResult in testResults)
	{
		if (testResult.result == TestResult.Result.Failed)
		{
			GD.Print(testResult.classType.Name + "." + testResult.testMethod.Name + "\n" + testResult.exception.Message);
		}
	}
}
```

Example of option 2

``` C#
async public override void _Ready()
{
	TestRunner testRunner = new TestRunner();
	int passed = 0;
	int failed = 0;

	await testRunner.Run((TestResult testResult) =>
	{
		if (testResult.result == TestResult.Result.Failed)
		{
			GD.Print(testResult.classType.Name + "." + testResult.testMethod.Name + "\n" + testResult.exception.Message);
		}
	});
}
```


## Running
To run, add a node to your scene and a C# script that contains whichever method you decided to use from the examples in the Setup section.

Next, simply hit F6 to run the scene you just created.


## Contributing
For now, the only rules for contributing are:
1. Follow the normal casing rules for C#.
1. Indent using tabs, not spaces
1. Use uncuddled curly brackets
1. If you aren't sure about something, please open an issue and ask :)

And... thanks!
