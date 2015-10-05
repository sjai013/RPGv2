using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{

    /// <summary>
    /// Class for managing all characters which can take action (i.e. player, monsters)
    /// </summary>
    public abstract class AbstractBattleCharacter: MonoBehaviour
    {
        /// <summary>
        /// General stats, such as character name.
        /// </summary>
        /// 
        public General General { get { return _general; } }
        [SerializeField] protected General _general;

        /// <summary>
        /// Character stats, which determine combat proficiency.
        /// </summary>
        public Stats Stats { get { return _stats; } }
        [SerializeField] protected Stats _stats;

        /// <summary>
        /// Helper class to calculate time between actions.
        /// </summary>
        [SerializeField] protected Ticks _ticks;

        /// <summary>
        /// Boolean to indicate whether character is in combat (i.e. it could be on the "side-benches").
        /// </summary>
        [SerializeField] protected Boolean _inCombat;

        /// <summary>
        /// Storage for all AbstractBattleCharacters instantiated.
        /// </summary>
        protected static List<AbstractBattleCharacter> _instances = new List<AbstractBattleCharacter>();

        /// <summary>
        /// Current active character (i.e. the one that is waiting to perform an action).
        /// </summary>
        [SerializeField] public static AbstractBattleCharacter ActiveBattleCharacter { get; private set; }

        public delegate void BattleCharBattleChar(AbstractBattleCharacter thisBattleCharacter, AbstractBattleCharacter otherBattleCharacter);
        public delegate void BattleChar(AbstractBattleCharacter thisBattleCharacter);
        public delegate void BattleCharInt(AbstractBattleCharacter thisBattleCharacter, int value);

        /// <summary>
        /// Methods to run when a new character is added to the list.  Called in the Awake method of sub classes.
        /// </summary>
        public event BattleChar AddCharacter;

        /// <summary>
        /// Methods to run when a character is removed from the list (in the case of monsters - when they die).  NEVER CALLED.
        /// </summary>
        public event BattleChar RemoveCharacter;

        /// <summary>
        /// Methods to run when a character replaces another character (i.e. swap side-bench and main).  NEVER CALLED.
        /// </summary>
        public event BattleCharBattleChar ReplaceCharacter;

        /// <summary>
        /// Methods to run when character is highlighted (i.e. targetted).  NEVER CALLED.
        /// </summary>
        public event BattleChar Highlight;

        /// <summary>
        /// Methods to ru nwhen character is unhighlighted (i.e. no longer targetted).  NEVER CALLED.
        /// </summary>
        public event BattleChar Unhighlight;

        /// <summary>
        /// Methods to run when turns are updated.  NEVER CALLED.
        /// </summary>
        public event BattleChar UpdateTurns;

        public static event BattleCharInt TakeAction;

        protected virtual void Awake()
        {
            _instances.Add(this);
            _ticks = new Ticks(Stats.Agi);
            TakeAction += UpdateTicks;
        }



        public void RefreshHandlers()
        {
            Stats.Refresh();
        }

        public void Remove()
        {
            OnRemoveCharacter(this);
        }

        protected void OnAddCharacter(AbstractBattleCharacter thisBattleCharacter)
        {
            var handler = AddCharacter;
            if (handler != null) handler(thisBattleCharacter);
        }

        protected void OnRemoveCharacter(AbstractBattleCharacter thisBattleCharacter)
        {

            var handler = RemoveCharacter;
            if (handler != null) handler(thisBattleCharacter);
        }


        protected void OnReplaceCharacter(AbstractBattleCharacter thisBattleCharacter, AbstractBattleCharacter otherBattleCharacter)
        {
            var handler = ReplaceCharacter;
            if (handler != null) handler(thisBattleCharacter, otherBattleCharacter);
        }

        public void Replace(AbstractBattleCharacter otherBattleCharacter)
        {
            OnReplaceCharacter(this, otherBattleCharacter);
        }

        protected virtual void OnHighlight(AbstractBattleCharacter thisbattlecharacter)
        {
            var handler = Highlight;
            if (handler != null) handler(thisbattlecharacter);
        }

        protected virtual void OnUnhighlight(AbstractBattleCharacter thisbattlecharacter)
        {
            var handler = Unhighlight;
            if (handler != null) handler(thisbattlecharacter);
        }

        protected virtual void OnUpdateTurns(AbstractBattleCharacter thisbattlecharacter)
        {
            var handler = UpdateTurns;
            if (handler != null) handler(thisbattlecharacter);
        }

        protected virtual void OnTakeAction(AbstractBattleCharacter thisbattlecharacter, int tickCost)
        {
            var handler = TakeAction;
            if (handler != null) handler(thisbattlecharacter, tickCost);

        }

        /// <summary>
        /// Calculate the ticks required for subsequent actions by the character.
        /// </summary>
        /// <param name="turns">Number of turns to look ahead.</param>
        /// <param name="nextActionCost">Cost of next action (added from Turn 2 onwards, assuming Turn 1 is the current action anticipated)</param>
        /// <returns>List of ticks required for the number of turns specified</returns>
        public List<int> CalculateTicks(int turns = 16, int nextActionCost = Ticks.DefaultActionCost)
        {
            return _ticks.PreviewTicksList(agi: _stats.Agi, nTurns: turns, nextActionCost: nextActionCost);
        }

        /// <summary>
        /// Update ticks for character (such as after action is taken).
        /// </summary>
        /// <param name="battleCharacter">Character taking action</param>
        /// <param name="tickCost">Tick cost of ability used by character</param>
        protected virtual void UpdateTicks(AbstractBattleCharacter battleCharacter, int tickCost = Ticks.DefaultActionCost)
        {
            if (battleCharacter == this)
            {
                _ticks.ResetTicks(tickCost);
                return;
            }

            _ticks.SubtractTicks(tickCost);
        }

        /// <summary>
        /// Return next 16 expected actions, in order.
        /// </summary>
        /// <param name="nextActionCost">Cost of anticipated action.</param>
        /// <returns>List of character/ticks</returns>
        public static List<KeyValuePair<AbstractBattleCharacter,int>> PreviewTurnsList(int nextActionCost)
        {
            List<KeyValuePair<AbstractBattleCharacter, int>> turns = new List<KeyValuePair<AbstractBattleCharacter, int>>();
            foreach (var character in _instances)
            {

                List<int> ticks = character == AbstractBattleCharacter.ActiveBattleCharacter ? character.CalculateTicks(nextActionCost: nextActionCost) : character.CalculateTicks();
                turns.AddRange(ticks.Select(tick => new KeyValuePair<AbstractBattleCharacter, int>(character, tick)));
            }
            turns = turns.OrderBy(turn => turn.Value).Take(16).ToList();
            return turns;
        }

        /// <summary>
        /// Calculates which character should move next, and assigns it to current active character.
        /// </summary>
        /// <returns>Returns next character who will move</returns>
        public static AbstractBattleCharacter SetNextTurn()
        {
            List<KeyValuePair<AbstractBattleCharacter, int>> turns = new List<KeyValuePair<AbstractBattleCharacter, int>>();
            foreach (var character in _instances)
            {
                if (!character._inCombat) continue;
                var ticks = character.CalculateTicks();
                turns.AddRange(ticks.Select(tick => new KeyValuePair<AbstractBattleCharacter, int>(character, tick)));
            }

            var nextTurnChar =  turns.OrderBy(turn => turn.Value).First().Key;
            ActiveBattleCharacter = nextTurnChar;
            return nextTurnChar;
        } 


    }

}
