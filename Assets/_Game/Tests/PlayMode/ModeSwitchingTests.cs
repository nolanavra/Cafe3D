using Cafe3D.Core.Bootstrap;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cafe3D.Tests.PlayMode
{
    public sealed class ModeSwitchingTests
    {
        [Test]
        public void GameModeController_ComponentExistsForSceneAssembly()
        {
            var go = new GameObject("ModeController");
            go.AddComponent<PlayerInput>();
            var controller = go.AddComponent<GameModeController>();
            Assert.IsNotNull(controller);
            Object.DestroyImmediate(go);
        }
    }
}
