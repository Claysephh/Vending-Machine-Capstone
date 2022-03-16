using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Capstone.Classes.Tests
{
    [TestClass()]
    public class AdjustBalanceTests
    {
        [TestMethod]
        public void AdjustBalance_HappyPath()
        {
            //arrange

            LogSheet logSheet = new LogSheet();
            decimal change = 1.00M;

            //act
            bool result = logSheet.AdjustBalance(change);

            //assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void AdjustBalance_DoesNotAcceptInvalidDollars()
        {
            //arrange

            LogSheet logSheet = new LogSheet();
            decimal change = 3.00M;

            //act
            bool result = logSheet.AdjustBalance(change);

            //assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void AdjustBalance_DoesNotExceptNegativeNumbers()
        {
            //arrange

            LogSheet logSheet = new LogSheet();
            decimal change = -1.00M;

            //act
            bool result = logSheet.AdjustBalance(change);

            //assert
            Assert.IsFalse(result);
        }

    }
}

