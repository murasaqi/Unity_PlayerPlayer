using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;


namespace sp4ghet
{
    public class PlayableController : MonoBehaviour
    {

        public enum ElementType
        {
            PlayableDirector,
            VFX,
            Animator,
            Custom
        };

        [System.Serializable]
        public struct ElementIndexer
        {
            public ElementType type;

            public int hash;

            public ElementIndexer(ElementType type, int hash)
            {
                this.type = type;
                this.hash = hash;
            }
        }

        private Dictionary<int, PlayableDirector> m_tracks;
        private Dictionary<int, Animation> m_animations;
        private Dictionary<int, UnityEngine.VFX.VisualEffect> m_vfxGraphs;

        private Dictionary<int, ICustomPlayable> m_customPlayables;

        [SerializeField]
        private List<ElementIndexer> m_masterList = new List<ElementIndexer>();

        [SerializeField]
        private Dictionary<ElementType, int> m_allPlayables = new Dictionary<ElementType, int>();

        [SerializeField]
        private bool m_respectStartOnAwake = false;

        public List<ElementIndexer> MasterList { get => m_masterList; }
        public Dictionary<ElementType, int> AllPlayables { get => m_allPlayables; }

        public void PlayElement(int masterIndex, bool solo = false)
        {
            var indexer = m_masterList[masterIndex];
            ElementType eType = indexer.type;
            var index = indexer.hash;

            switch (eType)
            {
                case ElementType.Animator:
                    var animator = m_animations[index];
                    break;
                case ElementType.PlayableDirector:
                    var track = m_tracks[index];
                    track.Play();
                    break;
                case ElementType.VFX:
                    var vfx = m_vfxGraphs[index];
                    vfx.Play();
                    vfx.pause = false;
                    break;
                case ElementType.Custom:
                    var custom = m_customPlayables[index];
                    custom.Play();
                    break;
                default:
                    break;
            }
        }

        public void PauseElement(int masterIndex)
        {
            var indexer = m_masterList[masterIndex];
            ElementType eType = indexer.type;
            var index = indexer.hash;

            switch (eType)
            {
                case ElementType.Animator:
                    var animator = m_animations[index];
                    break;
                case ElementType.PlayableDirector:
                    var track = m_tracks[index];
                    track.Pause();
                    break;
                case ElementType.VFX:
                    var vfx = m_vfxGraphs[index];
                    vfx.pause = true;
                    break;
                case ElementType.Custom:
                    var custom = m_customPlayables[index];
                    custom.Pause();
                    break;
                default:
                    break;
            }
        }


        public void StopElement(int masterIndex)
        {
            var indexer = m_masterList[masterIndex];
            ElementType eType = indexer.type;
            var index = indexer.hash;

            switch (eType)
            {
                case ElementType.Animator:
                    var animator = m_animations[index];
                    break;
                case ElementType.PlayableDirector:
                    var track = m_tracks[index];
                    track.Stop();
                    break;
                case ElementType.VFX:
                    var vfx = m_vfxGraphs[index];
                    vfx.Stop();
                    break;
                case ElementType.Custom:
                    var custom = m_customPlayables[index];
                    custom.Stop();
                    break;
                default:
                    break;
            }
        }

        public void StopAll()
        {

        }

        public void AddElement(ElementType type, int hash)
        {
            bool canAdd = false;
            switch (type)
            {
                case ElementType.Animator:
                    canAdd = m_animations[hash] != null;
                    break;
                case ElementType.VFX:
                    canAdd = m_vfxGraphs[hash] != null;
                    break;
                case ElementType.PlayableDirector:
                    canAdd = m_allPlayables != null;
                    break;
            }

            if (canAdd)
            {
                m_masterList.Add(new ElementIndexer(type, hash));
            }
        }

        public void DeregisterElement(int masterIndex)
        {

        }

        public void Init()
        {
            m_tracks = new Dictionary<int, PlayableDirector>(FindObjectsOfType<PlayableDirector>().ToDictionary(x => x.GetInstanceID()));
            m_animations = new Dictionary<int, Animation>(FindObjectsOfType<Animation>().ToDictionary(x => x.GetInstanceID()));
            m_vfxGraphs = new Dictionary<int, UnityEngine.VFX.VisualEffect>(FindObjectsOfType<UnityEngine.VFX.VisualEffect>().ToDictionary(x => x.GetInstanceID()));

            m_allPlayables.Add(ElementType.Animator, m_animations.Count);
            m_allPlayables.Add(ElementType.PlayableDirector, m_tracks.Count);
            m_allPlayables.Add(ElementType.VFX, m_vfxGraphs.Count);
        }


    }

}
