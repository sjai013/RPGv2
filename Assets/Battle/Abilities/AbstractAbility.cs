using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle.Abilities
{
    [RequireComponent(typeof(Button))]
    public abstract  class AbstractAbility: MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private String _name;
        protected String Name { get { return _name; } private set { _name = value; } }
   
        [Range(1,8)]
        [SerializeField] private int _actionCost;
        public int ActionCost { get { return _actionCost; } set { if (_actionCost == value) return; _actionCost = value;} }

        public delegate void Ability(AbstractAbility ability);

        public static event Ability SelectedAbilityChanged;

        private static List<AbstractAbility> _abilities = new List<AbstractAbility>();

        private static AbstractAbility _currentAbility;

        public static AbstractAbility CurrentSelectedAbility { get { return _currentAbility; }
            protected set { if (_currentAbility == value) return; _currentAbility = value; OnChangeSelection(value); } }

        void Awake()
        {
            _abilities.Add(this);
        }

        private static void OnChangeSelection(AbstractAbility selected)
        {
            if (selected == null) return;
            Debug.Log("Selected " + selected);
            selected.OnSelectedAbilityChanged(selected);
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
            if (handler != null) handler(ability);
        }
    }
}
