using System.Collections.Generic;
using System.Linq;

namespace NYPDShootingInc
{
    public class NYPDRaceStat
    {
        public string race;
        public bool isVictim;
    }

    public class NYPDRaceStatTools
    {
        private NYPDRaceStats stats;

        public NYPDRaceStatTools (NYPDRaceStats _stats) {
            stats = _stats;
        }

        public float getNumberOfRaceIncidents(bool isVictim) {
            if (isVictim) {
                return stats.victimdata.Count();
            }
            else {
                return stats.perpdata.Count();
            }
        }

        public string GetPercentageOfRaceIncidents(bool isVictim) {
            return (staticHelpers.getPercentageFromFloat(getNumberOfRaceIncidents(isVictim) / stats.totalIncidentCount));
        }
    }

    public class NYPDRaceStats
    {
        public string race;
        public List<NYPDRaceStat> victimdata;
        public List<NYPDRaceStat> perpdata;
        public float totalIncidentCount;
        public NYPDRaceStatTools statTools;

        public NYPDRaceStats(string _race, List<NYPDIncident> _data, float _tICount) {
            race = _race;
            totalIncidentCount = _tICount;

            victimdata = initializeData(true, _data);
            perpdata = initializeData(false, _data);

            statTools = new NYPDRaceStatTools(this);
        }

        private List<NYPDRaceStat> initializeData(bool isVictim, List<NYPDIncident> _data) {
            List<NYPDRaceStat> resultData = new List<NYPDRaceStat>();
            List<NYPDIncident> siftData;

            if (isVictim) {
                siftData = _data.Where(x => x.victim.race.Contains(race.ToUpper())).ToList();
            }
            else {
                siftData = _data.Where(x => x.perp.race.Contains(race.ToUpper())).ToList();
            }

            foreach (NYPDIncident incident in siftData) {
                NYPDRaceStat newStat = new NYPDRaceStat();

                newStat.race = race;
                newStat.isVictim = isVictim;

                resultData.Add(newStat);
            }

            return resultData;
        }
    }
}