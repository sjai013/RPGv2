using System;
using System.Collections.Generic;
using Battle.Abilities.Damage;
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

    public enum AnimationType
    {
        BasicAttack,
    }

    public abstract class AbstractActionAbility : AbstractAbility
    {
        private string name;
        private int actionCost;
        private AbilityType abilityType;
        private TargetTypes targetTypes;

        public TargetTypes TargetType { get; private set; }
        public DefaultTargetType DefaultTarget { get; private set; }
        //public AbstractAnimationBehaviour AnimationBehaviour { get; private set; }
        public AbstractDamageBehaviour DamageBehaviour { get; private set; }
        public AnimationType AnimationType { get; private set; }
  
        protected AbstractActionAbility(String name, int actionCost, AbilityType abilityType, TargetTypes targetTypes,
            DefaultTargetType defaultTarget, AnimationType animType , AbstractDamageBehaviour damageBehaviour) : base(name, actionCost, abilityType)
        {
            this.TargetType = targetTypes;
            this.DefaultTarget = defaultTarget;
            abilityType |= AbilityType.Action;
            //this.AnimationBehaviour = animBehaviour;
            this.DamageBehaviour = damageBehaviour;
            this.AnimationType = animType;
        }

  
        public AbstractDamageBehaviour.Damage DoDamage(AbstractBattleCharacter caster, List<AbstractBattleCharacter> targets)
        {
            return DamageBehaviour.DoDamage(caster, targets[0]);
        }

    }

    

}
