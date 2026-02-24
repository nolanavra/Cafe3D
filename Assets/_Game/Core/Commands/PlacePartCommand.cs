using UnityEngine;

namespace Cafe3D.Core.Commands
{
    public sealed class PlacePartCommand : IGameCommand
    {
        public PlacePartCommand(int tick, string partId, Vector2Int gridPosition, int rotationSteps)
        {
            Tick = tick;
            PartId = partId;
            GridPosition = gridPosition;
            RotationSteps = rotationSteps;
        }

        public int Tick { get; }

        public string PartId { get; }

        public Vector2Int GridPosition { get; }

        public int RotationSteps { get; }
    }
}
