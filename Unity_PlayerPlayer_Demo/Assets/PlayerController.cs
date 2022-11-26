using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace sp4ghet
{

    public class PlayerController : MonoBehaviour
    {

        [SerializeField]
        PlayableController m_player;

        [SerializeField]
        private bool m_playThing = false;

        // Start is called before the first frame update
        void Start()
        {
            m_player.Init();
            m_player.AddElement(PlayableController.ElementType.VFX, 0);
            Debug.Log(m_player.MasterList[0]);
        }


        // Update is called once per frame
        void Update()
        {
            if (m_playThing)
            {
                m_player.PlayElement(0);
            }
            else
            {
                m_player.PauseElement(0);
            }
        }
    }

}
