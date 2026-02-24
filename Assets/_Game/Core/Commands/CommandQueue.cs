using System.Collections.Generic;

namespace Cafe3D.Core.Commands
{
    public sealed class CommandQueue
    {
        private readonly Queue<IGameCommand> queue = new Queue<IGameCommand>();

        public void Enqueue(IGameCommand command)
        {
            queue.Enqueue(command);
        }

        public bool TryDequeue(out IGameCommand command)
        {
            if (queue.Count > 0)
            {
                command = queue.Dequeue();
                return true;
            }

            command = null;
            return false;
        }
    }
}
