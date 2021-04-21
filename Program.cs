using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

/*
TODO:
Being able to categorize by any attribute within NYPDIncident rather than just singular race at present.
*/

namespace NYPDShootingInc
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"G:\My Drive\Den\Helm\C#\NYPDShootingInc\NYPD_Shooting_Incident_Data__Historic_.csv";

            Console.WriteLine("This is JLM's NYPD Shooting Incident Data processing ranging from 2006 to 2020.");

            NYPDdataSource dataSource = new NYPDdataSource(fileName);

            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

            string[] races = {"Native", "Asian", "Black", "Hispanic", "White", "Unknown"};

            Console.WriteLine(dataSource.numberOfIncidents + " shooting incidents as recorded by the NYPD from 2006-2020. \n");

            List<NYPDRaceStats> racialStatistics = new List<NYPDRaceStats>();

            foreach (string race in races) {
                NYPDRaceStats newStats = new NYPDRaceStats(race, dataSource.data, dataSource.numberOfIncidents);
                Console.WriteLine(newStats.race + " persons accounted for " + newStats.statTools.GetPercentageOfRaceIncidents(true) + " of victims involved in shootings reported by the NYPD.");
                racialStatistics.Add(newStats);
            }

            Console.WriteLine("");

            foreach (NYPDRaceStats stat in racialStatistics) {
                Console.WriteLine(stat.race + " persons accounted for " + stat.statTools.GetPercentageOfRaceIncidents(false) + " of perps involved in shootings reported by the NYPD.");
            }

            Console.WriteLine("\n");

            List<string> boroughs = new List<string>();
            boroughs = dataSource.data.Select(x => x.borough).Distinct().ToList();

            foreach (string boro in boroughs) {
                float boroIncCount = dataSource.data.Where(x => x.borough == boro).Count();
                Console.WriteLine(boro + " borough accounted for " +  boroIncCount + " or " + staticHelpers.getPercentageFromFloat(boroIncCount/dataSource.numberOfIncidents) + " of shootings reported by the NYPD");
            }
        }
    }
}
