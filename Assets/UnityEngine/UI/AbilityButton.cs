using Battle;
using Battle.Abilities;
using Battle.Events.Abilities;
using JainEventAggregator;
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
        public AbstractBattleCharacter Caster { get; set; }
        
        void SetAbility (AbstractAbility ability)
        {
            _ability = ability;
            _buttonText.text = ability.Name;
            ability.AbilityButton = this;
        }

        public void OnSelect(BaseEventData eventData)
        {
            _pointer.SetActive(true);
            EventAggregator.RaiseEvent(new HighlightedAbilityChanged {Ability = this.Ability});
        }

        public void OnDeselect(BaseEventData eventData)
        {
            _pointer.SetActive(false);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            EventAggregator.RaiseEvent(new AbilitySubmitted() {Ability = Ability, Caster = Caster});
        }



    }
}
