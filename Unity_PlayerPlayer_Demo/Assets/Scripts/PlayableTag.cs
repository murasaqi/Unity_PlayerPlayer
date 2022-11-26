using UnityEngine;


namespace sp4ghet
{


    [ExecuteInEditMode]
    public class PlayableTag : MonoBehaviour
    {
        [SerializeField]
        string tag = null;

        public string Tag { get => tag; set => tag = value; }

        void Awake()
        {
            if (tag == null || tag == "")
            {
                tag = System.Guid.NewGuid().ToString();
            }
        }

    }
}
