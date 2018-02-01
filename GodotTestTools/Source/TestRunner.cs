using System;
using System.Reflection;
using System.Collections.Generic;
using NUnit.Framework;
using System.Threading.Tasks;

namespace GodotTestTools
{
	public class TestRunner
	{
		bool requireTestFixture;

		public int currentTest { get; private set; }
		public int testCount
		{
			get
			{
				if ( testMethods != null )
				{
					return testMethods.Count;
				}

				return 0;
			}
		}

		Dictionary<MethodInfo, object> testMethods = new Dictionary<MethodInfo, object>();
		Dictionary<Type, MethodInfo> setupMethods = new Dictionary<Type, MethodInfo>();
		Dictionary<Type, MethodInfo> teardownMethods = new Dictionary<Type, MethodInfo>();

		List<TestResult> testResults = new List<TestResult>();
		public TestResult[] TestResults { get { return testResults.ToArray(); } }

		public delegate void TestResultDelegate(TestResult testResult);


		public TestRunner(bool requireTestFixture = true)
		{
			this.requireTestFixture = requireTestFixture;

			IterateThroughAssemblies();
		}


		public async Task Run(TestResultDelegate resultCallback = null)
		{
			foreach ( MethodInfo method in testMethods.Keys )
			{
				object testObject = testMethods[method];
				TestResult testResult = new TestResult(method, null, TestResult.Result.Passed);

				try
				{
					if ( setupMethods.ContainsKey(method.DeclaringType) )
					{
						setupMethods[method.DeclaringType].Invoke(testObject,  new object[] {});
					}

					method.Invoke(testObject, new object[] {});
				}
				catch ( Exception e )
				{
					testResult = new TestResult(method, e.InnerException ?? e, TestResult.Result.Failed);
				}
				finally
				{
					testResults.Add(testResult);

					if ( teardownMethods.ContainsKey(method.DeclaringType) )
					{
						teardownMethods[method.DeclaringType].Invoke(testObject,  new object[] {});
					}
				}

				if ( resultCallback != null )
				{
					resultCallback(testResult);
				}

				await Task.Delay(1);
			}
		}


		void IterateThroughAssemblies()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			foreach ( Assembly assembly in assemblies )
			{
				IterateThroughTypes(assembly.GetTypes());
			}
		}


		void IterateThroughTypes(Type[] types)
		{
			foreach ( Type type in types )
			{
				TestFixtureAttribute testFixtureAttr = Attribute.GetCustomAttribute(type, typeof(TestFixtureAttribute), false) as TestFixtureAttribute;

				if (testFixtureAttr != null || !requireTestFixture)
				{
					MethodInfo[] methods = type.GetMethods();

					foreach ( MethodInfo method in methods )
					{
						TestAttribute testAttr = Attribute.GetCustomAttribute(method, typeof(TestAttribute), false) as TestAttribute;
						SetUpAttribute setupAttr = Attribute.GetCustomAttribute(method, typeof(SetUpAttribute), false) as SetUpAttribute;
						TearDownAttribute teardownAttr = Attribute.GetCustomAttribute(method, typeof(TearDownAttribute), false) as TearDownAttribute;

						if ( testAttr != null )
						{
							try
							{
								ConstructorInfo[] constructors = type.GetConstructors();
								object curTestObject = null;

								if ( constructors.Length > 0 )
								{
									curTestObject = constructors[0].Invoke(null);
								}

								if ( curTestObject != null )
								{
									testMethods[method] = curTestObject;
								}
							}
							catch
							{
								// Fail the test here?
							}
						}
						else if ( setupAttr != null )
						{
							setupMethods.Add(type, method);
						}
						else if ( teardownAttr != null )
						{
							teardownMethods.Add(type, method);
						}
					}
				}
			}
		}
	}


	public struct TestResult
	{
		public Type classType { get { return testMethod.DeclaringType; } }
		public MethodInfo testMethod { get; private set; }
		public Exception exception { get; private set; }
		public Result result { get; private set; }

		public enum Result
		{
			Passed,
			Failed
		}

		public TestResult(MethodInfo testMethod, Exception exception, Result result)
		{
			this.testMethod = testMethod;
			this.exception = exception;
			this.result = result;
		}
	}
}