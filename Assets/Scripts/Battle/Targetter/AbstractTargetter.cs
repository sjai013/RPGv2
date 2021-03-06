﻿using System;
using System.Collections.Generic;
using Battle.Abilities;
using Battle.Events.Abilities;
using Battle.Events.BattleCharacter;
using Battle.Events.Targetting;
using Battle.Events.TurnSystem;
using JainEventAggregator;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Targetter
{
    public abstract class AbstractTargetter : MonoBehaviour, IListener<AbilitySubmitted>, IListener<SubmitAbilityAtTarget>
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
            }
        }

        protected abstract void InitiateTargetting(AbstractAbility ability);

        protected abstract void Initialise();

        protected virtual void OnTargetChange(AbstractBattleCharacter battlechar)
        {
            //TODO: Where should OnTargetChange be called?  Need something like an OnSelect for pointer
            EventAggregator.RaiseEvent(new TargetChanged() {Caster = AbstractBattleCharacter.ActiveBattleCharacter, Target = battlechar});
        }


        protected abstract void EndTargetting();

        void OnDestroy()
        {
            this.UnregisterAllListeners();
        }

        public void Handle(SubmitAbilityAtTarget message)
        {
            this.Targets = message.Targets;
            EndTargetting();
            EventAggregator.RaiseEvent(new CharacterAnimating() {Ability = message.Ability, Caster = message.Caster, Targets = message.Targets});
        }

        public void Handle(AbilitySubmitted message)
        {
            PrepareTargets(message.Ability as AbstractActionAbility);
        }

    }


}
