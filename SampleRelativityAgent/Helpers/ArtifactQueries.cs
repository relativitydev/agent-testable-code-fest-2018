using System;
using System.Linq;
using kCura.Relativity.Client;
using kCura.Relativity.Client.DTOs;
using Relativity.API;

namespace SampleRelativityAgent.Helpers
{
    public class ArtifactQueries : IArtifactQueries
    {
        public int CreateFixedLengthTextField(int workspaceId, IServicesMgr svcMgr, ExecutionIdentity identity)
        {
            int fieldId = 0;

            try
            {
                using (IRSAPIClient client = svcMgr.CreateProxy<IRSAPIClient>(identity))
                {
                    //Set the workspace ID
                    client.APIOptions.WorkspaceID = workspaceId;

                    //Create a Field DTO
                    kCura.Relativity.Client.DTOs.Field fieldDto = new kCura.Relativity.Client.DTOs.Field();

                    //Set primary fields
                    //The name of the sample data is being set to a random string so that sample data can be debugged
                    //and never causes collisions. You can set this to any string that you want
                    fieldDto.Name = "Demo Document Field";
                    fieldDto.ObjectType = new kCura.Relativity.Client.DTOs.ObjectType() { DescriptorArtifactTypeID = (int)ArtifactType.Document };
                    fieldDto.FieldTypeID = FieldType.FixedLengthText;

                    //Set secondary fields
                    fieldDto.AllowHTML = false;
                    fieldDto.AllowGroupBy = false;
                    fieldDto.AllowPivot = false;
                    fieldDto.AllowSortTally = false;
                    fieldDto.IncludeInTextIndex = true;
                    fieldDto.IsRequired = false;
                    fieldDto.OpenToAssociations = false;
                    fieldDto.Length = 255;
                    fieldDto.Linked = false;
                    fieldDto.Unicode = true;
                    fieldDto.Width = "";
                    fieldDto.Wrapping = true;
                    fieldDto.IsRelational = false;

                    //Create the field
                    WriteResultSet<kCura.Relativity.Client.DTOs.Field> resultSet = client.Repositories.Field.Create(fieldDto);

                    //Check for success
                    if (!resultSet.Success)
                    {
                        Console.WriteLine("Field was not created");
                        return fieldId;
                    }

                    Result<kCura.Relativity.Client.DTOs.Field> firstOrDefault = resultSet.Results.FirstOrDefault();
                    if (firstOrDefault != null) fieldId = firstOrDefault.Artifact.ArtifactID;

                    return fieldId;
                }
            }
            catch (Exception)
            {
                throw new Exception("Failed in the create field method.");
            }
        }

        public int GetFieldArtifactId(string fieldName, int workspaceId, IServicesMgr svcMgr, ExecutionIdentity identity)
        {
            try
            {
                using (IRSAPIClient client = svcMgr.CreateProxy<IRSAPIClient>(identity))
                {
                    client.APIOptions.WorkspaceID = workspaceId;

                    Query<kCura.Relativity.Client.DTOs.Field> query = new Query<kCura.Relativity.Client.DTOs.Field>();
                    query.Fields = FieldValue.AllFields;
                    query.Condition = new TextCondition("Name", TextConditionEnum.Like, fieldName);

                    ResultSet<kCura.Relativity.Client.DTOs.Field> results = client.Repositories.Field.Query(query);
                    kCura.Relativity.Client.DTOs.Field fieldArtifact = results.Results.FirstOrDefault().Artifact;
                    return fieldArtifact.ArtifactID;
                }
            }
            catch (Exception)
            {
                throw new Exception($"Failed to get the artifact id for {fieldName}.");
            }
        }

        public int GetFieldCount(int fieldArtifactId, int workspaceId, IServicesMgr svcMgr, ExecutionIdentity identity)
        {
            try
            {
                using (IRSAPIClient client = svcMgr.CreateProxy<IRSAPIClient>(identity))
                {
                    client.APIOptions.WorkspaceID = workspaceId;

                    Query query = new Query();
                    query.Condition = new WholeNumberCondition("Artifact ID", NumericConditionEnum.EqualTo, fieldArtifactId);
                    QueryResult result = client.Query(client.APIOptions, query);
                    return result.TotalCount;
                }
            }
            catch (Exception)
            {
                throw new Exception($"Failed to get the field count for artifact id {fieldArtifactId}.");
            }
        }
    }
}
