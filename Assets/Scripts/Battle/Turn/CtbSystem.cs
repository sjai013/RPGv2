using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Abilities;
using Battle.Events.Abilities;
using Battle.Events.Targetting;
using Battle.Events.TurnSystem;
using ExtensionMethods;
using JainEventAggregator;
using UnityEngine;

namespace Battle.Turn
{


    /// <summary>
    /// Manages drawing of turn previews.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class CtbSystem : AbstractTurnSystem, IListener<HighlightedAbilityChanged>, IListener<TargetChanged>
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private GameObject _turnIndicator;
        [SerializeField] private List<TurnDisplay> _turnDisplays;
        Dictionary<AbstractBattleCharacter,Ticks> _tickSystems = new Dictionary<AbstractBattleCharacter, Ticks>();

        private AbstractAbility _ability;

        private CanvasGroup _canvasGroup;
        private List<KeyValuePair<AbstractBattleCharacter, int>> _prevTurns = new List<KeyValuePair<AbstractBattleCharacter, int>>(0);

        private bool _updatingTurns = false;

        protected override string Identifier { get { return "CTB System"; } }

        protected override void Initialise()
        {
            this.RegisterAllListeners();
            _canvasGroup = GetComponent<CanvasGroup>();

        }

        void Start()
        {


            foreach (var character in AbstractBattleCharacter.Characters)
            {
                _tickSystems[character] = new Ticks(character);

            }

            StartCoroutine(UpdateTurns(AbstractBattleCharacter.Characters));
        }

        void Update()
        {
            
        }

        IEnumerator UpdateTurns(List<AbstractBattleCharacter> characters)
        {
            _updatingTurns = true;
            Debug.Log("Update Turns");
            _canvasGroup.alpha = 0.0f;
            while (_updatingTurns)
            {

                foreach (var character in characters)
                {
                    if (!_tickSystems[character].IncrementTime()) continue;
                    EventAggregator.RaiseEvent(new TakeAction() {BattleCharacter = character});
                    StartCoroutine(_canvasGroup.Fade(0.0f, 1.0f, 0.15f));
                    _updatingTurns = false;
                    break;
                }

                yield return null;
            }

            Debug.Log("Stop Update Turns");
            _updatingTurns = false;
        } 

        /// <summary>
        /// Update CTB preview.
        /// </summary>
        /// <param name="ability">Use ability to determine cost and present appropriate preview</param>
        /// <param name="alliesAffected">List of allies that are affected by this ability</param>
        /// <param name="enemiesAffected">List of enemies that are affected by this ability</param>
        private void UpdatePreview(AbstractAbility ability, List<AbstractBattleCharacter> alliesAffected, List<AbstractBattleCharacter> enemiesAffected )
        {
            //ClearTurnDisplays();
            int cost = Ticks.DefaultActionCost;

            if (ability != null)
                cost = ability.ActionCost;

            var turns = PreviewTurnsList(cost);
            if (alliesAffected == null)
                alliesAffected = new List<AbstractBattleCharacter>();

            if (enemiesAffected == null)
                enemiesAffected = new List<AbstractBattleCharacter>();

            bool checkPrevTurns = _prevTurns.Count == turns.Count;
            for (int i = 0; i < turns.Count; i++)
            {
                var turn = turns[i];
                if (checkPrevTurns && turn.Equals(_prevTurns[i]))
                    continue;

                bool thisTurn = turn.Equals(turns.First());

                _turnDisplays[i].DrawTurn(turn.Value, turn.Key.CharSprite,
                    thisTurn);
                _turnDisplays[i].BattleCharacter = turn.Key;
            }

            _prevTurns = turns;

        }

        public void DrawPointers(List<AbstractBattleCharacter> alliesAffected, List<AbstractBattleCharacter> enemiesAffected)
        {
            foreach (var display in _turnDisplays)
            {
                if (alliesAffected.Contains(display.BattleCharacter))
                {
                    display.SetFriendlyPointer(true);
                    display.SetEnemyPointer(false);
                } else if (enemiesAffected.Contains(display.BattleCharacter))
                {
                    display.SetEnemyPointer(true);
                    display.SetFriendlyPointer(false);
                }
                else
                {
                    display.SetEnemyPointer(false);
                    display.SetFriendlyPointer(false);
                }
                    
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
            Ticks ticks = (Ticks) _tickSystems[character];
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

        private void ClearTurnDisplay(int i)
        {
            Destroy(_content.transform.GetChild(i).gameObject);
        }

        public void Handle(HighlightedAbilityChanged message)
        {
            _ability = message.Ability;
            UpdatePreview(message.Ability, null, null);
        }

        public void Handle(TargetChanged message)
        {
            var enemies = new List<AbstractBattleCharacter>();
            var friendlies = new List<AbstractBattleCharacter>();

            if (AbstractBattleCharacter.Enemies.Contains(message.Target))
                enemies.Add(message.Target);

            if (AbstractBattleCharacter.Friendlies.Contains(message.Target))
                friendlies.Add(message.Target);

            DrawPointers(friendlies,enemies);
        }
    }
}
