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
        public string[] values;
    }

    public class Traductable : MonoBehaviour
    {
        static readonly HashSet<Traductable> selves = new();
        public static bool fran_b;

        [SerializeField] string francais, english;

        //----------------------------------------------------------------------------------------------------------

        private void Awake()
        {
            selves.Add(this);
            Refresh();
        }

        private void OnDestroy() => selves.Remove(this);

        //----------------------------------------------------------------------------------------------------------

        public static void Toggle(in bool francais_b)
        {
            fran_b = francais_b;
            foreach (Traductable self in selves)
                self.Refresh();
        }

        void Refresh()
        {
            string text = fran_b ? francais : english;
            foreach (TextMeshProUGUI tmp in GetComponentsInChildren<TextMeshProUGUI>(true))
                tmp.text = text;
        }

        public void SetTrads(in string text)
        {
            francais = english = text;
            Refresh();
        }

        public void SetTrads(in string fr, in string en)
        {
            francais = fr;
            english = en;
            Refresh();
        }
    }
}