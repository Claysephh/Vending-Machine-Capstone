using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Classes.Tests
{
    [TestClass()]
    public class Audit
    {
        [TestMethod]
        public void Audit_HappyPath()
        {
            //Arrange
            decimal money = 5.00M;

            //Act
            bool result = new LogSheet().Audit(money);

            //Assert
            Assert.IsTrue(result);

        }
        [TestMethod]
        public void Audit_FoodHappyPath()
        {
            //Arrange
            Chip item = new Chip("here", "craig's crunchy cakes", 0.99M);

            //Act
            bool result = new LogSheet().Audit(item);

            //Assert
            Assert.IsTrue(result);

        }
    }
}
