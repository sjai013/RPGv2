using UnityEngine;
using UnityEngine.UI;

namespace Battle.Turn
{
    /// <summary>
    /// Represents "box" showing how long until a character is able to move.
    /// </summary>
    public class TurnDisplay : MonoBehaviour
    { 

        public Color BaseColor;
        public Color OverColor;

        [SerializeField] private Image _image;
        [SerializeField] private Image _baseTicksImage;
        [SerializeField] private Image _overTicksImage;
        [SerializeField] private Image _thisTurnImage;
        [SerializeField] private Image _targettedFriendlyImage;
        [SerializeField] private Image _targettedEnemyImage;
        [SerializeField] private int _maxBaseTicks;
        [SerializeField] private Vector2 _tickBarMaxSize;
    
        void Awake()
        {
            _baseTicksImage.color = BaseColor;
            _overTicksImage.color = OverColor;
        }

        /// <summary>
        /// Draws tick
        /// </summary>
        /// <param name="ticks">Ticks until action</param>
        /// <param name="sprite">Sprite representing character</param>
        /// <param name="thisTurn">Indicates whether this tick is in action</param>
        /// <param name="allyTarget">Is this a friendly target?</param>
        /// <param name="enemyTarget">Is this an enemy target?</param>
        public void DrawTurn(int ticks, Sprite sprite, bool thisTurn, bool allyTarget, bool enemyTarget)
        {
            var bars = (float) ticks/_maxBaseTicks;

            var baseBarSize = _tickBarMaxSize.x * bars;
            if (baseBarSize > _tickBarMaxSize.x)
                baseBarSize = _tickBarMaxSize.x;

            var overBarSize = (_tickBarMaxSize.x* (bars - 1))/2;
            if (overBarSize > _tickBarMaxSize.x)
                overBarSize = _tickBarMaxSize.x;

            _baseTicksImage.rectTransform.sizeDelta = new Vector2(baseBarSize,
                _tickBarMaxSize.y);
        
            _overTicksImage.rectTransform.sizeDelta = new Vector2(overBarSize,
                _tickBarMaxSize.y);

            _image.sprite = sprite;

            _baseTicksImage.rectTransform.anchoredPosition = new Vector2(0, 0);
            _overTicksImage.rectTransform.anchoredPosition = new Vector2(0, 0);
            _image.rectTransform.anchoredPosition = new Vector2(baseBarSize,0);


            if (_baseTicksImage.rectTransform.sizeDelta.x > 0)
                _baseTicksImage.gameObject.SetActive(true);

            if (_overTicksImage.rectTransform.sizeDelta.x > 0)
                _overTicksImage.gameObject.SetActive(true);
            
            _targettedFriendlyImage.gameObject.SetActive(allyTarget);

            _targettedFriendlyImage.gameObject.SetActive(enemyTarget);

            _thisTurnImage.gameObject.SetActive(thisTurn);

            _image.gameObject.SetActive(true);
        }
    }
}
