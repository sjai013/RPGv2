using UnityEngine;

namespace Battle.Abilities
{
    public abstract class AbilityContainer : AbstractAbility
    {
        protected AbilityContainer(string name, int actionCost, AbilityType abilityType) : base(name, actionCost, abilityType)
        {
        }

    }
}
