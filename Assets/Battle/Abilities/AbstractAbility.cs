using UnityEngine;
using System;
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
        public enum TargetTypes
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

        public enum DefaultTargetType
        {
            Self,
            Allies,
            Enemy
        }


        public abstract TargetTypes TargetType { get; }
        public abstract String Name { get; }
        public abstract DefaultTargetType DefaultTarget { get; }
        public abstract AbstractDamageBehaviour DamageBehaviour { get;  }

        public virtual int ActionCost { get; protected set; }

        public delegate void AbilityTargets(AbstractAbility ability, List<AbstractBattleCharacter> alliesAffected, List<AbstractBattleCharacter> enemiesAffected);
        public delegate void Ability(AbstractAbility ability);

        public static event AbilityTargets SelectedAbilityChanged;
        public static event Ability AbilitySubmit;

        private static Dictionary<String,AbstractAbility> _abilities = new Dictionary<String, AbstractAbility>();

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

        public virtual void OnSubmit(BaseEventData eventData)
        {
            
            Debug.Log("Bring up targetting window to select targets");
            Debug.Log(this.name);
            OnAbilitySubmit(this);

             _mainActionCanvasGroup.interactable = false;
             _mainActionCanvasGroup.gameObject.SetActive(false);
        }

        protected static void OnAbilitySubmit(AbstractAbility ability)
        {
            var handler = AbilitySubmit;
            if (handler != null) handler(ability);
        }

    }
}
