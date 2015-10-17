using System;
using UnityEngine;

namespace Battle
{
    [Serializable]
    public class General
    {
        [SerializeField] private string _name;
        public string Name { get { return _name; } set { if (_name == value) return; _name = value;} }

        [SerializeField] private float _attackRange;
        public float AttackRange { get { return _attackRange; } set { if (_attackRange == value) return; _attackRange = value;} }

        [SerializeField] private float _moveSpeed;
        public float MoveSpeed { get { return _moveSpeed;}  set { if (_moveSpeed == value) return; _moveSpeed = value; } }
    }
}
