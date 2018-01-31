using Godot;
using NUnit.Framework;

namespace GodotTestingProject.Tests.MathTests
{
	[TestFixture]
	public class MathfLerpTests
	{
		const float marginOfError = float.Epsilon;


		[Test]
		public void LerpShouldReturnPointFiveWhenHalfWayBetweenZeroAndOne()
		{
			Assert.That(Mathf.Lerp(0, 1, 0.5f), Is.EqualTo(0.5f).Within(marginOfError));
		}


		[Test]
		public void LerpShouldReturnZeroWhenTheWeightIsZeroBetweenZeroAndOne()
		{
			Assert.That(Mathf.Lerp(0, 1, 0), Is.EqualTo(0).Within(marginOfError));
		}


		[Test]
		public void LerpShouldReturnOneWhenTheWeightIsOneBetweenZeroAndOne()
		{
			Assert.That(Mathf.Lerp(0, 1, 1), Is.EqualTo(1).Within(marginOfError));
		}


		[Test]
		public void LerpShouldClampToOneWhenTheWeightIsTwoBetweenZeroAndOne()
		{
			Assert.That(Mathf.Lerp(0, 1, 2), Is.EqualTo(1).Within(marginOfError));
		}


		[Test]
		public void LerpShouldReturnPointFiveWhenTheWeightIsPointFiveBetweenOneAndZero()
		{
			Assert.That(Mathf.Lerp(1, 0, 0.5f), Is.EqualTo(0.5f).Within(marginOfError));
		}


		[Test]
		public void LerpShouldReturnNegativePointFiveWhenWeightIsPointFiveBetweenZeroAndNegativeOne()
		{
			Assert.That(Mathf.Lerp(0, -1, 0.5f), Is.EqualTo(-0.5f).Within(marginOfError));
		}
	}
}