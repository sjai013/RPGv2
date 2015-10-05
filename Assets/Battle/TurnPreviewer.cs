using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Battle.Abilities;


namespace Battle
{
    /// <summary>
    /// Manages drawing of turn previews.
    /// </summary>
    public class TurnPreviewer : MonoBehaviour
    {
        [SerializeField] private GameObject _content;

        [SerializeField] private GameObject _turnIndicator;

        void Awake()
        {
            AbstractAbility.SelectedAbilityChanged += UpdatePreview;
        }

        /// <summary>
        /// Update CTB preview.
        /// </summary>
        /// <param name="ability">Use ability to determine cost and present appropriate preview</param>
        private void UpdatePreview(AbstractAbility ability)
        {
            ClearTurnDisplays();
            var turns = AbstractBattleCharacter.PreviewTurnsList(ability.ActionCost);

            foreach (var turn in turns)
            {
                GameObject gO = Instantiate(_turnIndicator);
                gO.transform.SetParent(_content.transform);
                gO.transform.localScale = Vector3.one;
                gO.GetComponent<TurnDisplay>().DrawTurn(turn.Value - turns[0].Value, turn.Key.General.Name);

            }
        }

        /// <summary>
        /// Clears existing turn display.
        /// </summary>
        private void ClearTurnDisplays()
        {
            for (int i = 0; i < _content.transform.childCount; i++)
            {
                Destroy(_content.transform.GetChild(i).gameObject);
            }
            
        }

    }
}
