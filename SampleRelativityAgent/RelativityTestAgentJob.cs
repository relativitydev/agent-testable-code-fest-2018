using Relativity.API;
using SampleRelativityAgent.Helpers;
using System;

namespace SampleRelativityAgent
{
	[kCura.Agent.CustomAttributes.Name("RelativityTestAgent Job")]
	[System.Runtime.InteropServices.Guid("2E29383D-C993-4358-AE6D-8BC462703EE5")]
	public class RelativityTestAgentJob
	{
		private int _fieldArtifactId;
		public IArtifactQueries ArtifactQueries { get; set; }
		public IAPILog Logger { get; private set; }
		public IServicesMgr SvcManager { get; private set; }
		public ExecutionIdentity CurrentUserIdentity { get; set; }
		public int WorkspaceArtifactId { get; private set; }
		public IAgentHelper AgentHelper { get; private set; }


		public RelativityTestAgentJob(IArtifactQueries artifactQueries, IAPILog logger, IServicesMgr svcManager,
			ExecutionIdentity userIdentity, int workspaceId, IAgentHelper agentHelper)
		{
			ArtifactQueries = artifactQueries;
			Logger = logger;
			WorkspaceArtifactId = workspaceId;
			SvcManager = svcManager;
			CurrentUserIdentity = userIdentity;
			AgentHelper = agentHelper;
		}

		public void Execute()
		{

			int expectedArtifactID;

			//create field . Comment this

			expectedArtifactID = ArtifactQueries.CreateFixedLengthTextField(WorkspaceArtifactId, SvcManager, CurrentUserIdentity);

			if (expectedArtifactID == 0)
			{
				throw new Exception($"Field failed to create field");
				//throw new Exception($"Field failed to create field on workspace {WorkspaceArtifactId}");
			}

			//Uncomment this 

			//------------------------------------------------------


			//In a real scenario, workspace Artifact id would be coming in from a manager queue record.
			//Check to see if field exists



			//int newWorkspaceArtifactId = 1017097;

			//_fieldArtifactId = ArtifactQueries.GetFieldArtifactId("Demo Document Field", AgentHelper.GetDBContext(newWorkspaceArtifactId));
			//if (_fieldArtifactId == 0)
			//{
			//	Console.WriteLine("Field is already present in the database :)");
			//}

			//expectedArtifactID = ArtifactQueries.CreateFixedLengthTextField(newWorkspaceArtifactId, SvcManager, CurrentUserIdentity);

			//Uncomment this 

		}
	}
}
