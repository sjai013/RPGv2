using System.Collections.Generic;
using Battle.Abilities;

namespace Battle.Events.Targetting
{
    public class TargetChanged
    {
        public AbstractBattleCharacter Caster;
        public AbstractBattleCharacter Target;
    }

    public class SubmitAbilityAtTarget
    {
        public AbstractBattleCharacter Caster;
        public List<AbstractBattleCharacter> Targets;
        public AbstractActionAbility Ability;
    }
}
