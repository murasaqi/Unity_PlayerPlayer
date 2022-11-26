using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace sp4ghet
{

    public class LiveToolsUI : MonoBehaviour
    {

        private Foldout m_playableList;

        private void OnEnable()
        {
            var uiDocument = GetComponent<UIDocument>();

            m_playableList = uiDocument.rootVisualElement.Q<Foldout>("player-list");

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
