using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZombieRPG.Tests
{
    [TestClass]
    public class MoveTests
    {
        [TestMethod]
        public void HeroShouldMove()
        {
            Move move = new Move();

            Assert.AreEqual("Hello World!", move.ToString());
        }
    }
}
