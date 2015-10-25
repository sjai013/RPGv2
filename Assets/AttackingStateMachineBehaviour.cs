using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Battle;
using Battle.Abilities;
using Battle.Events.BattleCharacter;
using JainEventAggregator;

public class AttackingStateMachineBehaviour : StateMachineBehaviour
{

    private GameObject _projectilePrefab;
    private GameObject _projectileBone;
    private GameObject _projectile;
    private bool _projectileShooting = false;
    private AbstractBattleCharacter _caster;
    public AbstractActionAbility Ability;
    public List<AbstractBattleCharacter> Targets; 

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        _caster = animator.GetComponent<AbstractBattleCharacter>();
        _projectileShooting = false;

        _projectilePrefab = _caster.General.Projectile;
	    _projectileBone = _caster.General.ProjectileBone;

	    if (_projectilePrefab == null || _projectileBone == null) return;
	    if (_projectile != null) return;
	    _projectile = Instantiate(_projectilePrefab);
	    _projectile.transform.SetParent(_projectileBone.transform);
	    _projectile.transform.localPosition = Vector3.zero;
	    _projectile.transform.localScale = Vector3.one;
	    _projectile.transform.localRotation = Quaternion.Euler(new Vector3(340f, 180f, 0f));
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	    if (_projectile == null) return;
	    if ((_projectileShooting) || !(stateInfo.normalizedTime > 0.5)) return;
	    _projectileShooting = true;
	    _projectile.transform.SetParent(null);
	    _caster.StartCoroutine(Shoot(_projectile, Targets[0].gameObject));
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        _projectileShooting = false;
	    Ability = null;
	    Targets = null;
	}


    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

    private void SendDamageSignal()
    {
        EventAggregator.RaiseEvent(new DoDamage() {Ability = Ability, Targets = Targets});
    }
    protected IEnumerator Shoot(GameObject projectile, GameObject target)
    {

        var targetMid = target.GetComponentInChildren<Renderer>().bounds.center;
        while (Vector3.Distance(projectile.transform.position, targetMid) > 0.2)
        {
            projectile.transform.LookAt(targetMid);
            projectile.transform.position += projectile.transform.forward*Time.deltaTime*15;
            yield return null;
        }

        SendDamageSignal();

        yield return new WaitForSeconds(0.4f);
        Destroy(projectile);

        while (_projectileShooting == false)
        {
            yield return null;
        }

    }

}