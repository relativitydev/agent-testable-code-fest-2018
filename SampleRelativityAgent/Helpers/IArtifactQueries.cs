using Relativity.API;

namespace SampleRelativityAgent.Helpers
{
    public interface IArtifactQueries
    {
        int CreateFixedLengthTextField(int workspaceId, IServicesMgr svcMgr, ExecutionIdentity identity);
        int GetFieldArtifactId(string fieldName, IDBContext workspaceDbContext);
        int GetFieldCount(IDBContext workspaceDbContext, int fieldArtifactId);
    }
}
