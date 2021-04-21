using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;


namespace NYPDShootingInc
{
    public class NYPDdataSource
    {
        public List<NYPDIncident> data;

        public float numberOfIncidents
        {
            get{
                if (data == null) {
                    return 0;
                }
                else {
                    return data.Count;
                }
            }
        }

        public NYPDRaceStats getRaceStats(string race)
        {
            if (data == null) {
                return null;
            }
            else {
                NYPDRaceStats stats = new NYPDRaceStats(race, data.Where(x => 
                    x.victim.race.Contains(race.ToUpper())
                    ||
                    x.perp.race.Contains(race.ToUpper())
                    ).ToList(), numberOfIncidents);

                return stats;
            }
        }

        public NYPDdataSource(string fileName) {
            data = new List<NYPDIncident>();

            loadData(fileName);
        }

        public void loadData(string fileName) {
            // get data (set numberOfRows equal to Count of filestream)

            FileStream fs = File.OpenRead(fileName);
            var sr = new StreamReader(fs);

            string line;
            string headers = sr.ReadLine();

            while ((line = sr.ReadLine()) != null)
            {
                // make sure the line is not empty.
                if (line == null || line == "") {
                    continue;
                }
                string[] row = line.Split(',');
                //Console.WriteLine(line);
                var newIncident = new NYPDIncident();
                newIncident.incidentId = row[0];
                newIncident.occuranceDatetime = DateTime.Parse(row[1]) + TimeSpan.Parse(row[2]);
                newIncident.borough = row[3];
                newIncident.precinct = row[4];
                newIncident.jurisdictionCode = row[5];
                newIncident.statisticalMurderFlag = row[7]=="1";
                // perp
                var newPerp = new NYPDPerson();
                newPerp.age = row[8];
                newPerp.sex = row[9];
                newPerp.race = row[10];
                newPerp.isPerp = true;
                // victim
                var newVic = new NYPDPerson();
                newVic.age = row[11];
                newVic.sex = row[12];
                newVic.race = row[13];
                newVic.isPerp = false;
                // location
                var newLocation = new NYPDLocation();
                newLocation.locationDesc = row[6];
                newLocation.xCoord = row[14];
                newLocation.yCoord = row[15];
                newLocation.latitude = row[16];
                newLocation.longitude = row[17];
                newLocation.lonLat = row[18];
                // adding up
                newIncident.perp = newPerp;
                newIncident.victim = newVic;
                newIncident.location = newLocation;
                data.Add(newIncident);
            }
        }
    }
}