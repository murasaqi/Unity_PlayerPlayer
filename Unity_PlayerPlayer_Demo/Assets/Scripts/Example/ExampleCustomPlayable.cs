using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sp4ghet
{

    [RequireComponent(typeof(MeshRenderer))]
    public class ExampleCustomPlayable : CustomPlayable
    {


        MeshRenderer m_meshRenderer;

        public override void Play()
        {
            m_meshRenderer.material.color = Color.green;
        }

        public override void Stop()
        {
            m_meshRenderer.material.color = Color.red;
        }

        public override void Pause()
        {
            m_meshRenderer.material.color = Color.yellow;
        }


        // Start is called before the first frame update
        void Start()
        {
            m_meshRenderer = GetComponent<MeshRenderer>();
        }
    }

}
