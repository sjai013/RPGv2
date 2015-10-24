using System;
using System.Collections.Generic;
using Battle.Abilities;
using Battle.Events.BattleCharacter;
using Battle.Events.BattleCharacter.Targetting;
using ExtensionMethods;
using JainEventAggregator;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Targetter
{
    public abstract class AbstractTargetter : MonoBehaviour, IListener<SubmitAbilityAtTarget>
    {
        public static AbstractTargetter TargettingSystem;

        public abstract String Identifier { get; }

        public List<AbstractBattleCharacter> Targets { get; private set; }

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

                this.RegisterAllListeners();
                AbilityButton.AnAbilitySubmitted += InitiateTargetting;
            }
        }

        protected abstract void InitiateTargetting(AbstractAbility ability);

        protected abstract void Initialise();

        protected virtual void OnTargetChange(AbstractBattleCharacter battlechar)
        {
            EventAggregator.RaiseEvent(new TargetChanged() {Caster = AbstractBattleCharacter.ActiveBattleCharacter, Target = battlechar});
        }


        void OnDestroy()
        {
            this.UnregisterAllListeners();
        }

        public void Handle(SubmitAbilityAtTarget message)
        {
            this.Targets = message.Targets;
            EventAggregator.RaiseEvent(new CharacterAnimating() {Ability = message.Ability, Caster = message.Caster, Targets = message.Targets});
        }
    }


}
