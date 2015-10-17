

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Battle.Abilities.AnimationBehaviour;
using Battle.Abilities.Damage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle.Abilities
{

    public class Attack : AbstractActionAbility
    {
        private const String _name = "Attack";
        private static int _actionCost = 3;
        private static AbilityType _abilityType = AbilityType.Action | AbilityType.Base;
        private static TargetTypes _targetTypes = TargetTypes.OneEnemyOrFriendly;
        private static DefaultTargetType _defaultTargetType = DefaultTargetType.Enemy;
        private static AbstractAnimationBehaviour animationBehaviour = new NormalAttackBehaviour();

        public Attack() : 
            base(_name, _actionCost, _abilityType, _targetTypes, _defaultTargetType, animationBehaviour)
        {

        }

        protected override void DoAction()
        {
            Debug.Log("Submitted: " + Name + ".");
        }
    }
}
