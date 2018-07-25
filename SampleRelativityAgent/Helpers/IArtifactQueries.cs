using Relativity.API;

namespace SampleRelativityAgent.Helpers
{
    public interface IArtifactQueries
    {
        int CreateFixedLengthTextField(int workspaceId, IServicesMgr svcMgr, ExecutionIdentity identity);
        int GetFieldArtifactId(string fieldName, int workspaceId, IServicesMgr svcMgr, ExecutionIdentity identity);
        int GetFieldCount(int fieldArtifactId, int workspaceId, IServicesMgr svcMgr, ExecutionIdentity identity);
    }
}
