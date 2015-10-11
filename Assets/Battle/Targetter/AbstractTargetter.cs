using System.Collections.Generic;
using Battle.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Targetter
{
    public abstract class AbstractTargetter : MonoBehaviour
    {
        public delegate void BattleCharDelegate(AbstractBattleCharacter battleChar);
        public delegate void ActionAbilityDelegate(AbstractBattleCharacter caster, List<AbstractBattleCharacter> target, AbstractAbility ability);
        public event BattleCharDelegate TargetChange;
        public event ActionAbilityDelegate ActionTargetSelected;

        public abstract void PrepareTargets(AbstractAbility ability);

        protected virtual void Awake()
        {
            AbstractBattleCharacter.TargettingSystem = this;
        }

        protected virtual void OnTargetChange(AbstractBattleCharacter battlechar)
        {
            var handler = TargetChange;
            if (handler != null) handler(battlechar);
        }

        protected virtual void OnActionTargetSelected(AbstractBattleCharacter caster, List<AbstractBattleCharacter> targets, AbstractAbility ability)
        {
            var handler = ActionTargetSelected;
            if (handler != null) handler(caster, targets, ability);
        }
    }


}
