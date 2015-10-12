using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Abilities.Damage
{
    public class PhysicalDamageBehaviour : AbstractDamageBehaviour
    {


        public override void DoDamage(AbstractBattleCharacter caster, AbstractBattleCharacter target)
        {
            Debug.Log(_baseDamage(caster) * _damageReductionProportion(target));
        }

        public override void DoDamage(AbstractBattleCharacter caster, List<AbstractBattleCharacter> target)
        {
            throw new NotImplementedException();
        }

        private float _baseDamage(AbstractBattleCharacter caster)
        {
            return (((Mathf.Pow(caster.Stats.Str,3)) / 32f) + 32) * (damageConstant/16f);

        }

        private float _damageReductionProportion(AbstractBattleCharacter target)
        {
            return (((Mathf.Pow(target.Stats.Def - 280.4f,2))/110f) + 16)/730;
        }

        public PhysicalDamageBehaviour(int damageConstant) : base(damageConstant)
        {
        }
    }
}
