using UnityEngine;
using System.Collections;
using Battle;
using Battle.Events.TurnSystem;
using Battle.Targetter;
using JainEventAggregator;

public class MoveToTargetStateMachineBehaviour : StateMachineBehaviour
{

    private AbstractBattleCharacter _thisBattleCharacter;
    private Animator _animator;
    private AnimatorStateInfo _stateInfo;
    [SerializeField] private bool _target;
    [SerializeField] private bool _originalPosition;
    private float _baseMoveMultiplier;
        
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        this._animator = animator;
        this._stateInfo = stateInfo;

        _baseMoveMultiplier = _animator.GetFloat("moveMultiplier");

        _thisBattleCharacter = animator.GetComponent<AbstractBattleCharacter>();
        _thisBattleCharacter.StartCoroutine(RotateAndMoveTowards(_thisBattleCharacter.gameObject));

	}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    protected IEnumerator RotateAndMoveTowards(GameObject rotatee)
    {
        Quaternion targetRot = Quaternion.identity;
        Vector3 targetPos = Vector3.zero;
        var stopDist = _thisBattleCharacter.General.AttackRange / 100;
        _thisBattleCharacter.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (_target)
        {
            targetPos = AbstractTargetter.TargettingSystem.Targets[0].transform.position;
            var dir = targetPos - _thisBattleCharacter.transform.position;
            if (dir.sqrMagnitude > 0.1)
                targetRot = Quaternion.LookRotation(dir);
            yield return _thisBattleCharacter.StartCoroutine(RotateTowards(_thisBattleCharacter.gameObject, targetRot));
            yield return _thisBattleCharacter.StartCoroutine(MoveTowards(_thisBattleCharacter.gameObject, targetPos, stopDist));
            _originalPosition = false;

        }

        if (_originalPosition)
        {
            targetPos = _thisBattleCharacter.OriginalPosition;
            var dir = targetPos - _thisBattleCharacter.transform.position;
            if (dir.sqrMagnitude > 0.1)
                targetRot = Quaternion.LookRotation(dir);

             yield return _thisBattleCharacter.StartCoroutine(RotateTowards(_thisBattleCharacter.gameObject, targetRot));

             yield return _thisBattleCharacter.StartCoroutine(MoveTowards(_thisBattleCharacter.gameObject, targetPos, 0.2f, exact: true));
            
            //Also revert direction character  isfacing to original after doing the whole "move, attack, move back" shebang.
             yield return _thisBattleCharacter.StartCoroutine(RotateTowards(_thisBattleCharacter.gameObject, _thisBattleCharacter.OriginalRotation));
            _target = false;
            EventAggregator.RaiseEvent(new TurnFinished());
        }

        _animator.SetTrigger("moveComplete");

    }

    protected IEnumerator RotateTowards(GameObject rotatee, Quaternion targetRotation, float speed = 10.0f)
    {
        _animator.SetFloat("moveMultiplier", 0.0f);
        var startRot = rotatee.transform.rotation;
        float time = 0f;
        while (time < 1)
        {
            rotatee.transform.rotation = Quaternion.Slerp(startRot, targetRotation, time);
            time += Time.deltaTime * speed;
            yield return null;
        }
        

        rotatee.transform.rotation = targetRotation;
        _animator.SetFloat("moveMultiplier", _baseMoveMultiplier);
        yield return null;
    }


    protected IEnumerator MoveTowards(GameObject rotatee, Vector3 destination, float stopDist, bool exact = false, float speed = 10.0f)
    {
        _animator.SetFloat("moveMultiplier",_baseMoveMultiplier);

        Vector3 dir;
        while (Vector3.Distance(rotatee.transform.position, destination) > stopDist)
        {
            dir = Vector3.ClampMagnitude(destination - rotatee.transform.position,1.0f);
            rotatee.transform.position += dir * Time.deltaTime * _thisBattleCharacter.General.MoveSpeed;
            yield return null;
        }

        if (exact)
            _thisBattleCharacter.transform.position = destination;

    }

}
