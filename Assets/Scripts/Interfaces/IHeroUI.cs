using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.ScriptableObjects;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IHeroUI 
    {
        Hero Hero { get; }
        ActiveHeroSO GetActiveHero();
    }
}