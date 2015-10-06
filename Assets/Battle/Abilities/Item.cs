using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle.Abilities
{

    public class Item : AbstractAbilityContainer
    {
        public override int ActionCost { get { return 2; } }
        public override string Name { get { return "Item"; } }
    }
}
