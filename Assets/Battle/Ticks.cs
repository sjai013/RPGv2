using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Battle
{
    public class Ticks
    {
        private const int _defaultActionCost = 3;

        [SerializeField] private int _ticksUntilNextTurn;
        public int TicksUntilNextTurn { get { return _ticksUntilNextTurn; } set { if (_ticksUntilNextTurn == value) return; _ticksUntilNextTurn = value; _ticksUntilNextTurn = TicksUntilNextTurn; } }

        [SerializeField] private int _recoveryTimeThisTurn;
        public int RecoveryTimeThisTurn { get { return _recoveryTimeThisTurn; } set { if (_recoveryTimeThisTurn == value) return; _recoveryTimeThisTurn = value; _recoveryTimeThisTurn = RecoveryTimeThisTurn; } }

        private int _baseInitialTicks;

        public Ticks(int agi)
        {
            _baseInitialTicks = _ticksUntilNextTurn = BaseTickValue(agi);
        }

        public void ResetTicks(int actionCost = _defaultActionCost)
        {
            _ticksUntilNextTurn = _baseInitialTicks * _defaultActionCost;
        }

        public List<int> PreviewTicksList(int agi, int nTurns = 16)
        {
            int multiplier = 1;
            if (nTurns < 1)
            {
                Debug.LogError("nTurns should be > 1.  Returning assuming nTurns = 1");
                nTurns = 1;

            }

            List<int> tickList = new List<int> {_ticksUntilNextTurn * multiplier};

            for (int i = 1; i < nTurns; i++)
            {
                tickList.Add((int) (BaseTickValue(agi) * multiplier) + tickList[i-1]);
            }

            return tickList;
        }

        private static float AgiToBaseTicks(int agi)
        {

            float ticks =  34.08f * Mathf.Pow(agi + 1,-0.425f);


            return Mathf.RoundToInt(ticks);
        }

        private static int BaseTickValue(int agi)
        {
            return (int) (AgiToBaseTicks(agi)*_defaultActionCost);
        }

        public void SubtractTicks(int ticks)
        {
            _ticksUntilNextTurn -= ticks;
        }
    }

}
