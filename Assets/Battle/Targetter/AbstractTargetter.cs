using System;
using System.Collections.Generic;
using Battle.Abilities;
using ExtensionMethods;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Targetter
{
    public abstract class AbstractTargetter : MonoBehaviour
    {
        public static AbstractTargetter TargettingSystem;

        public abstract String Identifier { get; }

        public delegate void BattleCharDelegate(AbstractBattleCharacter battleChar);
        public delegate void ActionTargetDelegate(AbstractBattleCharacter caster, List<AbstractBattleCharacter> target, AbstractActionAbility ability);
        public event BattleCharDelegate TargetChange;
        public static event ActionTargetDelegate ActionTargetSubmitted;

        public abstract void PrepareTargets(AbstractActionAbility abstractActionAbility);

        void Awake()
        {
            if (TargettingSystem != null)
            {
                Debug.LogWarning("Multiple targetting systems trying to load.  Ignoring " + Identifier + ".");
            }
            else
            {
                Debug.Log("Loading targetting System: " + Identifier + ".");
                TargettingSystem = this;
                Initialise();
                AbilityButton.AnAbilitySubmitted += InitiateTargetting;
            }
        }

        protected abstract void InitiateTargetting(AbstractAbility ability);

        protected abstract void Initialise();

        protected virtual void OnTargetChange(AbstractBattleCharacter battlechar)
        {
            var handler = TargetChange;
            if (handler != null) handler(battlechar);
        }

        protected virtual void OnActionTargetSubmitted(AbstractBattleCharacter caster, List<AbstractBattleCharacter> targets, AbstractActionAbility ability)
        {
            var handler = ActionTargetSubmitted;
            if (handler != null) handler(caster, targets, ability);
        }

    }


}
