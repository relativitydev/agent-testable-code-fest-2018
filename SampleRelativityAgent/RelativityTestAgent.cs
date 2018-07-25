using Relativity.API;
using System;

namespace SampleRelativityAgent
{
	[kCura.Agent.CustomAttributes.Name("RelativityTestAgent")]
	[System.Runtime.InteropServices.Guid("b5ce36a5-dab7-4e80-ab21-3b9a76e0c560")]
	public class RelativityTestAgent : kCura.Agent.AgentBase
	{
		private IAPILog _logger;
		public IServicesMgr SvcManager;
		public ExecutionIdentity IdentityCurrentUser;
		public int WorkspaceId = -1;

		public override void Execute()
		{
			//Set up
			Helpers.IArtifactQueries artifactQueries = new Helpers.ArtifactQueries();
			_logger = Helper.GetLoggerFactory().GetLogger();
			SvcManager = Helper.GetServicesManager();
			IdentityCurrentUser = ExecutionIdentity.CurrentUser;

			try
			{
				//Create instance of Relativity Test Job  
				RelativityTestAgentJob job = new RelativityTestAgentJob(artifactQueries, _logger, SvcManager, IdentityCurrentUser, WorkspaceId, Helper);
				job.Execute();
			}
			catch (Exception ex)
			{
				//Your Agent caught an exception
				RaiseMessage(ex.Message, 0);
				RaiseError(ex.Message, ex.Message);
			}
		}

		public override string Name
		{
			get
			{
				return "RelativityTestAgent";
			}
		}
	}
}
