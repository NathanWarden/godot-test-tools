using Godot;
using NUnit.Framework;
using System.Collections;

namespace GodotTestingProject.Tests.NodeTests
{
	[TestFixture]
	public class RigidbodyTests : RigidBody
	{
		[Test]
		public IEnumerator RigidbodyShouldFallOverOneSecond()
		{
			int finishTime = OS.GetTicksMsec() + 1000;

			while (OS.GetTicksMsec() < finishTime)
			{
				yield return null;
			}

			Assert.That(Translation.y, Is.LessThan(-1));
		}


		[Test]
		public IEnumerator KinematicRigidbodyShouldNotFall()
		{
			int finishTime = OS.GetTicksMsec() + 1000;

			Mode = ModeEnum.Kinematic;

			while (OS.GetTicksMsec() < finishTime)
			{
				yield return null;
			}

			Assert.That(Translation.y, Is.EqualTo(0).Within(0.01f));
		}
	}
}