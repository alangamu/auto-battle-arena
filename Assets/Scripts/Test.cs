using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.Map;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class Test : MonoBehaviour
    {
        [SerializeField]
        private GameEvent testEvent;
        [SerializeField]
        private TileRuntimeSet _tileRuntimeSet;
        [SerializeField]
        private GameObjectVariable _activeMapHero;
        [SerializeField]
        private WeaponSO _unarmed;

        private void OnEnable()
        {
            testEvent.OnRaise += TestEvent_OnRaise;
        }

        private void OnDisable()
        {
            testEvent.OnRaise += TestEvent_OnRaise;
        }

        private async void TestEvent_OnRaise()
        {
            GameObject activeHero = _activeMapHero.Value;
            Hex initialHex = _tileRuntimeSet.GetHexAt(14, 4);

            if (activeHero.TryGetComponent(out IMapUnitController mapUnitController))
            {
                initialHex = _tileRuntimeSet.GetHexAt(mapUnitController.GetHexCoordinatesQ(), mapUnitController.GetHexCoordinatesQ());
            }

            Hex finalHex = _tileRuntimeSet.GetHexAt(14, 4);

            List<Hex> path = _tileRuntimeSet.FindPath(initialHex, finalHex);

            WeaponSO weapon = _unarmed;

            if (activeHero.TryGetComponent(out IWeaponController weaponController))
            {
                weapon = weaponController.GetWeapon();
            }
            if (activeHero.TryGetComponent(out IAnimationMovementController animationController))
            {
                animationController.Animate(weapon.WeaponType.RunAnimationClipName);
            }
            if (activeHero.TryGetComponent(out IMapMovementController movementController))
            {
                //movementController.DoMovement(path);
                await movementController.DoMovement(new List<Vector3>() { new Vector3(0, 0, 0), new Vector3(5, 0, 0), new Vector3(10, 0, 0) });
                animationController.Animate(weapon.WeaponType.IdleAnimationClipName);    
            }

            //if (path.Count > 0)
            //{
            //    foreach (Hex node in path)
            //    {
            //        Debug.Log($"[{node.Q}, {node.R}]"); 
            //    }
            //}
            //else
            //{
            //    Debug.Log("No se encontró un camino válido.");
            //}

        }
    }
}