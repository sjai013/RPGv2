namespace UnityEngine.UI
{
    [ExecuteInEditMode]
    public class ButtonText : MonoBehaviour
    {
        [SerializeField] private Text _buttonText;


        void Awake()
        {
       
        }
        // Use this for initialization
        void Start ()
        {
	
        }
	
   
        // Update is called once per frame
        void Update () {
            #if UNITY_EDITOR
                _buttonText.text = this.gameObject.name;
            #endif
        }
    }
}
