using System.Collections.Generic;
using Cafe3D.Core.Commands;
using Cafe3D.Core.Utils;
using UnityEngine;

namespace Cafe3D.Unity.ViewModels
{
    public sealed class CafePlayerController : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour inputSourceBehaviour;
        [SerializeField] private Camera cafeCamera;
        [SerializeField] private float interactDistance = 3f;
        [SerializeField] private CommandQueue commandQueue;

        private readonly List<RaycastHit> hitBuffer = new List<RaycastHit>();
        private IPlayerInputSource inputSource;
        private int tick;

        private void Awake()
        {
            inputSource = inputSourceBehaviour as IPlayerInputSource;
        }

        private void OnEnable()
        {
            if (inputSource != null)
            {
                inputSource.InteractPressed += OnInteract;
            }
        }

        private void OnDisable()
        {
            if (inputSource != null)
            {
                inputSource.InteractPressed -= OnInteract;
            }
        }

        private void Update()
        {
            tick++;
            var move = inputSource != null ? inputSource.Move : Vector2.zero;
            var look = inputSource != null ? inputSource.Look : Vector2.zero;
            _ = (move, look);
        }

        private void OnInteract()
        {
            if (cafeCamera == null || commandQueue == null)
            {
                return;
            }

            hitBuffer.Clear();
            var ray = new Ray(cafeCamera.transform.position, cafeCamera.transform.forward);
            var hits = Physics.RaycastAll(ray, interactDistance);
            hitBuffer.AddRange(hits);
            hitBuffer.Sort((a, b) => a.distance.CompareTo(b.distance));

            var targetId = hitBuffer.Count > 0 ? hitBuffer[0].collider.gameObject.name : "None";
            commandQueue.Enqueue(new TryInteractCommand(tick, targetId));
        }
    }
}
