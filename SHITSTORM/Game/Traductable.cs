using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _UTIL_
{
    public enum Languages : byte
    {
        English,
        French,
        _last_,
    }

    [Serializable]
    public struct Traductions
    {
        public string english, french;
        public readonly string Auto => Traductable.language switch
        {
            Languages.English => english,
            Languages.French => french,
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    public class Traductable : MonoBehaviour
    {
        static readonly HashSet<Traductable> selves = new();
        public static Languages language;

        [Obsolete, SerializeField] string english, francais;
        [SerializeField] Traductions traductions;

        //----------------------------------------------------------------------------------------------------------

#if UNITY_EDITOR
        [ContextMenu(nameof(InvertTrads))]
        void InvertTrads() => (traductions.french, traductions.english) = (traductions.english, traductions.french);

        [ContextMenu(nameof(ApplyTrads))]
        void ApplyTrads() => SetTrads(traductions);
#endif

        //----------------------------------------------------------------------------------------------------------

        private void Awake()
        {
            if (!string.IsNullOrWhiteSpace(francais) || !string.IsNullOrEmpty(english))
                traductions = new Traductions { english = english, french = francais };

            selves.Add(this);
            Refresh();
        }

        private void OnDestroy() => selves.Remove(this);

        //----------------------------------------------------------------------------------------------------------

        public static Languages GetSystemLanguage() => Application.systemLanguage switch
        {
            SystemLanguage.French => Languages.French,
            _ => Languages.English,
        };

        public IEnumerable<TextMeshProUGUI> AllTmps()
        {
            if (TryGetComponent(out TextMeshProUGUI tmp))
                yield return tmp;
            else
                for (int i = 0; i < transform.childCount; i++)
                    if (transform.GetChild(i).TryGetComponent(out tmp))
                        yield return tmp;
        }

        public static void SetLanguage(in Languages language)
        {
            Traductable.language = language;
            foreach (Traductable self in selves)
                self.Refresh();
        }

        void Refresh()
        {
            string text = traductions.Auto;

            if (string.IsNullOrWhiteSpace(text))
                text = traductions.english;

            foreach (TextMeshProUGUI tmp in AllTmps())
                tmp.text = text;
        }

        public void SetTrads(in Traductions traductions)
        {
            this.traductions = traductions;
            Refresh();
        }

        public void SetTrad(in string text) => SetTrads(new Traductions { english = text, french = text });

        [Obsolete]
        public void SetTrads_old(in string fr, in string en) => SetTrads(new Traductions { english = en, french = fr });
    }
}