namespace Battle
{
    public class PlayableCharacter : AbstractBattleCharacter {

        void Start()
        {
            OnAddCharacter(this);
            OnUpdateTurns(this);
        }

    }
}
