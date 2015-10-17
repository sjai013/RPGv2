using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Abilities.AnimationBehaviour
{

    //TODO: THIS CLASS
    public abstract class AbstractAnimationBehaviour
    {
        public abstract void Animate(AbstractBattleCharacter caster, List<AbstractBattleCharacter> targets );

        protected abstract IEnumerator doAnimation(AbstractBattleCharacter caster, List<AbstractBattleCharacter> targets);

        protected IEnumerator MoveToTarget(AbstractBattleCharacter caster, List<AbstractBattleCharacter> targets)
        {
            Debug.Log("Moving to target.");
            caster.transform.LookAt(targets[0].transform);
            while (Vector3.Distance(caster.transform.position, targets[0].transform.position) >
                   caster.General.AttackRange/100)
            {
                caster.AnimParameters.SetVariable("MoveBool",true);
                caster.transform.position += caster.transform.forward * Time.deltaTime * caster.General.MoveSpeed;

                yield return null;
            }
            caster.AnimParameters.SetVariable("MoveBool", false);
            yield return null;
        }

        protected IEnumerator DoAttackAnimation(AbstractBattleCharacter caster, List<AbstractBattleCharacter> targets)
        {
            Debug.Log("Attacking.");
            caster.AnimParameters.SetVariable("AttackTrigger");
            yield return null;
        }

    }
}
