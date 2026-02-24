namespace Cafe3D.Core.Commands
{
    public sealed class TryInteractCommand : IGameCommand
    {
        public TryInteractCommand(int tick, string targetId)
        {
            Tick = tick;
            TargetId = targetId;
        }

        public int Tick { get; }

        public string TargetId { get; }
    }
}
