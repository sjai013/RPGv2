using System.Collections.Generic;
using Battle.Abilities;
using Battle.Abilities.Damage;
using UnityEngine;

namespace Battle.Events.BattleCharacter
{
    public class AddBattleCharacter
    {
        public AbstractBattleCharacter BattleCharacter;
    }

    public class RemoveBattleCharacter
    {
        public AbstractBattleCharacter BattleCharacter;
    }

    public class HighlightBattleCharacter
    {
        public AbstractBattleCharacter BattleCharacter;
        public bool Highlighted;
    }

    public class DoingDamage
    {
        public List<AbstractBattleCharacter> Targets;
        public AbstractDamageBehaviour.Damage Damage;
    }

    public class DoDamage
    {
        public List<AbstractBattleCharacter> Targets;
        public AbstractActionAbility Ability;
    }

    public class TakingDamage
    {
        public AbstractBattleCharacter Target;
        public AbstractDamageBehaviour.Damage Damage;
    }
}
