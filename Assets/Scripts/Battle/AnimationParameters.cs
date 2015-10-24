using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class AnimationParameters
    {
        private Dictionary<String,int> animHashDictionary = new Dictionary<string, int>();
        private Animator _animator;

        public AnimationParameters(Animator animator)
        {
            _animator = animator;
            SetDefaultAnimHash();
        }

        private void SetDefaultAnimHash()
        {
            SetHash("IdleState", "Base.Idle");

            SetHash("MoveState", "Base.Moving");
            SetHash("MoveBool", "move");

            SetHash("AttackState", "Base.Attacking");
            SetHash("AttackTrigger", "attack");
        }


        public void SetHash(String key, String value)
        {
            animHashDictionary[key] = Animator.StringToHash(value);
        }


        public void SetVariable(String variableName) 
        {
            _animator.SetTrigger(animHashDictionary[variableName]);
        }

        public void SetVariable(String variableName, bool value)
        {
            _animator.SetBool(animHashDictionary[variableName], value);
        }
    

    }
}
