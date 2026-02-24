using System.Collections.Generic;

namespace Cafe3D.Core.Graph
{
    public enum GraphDomain
    {
        Electrical,
        Fluid
    }

    public sealed class GraphNode
    {
        public string Id;
        public GraphDomain Domain;
    }

    public sealed class GraphEdge
    {
        public string From;
        public string To;
        public float Capacity;
    }

    public sealed class MachineGraph
    {
        public List<GraphNode> Nodes = new List<GraphNode>();
        public List<GraphEdge> Edges = new List<GraphEdge>();
    }

    public sealed class DiagnosticReport
    {
        public bool ElectricalContinuity;
        public bool FluidContinuity;
        public float EstimatedPressure;
        public float EstimatedTemperature;
        public readonly List<string> Messages = new List<string>();
    }
}
