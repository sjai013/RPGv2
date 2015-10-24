using System;
using Battle.Events.BattleCharacter.Stats;
using JainEventAggregator;
using UnityEngine;


namespace Battle
{
    [Serializable]
    public class Stats
    {
        [SerializeField] private AbstractBattleCharacter _character;
        
        [SerializeField] private int _hp;
        public int Hp {get { return _hp; } set {if (_hp == value) return; _hp = Mathf.Clamp(value,0,MaxHp); OnHpChanged(); }}

        [SerializeField] private int _mp;
        public int Mp { get { return _mp; } set { if (_mp == value) return; _mp = Mathf.Clamp(value, 0, MaxMp); OnMpChanged(); } }

        [SerializeField] private int _maxHp;
        public int MaxHp { get { return _maxHp; } set { if (_maxHp == value) return; _maxHp = Mathf.Clamp(value,0,99999); OnMaxHpChanged(); } }

        [SerializeField] private int _maxMp;
        public int MaxMp { get { return _maxMp; } set { if (_maxMp == value) return; _maxMp = Mathf.Clamp(value, 0, 9999); OnMaxMpChanged(); } }

        [SerializeField] private int _str;
        public int Str { get { return _str; } set { if (_str == value) return; _str = value; OnStrChanged(); } }

        [SerializeField] private int _mag;
        public int Mag { get { return _mag; } set { if (_mag == value) return; _mag = value; OnMagChanged(); } }

        [SerializeField] private int _agi;
        public int Agi { get { return _agi; } set { if (_agi == value) return; _agi = value; OnAgiChanged(); } }

        [SerializeField] private int _def;
        public int Def { get { return _def; } set { if (_def == value) return; _def = value; OnDefChanged(); } }

        [SerializeField] private int _eva;
        public int Eva { get { return _eva; } set { if (_eva == value) return; _eva = value; OnEvaChanged(); } }

        [SerializeField] private int _acc;
        public int Acc { get { return _acc; } set { if (_acc == value) return; _acc = value; OnAccChanged(); } }

        [SerializeField] private int _mdef;
        public int Mdef { get { return _mdef; } set { if (_mdef == value) return; _mdef = value; OnMdefChanged(); } }

        [SerializeField] private int _luk;
        public int Luk { get { return _luk; } set { if (_luk == value) return; _luk = value; OnLukChanged(); } }

        private void OnHpChanged()
        {
            HpChanged.EState state;
            if (Hp <= 0)
                state = HpChanged.EState.Dead;
            else if (Hp <= MaxHp*0.25)
                state = HpChanged.EState.Critical;
            else
                state = HpChanged.EState.None;

            EventAggregator.RaiseEvent(new HpChanged() { BattleCharacter = _character, Hp = Hp, State = state});
        }

        private void OnMpChanged()
        {
            MpChanged.EState state;
            if (Mp <= 0)
                state = MpChanged.EState.Empty;
            else if (Mp <= MaxMp * 0.25)
                state = MpChanged.EState.Critical;
            else
                state = MpChanged.EState.None;

            EventAggregator.RaiseEvent(new MpChanged() { BattleCharacter = _character, Mp = Mp, State = state});
        }

        private void OnMaxHpChanged()
        {
            EventAggregator.RaiseEvent(new MaxHpChanged() { BattleCharacter = _character, MaxHp = MaxHp});
        }

        private void OnMaxMpChanged()
        {
            EventAggregator.RaiseEvent(new MaxMpChanged() { BattleCharacter = _character, MaxMp = MaxMp });
        }

        private void OnStrChanged()
        {
            EventAggregator.RaiseEvent(new StrChanged() { BattleCharacter = _character, Value = Str });
        }

        private void OnMagChanged()
        {
            EventAggregator.RaiseEvent(new MagChanged() { BattleCharacter = _character, Value = Mag });
        }

        private void OnAgiChanged()
        {
            EventAggregator.RaiseEvent(new AgiChanged() { BattleCharacter = _character, Value = Agi });
        }

        private void OnDefChanged()
        {
            EventAggregator.RaiseEvent(new DefChanged() { BattleCharacter = _character, Value = Def });
        }

        private void OnEvaChanged()
        {
            EventAggregator.RaiseEvent(new EvaChanged() { BattleCharacter = _character, Value = Eva });
        }

        private void OnAccChanged()
        {
            EventAggregator.RaiseEvent(new AccChanged() { BattleCharacter = _character, Value = Acc });
        }

        private void OnMdefChanged()
        {
            EventAggregator.RaiseEvent(new MdefChanged() { BattleCharacter = _character, Value = Mdef });
        }

        private void OnLukChanged()
        {
            EventAggregator.RaiseEvent(new LukChanged() { BattleCharacter = _character, Value = Luk });
        }

        public void Refresh()
        {
            OnHpChanged();
            OnMaxHpChanged();
            OnMpChanged();
            OnMaxMpChanged();
            OnAccChanged();
            OnAgiChanged();
            OnDefChanged();
            OnEvaChanged();
            OnLukChanged();
            OnMagChanged();
            OnStrChanged();
            OnMdefChanged();
        }

    }
}
