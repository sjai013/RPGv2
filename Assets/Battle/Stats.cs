using System;
using UnityEngine;


namespace Battle
{
    [Serializable]
    public class Stats
    {
        public delegate void UpdateStat(int value);
        public delegate void StatCritical(bool value);

        public event UpdateStat HpChanged;
        public event StatCritical HpCritical;
        public event UpdateStat MpChanged;
        public event StatCritical MpCritical;
        public event UpdateStat MaxHpChanged;
        public event UpdateStat MaxMpChanged;
        public event UpdateStat StrChanged;
        public event UpdateStat AgiChanged;
        public event UpdateStat DefChanged;
        public event UpdateStat EvaChanged;
        public event UpdateStat MagChanged;
        public event UpdateStat AccChanged;
        public event UpdateStat MdefChanged;
        public event UpdateStat LukChanged;

        [SerializeField] private int _hp;
        public int Hp {get { return _hp; } set {if (_hp == value) return; _hp = value; _hp = Hp; OnHpChanged(); }}

        [SerializeField] private int _mp;
        public int Mp { get { return _mp; } set { if (_mp == value) return; _mp = value; _mp = Mp; OnMpChanged(); } }

        [SerializeField] private int _maxHp;
        public int MaxHp { get { return _maxHp; } set { if (_maxHp == value) return; _maxHp = value; _maxHp = MaxHp; OnMaxHpChanged(); } }

        [SerializeField] private int _maxMp;
        public int MaxMp { get { return _maxMp; } set { if (_maxMp == value) return; _maxMp = value; _maxMp = MaxHp; OnMaxMpChanged(); } }

        [SerializeField] private int _str;
        public int Str { get { return _str; } set { if (_str == value) return; _str = value; _str = Str; OnStrChanged(); } }

        [SerializeField] private int _mag;
        public int Mag { get { return _mag; } set { if (_mag == value) return; _mag = value; _mag = Mag; OnMagChanged(); } }

        [SerializeField] private int _agi;
        public int Agi { get { return _agi; } set { if (_agi == value) return; _agi = value; _agi = Agi; OnAgiChanged(); } }

        [SerializeField] private int _def;
        public int Def { get { return _def; } set { if (_def == value) return; _def = value; _def = Def; OnDefChanged(); } }

        [SerializeField] private int _eva;
        public int Eva { get { return _eva; } set { if (_eva == value) return; _eva = value; _eva = Eva; OnEvaChanged(); } }

        [SerializeField] private int _acc;
        public int Acc { get { return _acc; } set { if (_acc == value) return; _acc = value; _acc = Acc; OnAccChanged(); } }

        [SerializeField] private int _mdef;
        public int Mdef { get { return _mdef; } set { if (_mdef == value) return; _mdef = value; _mdef = Mdef; OnMdefChanged(); } }

        [SerializeField] private int _luk;
        public int Luk { get { return _luk; } set { if (_luk == value) return; _luk = value; _luk = Luk; OnLukChanged(); } }

        private void OnHpChanged()
        {
            var handler = HpChanged;
            if (handler != null) handler(_hp);

            var critHandler = HpCritical;
            if (critHandler != null) critHandler(_hp <= MaxHp * 0.25);
        }

        private void OnMpChanged()
        {
            var handler = MpChanged;
            if (handler != null) handler(_mp);

            var critHandler = MpCritical;
            if (critHandler != null) critHandler(_hp <= MaxMp * 0.25);
        }

        private void OnMaxHpChanged()
        {
            var handler = MaxHpChanged;
            if (handler != null) handler(_hp);
        }

        private void OnMaxMpChanged()
        {
            var handler = MaxMpChanged;
            if (handler != null) handler(_mp);
        }

        private void OnStrChanged()
        {
            var handler = StrChanged;
            if (handler != null) handler(_str);
        }

        private void OnMagChanged()
        {
            var handler = MagChanged;
            if (handler != null) handler(_mag);
        }

        private void OnAgiChanged()
        {
            var handler = AgiChanged;
            if (handler != null) handler(_agi);
        }

        private void OnDefChanged()
        {
            var handler = DefChanged;
            if (handler != null) handler(_def);
        }

        private void OnEvaChanged()
        {
            var handler = EvaChanged;
            if (handler != null) handler(_eva);
        }

        private void OnAccChanged()
        {
            var handler = AccChanged;
            if (handler != null) handler(_acc);
        }

        private void OnMdefChanged()
        {
            var handler = MdefChanged;
            if (handler != null) handler(_mdef);
        }

        private void OnLukChanged()
        {
            var handler = LukChanged;
            if (handler != null) handler(_luk);
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
