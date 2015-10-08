using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle.Abilities
{
    [RequireComponent(typeof(Button))]
    public abstract  class AbstractAbility: MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler
    {
        protected enum TargetTypes
        {
            Self,
            OneFriendly,
            OneEnemy,
            OneEnemyOrFriendly,
            AllFriendly,
            AllEnemy,
            AllFriendlyOrEnemy,
            All
        };

        protected virtual TargetTypes TargetType { get; set; }
        public virtual String Name { get; protected set; }

        public virtual int ActionCost { get; protected set; }

        public delegate void Ability(AbstractAbility ability, List<AbstractBattleCharacter> alliesAffected, List<AbstractBattleCharacter> enemiesAffected);

        public static event Ability SelectedAbilityChanged;

        private static List<AbstractAbility> _abilities = new List<AbstractAbility>();

        private static AbstractAbility _currentAbility;

        public static AbstractAbility CurrentSelectedAbility
        {
            get
            {
                return _currentAbility; 
                
            }

            protected set
            {
                if (_currentAbility == value)
                    return;

                _currentAbility = value;
                if (value == null) return;
                value.OnSelectedAbilityChanged(value);
            }
        }

        void Awake()
        {
            _abilities.Add(this);
        }


        public void OnSelect(BaseEventData eventData)
        {
            CurrentSelectedAbility = this;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            CurrentSelectedAbility = null;
        }

        protected virtual void OnSelectedAbilityChanged(AbstractAbility ability)
        {
            var handler = SelectedAbilityChanged;
            if (handler != null) handler(ability, null, null);
        }

        public virtual void OnSubmit(BaseEventData eventData)
        {
            Debug.Log("Bring up targetting window to select targets");
        }
    }
}
