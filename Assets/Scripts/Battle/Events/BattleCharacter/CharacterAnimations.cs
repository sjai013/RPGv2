using System.Collections.Generic;
using Battle.Abilities;

namespace Battle.Events.BattleCharacter
{
    public class CharacterAnimating
    {
        public AbstractBattleCharacter Caster;
        public List<AbstractBattleCharacter> Targets;
        public AbstractActionAbility Ability;
    }
}
