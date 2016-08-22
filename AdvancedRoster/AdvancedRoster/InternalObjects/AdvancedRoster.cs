using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdvancedRoster.InternalObjects
{
    public class AdvancedRoster : List<AdvancedRosterKerbal>
    {
        public static List<string> AvailableNames = new List<string>() { "Frank Plattel", "Johan Verhaar", "Jasper Verhaar", "Johan van de Slikke", "Wouter Stolk", "Haiko van der Velden", "Kees Zaal", "Marjan de Groot", "Hasan Guzel", "Ron Kanhai","Dennis Cozijnsen" };
        public static Dictionary<string, KerbalGender> availableKerbals = new Dictionary<string, KerbalGender>()
        {
            { "Frank Plattel", KerbalGender.Male},
            { "Johan Verhaar", KerbalGender.Male},
            { "Jasper Verhaar", KerbalGender.Male},
            { "Johan van de Slikke", KerbalGender.Male},
            { "Wouter Stolk", KerbalGender.Male},
            { "Haiko van der Velden", KerbalGender.Male},
            { "Rik Jacobs", KerbalGender.Male},           
            { "David Sterkenburg", KerbalGender.Male},
            { "Roan Hageman", KerbalGender.Male},
            { "Wesley den Breejen", KerbalGender.Male},
            { "Tim de Gruijter", KerbalGender.Male},
            { "Adem Kanlioglu", KerbalGender.Male},

            { "Kees Zaal", KerbalGender.Male},
            { "Marjan de Groot", KerbalGender.Female},
            { "Hasan Guzel", KerbalGender.Male},
            { "Ron Kanhai", KerbalGender.Male},
            { "Edwin van der Ham", KerbalGender.Male},
            { "Rianne van der Ham", KerbalGender.Female},
            { "Diana Groenenberg", KerbalGender.Female},
            { "Els van Ooijen", KerbalGender.Female},
            { "Paul Brekelmans", KerbalGender.Male},
            { "Sam de Man", KerbalGender.Male},
            { "Areke van den Hof", KerbalGender.Female},
            { "Diana Elkhuizen", KerbalGender.Female},
            { "Kees van Noorloos", KerbalGender.Male},
            { "Karolina Pozniak", KerbalGender.Female},
            { "Corina Versfeld", KerbalGender.Female}
        };

        public bool NameExists(string name)
        {
            foreach(AdvancedRosterKerbal kerbal in this)
            {
                if (kerbal.CrewMember.name == name)
                {
                    return true;
                }                
            }
            return false;
        }

        public KeyValuePair<string,KerbalGender> GetRandomKerbal()
        {

            int index = (int)Math.Ceiling((double)UnityEngine.Random.Range(0, availableKerbals.Count));
            KeyValuePair <string, KerbalGender> chosen = availableKerbals.ElementAt(index);
            while (NameExists(chosen.Key))
            {
                index = (int)Math.Ceiling((double)UnityEngine.Random.Range(0, availableKerbals.Count));
                chosen = availableKerbals.ElementAt(index);
            }
            return chosen;
        }

    }
}
