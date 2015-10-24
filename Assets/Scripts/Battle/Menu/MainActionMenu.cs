using System.Collections.Generic;
using Battle.Abilities;
using Battle.Events.Abilities;
using ExtensionMethods;
using JainEventAggregator;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Menu
{
    [RequireComponent(typeof(CanvasGroup))]
    sealed class MainActionMenu : AbstractActionMenu, IListener<AbilitySubmitted>
    {
        private static MainActionMenu instance;
        

        protected override void Awake()
        {
            base.Awake();
            if (instance != null)
            {
                Debug.LogError("Multiple MainActionMenu found.  Ignoring this one.");
            }

            instance = this;
            this.RegisterAllListeners();
        }

        private void FadeMenu()
        {
            StartCoroutine(_canvasGroup.Fade(1.0f, 0.0f, 0.15f, HideMenu));
        }

        protected override void ShowMenu(AbstractBattleCharacter battleCharacter)
        {

            Debug.Log("Showing menu");
            StartCoroutine(_canvasGroup.Fade(0.0f, 1.0f, 0.25f));
            _canvasGroup.interactable = true;

            //TODO: Code for loading menu based on actions character can actually perform.
            _abilities = battleCharacter.Abilities.FilterAbilities(AbilityType.Base);

            foreach (var ability in _abilities)
            {
                var go = Instantiate(AbilityButtonPrefab);
                go.transform.SetParent(_content.transform);
                go.transform.localScale = Vector3.one;
                var abilityButton = go.GetComponent<AbilityButton>();
                abilityButton.Ability = ability;
                go.SetActive(true);
            }

            //Select the first item
            _content.GetComponentInChildren<Selectable>().Select();

            //Print list of items displayed for debugging
            Debug.Log(battleCharacter.Abilities.FilterAbilities(AbilityType.Base).PrintList());
        }

        public static void HideMenu()
        {
            instance.gameObject.SetActive(false);    
        }

        public void Handle(AbilitySubmitted message)
        {
            FadeMenu();
        }
    }
}
