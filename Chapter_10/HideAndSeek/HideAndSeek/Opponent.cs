using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeek {
    public class Opponent {

        public readonly string Name;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        public Opponent(string name) => Name = name;

        public override string ToString() => Name;


        /// <summary>
        /// 敌人的隐藏函数
        /// </summary>
        public Location Hide() {
            var currentLocation = House.入口;

            var locationsToMoveThrough = House.Random.Next(10, 20);

            for (int i = 0; i < locationsToMoveThrough; i++)
                currentLocation = House.RandomExit(currentLocation);

            while (!(currentLocation is LocationWithHidingPlace)) {
                currentLocation = House.RandomExit(currentLocation);
            }

            (currentLocation as LocationWithHidingPlace).Hide(this);

            System.Diagnostics.Debug.WriteLine(
                $"{Name} 正藏在 {(currentLocation as LocationWithHidingPlace).HidingPlace} ，在 {currentLocation.Name}");

            return currentLocation;
        }
    }
}
