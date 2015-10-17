using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Abilities.AnimationBehaviour
{
    public class NormalAttackBehaviour : AbstractAnimationBehaviour
    {
        public override void Animate(AbstractBattleCharacter caster, List<AbstractBattleCharacter> targets)
        {
            caster.StartCoroutine(doAnimation(caster, targets));
        }

        protected override IEnumerator doAnimation(AbstractBattleCharacter caster, List<AbstractBattleCharacter> targets)
        {
            Debug.Log("Animating");
            yield return caster.StartCoroutine(MoveToTarget(caster, targets));
            yield return caster.StartCoroutine(DoAttackAnimation(caster, targets));
        }
    }
}
