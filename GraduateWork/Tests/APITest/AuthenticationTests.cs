using System.Net;
using Allure.Net.Commons;
using GraduateWork.Helpers;
using GraduateWork.Models;
using Newtonsoft.Json;
using NLog;
using NUnit.Allure.Attributes;

namespace GraduateWork.Tests;

[AllureSuite("API Authentication Tests")]
public class AuthenticationTests : BaseApiTest
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    [Test]
    [Category("NFE")]
    public void GetAuthenticationInfoTest()
    {
        AllureApi.Step("GetAuthenticationInfoTest запущен.");
        
        var authenticationInfo = AuthenticationServices!.GetAuthenticationInfo();
        
        Assert.That(authenticationInfo.Result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        Account user = JsonConvert.DeserializeObject<Account>(authenticationInfo.Result.Content);
        
        Assert.That(user.Email, Is.EqualTo(Configurator.AppSettings.Username));
        
        AllureApi.Step("GetAuthenticationInfoTest выполнен.");
    }
}