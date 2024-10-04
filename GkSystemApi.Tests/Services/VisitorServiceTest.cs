using gk_system_api.Entities;
using gk_system_api.Models;
using gk_system_api.Services;
using gk_system_api.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace GkSystemApi.Tests.Services
{
    public class VisitorServiceTest
    {
        private readonly Mock<ILogger<VisitorService>> _loggerMock;
        private readonly Mock<IDatabaseService> _databaseServiceMock;
        private readonly IVisitorService _visitorService;

        public VisitorServiceTest()
        {
            _loggerMock = new Mock<ILogger<VisitorService>>();
            _databaseServiceMock = new Mock<IDatabaseService>();
            _visitorService = new VisitorService(_databaseServiceMock.Object, _loggerMock.Object);

            _databaseServiceMock.Setup(s => s.GetAllVisitors()).Returns(new List<Visitor>() {
                new Visitor{FingerPrint = "test_fingerprint_1", Localisation = new Localisation { IPv4 = "test_ip_1"}}
            });

            _databaseServiceMock.Setup(s => s.GetTotalVisitors()).Returns(1);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.TestCheckVisitorData), MemberType = typeof(TestDataGenerator))]
        public void TestCheckVisitor(VisitorViewModel visitor, Times times)
        {
            _visitorService.CheckVisitor(visitor);
            _databaseServiceMock.Verify(d => d.AddVisitor(It.IsAny<VisitorViewModel>()), times);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.TestGetVisitorData), MemberType = typeof(TestDataGenerator))]
        public void TestGetVisitors(string[] topVisitors)
        {
            _databaseServiceMock.Setup(s => s.GetTopVisitors()).Returns(topVisitors);
            var result = _visitorService.GetVisitors();
            if (topVisitors == null)
            {
                Assert.Null(result);
                return;
            }

            Assert.Equal(topVisitors, result.Top);
            Assert.Equal(1, result.Total);
        }
    }

    public class TestDataGenerator
    {
        public static IEnumerable<object[]> TestGetVisitorData => new List<object[]>
        {
            new object[] { null },
            new object[] { new string[] { "visitor1" } }
        };

        public static IEnumerable<object[]> TestCheckVisitorData => new List<object[]>
        {
            new object[]
            {
                new VisitorViewModel{ Fingerprint = "test_fingerprint_1", Localisation = new LocalisationViewModel {IPv4 ="test_ip_1"}},
                Times.Never()
            },
            new object[]
            {
                new VisitorViewModel{ Fingerprint = "test_fingerprint_1", Localisation = new LocalisationViewModel {IPv4 ="test_ip_2"}},
                Times.Once()
            },
            new object[]
            {
                new VisitorViewModel{ Fingerprint = "test_fingerprint_2", Localisation = new LocalisationViewModel {IPv4 ="test_ip_1"}},
                Times.Never()
            },
            new object[]
            {
                new VisitorViewModel{ Fingerprint = "test_fingerprint_2", Localisation = new LocalisationViewModel {IPv4 ="test_ip_2"}},
                Times.Once()
            }
        };
    }
}
