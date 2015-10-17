using System;
using Battle.Abilities.AnimationBehaviour;
using Battle.Targetter;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Abilities
{

    public enum TargetTypes
    {
        Self,
        OneFriendly,
        OneEnemy,
        OneEnemyOrFriendly,
        AllFriendly,
        AllEnemy,
        AllFriendlyOrEnemy,
        All
    };

    public enum DefaultTargetType
    {
        Self,
        Allies,
        Enemy
    }

    public abstract class AbstractActionAbility : AbstractAbility
    {
        public TargetTypes TargetType { get; private set; }
        public DefaultTargetType DefaultTarget { get; private set; }
        public AbstractAnimationBehaviour AnimationBehaviour { get; private set; }
  
        protected AbstractActionAbility(String name, int actionCost, AbilityType abilityType, TargetTypes targetTypes,
            DefaultTargetType defaultTarget, AbstractAnimationBehaviour animBehaviour) : base(name, actionCost, abilityType)
        {
            this.TargetType = targetTypes;
            this.DefaultTarget = defaultTarget;
            abilityType |= AbilityType.Action;
            this.AnimationBehaviour = animBehaviour;
        }

    }
}
