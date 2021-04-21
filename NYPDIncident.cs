using System;

namespace NYPDShootingInc
{
    public class NYPDIncident {
        public string incidentId;
        public DateTime occuranceDatetime;
        public string borough;
        public string precinct;
        public string jurisdictionCode;
        public bool statisticalMurderFlag;
        public NYPDPerson perp;
        public NYPDPerson victim;
        public NYPDLocation location;
    }
}