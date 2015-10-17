﻿using Battle.Abilities;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    [ExecuteInEditMode]

    [RequireComponent(typeof(Button))]
    public class AbilityButton : MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler
    {
        [SerializeField] private Text _buttonText;
        [SerializeField] private GameObject _pointer;
        private AbstractAbility _ability;
        public AbstractAbility Ability { get {return _ability;} set {SetAbility(value);} }
        

        public delegate void AbilityDelegate(AbstractAbility ability);
        public delegate void Action();

        public static event AbilityDelegate SelectedAbilityChanged;
        public static event AbilityDelegate AnAbilitySubmitted;
        public event Action AbilitySubmitted;
        

        void SetAbility (AbstractAbility ability)
        {
            _ability = ability;
            _buttonText.text = ability.Name;
            ability.AbilityButton = this;
        }

        public void OnSelect(BaseEventData eventData)
        {
            _pointer.SetActive(true);
            OnSelectedAbilityChanged(Ability);

        }

        public void OnDeselect(BaseEventData eventData)
        {
            _pointer.SetActive(false);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            OnAbilitySubmitted();
            OnAnAbilitySubmitted(Ability);
        }

        private static void OnSelectedAbilityChanged(AbstractAbility ability)
        {
            var handler = SelectedAbilityChanged;
            if (handler != null) handler(ability);
        }

        private void OnAbilitySubmitted()
        {
            var handler = AbilitySubmitted;
            if (handler != null) handler();
        }

        private static void OnAnAbilitySubmitted(AbstractAbility ability)
        {
            var handler = AnAbilitySubmitted;
            if (handler != null) handler(ability);
        }
    }
}