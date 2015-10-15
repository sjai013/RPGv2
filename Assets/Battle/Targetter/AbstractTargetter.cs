using System.Collections.Generic;
using Battle.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Targetter
{
    public abstract class AbstractTargetter : MonoBehaviour
    {
        public delegate void BattleCharDelegate(AbstractBattleCharacter battleChar);
        public delegate void ActionTargetDelegate(AbstractBattleCharacter caster, List<AbstractBattleCharacter> target, AbstractActionAbility ability);
        public event BattleCharDelegate TargetChange;
        public static event ActionTargetDelegate ActionTargetSelected;

        public abstract void PrepareTargets(AbstractActionAbility abstractActionAbility);

        void Awake()
        {
            AbstractBattleCharacter.TargettingSystem = this;
            Initialise();
        }

        protected abstract void Initialise();

        protected virtual void OnTargetChange(AbstractBattleCharacter battlechar)
        {
            var handler = TargetChange;
            if (handler != null) handler(battlechar);
        }

        protected virtual void OnActionTargetSelected(AbstractBattleCharacter caster, List<AbstractBattleCharacter> targets, AbstractActionAbility ability)
        {
            var handler = ActionTargetSelected;
            if (handler != null) handler(caster, targets, ability);
        }

    }


}
