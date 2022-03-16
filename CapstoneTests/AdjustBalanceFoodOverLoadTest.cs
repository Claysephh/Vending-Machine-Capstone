using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Capstone.Classes.Tests
{
    [TestClass()]
    public class AdjustBalanceFoodOverLoadTest
    {
        [TestMethod]
        public void AdjustBalanceFoodOverLoadTest_HappyPath()
        {
            //arrange
            LogSheet logSheet = new LogSheet();
            Chip item = new Chip("here", "craig's crunchy cakes", 0.99M);
            logSheet.AdjustBalance(5M);

            //act
           bool result = logSheet.AdjustBalance(item);

            //assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void AdjustBalanceFoodOverLoadTest_HappyPathFalse()
        {
            //arrange
            LogSheet logSheet = new LogSheet();
            Chip item = new Chip("here", "craig's crunchy cakes", 0.99M);

            //act
            bool result = logSheet.AdjustBalance(item);

            //assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void AdjustBalanceFoodOverLoadTest_IsFalseWithBalance()
        {
            //arrange
            LogSheet logSheet = new LogSheet();
            Chip item = new Chip("here", "craig's crunchy cakes", 1.99M);
            logSheet.AdjustBalance(1M);

            //act
            bool result = logSheet.AdjustBalance(item);

            //assert
            Assert.IsFalse(result);
        }
    }
}
