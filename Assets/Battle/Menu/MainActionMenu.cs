using System.Collections.Generic;
using Battle.Abilities;
using ExtensionMethods;
using UnityEngine;

namespace Battle.Menu
{
    [RequireComponent(typeof(CanvasGroup))]
    class MainActionMenu : AbstractActionMenu
    {
        public static List<AbstractAbility> Abilities = new List<AbstractAbility>();
        private static MainActionMenu instance;

        protected override void Awake()
        {
            base.Awake();
            instance = this;
        }

        protected override void ShowMenu(AbstractBattleCharacter battleCharacter)
        {
            Debug.Log("Showing menu");
            StartCoroutine(_canvasGroup.Fade(0.0f, 1.0f, 0.25f));
            _canvasGroup.interactable = true;

            //TODO: Code for loading menu based on actions character can actually perform.
            foreach (var ability in _abilities)
            {
                Abilities.Add(ability);
                /*
                ability.gameObject.setActive(battleChar.knowsAbility(ability))
                */
            }

            _abilities[0].Select();
        }

        public static void HideMenu()
        {
            instance.gameObject.SetActive(false);    
        }
        
    }
}
