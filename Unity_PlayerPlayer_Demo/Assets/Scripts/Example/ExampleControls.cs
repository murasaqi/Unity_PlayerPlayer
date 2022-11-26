using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace sp4ghet
{

    [RequireComponent(typeof(PlayableManager))]
    public class ExampleControls : MonoBehaviour
    {

        [SerializeField]
        PlayableManager m_player;

        enum State
        {
            Play, Pause, Stop
        };

        State m_state = State.Stop;

        // Start is called before the first frame update
        void Start()
        {
            m_player.Init();
            var firstCustomHash = m_player.AllPlayables[PlayableManager.ElementType.Custom][0];
            m_player.AddElement(PlayableManager.ElementType.Custom, firstCustomHash);
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                m_state = State.Play;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                m_state = State.Pause;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                m_state = State.Stop;
            }

            switch (m_state)
            {
                case State.Stop:
                    m_player.StopElement(0);
                    break;
                case State.Play:
                    m_player.PlayElement(0);
                    break;
                case State.Pause:
                    m_player.PauseElement(0);
                    break;
            }
        }
    }

}
