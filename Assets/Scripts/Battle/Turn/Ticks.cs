using System.Collections.Generic;
using Battle.Events.Abilities;
using JainEventAggregator;
using UnityEngine;

namespace Battle.Turn
{
    /// <summary>
    /// Manages ticks required for actions and characters.
    /// </summary>
    public class Ticks : ITurn, IListener<AbilitySubmitted>
    {
        public AbstractBattleCharacter Character { get; set; }

        public bool IncrementTime()
        {
            return --RecoveryTime == 0;
        }

        /// <summary>
        /// Cost of a default action (i.e. Attack).
        /// </summary>
        [SerializeField] public const int DefaultActionCost = 3;

        /// <summary>
        /// Determines recovery time due to previous action.
        /// </summary>
        public int RecoveryTime { get; private set; }
   
        /// <summary>
        /// Starting ticks, under normal conditions (i.e. previous action was Attack).
        /// </summary>
        private readonly int _baseInitialTicks;

        public Ticks(AbstractBattleCharacter character)
        {
            _baseInitialTicks = TicksRequired(character.Stats.Agi,1);
            RecoveryTime = _baseInitialTicks*DefaultActionCost;
            Character = character;
            this.RegisterAllListeners();
        }

        /// <summary>
        /// Reset ticks after taking action.  Adds recovery time based on action cost.
        /// </summary>
        /// <param name="actionCost">Cost of previous action taken.</param>
        public void ResetTicks(int actionCost = DefaultActionCost)
        {
            RecoveryTime = _baseInitialTicks * actionCost;
        }

        /// <summary>
        /// Creates list of ticks requried for subsequent actions
        /// </summary>
        /// <param name="agi">Agility of character</param>
        /// <param name="nTurns">Number of turns to forecast</param>
        /// <param name="nextActionCost">Cost of next action</param>
        /// <returns>List of ticks required</returns>
        public List<int> PreviewTicksList(int agi, int nTurns = 16, int nextActionCost = DefaultActionCost)
        {
            int multiplier = 1;
            if (nTurns < 1)
            {
                Debug.LogError("nTurns should be > 1.  Returning assuming nTurns = 1");
                nTurns = 1;

            }
            List<int> tickList = new List<int> {RecoveryTime};
            for (int i = 1; i < nTurns; i++)
            {
                if (i == 1)
                {
                    tickList.Add((int) (TicksRequired(agi, nextActionCost)*multiplier) + tickList[i - 1]);
                    
                }
                else
                {
                    tickList.Add((int)(TicksRequired(agi) * multiplier) + tickList[i - 1]);
                }
            }

            return tickList;
        }

        /// <summary>
        /// Convert agility to ticks
        /// </summary>
        /// <param name="agi">Agility of character</param>
        /// /// <param name="actionCost">Action cost</param>
        /// <returns>Base ticks required for action.</returns>
        private static int TicksRequired(int agi, int actionCost = DefaultActionCost)
        {
            float ticks =  34.08f * Mathf.Pow(agi + 1,-0.425f) * actionCost;
            return Mathf.RoundToInt(ticks);
        }

        /// <summary>
        /// Subtract tick time from next action
        /// </summary>
        /// <param name="ticks">Amount of ticks to subtract</param>
        public void SubtractTicks(int ticks)
        {
            RecoveryTime -= ticks;
        }

        public void Handle(AbilitySubmitted message)
        {
            if (message.Caster != this.Character) return;
            ResetTicks(message.Ability.ActionCost);
        }
    }

}
