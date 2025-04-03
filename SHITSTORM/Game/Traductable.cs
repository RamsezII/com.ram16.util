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

        //--------------------------------------------------------------------------------------------------------------

        public Traductions(in string all)
        {
            english = all;
            french = all;
        }

        //--------------------------------------------------------------------------------------------------------------

        public override readonly string ToString() => Traductable.language switch
        {
            Languages.English => string.IsNullOrWhiteSpace(english) ? "[EMPTY]" : english,
            Languages.French => string.IsNullOrWhiteSpace(french) ? "[VIDE]" : french,
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    public class Traductable : MonoBehaviour
    {
        static readonly HashSet<Traductable> instances = new();
        public static Languages language;

        [SerializeField] bool autoSize;
        [SerializeField] Traductions traductions;
        [Obsolete, SerializeField] string english, francais;
        Action<Traductable> onChange;

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

            instances.Add(this);
            Refresh();
        }

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
                foreach (TextMeshProUGUI child in GetComponentsInChildren<TextMeshProUGUI>())
                    yield return child;
        }

        public static void SetLanguage(in Languages language)
        {
            Traductable.language = language;
            foreach (Traductable self in instances)
                self.Refresh();
        }

        //----------------------------------------------------------------------------------------------------------

        public void AddListener(in Action<Traductable> action)
        {
            onChange += action;
            action(this);
        }

        public void RemoveListener(in Action<Traductable> action) => onChange -= action;

        void Refresh()
        {
            string text = traductions.ToString();

            if (string.IsNullOrWhiteSpace(text))
                text = traductions.english;

            foreach (TextMeshProUGUI tmp in AllTmps())
            {
                tmp.text = text;
                if (autoSize)
                    tmp.AutoSize();
            }

            onChange?.Invoke(this);
        }

        public void SetTrads(in Traductions traductions)
        {
            this.traductions = traductions;
            Refresh();
        }

        public void SetTrad(in string text) => SetTrads(new Traductions { english = text, french = text });

        [Obsolete]
        public void SetTrads_old(in string fr, in string en) => SetTrads(new Traductions { english = en, french = fr });

        //----------------------------------------------------------------------------------------------------------

        private void OnDestroy() => instances.Remove(this);
    }
}