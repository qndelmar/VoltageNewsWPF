using Microsoft.VisualStudio.TestTools.UnitTesting;
using VoltageNews.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoltageNews.Models;

namespace VoltageNews.ViewModels.Tests
{
    [TestClass()]
    public class DifficultyProgressTests
    {
        [TestMethod()]
        public void ValidatePasswordTest()
        {
            DifficultyProgress difficultyProgress = ViewModels.DifficultyProgress.ValidatePassword("asdasdasd");
            DifficultyProgress expected = new("Пароль должен быть больше 8 символов,\nи содержать в себе цифру, минимум 1 латинскую маленькую\nи большую букву, и специальный символ.", 10);
            Assert.AreEqual(expected.difficultyProgress, difficultyProgress.difficultyProgress);
        }
        [TestMethod()]
        public async Task authorizeTest()
        {
            int realResult = await User.authorize("rnc@gmail.com", "12345678Aa_");
            Assert.AreEqual(200, realResult);
        }
        [TestMethod()]
        public async Task authorizeNotFoundTest()
        {
            int realResult = await User.authorize("rnfdsfdsfsc@gmail.com", "12345678Aa_");
            Assert.AreEqual(404, realResult);
        }
        [TestMethod()]
        public async Task authorizeIncorrectPasswordTest()
        {
            int realResult = await User.authorize("rnc@gmail.com", "123456789Aa_");
            Assert.AreEqual(401, realResult);
        }
    }
}