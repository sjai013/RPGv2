using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Battle.Abilities;
using Battle.Turn;
using UnityEngine.Events;


namespace Battle
{
    /// <summary>
    /// Manages drawing of turn previews.
    /// </summary>
    public class TurnPreviewer : AbstractTurnDisplay
    {
        [SerializeField] private GameObject _content;
        
        [SerializeField] private GameObject _turnIndicator;

        void Awake()
        {
            AbstractAbility.SelectedAbilityChanged += UpdatePreview;
        }

        void Start()
        {
            foreach (var character in AbstractBattleCharacter.Characters)
            {
                character.TurnSystem = new Ticks(character.Stats.Agi);
            }
        }

        /// <summary>
        /// Update CTB preview.
        /// </summary>
        /// <param name="ability">Use ability to determine cost and present appropriate preview</param>
        private void UpdatePreview(AbstractAbility ability)
        {
            ClearTurnDisplays();
            var turns = PreviewTurnsList(ability.ActionCost);

            foreach (var turn in turns)
            {
                bool thisTurn = turn.Equals(turns.First());
                GameObject gO = Instantiate(_turnIndicator);
                gO.transform.SetParent(_content.transform);
                gO.transform.localScale = Vector3.one;
                gO.GetComponent<TurnDisplay>().DrawTurn(turn.Value - turns[0].Value, turn.Key.CharSprite, thisTurn, false, false);

            }
        }

        /// <summary>
        /// Return next 16 expected actions, in order.
        /// </summary>
        /// <param name="nextActionCost">Cost of anticipated action.</param>
        /// <returns>List of character/ticks</returns>
        private List<KeyValuePair<AbstractBattleCharacter, int>> PreviewTurnsList(int nextActionCost)
        {
            List<KeyValuePair<AbstractBattleCharacter, int>> turns = new List<KeyValuePair<AbstractBattleCharacter, int>>();
            foreach (var character in AbstractBattleCharacter.Characters)
            {
                List<int> ticks = character == AbstractBattleCharacter.ActiveBattleCharacter ? CalculateTicks(character, nextActionCost: nextActionCost) : CalculateTicks(character);
                turns.AddRange(ticks.Select(tick => new KeyValuePair<AbstractBattleCharacter, int>(character, tick)));
            }
            turns = turns.OrderBy(turn => turn.Value).Take(16).ToList();
            return turns;
        }



        /// <summary>
        /// Calculate the ticks required for subsequent actions by the character.
        /// </summary>
        /// <param name="turns">Number of turns to look ahead.</param>
        /// <param name="nextActionCost">Cost of next action (added from Turn 2 onwards, assuming Turn 1 is the current action anticipated)</param>
        /// <returns>List of ticks required for the number of turns specified</returns>
        private List<int> CalculateTicks(AbstractBattleCharacter character, int turns = 16, int nextActionCost = Ticks.DefaultActionCost)
        {
            Ticks ticks = (Ticks) character.TurnSystem;
            return ticks.PreviewTicksList(agi: character.Stats.Agi, nTurns: turns, nextActionCost: nextActionCost);
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
