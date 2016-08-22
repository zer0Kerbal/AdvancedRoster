using AdvancedRoster.InternalObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdvancedRoster
{
    [KSPScenario(ScenarioCreationOptions.AddToNewCareerGames | ScenarioCreationOptions.AddToExistingCareerGames, new GameScenes[] { GameScenes.SPACECENTER, GameScenes.EDITOR, GameScenes.FLIGHT })]
    public class AdvancedRosterAddon : ScenarioModule
    {

        public InternalObjects.AdvancedRoster Roster = new InternalObjects.AdvancedRoster();
        private bool initialized = false;

        public override void OnAwake()
        {
           
        }

        private ProtoCrewMember FindExistingKerbal(string name)
        {
            foreach (ProtoCrewMember member in HighLogic.CurrentGame.CrewRoster.Crew)
            {
                if (member.name == name)
                    return member;
            }
            foreach (ProtoCrewMember member in HighLogic.CurrentGame.CrewRoster.Applicants)
            {
                if (member.name == name)
                    return member;
            }
            return null;
        }

        public override void OnLoad(ConfigNode node)
        {
           
            //clear roster
            Roster.Clear();

            var rootNode = node.GetNode("ADVANCED_ROSTER");
            if(rootNode != null)
            {
                var rosterNode = rootNode.GetNode("ROSTER");
                foreach (var memberNode in rosterNode.GetNodes("KERBAL"))
                {
                    string name = memberNode.GetValue("name");
                    float courage = float.Parse(memberNode.GetValue("courage"));
                    float stupidity = float.Parse(memberNode.GetValue("stupidity"));
                    bool baddass = bool.Parse(memberNode.GetValue("badass"));
                    ProtoCrewMember member = FindExistingKerbal(name);
                    if (member != null)
                    {
                        Debug.Log("Loaded " + member.name);
                        Roster.Add(new InternalObjects.AdvancedRosterKerbal(member));
                    }                   
                }
            }

            if(Roster.Count <= 0)
            {
                if (HighLogic.CurrentGame.CrewRoster != null && HighLogic.CurrentGame.CrewRoster.Applicants != null)
                {
                    Debug.Log("Renaming Crew Kerbals");
                    int index = 0;
                    foreach (ProtoCrewMember member in HighLogic.CurrentGame.CrewRoster.Crew)
                    {
                        KeyValuePair<string, KerbalGender> chosen = Roster.GetRandomKerbal();
                        member.name = chosen.Key;
                        if (chosen.Value == KerbalGender.Male)
                            member.gender = ProtoCrewMember.Gender.Male;
                        else
                            member.gender = ProtoCrewMember.Gender.Female;
                        HighLogic.CurrentGame.CrewRoster.Update(1);
                        Roster.Add(new InternalObjects.AdvancedRosterKerbal(member));
                        Debug.Log(index.ToString());
                        index++;
                    }

                    initialized = true;
                    Debug.Log("Crew Kerbals Renamed");

                    Debug.Log("Renaming Applicant Kerbals");
                    foreach (ProtoCrewMember member in HighLogic.CurrentGame.CrewRoster.Applicants)
                    {
                        KeyValuePair<string, KerbalGender> chosen = Roster.GetRandomKerbal();
                        member.name = chosen.Key;
                        if (chosen.Value == KerbalGender.Male)
                            member.gender = ProtoCrewMember.Gender.Male;
                        else
                            member.gender = ProtoCrewMember.Gender.Female;
                        HighLogic.CurrentGame.CrewRoster.Update(1);
                        Roster.Add(new InternalObjects.AdvancedRosterKerbal(member));
                    }
                    Debug.Log("Applicant Kerbals Renamed");
                }
            }           
        }

//        var rootNode = node.GetNode("ADVANCED_ROSTER");
//                if(rootNode != null)
//                {
//                    var rosterNode = rootNode.GetNode("ROSTER");
//                    foreach (var memberNode in rosterNode.GetNodes("KERBAL"))
//                    {
//                        string name = memberNode.GetValue("name");
//        //float courage = float.Parse(memberNode.GetValue("courage"));
//        //float stupidity = float.Parse(memberNode.GetValue("stupidity"));
//        //bool baddass = bool.Parse(memberNode.GetValue("badass"));
//        ProtoCrewMember member = FindExistingKerbal(name);
//                        if (member != null)
//                        {
//                            Roster.Add(new InternalObjects.AdvancedRosterKerbal(member));
//                        }
//}
//                    foreach (InternalObjects.AdvancedRosterKerbal k in Roster)
//                    {
//                        Debug.Log(k.CrewMember.name);
//                    }
//                }
//                else
//                {

        public override void OnSave(ConfigNode node)
        {
            Debug.Log("SAVING NEW NAMES");
            node.ClearNodes();

            var configNode = new ConfigNode("ADVANCED_ROSTER");
            var rosterNode = new ConfigNode("ROSTER");
            configNode.AddNode(rosterNode);
            foreach(InternalObjects.AdvancedRosterKerbal kerbal in Roster)
            {
                var kerbalNode = new ConfigNode("KERBAL");
                kerbalNode.AddValue("name", kerbal.CrewMember.name);
                kerbalNode.AddValue("courage", kerbal.CrewMember.courage);
                kerbalNode.AddValue("stupidity", kerbal.CrewMember.stupidity);
                kerbalNode.AddValue("badass", kerbal.CrewMember.isBadass);
                rosterNode.AddNode(kerbalNode);
            }
            node.AddNode(configNode);
        }

    }
}
