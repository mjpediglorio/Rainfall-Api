using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rainfall.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void RainFallControllerTest_200()
        {
            var res = _sut.Get(3680, 10);
            res.Wait();
            var status_code = GetStatusCode(res.Result);
            Assert.AreEqual(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, status_code, "Status code is not ok!");
        }
        [TestMethod()]
        public void RainFallControllerTest_400()
        {
            var res = _sut.Get(3680, 0);
            res.Wait();
            var status_code = GetStatusCode(res.Result);
            Assert.AreEqual(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest, status_code, "Status code is not ok!");
        }
        [TestMethod()]
        public void RainFallControllerTest_404()
        {
            var res = _sut.Get(1, 10);
            res.Wait();
            var status_code = GetStatusCode(res.Result);
            Assert.AreEqual(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, status_code, "Status code is not ok!");
        }

        private static int? GetStatusCode<T>(ActionResult<T?> actionResult)
        {
            // source https://stackoverflow.com/questions/73594323/how-to-get-actionresult-statuscode-in-asp-net-core
            IConvertToActionResult convertToActionResult = actionResult; // ActionResult implicit implements IConvertToActionResult
            var actionResultWithStatusCode = convertToActionResult.Convert() as IStatusCodeActionResult;
            return actionResultWithStatusCode?.StatusCode;
        }
    }
}