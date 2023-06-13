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
        private int _centerTileColumn;
        [SerializeField]
        private int _centerTileRow;

        private void OnEnable()
        {
            testEvent.OnRaise += TestEvent_OnRaise;
        }

        private void OnDisable()
        {
            testEvent.OnRaise += TestEvent_OnRaise;
        }

        private void TestEvent_OnRaise()
        {
            Hex initialHex = _tileRuntimeSet.GetHexAt(11, 2);
            Hex finalHex = _tileRuntimeSet.GetHexAt(14, 4);



            //foreach (var item in _tileRuntimeSet.Items)
            //{
            //    if (item.GetGameObject().TryGetComponent(out MapTileIndicators tileIndicator))
            //    {
            //        tileIndicator.SetElevation(item.GetHex().Elevation.ToString());
            //        //tileIndicator.SetMoisture(item.GetHex().Moisture.ToString());
            //        tileIndicator.SetMoisture(item.GetHex().IsWalkable.ToString());
            //    }
            //}

            //Pathfinding hexGrid = new Pathfinding(10, 10); // Tamaño del grid hexagonal

            // Configurar nodos transitables e intransitables en el grid
            //hexGrid.SetWalkable(1, 1, false);
            //hexGrid.SetWalkable(2, 1, false);
            //hexGrid.SetWalkable(3, 1, false);
            //hexGrid.SetWalkable(4, 2, false);
            //hexGrid.SetWalkable(4, 3, false);
            // ...

            //HexNode startNode = new HexNode(0, 0); // Nodo de inicio
            //HexNode endNode = new HexNode(9, 9); // Nodo de destino

            List<Hex> path = _tileRuntimeSet.FindPath(initialHex, finalHex);

            if (path.Count > 0)
            {
                foreach (Hex node in path)
                {
                    Debug.Log($"[{node.Q}, {node.R}]"); 
                }
            }
            else
            {
                Debug.Log("No se encontró un camino válido.");
            }

        }
    }
}