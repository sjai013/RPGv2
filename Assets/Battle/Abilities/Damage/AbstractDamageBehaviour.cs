using System.Collections.Generic;

namespace Battle.Abilities.Damage
{
    public abstract class AbstractDamageBehaviour
    {
        protected int damageConstant;

        protected AbstractDamageBehaviour(int damageConstant)
        {
            this.damageConstant = damageConstant;
        }

        public abstract void DoDamage(AbstractBattleCharacter caster, AbstractBattleCharacter target);
        public abstract void DoDamage(AbstractBattleCharacter caster, List<AbstractBattleCharacter> target);
    }
}
