using System.Collections.Generic;
using Battle.Abilities;
using ExtensionMethods;
using UnityEngine;

namespace Battle.Menu
{
    abstract class AbstractActionMenu : MonoBehaviour {

        protected List<AbstractAbility> _abilities;
        protected CanvasGroup _canvasGroup;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0.0f;
            _canvasGroup.interactable = false;
            PlayableCharacter.BeginTurn += ShowMenu;

            _abilities = new List<AbstractAbility>();
            foreach (var ability in GetComponentsInChildren<AbstractAbility>())
            {
                _abilities.Add(ability);
            }
        }

        protected abstract void ShowMenu(AbstractBattleCharacter battleCharacter);

        public void OnDestroy()
        {
            PlayableCharacter.BeginTurn -= ShowMenu;
        }

    }
}
