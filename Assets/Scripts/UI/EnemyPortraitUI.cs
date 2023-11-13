using AutoFantasy.Scripts.Enemy;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI
{
    public class EnemyPortraitUI : MonoBehaviour
    {
        [SerializeField]
        private Camera _heroCamera;
        [SerializeField]
        private RawImage _rawImage;
        [SerializeField]
        private Transform _enemyVisualTransform;
        [SerializeField]
        private EnemyController _enemyController;

        public void Initialize(EnemySO enemy)
        {
            var tex = new RenderTexture(512, 512, 16);
            _heroCamera.targetTexture = tex;
            _rawImage.texture = tex;

            GameObject enemyObject = _enemyController.Initialize(enemy.EnemyVisualPrefab);

            if (TryGetComponent(out IAnimationController animationController))
            {
                if (enemyObject.TryGetComponent(out Animator animator))
                {
                    animationController.SetAnimator(animator);
                    animationController.SetWeaponType(enemy.Weapon.WeaponType);
                    animationController.Idle();
                }
            }

            if (enemyObject.TryGetComponent(out IWeaponController weaponController))
            {
                weaponController.ShowWeapon(enemy.Weapon);
            }

            enemyObject.transform.SetParent(_enemyVisualTransform);
        }
    }
}