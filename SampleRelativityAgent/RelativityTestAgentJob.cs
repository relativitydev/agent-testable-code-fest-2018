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
			
			_fieldArtifactId = ArtifactQueries.CreateFixedLengthTextField(WorkspaceArtifactId, SvcManager, CurrentUserIdentity);

			if (_fieldArtifactId == 0)
			{
				throw new Exception($"Field failed to create field");
			}

			#region
			//if (_fieldArtifactId == 0)
			//{
			//	throw new Exception($"Field failed to create field on workspace {WorkspaceArtifactId}");
			//}
			#endregion

			#region
			////In a real scenario, workspace Artifact id would be coming in from a manager queue record.
			//int newWorkspaceArtifactId = 1017097;

			////Check to see if field exists
			//_fieldArtifactId = ArtifactQueries.GetFieldArtifactId("Demo Document Field", newWorkspaceArtifactId, SvcManager, CurrentUserIdentity);

			//if (_fieldArtifactId != 0)
			//{
			//	Console.WriteLine("Field is already present in the database :)");
			//}
			//else
			//{
			//	// Create field
			//	_fieldArtifactId = ArtifactQueries.CreateFixedLengthTextField(newWorkspaceArtifactId, SvcManager, CurrentUserIdentity);
			//}

			//if (_fieldArtifactId == 0)
			//{
			//	throw new Exception($"Field failed to create field");
			//}
			#endregion
		}
	}
}
