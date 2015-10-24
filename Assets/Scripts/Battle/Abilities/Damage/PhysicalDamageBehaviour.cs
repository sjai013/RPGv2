using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ExtensionMethods;
using Random = UnityEngine.Random;

namespace Battle.Abilities.Damage
{
    public class PhysicalDamageBehaviour : AbstractDamageBehaviour
    {

        public override Damage DoDamage(AbstractBattleCharacter caster, AbstractBattleCharacter target)
        {
            Damage damage = new Damage
            {
                damage = (int) (_baseDamage(caster)*_damageReductionProportion(target)*Random.Range(0.95f, 1.05f)),
                hit = _isHit(caster, target)
            };
            return damage;
        }

        public override Damage DoDamage(AbstractBattleCharacter caster, List<AbstractBattleCharacter> target)
        {
            Damage damage = new Damage
            {
                damage = (int)(_baseDamage(caster) * _damageReductionProportion(target[0]) * Random.Range(0.95f, 1.05f)),
                hit = _isHit(caster, target[0])
            };
            return damage;
        }

        private float _baseDamage(AbstractBattleCharacter caster)
        {
            return (((Mathf.Pow(caster.Stats.Str,3)) / 32f) + 32) * (_damageConstant/16f);
        }

        private static float _damageReductionProportion(AbstractBattleCharacter target)
        {
            return (((Mathf.Pow(target.Stats.Def - 280.4f,2))/110f) + 16)/730;
        }

        private static bool _isHit(AbstractBattleCharacter caster, AbstractBattleCharacter target)
        {
            float acNum = caster.Stats.Acc*0.4f - target.Stats.Eva + 9;
            float missChance = acNum.Polynomial(1.1688f, -0.8506f, 28.576f)/100;
            bool miss = missChance.ToRandom();
            return miss;
        }

        public PhysicalDamageBehaviour(int damageConstant) : base(damageConstant)
        {
        }
    }
}
