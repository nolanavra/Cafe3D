using Cafe3D.Core.Graph;

namespace Cafe3D.Core.Events
{
    public sealed class DiagnosticsCompletedEvent : IGameEvent
    {
        public DiagnosticsCompletedEvent(int tick, DiagnosticReport report)
        {
            Tick = tick;
            Report = report;
        }

        public int Tick { get; }

        public DiagnosticReport Report { get; }
    }
}
