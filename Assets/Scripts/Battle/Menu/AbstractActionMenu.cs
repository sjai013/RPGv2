using System.Collections.Generic;
using Battle.Abilities;
using Battle.Events.TurnSystem;
using Battle.Turn;
using ExtensionMethods;
using JainEventAggregator;
using UnityEngine;

namespace Battle.Menu
{
    abstract class AbstractActionMenu : MonoBehaviour, IListener<TakeAction>
    {

        protected List<AbstractAbility> _abilities;
        protected CanvasGroup _canvasGroup;
        [SerializeField] protected GameObject AbilityButtonPrefab;
        [SerializeField] protected GameObject _content;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0.0f;
            _canvasGroup.interactable = false;
            this.RegisterAllListeners();
        }

        protected abstract void ShowMenu(AbstractBattleCharacter battleCharacter);

        public void OnDestroy()
        {
            this.UnregisterAllListeners();
        }

        public void Handle(TakeAction message)
        {
            ShowMenu(message.BattleCharacter);
        }
    }
}
