using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace sp4ghet
{

    public abstract class CustomPlayable : MonoBehaviour
    {
        public abstract void Play();
        public abstract void Pause();
        public abstract void Stop();
    }
}
