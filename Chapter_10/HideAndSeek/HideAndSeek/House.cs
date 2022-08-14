using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeek {
    public static class House {
        public readonly static Location 入口;

        private static readonly IEnumerable<Location> locations;


        static House() {
            入口 = new Location("入口");

            var 车库 = new LocationWithHidingPlace("车库", "车后面");
            var 一楼走廊 = new Location("一楼走廊");
            var 客厅 = new LocationWithHidingPlace("客厅", "沙发后面");
            var 厨房 = new LocationWithHidingPlace("厨房", "炉子旁边");
            var 一楼浴室 = new LocationWithHidingPlace("一楼浴室", "浴缸里面");
            var 二楼走廊 = new Location("二楼走廊");
            var 大房间 = new LocationWithHidingPlace("大房间", "床上");
            var 大房间浴室 = new LocationWithHidingPlace("大房间浴室", "衣柜里");
            var 二楼浴室 = new LocationWithHidingPlace("二楼浴室", "门后面");
            var 小房间 = new LocationWithHidingPlace("小房间", "床底下");
            var 书房 = new LocationWithHidingPlace("书房", "桌子底下");
            var 杂物间 = new LocationWithHidingPlace("杂物间", "天花板");
            var 阁楼 = new LocationWithHidingPlace("阁楼", "大箱子里");

            入口.AddExit(Direction.外, 车库);
            入口.AddExit(Direction.东, 一楼走廊);
            一楼走廊.AddExit(Direction.西北, 厨房);
            一楼走廊.AddExit(Direction.南, 客厅);
            一楼走廊.AddExit(Direction.北, 一楼浴室);
            一楼走廊.AddExit(Direction.上, 二楼走廊);
            二楼走廊.AddExit(Direction.西北, 大房间);
            二楼走廊.AddExit(Direction.西, 二楼浴室);
            二楼走廊.AddExit(Direction.东南, 书房);
            二楼走廊.AddExit(Direction.西南, 小房间);
            二楼走廊.AddExit(Direction.南, 杂物间);
            二楼走廊.AddExit(Direction.上, 阁楼);
            大房间.AddExit(Direction.东, 大房间浴室);


            locations = new List<Location>() { 入口, 车库, 一楼走廊, 客厅, 厨房, 一楼浴室, 二楼走廊, 大房间, 大房间浴室, 二楼浴室, 小房间, 书房, 杂物间, 阁楼 };

        }

        /// <summary>
        /// 通过名字获取房间位置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Location GetLocationByName( string name ) {
            var found = locations.Where(l => l.Name == name);
            return found.Count() > 0 ? found.First() : 入口;
        }

        public static Random Random = new Random();

        public static Location RandomExit( Location location ) =>
            GetLocationByName(
                location
                    .Exits
                    .OrderBy(exit => exit.Value.Name)
                    .Select(exit => exit.Value.Name)
                    .Skip(Random.Next(0, location.ExitList.Count()))
                    .First());

        public static void ClearHidingPlaces() {
            foreach (var location in locations)
                if (location is LocationWithHidingPlace hidingPlace)
                    hidingPlace.CheckHidingPlace();
        }
    }
}
