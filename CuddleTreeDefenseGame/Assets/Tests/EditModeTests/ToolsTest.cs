using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ToolsTest
{
    [Test]
    public void GetScreenToWorlPoint2DTestBasic()
    {
        Assert.AreEqual(new Vector3(1, 1, 0), Tools.GetScreenToWorldPoint2D(new Vector3(1, 1, 1)));
    }

    [Test]
    public void GetScreenToWorlPoint2DTestNegative()
    {
        Assert.AreEqual(new Vector3(-1, -1, 0), Tools.GetScreenToWorldPoint2D(new Vector3(-1, -1, -1)));
    }

    [Test]
    public void GetScreenToWorlPoint2DTestUninitialized()
    {
        Assert.AreEqual(new Vector3(0,0,0), Tools.GetScreenToWorldPoint2D(new Vector3()));
    }
}
