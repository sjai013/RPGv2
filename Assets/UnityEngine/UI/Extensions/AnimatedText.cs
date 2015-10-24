using System.Collections;

namespace UnityEngine.UI.Extensions
{
    [RequireComponent(typeof(Text), typeof(RectTransform))]
    [AddComponentMenu("UI/Effects/Extensions/Animated Text")]
    public class AnimatedText : BaseMeshEffect
    {
        public AnimationCurve PositionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        public float PositionMultiplier = 1;
        public AnimationCurve TimeCurve = AnimationCurve.Linear(0, 0, 1, 1);
        public float AnimationDuration = 1;
        private VertexHelper _origVertexHelper = null;
        public int Repeats = -1;

        private int _yPosRepeated = 0;
    
        private float time;

        private float _yPosRefTime;


        protected override void Awake()
        {
            base.Awake();
     
            OnRectTransformDimensionsChange();
            StartAnimation();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            OnRectTransformDimensionsChange();
            _yPosRepeated = Repeats;
            _origVertexHelper = null;
            StartAnimation();
        }

        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive())
                return;

            if (!Application.isPlaying)
                return;

            if (_origVertexHelper == null)
                _origVertexHelper = vh;

            YPositionAnimation(vh);


        }

        private void YPositionAnimation(VertexHelper vh)
        {
            int nVert = vh.currentVertCount;
            for (int i = 0; i < nVert; i++) //Iterate across each character
            {

                var uiVertex = new UIVertex();
                var origUiVertex = new UIVertex();

                float timeOffset = TimeCurve.Evaluate(i / (nVert + 1f));  //Get time at which to start the animation sequence

                //If this is before the starting point, then we keep the character at original position, otherwise we need to calculate the time to use from the position curve
                float curveTime = (time - timeOffset) / AnimationDuration;
                if (curveTime < 0)
                    curveTime = 0;

                if (i == nVert - 1 && curveTime >= 1)
                {
                    _yPosRefTime = Time.time;
                    _yPosRepeated--;
                }

                float position = PositionCurve.Evaluate(curveTime);
                var vertexId = i;

                vh.PopulateUIVertex(ref uiVertex, vertexId);
                _origVertexHelper.PopulateUIVertex(ref origUiVertex, vertexId);
                uiVertex.position.y = origUiVertex.position.y + (position * PositionMultiplier);

                vh.SetUIVertex(uiVertex, vertexId);

            }
        }

        public void StartAnimation()
        {
            StartCoroutine(AnimationCoroutine());
        }

        public void StopAnimation()
        {
            _yPosRepeated = 0;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _yPosRepeated = 0;
        }


        private IEnumerator AnimationCoroutine()
        {
            _yPosRefTime = Time.time;
            while (_yPosRepeated > 0)
            {
                time = Time.time - _yPosRefTime;
                GetComponent<Text>().SetVerticesDirty();
                yield return null;
            }


        }
    }
}
