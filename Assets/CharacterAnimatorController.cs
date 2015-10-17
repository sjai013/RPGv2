using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Battle;
using Battle.Abilities;
using Battle.Targetter;

public class CharacterAnimatorController : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    AbstractTargetter.ActionTargetSubmitted += Animate;
	}

    private void Animate(AbstractBattleCharacter caster, List<AbstractBattleCharacter> targets, AbstractActionAbility ability)
    {
        ability.AnimationBehaviour.Animate(caster, targets);
    }

    // Update is called once per frame
	void Update () {
	
	}
}
