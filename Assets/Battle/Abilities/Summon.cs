using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle.Abilities
{

    public class Summon : AbstractAbilityContainer
    {
        public override int ActionCost { get { return 3; } }
        public override string Name { get { return "Summon"; } }
    }
}
