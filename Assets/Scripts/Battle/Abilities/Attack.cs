

using System;
using Battle.Abilities.Damage;
using UnityEngine;

namespace Battle.Abilities
{

    public class Attack : AbstractActionAbility
    {
        private const String _name = "Attack";
        private static int _actionCost = 3;
        private static AbilityType _abilityType = AbilityType.Action | AbilityType.Base;
        private static TargetTypes _targetTypes = TargetTypes.OneEnemyOrFriendly;
        private static DefaultTargetType _defaultTargetType = DefaultTargetType.Enemy;
        private static AnimationType _animationType = AnimationType.BasicAttack;
        private static AbstractDamageBehaviour _damageBehaviour = new PhysicalDamageBehaviour(16);

        public Attack() : 
            base(name: _name, actionCost: _actionCost, abilityType: _abilityType, targetTypes: _targetTypes, defaultTarget: _defaultTargetType, animType: _animationType, damageBehaviour: _damageBehaviour)
        {

        }

        protected override void DoAction()
        {
            Debug.Log("Submitted: " + Name + ".");
        }
    }
}
