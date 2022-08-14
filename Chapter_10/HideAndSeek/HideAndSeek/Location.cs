using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeek {
    public enum Direction {
        北 = -1,
        南 = 1,
        东 = -2,
        西 = 2,
        东北 = -3,
        西南 = 3,
        东南 = -4,
        西北 = 4,
        上 = -5,
        下 = 5,
        里 = -6,
        外 = 6,
    }
    public class Location {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 这个位置可离开的出口
        /// </summary>
        public IDictionary<Direction, Location> Exits { get; private set; } = new Dictionary<Direction, Location>();

        /// <summary>
        /// 构造函数，设置名字
        /// </summary>
        /// <param name="name">此位置名字</param>
        public Location( string name ) => this.Name = name;

        public override string ToString() => Name;


        /// <summary>
        ///方向描述 (e.g. "in" vs. "to the North")
        /// </summary>
        /// <param name="d">描述方向</param>
        /// <returns>描述方向的字符串</returns>
        private string DescribeDirection( Direction d ) => d switch {
            Direction.上 => "上面",
            Direction.下 => "下面",
            Direction.里 => "里面",
            Direction.外 => "外面",
            _ => $"{d}方向",
        };



        /// <summary>
        /// 返回出口描述序列，按方向排序
        /// </summary>
        public IEnumerable<string> ExitList =>
            Exits.OrderBy(keyValuePair => (int)keyValuePair.Key)
            .OrderBy(keyValuePar => Math.Abs((int)keyValuePar.Key))
            .Select(keyValuePair => $"{keyValuePair.Value} 在 {DescribeDirection(keyValuePair.Key)}");




        /// <summary>
        /// 将返回出口添加到连接位置
        /// </summary>
        /// <param name="direction">方向</param>
        /// <param name="connectingLocation">Connecting location to add the return exit to</param>
        private void AddReturnExit( Direction direction, Location connectingLocation ) =>
            Exits.Add((Direction)(-(int)direction), connectingLocation);




        /// <summary>
        /// 将出口添加到此位置
        /// </summary>
        /// <param name="direction">连接位置的方向</param>
        /// <param name="connectingLocation">要添加的连接位置</param>
        public void AddExit( Direction direction, Location connectingLocation ) {
            Exits.Add(direction, connectingLocation);
            connectingLocation.AddReturnExit(direction, this);
        }



        /// <summary>
        /// 获取方向上的出口位置
        /// </summary>
        /// <param name="direction">方向</param>
        /// <returns>出口位置，或者如果在该方向上没有出口</returns>
        public Location GetExit( Direction direction ) => Exits.ContainsKey(direction) ? Exits[direction] : this;
    }
}
