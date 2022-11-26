using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;


namespace sp4ghet
{
    public class PlayableManager : MonoBehaviour
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

            public System.Guid hash;

            public ElementIndexer(ElementType type, System.Guid hash)
            {
                this.type = type;
                this.hash = hash;
            }
        }

        private Dictionary<System.Guid, PlayableDirector> m_tracks;
        private Dictionary<System.Guid, Animation> m_animations;
        private Dictionary<System.Guid, UnityEngine.VFX.VisualEffect> m_vfxGraphs;

        private Dictionary<System.Guid, CustomPlayable> m_customPlayables;

        [SerializeField]
        private List<ElementIndexer> m_masterList = new List<ElementIndexer>();

        [SerializeField]
        private Dictionary<ElementType, List<System.Guid>> m_allPlayables = new Dictionary<ElementType, List<System.Guid>>();

        [SerializeField]
        private bool m_respectStartOnAwake = false;

        public List<ElementIndexer> MasterList { get => m_masterList; }
        public Dictionary<ElementType, List<System.Guid>> AllPlayables { get => m_allPlayables; }

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
            throw new System.NotImplementedException("todo");
        }

        public void AddElement(ElementType type, System.Guid hash)
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
                    canAdd = m_tracks[hash] != null;
                    break;
                case ElementType.Custom:
                    canAdd = m_customPlayables[hash] != null;
                    break;
            }

            if (canAdd)
            {
                m_masterList.Add(new ElementIndexer(type, hash));
            }
            else
            {
                Debug.LogError("Failed to add element");
            }
        }

        public void DeregisterElement(int masterIndex)
        {
            throw new System.NotImplementedException("todo");
        }

        public void Init()
        {
            m_tracks = new Dictionary<System.Guid, PlayableDirector>(FindObjectsOfType<PlayableDirector>().ToDictionary(x => System.Guid.Parse(x.GetComponent<PlayableTag>().Tag)));
            m_animations = new Dictionary<System.Guid, Animation>(FindObjectsOfType<Animation>().ToDictionary(x => System.Guid.Parse(x.GetComponent<PlayableTag>().Tag)));
            m_vfxGraphs = new Dictionary<System.Guid, UnityEngine.VFX.VisualEffect>(FindObjectsOfType<UnityEngine.VFX.VisualEffect>().ToDictionary(x => System.Guid.Parse(x.GetComponent<PlayableTag>().Tag)));
            m_customPlayables = new Dictionary<System.Guid, CustomPlayable>(FindObjectsOfType<CustomPlayable>().ToDictionary(x => System.Guid.Parse(x.GetComponent<PlayableTag>().Tag)));

            m_allPlayables.Add(ElementType.Animator, m_animations.Keys.ToList());
            m_allPlayables.Add(ElementType.PlayableDirector, m_tracks.Keys.ToList());
            m_allPlayables.Add(ElementType.VFX, m_vfxGraphs.Keys.ToList());
            m_allPlayables.Add(ElementType.Custom, m_customPlayables.Keys.ToList());
        }

        [ContextMenu("Tag Playables with GUID")]
        public void TagPlayables()
        {
#if UNITY_EDITOR
            foreach (var x in FindObjectsOfType<PlayableDirector>())
            {
                var tag = x.GetComponent<PlayableTag>();
                if (tag == null)
                {
                    x.gameObject.AddComponent<PlayableTag>();
                }
            }

            foreach (var x in FindObjectsOfType<Animation>())
            {
                var tag = x.GetComponent<PlayableTag>();
                if (tag == null)
                {
                    x.gameObject.AddComponent<PlayableTag>();
                }
            }


            foreach (var x in FindObjectsOfType<UnityEngine.VFX.VisualEffect>())
            {
                var tag = x.GetComponent<PlayableTag>();
                if (tag == null)
                {
                    x.gameObject.AddComponent<PlayableTag>();
                }
            }

            foreach (var x in FindObjectsOfType<CustomPlayable>())
            {
                var tag = x.GetComponent<PlayableTag>();
                if (tag == null)
                {
                    x.gameObject.AddComponent<PlayableTag>();
                }
            }
#endif
        }

    }

}
