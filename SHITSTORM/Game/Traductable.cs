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
    }

    public class Traductable : MonoBehaviour
    {
        static readonly HashSet<Traductable> selves = new();
        public static Languages language;

        [Obsolete, SerializeField] string francais, english;
        [SerializeField] Traductions traductions;

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

        [Obsolete]
        public static void Toggle(in bool fran_b) => SetLanguage(fran_b ? Languages.French : Languages.English);
        public static void SetLanguage(in Languages language)
        {
            Traductable.language = language;
            foreach (Traductable self in selves)
                self.Refresh();
        }

        void Refresh()
        {
            string text = language switch
            {
                Languages.English => traductions.english,
                Languages.French => traductions.french,
                _ => throw new ArgumentOutOfRangeException(),
            };

            foreach (TextMeshProUGUI tmp in GetComponentsInChildren<TextMeshProUGUI>(true))
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