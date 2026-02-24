using Cafe3D.Core.Graph;
using NUnit.Framework;

namespace Cafe3D.Tests.EditMode
{
    public sealed class DiagnosticSolverTests
    {
        [Test]
        public void Evaluate_ReturnsDeterministicValues()
        {
            var graph = new MachineGraph();
            graph.Nodes.Add(new GraphNode { Id = "A", Domain = GraphDomain.Electrical });
            graph.Nodes.Add(new GraphNode { Id = "B", Domain = GraphDomain.Electrical });
            graph.Edges.Add(new GraphEdge { From = "A", To = "B", Capacity = 1f });

            var solver = new DiagnosticSolver();
            var report = solver.Evaluate(graph);

            Assert.IsTrue(report.ElectricalContinuity);
            Assert.IsTrue(report.FluidContinuity);
            Assert.AreEqual(9.0f, report.EstimatedPressure);
        }
    }
}
