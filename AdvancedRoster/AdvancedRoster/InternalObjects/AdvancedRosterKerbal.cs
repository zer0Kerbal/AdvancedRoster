using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedRoster.InternalObjects
{
    public class AdvancedRosterKerbal
    {
        public ProtoCrewMember CrewMember { get; private set; }

        public AdvancedRosterKerbal(ProtoCrewMember member)
        {
            CrewMember = member;
        }

    }
}
