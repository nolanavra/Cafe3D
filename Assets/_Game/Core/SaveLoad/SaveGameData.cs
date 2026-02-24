using System;
using System.Collections.Generic;

namespace Cafe3D.Core.SaveLoad
{
    [Serializable]
    public sealed class SaveGameData
    {
        public string Version = "1.0.0";
        public int DayIndex;
        public string CurrentCafeId;
        public List<string> CompletedQuestIds = new List<string>();
    }
}
