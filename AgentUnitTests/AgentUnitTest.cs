using Moq;
using NUnit.Framework;
using Relativity.API;
using SampleRelativityAgent;
using SampleRelativityAgent.Helpers;

namespace AgentUnitTests
{
    [TestFixture]
    public class AgentUnitTest
    {
        #region Variables

        //Declaring Mocks for all the objects needs
        public Mock<IArtifactQueries> MockArtifactQueries;
        public RelativityTestAgentJob Sut;
        public Mock<IAPILog> MockLogger;
        public Mock<IServicesMgr> MockServiceMgr;
        public Mock<IAgentHelper> MockAgentHelper;
        public Mock<IDBContext> MockworkspaceDbContext;
        public const int WorkspaceId = 123;

        #endregion

        #region TestSetup

        [SetUp]
        public void TestSetup()
        {
            // Creating the mocks
            MockArtifactQueries = new Mock<IArtifactQueries>();
            MockLogger = new Mock<IAPILog>();
            MockServiceMgr = new Mock<IServicesMgr>();
            MockAgentHelper = new Mock<IAgentHelper>();
            MockworkspaceDbContext = new Mock<IDBContext>();
            Sut = new SampleRelativityAgent.RelativityTestAgentJob(MockArtifactQueries.Object, MockLogger.Object, MockServiceMgr.Object, ExecutionIdentity.System, WorkspaceId, MockAgentHelper.Object);
        }

        #endregion

        #region TestTearDown

        [TearDown]
        public void TestTeardown()
        {
            MockArtifactQueries = null;
            MockLogger = null;
            MockServiceMgr = null;
            Sut = null;
        }

        #endregion

        #region Tests

        [Test]
        public void AgentTest1()
        {
            //Arrange
            MockArtifactQueries.Setup(x => x.CreateFixedLengthTextField(It.IsAny<int>(), MockServiceMgr.Object,
                ExecutionIdentity.System)).Returns(4534543);

            MockArtifactQueries.Setup(x => x.GetFieldArtifactId(It.IsAny<string>(),
                MockworkspaceDbContext.Object)).Returns(423656);

            //Act
            Sut.Execute();

            //Assert
            MockArtifactQueries.Verify(x => x.CreateFixedLengthTextField(It.IsAny<int>(), MockServiceMgr.Object, 
                ExecutionIdentity.System), Times.Once);
        }

        #endregion
    }
}
