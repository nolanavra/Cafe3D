using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cafe3D.Unity.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Cafe3D/Part Presentation Catalog")]
    public sealed class PartPresentationCatalog : ScriptableObject
    {
        public List<PartPresentationEntry> Entries = new List<PartPresentationEntry>();
    }

    [Serializable]
    public sealed class PartPresentationEntry
    {
        public string PartId;
        public GameObject Prefab;
        public Material Material;
        public Sprite Icon;
    }
}
