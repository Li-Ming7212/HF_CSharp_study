using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetMission {
    class Mars : PlanetMission//火星
    {
        public Mars() {
            kmToPlanet = 92000000;
            fuelPerKm = 1.73f;
            kmPerHour = 37000;
        }
    }

}
