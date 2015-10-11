using ExtensionMethods;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.Abilities
{
    /// <summary>
    /// Represents abilities which open up a window with abilities to select (such as Item or Magic).
    /// The "Action" should be overloaded to determine how to draw the target menu.
    /// </summary>
    public abstract class AbstractAbilityContainer : AbstractAbility
    {
        public override void OnSubmit(BaseEventData eventData)
        {

            Debug.Log("Bring up another window to select sub-ability");
            Debug.Log(this.name);
            //OnAbilitySubmit(this);
            _mainActionCanvasGroup.interactable = false;

            StartCoroutine(_mainActionCanvasGroup.Fade(1.0f, 0.0f, 0.25f,
                delegate() { _mainActionCanvasGroup.gameObject.SetActive(false); }));
           // StartCoroutine(_mainActionCanvasGroup.Fade(1.0f, 0.0f, 5.15f,
             //   delegate() { _mainActionCanvasGroup.gameObject.SetActive(false); Debug.Log("ASDA");}));


        }

    }
}
