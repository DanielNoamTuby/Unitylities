using System.Collections.Generic;
using UnityEngine;

namespace Unitylities
{
    public class NPCSystem
    {
        #region Fields
        private List<NPC> npcs;
        #endregion

        #region Properties
        public List<NPC> NPCs => npcs;
        #endregion

        #region Constructor
        public NPCSystem(List<NPC> npcs)
        {
            this.npcs = npcs;
        }
        #endregion

        #region Methods
        public void Add(NPC npc)
        {
            npcs.Add(npc);
        }
        public void Remove(NPC npc)
        {
            npcs.Remove(npc);
        }
        public NPC GetNPC(string name)
        {
            if (npcs.Count > 0)
            {
                foreach (NPC n in npcs)
                {
                    if (n.Name == name)
                    {
                        return n;
                    }
                }
            }

            return null;
        }
        public List<NPC> GetAll()
        {
            List<NPC> list = new List<NPC>();

            if (npcs.Count > 0)
            {
                foreach (NPC n in npcs)
                {
                    list.Add(n);
                }

                return list;
            }

            return list;
        }
        #endregion
    }
}