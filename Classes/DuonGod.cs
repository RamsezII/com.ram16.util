using System.Collections.Generic;
using UnityEngine;

namespace _UTIL_
{
    public interface IDuon
    {
        byte DuonID { get; set; }
        void DestroyDuon();
    }

    public class DuonGod
    {
        public readonly IDuon[] duons;
        [SerializeField] byte currentID;

        //--------------------------------------------------------------------------------------------------------------

        public DuonGod(in byte count)
        {
            duons = new IDuon[count];
        }

        //--------------------------------------------------------------------------------------------------------------

        public void ReserveDuon(in IDuon duon)
        {
            currentID = (byte)((currentID + 1) % duons.Length);
            if (duons[currentID] != null)
                duons[currentID].DestroyDuon();
            duons[currentID] = duon;
            duon.DuonID = currentID;
        }
    }
}