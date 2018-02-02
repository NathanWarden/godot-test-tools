using Godot;
using NUnit.Framework;

namespace GodotTestingProject.Tests.NodeTests
{
	[TestFixture]
	public class SpatialTests
	{
		const float marginOfError = 0.01f;
		Spatial spatial;


		// Z basis after Y Rotations

		[SetUp]
		public void SetUp()
		{
			spatial = new Spatial();
		}

		[Test]
		public void BasisZShouldBePointSeven_Zero_PointSeven_WhenRotating45DegreesAround_Y()
		{
			spatial.RotateY(Mathf.PI / 4.0f);
			CompareVector3s(spatial.Transform.basis.z, new Vector3(0.7f,0,0.7f), marginOfError);
		}

		[Test]
		public void BasisZShouldBeAt_One_Zero_Zero_WhenRotated90Around_Y()
		{
			spatial.RotateY(Mathf.PI / 2.0f);
			CompareVector3s(spatial.Transform.basis.z, new Vector3(1,0,0), marginOfError);
		}

		[Test]
		public void BasisZShouldBeAt_PointSeven_Zero_NegativePointSeven_WhenRotated135Around_Y()
		{
			spatial.RotateY(Mathf.PI * (3.0f/4.0f));
			CompareVector3s(spatial.Transform.basis.z, new Vector3(0.7f,0,-0.7f), marginOfError);
		}

		[Test]
		public void BasisZShouldBeAt_Zero_Zero_NegativeOne_WhenRotated180Around_Y()
		{
			spatial.RotateY(Mathf.PI);
			CompareVector3s(spatial.Transform.basis.z, new Vector3(0,0,-1), marginOfError);
		}

		[Test]
		public void BasisZShouldBeAt_NegativePointSeven_Zero_NegativePointSeven_WhenRotated225Around_Y()
		{
			spatial.RotateY(Mathf.PI + Mathf.PI / 4.0f);
			CompareVector3s(spatial.Transform.basis.z, new Vector3(-0.7f,0,-0.7f), marginOfError);
		}

		[Test]
		public void BasisZShouldBeAt_NegativeOne_Zero_Zero_WhenRotated270Around_Y()
		{
			spatial.RotateY(Mathf.PI + Mathf.PI / 2.0f);
			CompareVector3s(spatial.Transform.basis.z, new Vector3(-1,0,0), marginOfError);
		}

		[Test]
		public void BasisZShouldBeAt_PointSeven_Zero_NegativePointSeven_WhenRotated315Around_Y()
		{
			spatial.RotateY(Mathf.PI + Mathf.PI * (3.0f/4.0f));
			CompareVector3s(spatial.Transform.basis.z, new Vector3(-0.7f,0,0.7f), marginOfError);
		}

		[Test]
		public void BasisZShouldBeAt_Zero_Zero_One_WhenRotated360Around_Y()
		{
			spatial.RotateY(Mathf.PI * 2.0f);
			CompareVector3s(spatial.Transform.basis.z, new Vector3(0,0,1), marginOfError);
		}

		// X basis after Y Rotations

		[Test]
		public void XShouldBeAt_PointSeven_Zero_NegativePointSeven_WhenRotated45Around_Y()
		{
			spatial.RotateY(Mathf.PI / 4.0f);
			CompareVector3s(spatial.Transform.basis.x, new Vector3(0.7f,0,-0.7f), marginOfError);
		}

		[Test]
		public void XShouldBeAt_NegativePointSeven_Zero_NegativePointSeven_WhenRotated135Around_Y()
		{
			spatial.RotateY(Mathf.PI * (3.0f/4.0f));
			CompareVector3s(spatial.Transform.basis.x, new Vector3(-0.7f,0,-0.7f), marginOfError);
		}

		[Test]
		public void XShouldBeAt_NegativePointSeven_Zero_PointSeven_WhenRotated225Around_Y()
		{
			spatial.RotateY(Mathf.PI + Mathf.PI / 4.0f);
			CompareVector3s(spatial.Transform.basis.x, new Vector3(-0.7f,0,0.7f), marginOfError);
		}

		[Test]
		public void XShouldBeAt_PointSeven_Zero_PointSeven_WhenRotated315Around_Y()
		{
			spatial.RotateY(Mathf.PI + Mathf.PI * (3.0f/4.0f));
			CompareVector3s(spatial.Transform.basis.x, new Vector3(0.7f,0,0.7f), marginOfError);
		}

		// Y basis after X Rotations

		[Test]
		public void YShouldBeAt_Zero_One_Zero_WhenRotated45Around_Y()
		{
			spatial.RotateY(Mathf.PI / 4.0f);
			CompareVector3s(spatial.Transform.basis.y, new Vector3(0,1,0), marginOfError);
		}

		[Test]
		public void YShouldBeAt_Zero_PointSeven_PointSeven_WhenRotated45Around_X()
		{
			spatial.RotateX(Mathf.PI / 4.0f);
			CompareVector3s(spatial.Transform.basis.y, new Vector3(0,0.7f,0.7f), marginOfError);
		}

		[Test]
		public void YShouldBeAt_Zero_NegativePointSeven_PointSeven_WhenRotated135Around_X()
		{
			spatial.RotateX(Mathf.PI * (3.0f/4.0f));
			CompareVector3s(spatial.Transform.basis.y, new Vector3(0,-0.7f,0.7f), marginOfError);
		}

		[Test]
		public void YShouldBeAt_Zero_NegativePointSeven_NegativePointSeven_WhenRotated225Around_X()
		{
			spatial.RotateX(Mathf.PI + Mathf.PI / 4.0f);
			CompareVector3s(spatial.Transform.basis.y, new Vector3(0,-0.7f,-0.7f), marginOfError);
		}

		[Test]
		public void YShouldBeAt_Zero_PointSeven_NegativePointSeven_WhenRotated315Around_X()
		{
			spatial.RotateX(Mathf.PI + Mathf.PI * (3.0f/4.0f));
			CompareVector3s(spatial.Transform.basis.y, new Vector3(0,0.7f,-0.7f), marginOfError);
		}

		void CompareVector3s(Vector3 actual, Vector3 expected, float within = 0)
		{
			Assert.That(actual.x, Is.EqualTo(expected.x).Within(within));
			Assert.That(actual.y, Is.EqualTo(expected.y).Within(within));
			Assert.That(actual.z, Is.EqualTo(expected.z).Within(within));
		}
	}
}