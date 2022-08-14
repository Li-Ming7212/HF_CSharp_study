using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace HideAndSeek {
    public class GameController {

        /// <summary>
        ///玩家在房子中的当前位置
        /// </summary>
        public Location CurrentLocation { get; private set; }


        /// <summary>
        /// 返回要显示给播放机的当前状态
        /// </summary>
        //public string Status => $"当前位置：{CurrentLocation.Name}，可以去的地方有：" + Environment.NewLine + $" - {string.Join(Environment.NewLine + " - ", CurrentLocation.ExitList)}";
        public string Status {
            get {
                var found = foundOpponents.Count() == 0 ? "暂时没找到任何对手" : $"共有{Opponents.Count()}个对手，找到了{foundOpponents.Count()}个：{string.Join(", ", foundOpponents.Select(o => o.Name))}";

                var hidingPlace = (CurrentLocation is LocationWithHidingPlace location) ? $"{Environment.NewLine}有人可能躲在{location.HidingPlace}" : "";

                return $"当前位置：{CurrentLocation.Name}，可以去的地方有：" + Environment.NewLine + $" - {string.Join(Environment.NewLine + " - ", CurrentLocation.ExitList)}"
                    + $"{hidingPlace}"
                    + $"{Environment.NewLine}{found}";
            }
        }


        /// <summary>
        /// 玩家的移动次数
        /// </summary>
        public int MoveNumber { get; private set; } = 1;
        /// <summary>
        /// 玩家需要找到的对手
        /// </summary>
        public readonly IEnumerable<Opponent> Opponents = new List<Opponent>()
        {
             new Opponent("Joe"),
             new Opponent("Bob"),
             new Opponent("Ana"),
             new Opponent("Owen"),
             new Opponent("Jimmy"),
        };

        /// <summary>
        /// 玩家已经找到的对手
        /// </summary>
        private readonly List<Opponent> foundOpponents = new List<Opponent>();





        /// <summary>
        /// 游戏结束时返回true
        /// </summary>
        public bool GameOver => Opponents.Count() == foundOpponents.Count();


        /// <summary>
        /// 向玩家显示的提示
        /// </summary>
        public string Prompt => $"{MoveNumber}:你想往哪个方向走（或者检查此地）：";

        /// <summary>
        /// 跟踪对手位置的字典
        /// </summary>
        private Dictionary<string, string> opponentLocations = new Dictionary<string, string>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public GameController() {
            House.ClearHidingPlaces();
            foreach (var opponent in Opponents)
                //opponent.Hide();
                opponentLocations.Add(opponent.Name, opponent.Hide().Name);

            CurrentLocation = House.入口;
        }


        /// <summary>
        /// 沿一个方向移动到该位置
        /// </summary>
        /// <param name="direction">The direction to move</param>
        /// <returns>如果玩家可以朝那个方向移动，则为真，反之为假</returns>
        public bool Move( Direction direction ) {
            var oldLocation = CurrentLocation;
            CurrentLocation = CurrentLocation.GetExit(direction);
            return (oldLocation != CurrentLocation);
        }


        /// <summary>
        /// 解析输入并更新状态
        /// </summary>
        /// <param name="input">Input to parse</param>
        /// <returns>解析输入的结果</returns>
        public string ParseInput( string input ) {
            var results = "这不是一个有效的方向";

            if (input.ToLower().StartsWith("保存")) {
                var filename = "存档_1";
                results = Save(filename);
            }

            else if (input.ToLower().StartsWith("读取")) {
                var filename = "存档_1";
                results = Load(filename);
            }

            else if (input.ToLower() == "检查") {
                MoveNumber++;
                if (CurrentLocation is LocationWithHidingPlace locationWithHidingPlace) {
                    var found = locationWithHidingPlace.CheckHidingPlace();
                    if (found.Count() == 0)
                        results = $"没人躲在 {locationWithHidingPlace.HidingPlace}";
                    else {
                        foundOpponents.AddRange(found);
                        results = $"你找到了{found.Count()}个对手，他躲在{locationWithHidingPlace.HidingPlace}";
                    }
                }
                else {
                    results = $"这里没有可以躲藏的地方";
                }
            }

            else if (Enum.TryParse(typeof(Direction), input, out object direction)) {
                if (!Move((Direction)direction))
                    results = "那个方向没有出口";
                else {
                    MoveNumber++;
                    results = $"向{direction}移动";
                }
            }
            return results;
        }

        private string Save( string filename ) {
            if (filename.Contains("/") || filename.Contains("\\") || filename.Contains(" "))
                return "请输入不带斜杠或空格的文件名";
            else {
                var savedGame = new SavedGame() {
                    PlayerLocation = CurrentLocation.Name,
                    OpponentLocations = opponentLocations,
                    FoundOpponents = foundOpponents.Select(opponent => opponent.Name).ToList(),
                    MoveNumber = this.MoveNumber,
                };
                var json = JsonSerializer.Serialize<SavedGame>(savedGame);
                File.WriteAllText($"{filename}.json", json);
                return $"将当前游戏保存到{filename}";
            }
        }

        private string Load( string filename ) {
            if (filename.Contains("/") || filename.Contains("\\") || filename.Contains(" "))
                return "请输入不带斜杠或空格的文件名";
            else if (!File.Exists($"{filename}.json"))
                return "该保存文件不存在";
            else {
                var json = File.ReadAllText($"{filename}.json");
                var savedGame = JsonSerializer.Deserialize<SavedGame>(json);
                House.ClearHidingPlaces();
                CurrentLocation = House.GetLocationByName(savedGame.PlayerLocation);
                foreach (var opponentName in savedGame.OpponentLocations.Keys) {
                    var opponent = new Opponent(opponentName);
                    var locationName = savedGame.OpponentLocations[opponentName];
                    if (House.GetLocationByName(locationName) is LocationWithHidingPlace location)
                        location.Hide(opponent);
                }
                foundOpponents.Clear();
                foundOpponents.AddRange(savedGame.FoundOpponents.Select(name => new Opponent(name)));
                MoveNumber = savedGame.MoveNumber;
                return $"加载{filename}";
            }
        }
    }
}
