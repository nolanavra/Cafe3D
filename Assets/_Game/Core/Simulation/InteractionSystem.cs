using System.Collections.Generic;
using UnityEngine;

namespace Cafe3D.Core.Simulation
{
    public static class InteractionSystem
    {
        public static string SelectTarget(IReadOnlyList<RaycastHit> hits)
        {
            var sorted = new List<RaycastHit>(hits);
            sorted.Sort((a, b) =>
            {
                var distanceSort = a.distance.CompareTo(b.distance);
                if (distanceSort != 0)
                {
                    return distanceSort;
                }

                return string.CompareOrdinal(a.collider.gameObject.name, b.collider.gameObject.name);
            });

            return sorted.Count > 0 ? sorted[0].collider.gameObject.name : string.Empty;
        }
    }
}
