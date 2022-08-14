namespace HideAndSeekTests {
    [TestClass]
    public class GameControllerTests {

        GameController controller;
        [TestInitialize]
        public void Initialize() {
            controller = new GameController();
        }

        [TestMethod]
        public void TestMovement() {
            Assert.AreEqual("入口", controller.CurrentLocation.Name);
            Assert.IsFalse(controller.Move(Direction.上));
            Assert.AreEqual("入口", controller.CurrentLocation.Name);
            Assert.IsTrue(controller.Move(Direction.东));
            Assert.AreEqual("一楼走廊", controller.CurrentLocation.Name);
            Assert.IsTrue(controller.Move(Direction.上));
            Assert.AreEqual("二楼走廊", controller.CurrentLocation.Name);
        }


        [TestMethod]
        public void TestParseInput() {
            var initialStatus = controller.Status;
            Assert.AreEqual("这不是一个有效的方向", controller.ParseInput("X"));
            Assert.AreEqual(initialStatus, controller.Status);
            Assert.AreEqual("那个方向没有出口", controller.ParseInput("上"));
            Assert.AreEqual(initialStatus, controller.Status);
            Assert.AreEqual("向东移动", controller.ParseInput("东"));
            Assert.AreEqual("当前位置：一楼走廊，可以去的地方有：" +
            Environment.NewLine + " - 一楼浴室 在 北方向" +
            Environment.NewLine + " - 客厅 在 南方向" +
            Environment.NewLine + " - 入口 在 西方向" +
            Environment.NewLine + " - 厨房 在 西北方向" +
            Environment.NewLine + " - 二楼走廊 在 上面"+
            Environment.NewLine + "暂时没找到任何对手", controller.Status);
            Assert.AreEqual("向北移动", controller.ParseInput("北"));
            Assert.AreEqual("当前位置：一楼浴室，可以去的地方有：" +
            Environment.NewLine + " - 一楼走廊 在 南方向" +
            Environment.NewLine + "有人可能躲在浴缸里面" +
            Environment.NewLine + "暂时没找到任何对手", controller.Status);
        }


        [TestMethod]
        public void TestParseCheck() {
            Assert.IsFalse(controller.GameOver);

            // Clear the hiding places and hide the opponents in specific rooms
            House.ClearHidingPlaces();
            var joe = controller.Opponents.ToList()[0];
            (House.GetLocationByName("车库") as LocationWithHidingPlace).Hide(joe);
            var bob = controller.Opponents.ToList()[1];
            (House.GetLocationByName("厨房") as LocationWithHidingPlace).Hide(bob);
            var ana = controller.Opponents.ToList()[2];
            (House.GetLocationByName("阁楼") as LocationWithHidingPlace).Hide(ana);
            var owen = controller.Opponents.ToList()[3];
            (House.GetLocationByName("阁楼") as LocationWithHidingPlace).Hide(owen);
            var jimmy = controller.Opponents.ToList()[4];
            (House.GetLocationByName("厨房") as LocationWithHidingPlace).Hide(jimmy);

            // Check the Entry -- there are no players hiding there
            Assert.AreEqual(1, controller.MoveNumber);
            Assert.AreEqual("这里没有可以躲藏的地方", controller.ParseInput("检查"));
            Assert.AreEqual(2, controller.MoveNumber);

            // Move to the Garage
            controller.ParseInput("外");
            Assert.AreEqual(3, controller.MoveNumber);

            // We hid Joe in the Garage, so validate ParseInput's return value and the properties
            Assert.AreEqual("你找到了1个对手，他躲在车后面", controller.ParseInput("检查"));
            Assert.AreEqual("当前位置：车库，可以去的地方有：" +
                               Environment.NewLine + " - 入口 在 里面" +
                               Environment.NewLine + "有人可能躲在车后面" +
                               Environment.NewLine + "共有5个对手，找到了1个：Joe",
                            controller.Status);
            Assert.AreEqual("4:你想往哪个方向走（或者检查此地）：",
                            controller.Prompt);
            Assert.AreEqual(4, controller.MoveNumber);

            //// Move to the bathroom, where nobody is hiding
            //gameController.ParseInput("In");
            //gameController.ParseInput("East");
            //gameController.ParseInput("North");
            //// Check the Bathroom to make sure nobody is hiding there
            //Assert.AreEqual("Nobody was hiding behind the door", gameController.ParseInput("check"));
            //Assert.AreEqual(8, gameController.MoveNumber);

            //// Check the Bathroom to make sure nobody is hiding there
            //gameController.ParseInput("South");
            //gameController.ParseInput("Northwest");
            //Assert.AreEqual("You found 2 opponents hiding next to the stove",
            //                gameController.ParseInput("check"));
            //Assert.AreEqual("You are in the Kitchen. You see the following exits:" +
            //                 Environment.NewLine + " - the Hallway is to the Southeast" +
            //                 Environment.NewLine + "Someone could hide next to the stove" +
            //                 Environment.NewLine + "You have found 3 of 5 opponents: Joe, Bob, Jimmy",
            //                gameController.Status);
            //Assert.AreEqual("11: Which direction do you want to go (or type 'check'): ",
            //                gameController.Prompt);
            //Assert.AreEqual(11, gameController.MoveNumber);

            //Assert.IsFalse(gameController.GameOver);

            //// Head up to the Landing, then check the Pantry (nobody's hiding there)
            //gameController.ParseInput("Southeast");
            //gameController.ParseInput("Up");
            //Assert.AreEqual(13, gameController.MoveNumber);

            //gameController.ParseInput("South");
            //Assert.AreEqual("Nobody was hiding inside a cabinet",
            //                gameController.ParseInput("check"));
            //Assert.AreEqual(15, gameController.MoveNumber);

            //// Check the Attic to find the last two opponents, make sure the game is over
            //gameController.ParseInput("North");
            //gameController.ParseInput("Up");
            //Assert.AreEqual(17, gameController.MoveNumber);

            //Assert.AreEqual("You found 2 opponents hiding in a trunk",
            //                gameController.ParseInput("check"));
            //Assert.AreEqual("You are in the Attic. You see the following exits:" +
            //                   Environment.NewLine + " - the Landing is Down" +
            //                   Environment.NewLine + "Someone could hide in a trunk" +
            //                   Environment.NewLine +
            //                   "You have found 5 of 5 opponents: Joe, Bob, Jimmy, Ana, Owen",
            //                gameController.Status);
            //Assert.AreEqual("18: Which direction do you want to go (or type 'check'): ",
            //                gameController.Prompt);
            //Assert.AreEqual(18, gameController.MoveNumber);

            //Assert.IsTrue(gameController.GameOver);
        }
    }
}
