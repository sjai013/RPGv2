
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Battle.Turn
{
    public delegate void BattleCharDelegate(AbstractBattleCharacter thisBattleCharacter);
    public delegate void NoReturnDelegate();
    public interface ITurn
    {
        /// <summary>
        /// Raised when it is a character's turn.  Function signature is void (AbstractBattleChar), 
        /// where the AbstractBattleChar contains information on which character is to move.
        /// </summary>
        event BattleCharDelegate TakeAction;
        AbstractBattleCharacter Character { get; set; }

        /// <summary>
        /// Progress time for battle characters.
        /// </summary>
        /// <returns>True if it is the character's turn as a result of time increment, false otherwise</returns>
        bool IncrementTime();
    }
}
