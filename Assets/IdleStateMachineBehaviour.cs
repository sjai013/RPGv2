using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Battle;
using Battle.Abilities;
using Battle.Events.BattleCharacter;
using JainEventAggregator;
using UnityEngine.Assertions;

public class IdleStateMachineBehaviour : StateMachineBehaviour, IListener<CharacterAnimating>
{

    private AbstractBattleCharacter _thisBattleCharacter;
    private Animator _animator;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        this.RegisterAllListeners();
	    _thisBattleCharacter = animator.GetComponent<AbstractBattleCharacter>(); 
	    Assert.IsNotNull(_thisBattleCharacter, "Battle Character not found");
	    this._animator = animator;
	}

    private void SelectNextState(AbstractBattleCharacter caster, List<AbstractBattleCharacter> targets, AbstractActionAbility ability)
    {
        //TODO: Code to implement something like an AbilityAnimationBehaviour class, so we can select the appropriate next state based on that
        Assert.IsNotNull(_thisBattleCharacter);
        if (caster != _thisBattleCharacter) return;

        if (ability.AnimationType == AnimationType.BasicAttack)
        {
            _animator.SetTrigger("doNormalAttack");

            foreach (var smb in _animator.GetBehaviours<AttackingStateMachineBehaviour>())
            {
                smb.Ability = ability;
                smb.Targets = targets;
            }

        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        this.UnregisterAllListeners();
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}


    public void Handle(CharacterAnimating message)
    {
        SelectNextState(message.Caster,message.Targets,message.Ability);
    }
}
