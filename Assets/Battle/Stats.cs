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
        public int Hp {get { return _hp; } set {if (_hp == value) return; _hp = value; OnHpChanged(); }}

        [SerializeField] private int _mp;
        public int Mp { get { return _mp; } set { if (_mp == value) return; _mp = value; OnMpChanged(); } }

        [SerializeField] private int _maxHp;
        public int MaxHp { get { return _maxHp; } set { if (_maxHp == value) return; _maxHp = value; OnMaxHpChanged(); } }

        [SerializeField] private int _maxMp;
        public int MaxMp { get { return _maxMp; } set { if (_maxMp == value) return; _maxMp = value; OnMaxMpChanged(); } }

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
            var handler = HpChanged;
            if (handler != null) handler(Hp);

            var critHandler = HpCritical;
            if (critHandler != null) critHandler(Hp <= MaxHp * 0.25);
        }

        private void OnMpChanged()
        {
            var handler = MpChanged;
            if (handler != null) handler(Mp);

            var critHandler = MpCritical;
            if (critHandler != null) critHandler(Hp <= MaxMp * 0.25);
        }

        private void OnMaxHpChanged()
        {
            var handler = MaxHpChanged;
            if (handler != null) handler(Hp);
        }

        private void OnMaxMpChanged()
        {
            var handler = MaxMpChanged;
            if (handler != null) handler(Mp);
        }

        private void OnStrChanged()
        {
            var handler = StrChanged;
            if (handler != null) handler(Str);
        }

        private void OnMagChanged()
        {
            var handler = MagChanged;
            if (handler != null) handler(Mag);
        }

        private void OnAgiChanged()
        {
            var handler = AgiChanged;
            if (handler != null) handler(Agi);
        }

        private void OnDefChanged()
        {
            var handler = DefChanged;
            if (handler != null) handler(Def);
        }

        private void OnEvaChanged()
        {
            var handler = EvaChanged;
            if (handler != null) handler(Eva);
        }

        private void OnAccChanged()
        {
            var handler = AccChanged;
            if (handler != null) handler(Acc);
        }

        private void OnMdefChanged()
        {
            var handler = MdefChanged;
            if (handler != null) handler(Mdef);
        }

        private void OnLukChanged()
        {
            var handler = LukChanged;
            if (handler != null) handler(Luk);
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
