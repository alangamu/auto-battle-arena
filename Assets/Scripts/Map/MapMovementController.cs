using AutoFantasy.Scripts.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapMovementController : MonoBehaviour, IMapMovementController
    {
        [SerializeField]
        private float _movementTime;

        private Task MoveObjectToPosition(Vector3 position)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            LeanTween.move(gameObject, position, _movementTime)
                .setOnComplete(() => tcs.SetResult(null));

            return tcs.Task;
        }

        async Task IMapMovementController.DoMovement(List<Vector3> path)
        {
            foreach (Vector3 position in path)
            {
                transform.LookAt(position);
                await MoveObjectToPosition(position);
            }
        }
    }
}