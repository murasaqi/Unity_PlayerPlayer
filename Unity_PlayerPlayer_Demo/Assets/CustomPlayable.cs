using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace sp4ghet
{

    public interface ICustomPlayable
    {
        public void Play();
        public void Pause();
        public void Stop();
    }
}
