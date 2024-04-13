using System.Net;
using GraduateWork.Helpers.API;
using GraduateWork.Models;
using Newtonsoft.Json;
using NLog;

namespace GraduateWork.Tests;

public class AuthenticationTests : BaseApiTest
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    [Test]
    [Category("NFE")]
    public void GetAuthenticationInfoTest()
    {
        _logger.Info("GetAuthenticationInfoTest запущен.");
        
        var authenticationInfo = AuthenticationServices!.GetAuthenticationInfo();
        
        Assert.That(authenticationInfo.Result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        Account user = JsonConvert.DeserializeObject<Account>(authenticationInfo.Result.Content);
        
        Assert.That(user.Email, Is.EqualTo(Configurator.AppSettings.Username));
        
        _logger.Info("GetAuthenticationInfoTest выполнен.");
    }
}