using System.Collections.Generic;
using Battle.Abilities;
using Battle.Turn;
using ExtensionMethods;
using UnityEngine;

namespace Battle.Menu
{
    abstract class AbstractActionMenu : MonoBehaviour {

        protected List<AbstractAbility> _abilities;
        protected CanvasGroup _canvasGroup;
        [SerializeField] protected GameObject AbilityButtonPrefab;
        [SerializeField] protected GameObject _content;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0.0f;
            _canvasGroup.interactable = false;
            
            AbstractTurnSystem.TurnSystem.TakeAction += ShowMenu;
        }

        protected abstract void ShowMenu(AbstractBattleCharacter battleCharacter);

        public void OnDestroy()
        {
            AbstractTurnSystem.TurnSystem.TakeAction -= ShowMenu;
        }

    }
}
