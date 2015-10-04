using UnityEngine;
using System.Collections.Generic;
using System.Linq;


namespace Battle
{
    public class TurnPreviewer : MonoBehaviour
    {
        [SerializeField] private GameObject _content;

        [SerializeField] private GameObject _turnIndicator;

        private List<KeyValuePair<AbstractBattleCharacter, int>> turns;


        void Awake()
        {

        }

        void UpdateTurns(AbstractBattleCharacter character)
        {
            
        }

        public void GetAllTurns()
        {
            var thisTurn = AbstractBattleCharacter.NextTurn();
            Debug.Log("Next: " + thisTurn.General.Name);
            var turns = AbstractBattleCharacter.PreviewTurnsList();
           

            foreach (var turn in turns)
            {
                GameObject gO = Instantiate(_turnIndicator);
                gO.transform.SetParent(_content.transform);
                gO.transform.localScale = Vector3.one;
                gO.GetComponent<TurnDisplay>().DrawTurn(turn.Value - thisTurn.CalculateTicks(1)[0],turn.Key.General.Name);
                
            }


        }

    }
}
