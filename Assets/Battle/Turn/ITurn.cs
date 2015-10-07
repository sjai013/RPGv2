
namespace Battle.Turn
{
    public delegate void BattleChar(AbstractBattleCharacter thisBattleCharacter);
    public interface ITurn
    {
        /// <summary>
        /// Raised when it is a character's turn.  Function signature is void (AbstractBattleChar), 
        /// where the AbstractBattleChar contains information on which character is to move.
        /// </summary>
        event BattleChar TakeAction;
        

        /*
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
        */
    }
}
