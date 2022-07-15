using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetMission
{
    abstract class PlanetMission//飞行任务
    {
        protected float fuelPerKm;//每公里燃油
        protected long kmPerHour;//公里/小时
        protected long kmToPlanet;//飞行距离

        public string MissionInfo()
        {
            long fuel = (long)(kmToPlanet * fuelPerKm);//燃油
            long time = kmToPlanet / kmPerHour;//时间
            return $"We'll burn {fuel} units of fuel in {time} hours";
        }
    }
}
