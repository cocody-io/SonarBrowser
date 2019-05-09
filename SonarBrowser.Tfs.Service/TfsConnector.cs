using SonarBrowser.Tfs.Service.Connectors;
using SonarBrowser.Tfs.Service.DTO;
using SonarBrowser.Tfs.Service.Interface;
using SonarBrowser.Tfs.Service.Rules;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System;
using SonarBrowser.Infrastructure.WebClient;
using SonarBrowser.Infrastructure.Cache;

namespace SonarBrowser.Tfs.Service
{
    public class TfsConnector : ITfsConnector
    {
        private readonly ChangesetsConnector _changesets;
        private readonly VersionControlConnector _vcc;
        private readonly WorkItemConnector _wit;
        private readonly IHttpApiClient _httpApiClient;
        private readonly ICacheManager _cacheManager;

        public TfsConnector(TfsSettings tfsSettings, ICacheManager cacheManager, IHttpApiClient httpApiClient)
        {
            _httpApiClient = httpApiClient;
            _cacheManager = cacheManager;
            _changesets = new ChangesetsConnector(tfsSettings, _httpApiClient);
            _vcc = new VersionControlConnector(tfsSettings, _httpApiClient);
            _wit = new WorkItemConnector(tfsSettings, _httpApiClient);
        }


        /// <summary>
        /// Helper method that request the agrsso code with a given changeset id
        /// </summary>
        /// <param name="changesetId"></param>
        /// <returns></returns>
        public async Task<string> GetAgressoCode(int? changesetId)
        {
            if (!changesetId.HasValue) return string.Empty;

            string cacheKey = string.Format("GetAgressoCode{0}", changesetId.Value);
            return await _cacheManager.Resolve(cacheKey, async () =>
            {
                Changeset changeset = await _changesets.GetChangeset(changesetId.ToString());
                if (changeset?.WitList != null && changeset.WitList.Count > 0)
                {
                    WorkItem wit = await _wit.GetWorkItem(changeset.WitList?.First()?.Id.ToString());
                    string codeProject = wit?.WitFields?.ProjectCodeAgresso ?? wit?.WitFields?.MantisProject;
                    return codeProject;
                }
                return "no project name";
            });
        }

        /// <summary>
        /// Helper method that request the number of lines of a given changeset id
        /// </summary>
        /// <param name="changesetId"></param>
        /// <returns></returns>
        public async Task<int> GetNumberOfImpactedLines(int? changesetId)
        {
            int resultNumber = 0;

            if (changesetId.HasValue)
            {

                string cacheKey = string.Format("GetNumberOfImpactedLines{0}", changesetId.Value);
                return await _cacheManager.Resolve(cacheKey, async () =>
                 {
                    // 1 - Récupérer les fichiers du changeset
                    Changes cs = await _changesets.GetChangesetChanges(changesetId.Value.ToString());
                     foreach (ValueItem file in cs.Value)
                     {
                         if (!file.Item.IsFolder && IsFileAnalized(file.Item.Path))
                         {
                             resultNumber += await AnalyzeFile(file, changesetId.Value);
                         }
                     }
                     return resultNumber;
                 });
            }
            else
            {
                resultNumber = -1;
            }
            return resultNumber;
        }

        private bool IsFileAnalized(string path)
        {
            return path.Contains(".cs");
        }

        private async Task<int> AnalyzeFile(ValueItem file, int changesetId)
        {
            int resultNumber = 0;
            if (!file.ChangeType.Contains("delete"))
            {
                // 2.3 - Sortir les différences 
                FileComparerResult diffResult = await _vcc.CompareChangesetFile(file.Item.Path, "P" + changesetId, changesetId.ToString()).ConfigureAwait(false);

                if (diffResult.Blocks.Count == 1)
                {
                    //c'est le premier ajout sur le silo
                    resultNumber += diffResult.Blocks[0].ModifiedLinesCount;
                }
                else
                {
                    // 2.4 - Pour chaque différence
                    foreach (Block block in diffResult.Blocks)
                    {
                        resultNumber += BlockRule.GetBlockModifiedLinesCount(block);
                    }
                }
            }
            return resultNumber;
        }
    }
}
