using Godot;
using NUnit.Framework;

namespace GodotTestingProject.Tests.StructTests
{
	[TestFixture]
	public class BasisTests
	{
		const float marginOfError = 0.01f;
		Basis b;


		[SetUp]
		public void SetUp()
		{
			b = new Basis(Quat.Identity);
		}

		[Test]
		public void ZShouldBeAtZeroZeroOneWhenFacingForward()
		{
			CompareVector3s(b.z, new Vector3(0,0,1), marginOfError);
		}

		[Test]
		public void ZShouldBeAt_NegativePointSeven_Zero_PointSeven_WhenRotated45()
		{
			b = b.Rotated(new Vector3(0,1,0), Mathf.PI / 4.0f);
			CompareVector3s(b.z, new Vector3(0.7f,0,0.7f), marginOfError);
		}


		void CompareVector3s(Vector3 actual, Vector3 expected, float within = 0)
		{
			Assert.That(actual.x, Is.EqualTo(expected.x).Within(within));
			Assert.That(actual.y, Is.EqualTo(expected.y).Within(within));
			Assert.That(actual.z, Is.EqualTo(expected.z).Within(within));
		}
	}
}