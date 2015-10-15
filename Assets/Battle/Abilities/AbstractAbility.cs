using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Battle.Abilities.Damage;
using Battle.Menu;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle.Abilities
{


    [RequireComponent(typeof(Button))]
    public abstract  class AbstractAbility: MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler
    {
        public abstract String Name { get; }

        public virtual int ActionCost { get; set; }

        public delegate void AbilityTargets(AbstractAbility ability, List<AbstractBattleCharacter> alliesAffected, List<AbstractBattleCharacter> enemiesAffected);
        public delegate void ActionAbility(AbstractActionAbility ability);

        public static event AbilityTargets SelectedAbilityChanged;
        public static event ActionAbility AbilitySubmit;

        private static Dictionary<String,AbstractAbility> _abilities = new Dictionary<String, AbstractAbility>();

        private static AbstractAbility _currentAbility;

        public static AbstractAbility CurrentSelectedAbility { 
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

        [SerializeField] private GameObject _pointer;
        [SerializeField] protected CanvasGroup _mainActionCanvasGroup;

        private Button _button;

        void Awake()
        {
            _abilities.Add(this.Name,this);
            _button = GetComponent<Button>();

        }

        public void Select()
        {
            OnSelect(null);
        }

        public void OnSelect(BaseEventData eventData)
        {
            _button.OnSelect(eventData);
            _pointer.SetActive(true);            
            CurrentSelectedAbility = this;
        }

        public void OnDeselect(BaseEventData eventData)
        {

            _pointer.SetActive(false);
            CurrentSelectedAbility = null;
        }

        protected virtual void OnSelectedAbilityChanged(AbstractAbility ability)
        {
            var handler = SelectedAbilityChanged;
            if (handler != null) handler(ability, null, null);
        }

        public abstract void OnSubmit(BaseEventData eventData);

        protected static void OnAbilitySubmit(AbstractActionAbility ability)
        {
            var handler = AbilitySubmit;
            if (handler != null) handler(ability);
        }

    }
}
