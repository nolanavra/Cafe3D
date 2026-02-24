using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cafe3D.Unity.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Cafe3D/Cafe Presentation Catalog")]
    public sealed class CafePresentationCatalog : ScriptableObject
    {
        public List<CafePresentationEntry> Entries = new List<CafePresentationEntry>();
    }

    [Serializable]
    public sealed class CafePresentationEntry
    {
        public string CafeId;
        public GameObject SceneRootPrefab;
        public Sprite Thumbnail;
    }
}
