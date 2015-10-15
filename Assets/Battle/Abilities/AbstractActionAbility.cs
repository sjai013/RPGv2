using System.Collections;
using System.Collections.Generic;
using Battle.Abilities.AnimationBehaviour;
using Battle.Abilities.Damage;
using Battle.Targetter;
using UnityEngine;
using UnityEngine.EventSystems;

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
        public abstract TargetTypes TargetType { get; }
        public abstract DefaultTargetType DefaultTarget { get; }
        protected abstract List<AbstractDamageBehaviour> DamageBehaviour { get; set; }

        private AbstractAnimationBehaviour casterAnimationBehaviour;
        private AbstractAnimationBehaviour targetAnimationBehaviour;


        public override void OnSubmit(BaseEventData eventData)
        {
            Debug.Log("Bringing up targetting window to select targets");
            Debug.Log(this.name);
            OnAbilitySubmit(this);

            AbstractTargetter.ActionTargetSelected += DoAbilityAnimation;

            _mainActionCanvasGroup.interactable = false;
            _mainActionCanvasGroup.gameObject.SetActive(false);

            //TODO: New class to define actions after target is selected (Ability class shouldn't define action, but should register to this new class?)
        }

        private void DoAbilityAnimation(AbstractBattleCharacter caster, List<AbstractBattleCharacter> target, AbstractActionAbility ability)
        {
            AbstractTargetter.ActionTargetSelected -= DoAbilityAnimation;
            Debug.Log(caster + " " + target[0] + " " + ability);

            throw new System.NotImplementedException();
            
        }
    }
}
