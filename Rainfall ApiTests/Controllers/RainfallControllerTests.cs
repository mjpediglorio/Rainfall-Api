using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rainfall.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rainfall.Api;

namespace Rainfall.Controllers.Tests
{
    [TestClass()]
    public class RainfallControllerTests
    {
        RainfallController _sut;
        public RainfallControllerTests()
        {
            _sut = new RainfallController(new Api.Api());
        }

        [TestMethod()]
        public void RainFallControllerTest_OK()
        {
            var result = _sut.Get(6830, 10);

            var status_code = GetStatusCode(result.Result);

            Assert.AreEqual(200, status_code, "Status code is not ok!");
        }

        private int? GetStatusCode<T>(ActionResult<T?> actionResult)
        {
            // source https://stackoverflow.com/questions/73594323/how-to-get-actionresult-statuscode-in-asp-net-core
            IConvertToActionResult convertToActionResult = actionResult; // ActionResult implicit implements IConvertToActionResult
            var actionResultWithStatusCode = convertToActionResult.Convert() as IStatusCodeActionResult;
            return actionResultWithStatusCode?.StatusCode;
        }
    }
}