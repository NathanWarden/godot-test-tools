﻿using Godot;
using NUnit.Framework;

namespace GodotTestingProject.Tests.NodeTests
{
	[TestFixture]
	public class NodeTests : Node
	{
		[Test]
		public void SpawnAndFreeManyObjectsInLessThanFiveSeconds()
		{
			const int seconds = 5;
			int startTime = OS.GetTicksMsec();

			for (var i = 0; i < 100000; i++)
			{
				new Node().Free();
			}

			Assert.That((OS.GetTicksMsec() - startTime) / 1000.0f, Is.LessThan(seconds));
		}
	}
}