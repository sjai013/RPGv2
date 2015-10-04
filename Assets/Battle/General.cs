using System;
using UnityEngine;

namespace Battle
{
    [Serializable]
    public class General
    {
        [SerializeField] private string _name;
        public string Name { get { return _name; } set { if (_name == value) return; _name = value; _name = Name; } }
    }
}
