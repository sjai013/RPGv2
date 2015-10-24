using System.Collections;
using System.Collections.Generic;

namespace Battle.Abilities.Damage
{
    public abstract class AbstractDamageBehaviour
    {
        public struct Damage
        {
            public int damage;
            public bool hit;

            public override string ToString()
            {
                return "Damage: " + damage + ", Hit: " + hit;
            }
        }

        protected int _damageConstant;

        protected AbstractDamageBehaviour(int damageConstant)
        {
            this._damageConstant = damageConstant;
        }

        /// <summary>
        /// Perform damage calculations.  Defines all the necessary code to determine exactly how much damage is done.
        /// </summary>
        /// <param name="caster"> Caster [[AbstractBattleCharacter]] </param>
        /// <param name="target"> Target [[AbstractBattleCharacter]] </param>
        public abstract Damage DoDamage(AbstractBattleCharacter caster, AbstractBattleCharacter target);

        /// <summary>
        /// Perform damage calculations.  Defines all the necessary code to determine exactly how much damage is done,
        /// and also all the necessary steps.
        /// <para /> This includes performing the caster animation, spawning any particles or projectiles, and animating the target.
        /// <para /> Multi-hit abilities can also be implemented by defining the appropriate sequence in this method.
        /// </summary>
        /// <param name="caster"> Caster [[AbstractBattleCharacter]] </param>
        /// <param name="target"> Target [[AbstractBattleCharacters (List)]] </param>
        public abstract Damage DoDamage(AbstractBattleCharacter caster, List<AbstractBattleCharacter> target);
    }
}
