﻿using BusinessLogic.Report_Components;
using BusinessLogic.Dtos_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTests.Dtos_Tests
{
    [TestClass]
    public class RangeOfDatesTests
    {
        #region Initial Date

        [TestMethod]
        public void GivenInitialDate_ShouldBeSetted()
        {
            RangeOfDatesDTO myRange = new RangeOfDatesDTO();

            myRange.InitialDate = new DateTime(2023, 5, 12);

            Assert.AreEqual(myRange.InitialDate, new DateTime(2023, 5, 12));

        }

        #endregion

        #region Final Date

        [TestMethod]
        public void GivenFinalDate_ShouldBeSetted()
        {
            RangeOfDatesDTO myRange = new RangeOfDatesDTO();

            myRange.InitialDate = new DateTime(2023, 5, 12);

            Assert.AreEqual(myRange.InitialDate, new DateTime(2023, 5, 12));
        }

        #endregion
    }
}
