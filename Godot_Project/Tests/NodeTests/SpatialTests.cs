using Godot;
using NUnit.Framework;

namespace GodotTestingProject.Tests.NodeTests
{
	[TestFixture]
	public class SpatialTests
	{
		const float marginOfError = 0.01f;
		Spatial spatial;


		[SetUp]
		public void SetUp()
		{
			spatial = new Spatial();
		}

		[Test]
		public void BasisZShouldBePointSeven_Zero_PointSeven_WhenRotating45Degrees()
		{
			float radians = Mathf.PI / 4.0f;

			spatial.RotateY(radians);
			CompareVector3s(spatial.Transform.basis.z, new Vector3(0.7f,0,0.7f), marginOfError);
		}

		void CompareVector3s(Vector3 actual, Vector3 expected, float within = 0)
		{
			Assert.That(actual.x, Is.EqualTo(expected.x).Within(within));
			Assert.That(actual.y, Is.EqualTo(expected.y).Within(within));
			Assert.That(actual.z, Is.EqualTo(expected.z).Within(within));
		}
	}
}