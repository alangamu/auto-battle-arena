using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "ItemRaritySO", menuName = "Items/ItemRaritySO")]
    public class ItemRaritySO : ScriptableObject
    {
        public int ItemRarityId;
        public int ArmorRandomStatCount;
        public int ArmorFixedStatCount;
        public int WeaponRandomStatCount;
        public int WeaponFixedStatCount;
    }
}