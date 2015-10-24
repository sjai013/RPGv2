namespace Battle.Events.BattleCharacter.Stats
{
    public class HpChanged
    {
        public enum EState
        {
            None,Critical,Dead
        }
        public AbstractBattleCharacter BattleCharacter;
        public int Hp;
        public EState State;
    }

    public class MpChanged
    {
        public enum EState
        {
            None, Critical, Empty
        }
        public AbstractBattleCharacter BattleCharacter;
        public int Mp;
        public EState State;
    }

    public class MaxHpChanged
    {
        public AbstractBattleCharacter BattleCharacter;
        public int MaxHp;
    }

    public class MaxMpChanged
    {
        public AbstractBattleCharacter BattleCharacter;
        public int MaxMp;
    }

    public class StrChanged
    {
        public AbstractBattleCharacter BattleCharacter;
        public int Value;
    }

    public class AgiChanged
    {
        public AbstractBattleCharacter BattleCharacter;
        public int Value;
    }

    public class DefChanged
    {
        public AbstractBattleCharacter BattleCharacter;
        public int Value;
    }

    public class EvaChanged
    {
        public AbstractBattleCharacter BattleCharacter;
        public int Value;
    }

    public class MagChanged
    {
        public AbstractBattleCharacter BattleCharacter;
        public int Value;
    }

    public class AccChanged
    {
        public AbstractBattleCharacter BattleCharacter;
        public int Value;
    }

    public class MdefChanged
    {
        public AbstractBattleCharacter BattleCharacter;
        public int Value;
    }

    public class LukChanged
    {
        public AbstractBattleCharacter BattleCharacter;
        public int Value;
    }

}
