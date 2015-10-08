using ExtensionMethods;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class PlayableCharacter : AbstractBattleCharacter
    {

        [SerializeField] private CanvasGroup _mainMenuCanvasGroup;

        void Start()
        {
            OnAddCharacter(this);
            OnUpdateTurns(this);
            _mainMenuCanvasGroup.alpha = 0.0f;
        }

        protected override void TakeAction(AbstractBattleCharacter character)
        {
            _mainMenuCanvasGroup.gameObject.SetActive(true);
            StartCoroutine(_mainMenuCanvasGroup.Fade(0.0f, 1.0f, 0.25f));
            _mainMenuCanvasGroup.interactable = true;   
            _mainMenuCanvasGroup.gameObject.GetComponentInChildren<Selectable>().Select();
            Debug.Log(General.Name + " performing action.");
            
        }
    }
}
