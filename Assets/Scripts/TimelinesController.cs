using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Skills;
using UnityEngine;
using UnityEngine.Playables;

namespace AutoFantasy.Scripts
{
    public class TimelinesController : MonoBehaviour
    {
        [SerializeField]
        private PlayableDirector _newRoundTimeline;
        [SerializeField]
        private GameEvent _newRoundEvent;
        [SerializeField]
        private GameEvent _heroSkillEvent;
        [SerializeField]
        private PlayableDirector _skillTimeline;
        [SerializeField]
        private ActiveHeroSO _activeHero;
        [SerializeField]
        private SkillDatabase _skillDatabase;

        private void OnEnable()
        {
            _newRoundEvent.OnRaise += NewRound;
            _heroSkillEvent.OnRaise += BeginSkill;
        }

        private void OnDisable()
        {
            _newRoundEvent.OnRaise -= NewRound;
            _heroSkillEvent.OnRaise -= BeginSkill;
        }

        private void BeginSkill()
        {
            Hero hero = _activeHero.ActiveHero;
            SkillSO skillSO = _skillDatabase.GetSkillById(hero.SkillId);

            _skillTimeline.playableAsset = skillSO.SkillTimeline;
            _skillTimeline.Play();
        }

        private void NewRound()
        {
            _newRoundTimeline.Play();
        }
    }
}