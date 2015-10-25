
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Battle.Turn
{
    public interface ITurn
    {
        AbstractBattleCharacter Character { get; set; }

        /// <summary>
        /// Progress time for battle characters.
        /// </summary>
        /// <returns>True if it is the character's turn as a result of time increment, false otherwise</returns>
        bool IncrementTime();
    }
}
