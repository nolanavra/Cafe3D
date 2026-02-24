using System.IO;
using UnityEngine;

namespace Cafe3D.Core.SaveLoad
{
    public sealed class SaveGameService
    {
        public void Save(string path, SaveGameData data)
        {
            var json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
        }

        public SaveGameData Load(string path)
        {
            var json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveGameData>(json);
        }
    }
}
