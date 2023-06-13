using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace AutoFantasy.Scripts.Map
{
    public class MapTileBordersController : MonoBehaviour
    {
        [SerializeField]
        private IntVariable _mapNumColumns;
        [SerializeField]
        private Transform _topRightShoreTransform;
        [SerializeField]
        private Transform _rightShoreTransform;
        [SerializeField]
        private Transform _botRightShoreTransform;
        [SerializeField]
        private Transform _botLeftShoreTransform;
        [SerializeField]
        private Transform _leftShoreTransform;
        [SerializeField]
        private Transform _topLeftShoreTransform;

        [SerializeField]
        private Transform _topNoWaterCornerTransform;
        [SerializeField]
        private Transform _topTopRightWaterCornerTransform;
        [SerializeField]
        private Transform _topTopLeftWaterCornerTransform;

        [SerializeField]
        private Transform _topRightNoWaterCornerTransform;
        [SerializeField]
        private Transform _topRightRightWaterCornerTransform;
        [SerializeField]
        private Transform _topRightTopRightWaterCornerTransform;

        [SerializeField]
        private Transform _botRightNoWaterCornerTransform;
        [SerializeField]
        private Transform _botRightBotRightWaterCornerTransform;
        [SerializeField]
        private Transform _botRightRightWaterCornerTransform;

        [SerializeField]
        private Transform _botNoWaterCornerTransform;
        [SerializeField]
        private Transform _botBotRightWaterCornerTransform;
        [SerializeField]
        private Transform _botBotLeftWaterCornerTransform;

        [SerializeField]
        private Transform _botLeftNoWaterCornerTransform;
        [SerializeField]
        private Transform _botLeftBotLeftWaterCornerTransform;
        [SerializeField]
        private Transform _botLeftLeftWaterCornerTransform;

        [SerializeField]
        private Transform _topLeftNoWaterCornerTransform;
        [SerializeField]
        private Transform _topLeftLeftWaterCornerTransform;
        [SerializeField]
        private Transform _topLeftTopLeftWaterCornerTransform;

        //public void ShowTopCorner(GameObject cornerNoWaterPrefab, GameObject cornerWithWaterPrefab)
        //{
        //    if (TryGetComponent(out ITile myTile))
        //    {
        //        Hex myHex = myTile.GetHex();
        //        Hex topRightNeighbourHex = myHex.TopRightNeighbourHex;
        //        Hex topLeftNeighbourHex = myHex.TopLeftNeighbourHex;

        //        if (topRightNeighbourHex != null && topLeftNeighbourHex != null)
        //            if (topRightNeighbourHex.Elevation >= 0)
        //                if (topLeftNeighbourHex.Elevation >= 0)
        //                    Instantiate(cornerNoWaterPrefab, _topNoWaterCornerTransform);
        //                else
        //                    Instantiate(cornerWithWaterPrefab, _topTopLeftWaterCornerTransform);
        //            else
        //                if (topLeftNeighbourHex.Elevation >= 0)
        //                    Instantiate(cornerWithWaterPrefab, _topTopRightWaterCornerTransform);
        //    }
        //}

        //public void ShowTopRightCorner(GameObject cornerNoWaterPrefab, GameObject cornerWithWaterPrefab)
        //{
        //    if (TryGetComponent(out ITile myTile))
        //    {
        //        Hex myHex = myTile.GetHex();
        //        Hex topRightNeighbourHex = myHex.TopRightNeighbourHex;
        //        Hex rightNeighbourHex = myHex.RightNeighbourHex;

        //        if (topRightNeighbourHex != null)
        //            if (topRightNeighbourHex.Elevation >= 0)
        //                if (rightNeighbourHex.Elevation >= 0)
        //                    Instantiate(cornerNoWaterPrefab, _topRightNoWaterCornerTransform);
        //                else
        //                    Instantiate(cornerWithWaterPrefab, _topRightRightWaterCornerTransform);
        //            else
        //                if (rightNeighbourHex.Elevation >= 0)
        //                    Instantiate(cornerWithWaterPrefab, _topRightTopRightWaterCornerTransform);
        //    }
        //}

        //public void ShowBotRightCorner(GameObject cornerNoWaterPrefab, GameObject cornerWithWaterPrefab)
        //{
        //    if (TryGetComponent(out ITile myTile))
        //    {
        //        Hex myHex = myTile.GetHex();
        //        Hex botRightNeighbourHex = myHex.BotRightNeighbourHex;
        //        Hex rightNeighbourHex = myHex.RightNeighbourHex;

        //        if (botRightNeighbourHex != null)
        //            if (botRightNeighbourHex.Elevation >= 0)
        //                if (rightNeighbourHex.Elevation >= 0)
        //                    Instantiate(cornerNoWaterPrefab, _botRightNoWaterCornerTransform);
        //                else
        //                    Instantiate(cornerWithWaterPrefab, _botRightRightWaterCornerTransform);
        //                else
        //                    if (rightNeighbourHex.Elevation >= 0)
        //                        Instantiate(cornerWithWaterPrefab, _botRightBotRightWaterCornerTransform);
        //    }
        //}

        //public void ShowBotCorner(GameObject cornerNoWaterPrefab, GameObject cornerWithWaterPrefab)
        //{
        //    if (TryGetComponent(out ITile myTile))
        //    {
        //        Hex myHex = myTile.GetHex();
        //        Hex botRightNeighbourHex = myHex.BotRightNeighbourHex;
        //        Hex botLeftNeighbourHex = myHex.BotLeftNeighbourHex;

        //        if (botRightNeighbourHex != null && botLeftNeighbourHex != null)
        //            if (botRightNeighbourHex.Elevation >= 0)
        //                if (botLeftNeighbourHex.Elevation >= 0)
        //                    Instantiate(cornerNoWaterPrefab, _botNoWaterCornerTransform);
        //                else
        //                    Instantiate(cornerWithWaterPrefab, _botBotLeftWaterCornerTransform);
        //            else
        //                if (botLeftNeighbourHex.Elevation >= 0)
        //                    Instantiate(cornerWithWaterPrefab, _botBotRightWaterCornerTransform);
        //    }
        //}

        //public void ShowBotLeftCorner(GameObject cornerNoWaterPrefab, GameObject cornerWithWaterPrefab)
        //{
        //    if (TryGetComponent(out ITile myTile))
        //    {
        //        Hex myHex = myTile.GetHex();
        //        Hex leftNeighbourHex = myHex.LeftNeighbourHex;
        //        Hex botLeftNeighbourHex = myHex.BotLeftNeighbourHex;

        //        if (leftNeighbourHex != null && botLeftNeighbourHex != null)
        //            if (leftNeighbourHex.Elevation >= 0)
        //                if (botLeftNeighbourHex.Elevation >= 0)
        //                    Instantiate(cornerNoWaterPrefab, _botLeftNoWaterCornerTransform);
        //                else
        //                    Instantiate(cornerWithWaterPrefab, _botLeftBotLeftWaterCornerTransform);
        //            else
        //                if (botLeftNeighbourHex.Elevation >= 0)
        //                    Instantiate(cornerWithWaterPrefab, _botLeftLeftWaterCornerTransform);
        //    }
        //}

        //public void ShowTopLeftCorner(GameObject cornerNoWaterPrefab, GameObject cornerWithWaterPrefab)
        //{
        //    if (TryGetComponent(out ITile myTile))
        //    {
        //        Hex myHex = myTile.GetHex();
        //        Hex leftNeighbourHex = myHex.LeftNeighbourHex;
        //        Hex topLeftNeighbourHex = myHex.TopLeftNeighbourHex;

        //        if (leftNeighbourHex != null && topLeftNeighbourHex != null)
        //            if (leftNeighbourHex.Elevation >= 0)
        //                if (topLeftNeighbourHex.Elevation >= 0)
        //                    Instantiate(cornerNoWaterPrefab, _topLeftNoWaterCornerTransform);
        //                else
        //                    Instantiate(cornerWithWaterPrefab, _topLeftTopLeftWaterCornerTransform);
        //            else
        //                if (topLeftNeighbourHex.Elevation >= 0)
        //                    Instantiate(cornerWithWaterPrefab, _topLeftLeftWaterCornerTransform);
        //    }
        //}

        //public void ShowBorders(GameObject borderPrefab)
        //{
        //    if (TryGetComponent(out ITile myTile))
        //    {
        //        Hex myHex = myTile.GetHex();
        //        Hex topRightNeighbourHex = myHex.TopRightNeighbourHex;
        //        Hex rightNeighbourHex = myHex.RightNeighbourHex;
        //        Hex botRightNeighbourHex = myHex.BotRightNeighbourHex;
        //        Hex botLeftNeighbourHex = myHex.BotLeftNeighbourHex;
        //        Hex leftNeighbourHex = myHex.LeftNeighbourHex;
        //        Hex topLeftNeighbourHex = myHex.TopLeftNeighbourHex;

        //        if (topRightNeighbourHex != null)
        //            if (topRightNeighbourHex.Elevation >= 0)
        //                Instantiate(borderPrefab, _topRightShoreTransform);

        //        if (rightNeighbourHex.Elevation >= 0)
        //            Instantiate(borderPrefab, _rightShoreTransform);

        //        if (botRightNeighbourHex != null)
        //            if (botRightNeighbourHex.Elevation >= 0)
        //                Instantiate(borderPrefab, _botRightShoreTransform);

        //        if (botLeftNeighbourHex != null)
        //            if (botLeftNeighbourHex.Elevation >= 0)
        //                Instantiate(borderPrefab, _botLeftShoreTransform); 

        //        if (leftNeighbourHex.Elevation >= 0)
        //            Instantiate(borderPrefab, _leftShoreTransform);

        //        if (topLeftNeighbourHex != null)
        //            if (topLeftNeighbourHex.Elevation >= 0)
        //                Instantiate(borderPrefab, _topLeftShoreTransform);
        //    }
        //}
    }
}