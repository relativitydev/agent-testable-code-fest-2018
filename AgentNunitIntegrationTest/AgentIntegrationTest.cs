using kCura.Relativity.Client;
using kCura.Relativity.Client.DTOs;
using NUnit.Framework;
using Relativity.API;
using Relativity.Test.Helpers;
using Relativity.Test.Helpers.ServiceFactory.Extentions;
using Relativity.Test.Helpers.WorkspaceHelpers;
using System;
using System.Configuration;
using System.Linq;

namespace AgentNunitIntegrationTest
{
	[TestFixture]
	public class AgentIntegrationTest
	{
		#region Variables

		public IRSAPIClient _client;
		public int _workspaceId;
		public IServicesMgr ServicesManager;
		public const string NewFieldName = "Demo Document Field";
		public string _workspaceName = string.Concat(ConfigurationManager.AppSettings["TestWorkspaceName"], Guid.NewGuid()).Substring(0,32);
		public string _adminUsername = ConfigurationManager.AppSettings["AdminUsername"];
		public string _adminPassword = ConfigurationManager.AppSettings["AdminPassword"];

		#endregion

		#region TestfixtureSetup

		[TestFixtureSetUp]
		public void Execute_TestFixtureSetup()
		{
			//_workspaceId = CreateWorkspace.CreateWorkspaceAsync(_workspaceName, ConfigurationManager.AppSettings["TestWorkspaceTemplateName"], ServicesManager, _adminUsername, _adminPassword).Result;
			_workspaceId = 1017097;

			//Create instance of Test Helper & set up services manager
			TestHelper helper = new TestHelper();
			ServicesManager = helper.GetServicesManager();

			//Create client
			_client = ServicesManager.GetProxy<IRSAPIClient>(_adminUsername, _adminPassword);
		}

		#endregion

		#region TestfixtureTeardown

		[TestFixtureTearDown]
		public void Execute_TestFixtureTeardown()
		{
			//DeleteWorkspace.DeleteTestWorkspace(_workspaceId, ServicesManager, _adminUsername, _adminPassword);
		}

		#endregion

		#region TestTeardown
		[TearDown]
		public void Execute_Teardown()
		{
			DeleteField(NewFieldName, _workspaceId, _client);
		}
		#endregion

		#region Golden Flow

		[Test, Description("Golden Flow Unit Test")]
		public void Integration_Test_Golden_Flow_Valid()
		{
			//Arrange and Act
			ResultSet<kCura.Relativity.Client.DTOs.Field> fieldQueryResult = GetField(NewFieldName, _workspaceId, _client);

			//Assert
			Assert.That(fieldQueryResult.Results.Count, Is.EqualTo(1));
			Assert.That(fieldQueryResult.Results.FirstOrDefault().Artifact.ArtifactID, Is.Not.Null);

			Console.WriteLine($"Found {fieldQueryResult.Results.Count} field");
			Console.WriteLine($"Name: {fieldQueryResult.Results.FirstOrDefault().Artifact.Name}");
			Console.WriteLine($"Artifact ID: {fieldQueryResult.Results.FirstOrDefault().Artifact.ArtifactID}");
		}

		#endregion

		#region private helpers

		private ResultSet<kCura.Relativity.Client.DTOs.Field> GetField(string fieldname, int workspaceID, IRSAPIClient _client)
		{
			_client.APIOptions.WorkspaceID = workspaceID;

			Query<kCura.Relativity.Client.DTOs.Field> query = new Query<kCura.Relativity.Client.DTOs.Field>();
			query.Fields = FieldValue.AllFields;
			query.Condition = new TextCondition("Name", TextConditionEnum.Like, fieldname);

			ResultSet<kCura.Relativity.Client.DTOs.Field> results = _client.Repositories.Field.Query(query);

			return results;
		}

		private void DeleteField(string fieldname, int workspaceID, IRSAPIClient _client)
		{
			_client.APIOptions.WorkspaceID = workspaceID;

			try
			{
				_client.Repositories.Field.DeleteSingle(GetField(fieldname, workspaceID, _client).Results.FirstOrDefault().Artifact.ArtifactID);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred deleting field {fieldname}: {ex.Message}");
			}
		}

		#endregion
	}
}
