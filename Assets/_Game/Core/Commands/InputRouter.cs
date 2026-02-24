using Cafe3D.Core.Utils;

namespace Cafe3D.Core.Commands
{
    public sealed class InputRouter
    {
        private readonly IPlayerInputSource inputSource;
        private readonly CommandQueue commandQueue;

        public InputRouter(IPlayerInputSource inputSource, CommandQueue commandQueue)
        {
            this.inputSource = inputSource;
            this.commandQueue = commandQueue;
        }

        public void BindCafe(int tick, string targetId)
        {
            inputSource.InteractPressed += () => commandQueue.Enqueue(new TryInteractCommand(tick, targetId));
        }

        public void BindRepair(int tick, string partId)
        {
            inputSource.PointerClickPressed += () => commandQueue.Enqueue(new PlacePartCommand(tick, partId, UnityEngine.Vector2Int.zero, 0));
        }
    }
}
