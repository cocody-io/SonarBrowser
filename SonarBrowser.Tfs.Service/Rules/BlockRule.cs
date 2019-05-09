using SonarBrowser.Tfs.Service.DTO;
using System.Linq;

namespace SonarBrowser.Tfs.Service.Rules
{
    public static class BlockRule
    {
        public static int GetBlockModifiedLinesCount(Block block)
        {
            if (block.ChangeTypeIn(1, 2, 3))
            {
                return block.ModifiedLinesCount;
            }

            return 0;
        }

        private static bool ChangeTypeIn(this Block bloc, params int[] values)
        {
            return values.Contains(bloc.ChangeType);
        }
    }
}
