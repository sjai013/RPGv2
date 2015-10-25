using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using Battle;
using Battle.Events.BattleCharacter;
using JainEventAggregator;

public class AnimationStateMachineBehaviour : StateMachineBehaviour, IListener<TakingDamage>
{

    private Animator animator;
    private AbstractBattleCharacter battleCharacter;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;
        this.battleCharacter = animator.GetComponent<AbstractBattleCharacter>();
        this.RegisterAllListeners();

    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMachineEnter is called when entering a statemachine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){
    //
    //}

    // OnStateMachineExit is called when exiting a statemachine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
        this.UnregisterAllListeners();
    }
    public void Handle(TakingDamage message)
    {
        if (message.Target != battleCharacter) return;
        animator.SetTrigger("takeDamage");
    }
}
