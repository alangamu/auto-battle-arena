using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapMovementController : MonoBehaviour, IMapMovementController
    {
        [SerializeField]
        private float _movementTime;
        [SerializeField]
        private GameObjectVariable _activeHeroStandingTile;

        async Task IMapMovementController.DoMovement(List<ITile> path)
        {
            foreach (ITile tile in path)
            {
                GameObject tileGO = tile.GetGameObject();
                Vector3 tilePosition = tileGO.transform.position;
                transform.LookAt(tilePosition);
                await MoveObjectToPosition(tilePosition);
                if (TryGetComponent(out IMapUnitController mapUnit))
                {
                    Hex heroHex = tile.GetHex();
                    mapUnit.SetHexCoordinates(heroHex.Q, heroHex.R);
                }
                _activeHeroStandingTile.SetActiveGameObject(tileGO);
            }
        }

        private Task MoveObjectToPosition(Vector3 position)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            LeanTween.move(gameObject, position, _movementTime)
                .setOnComplete(() => tcs.SetResult(null));

            return tcs.Task;
        }
    }
}