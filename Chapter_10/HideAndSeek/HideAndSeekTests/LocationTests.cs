namespace HideAndSeekTests {

    //using System.Collections.Generic;
    //using System.Linq;
    [TestClass]
    public class LocationTests {

        private Location center;

        /// <summary>
        /// 通过 在 测试前设置、创建新的中心位置并 在 每个方向上添加房间来初始化每个单元测试
        /// </summary>
        [TestInitialize]
        public void Initialize() {
            // You'll use this to create a bunch of locations before each test
            center = new Location("center room");
            Assert.AreSame("center room", center.Name);
            Assert.AreEqual(0, center.ExitList.Count());

            center.AddExit(Direction.北, new Location("North Room"));
            center.AddExit(Direction.东北, new Location("Northeast Room"));
            center.AddExit(Direction.东, new Location("East Room"));
            center.AddExit(Direction.东南, new Location("Southeast Room"));
            center.AddExit(Direction.南, new Location("South Room"));
            center.AddExit(Direction.西南, new Location("Southwest Room"));
            center.AddExit(Direction.西, new Location("West Room"));
            center.AddExit(Direction.西北, new Location("Northwest Room"));
            center.AddExit(Direction.上, new Location("Upper Room"));
            center.AddExit(Direction.下, new Location("Lower Room"));
            center.AddExit(Direction.里, new Location("Inside Room"));
            center.AddExit(Direction.外, new Location("Outside Room"));

            Assert.AreEqual(12, center.ExitList.Count());
        }


        /// <summary>
        /// 确保GetExit仅 在 某个方向上返回该位置（如果该位置存 在 ）
        /// </summary>
        [TestMethod]
        public void TestGetExit() {
            var upRoom = center.GetExit(Direction.上);

            Assert.AreEqual("Upper Room", upRoom.Name);
            Assert.AreSame(center, upRoom.GetExit(Direction.下));
            Assert.AreSame(upRoom, upRoom.GetExit(Direction.上));
        }


        /// <summary>
        /// 验证退出列表是否正常工作
        /// </summary>
        [TestMethod]
        public void TestExitList() {
            Assert.AreEqual("North Room 在 北方向", center.ExitList.ToList()[0]);
            Assert.AreEqual("Northeast Room 在 东北方向", center.ExitList.ToList()[4]);
            Assert.AreEqual("Southwest Room 在 西南方向", center.ExitList.ToList()[5]);
            Assert.AreEqual("Outside Room 在 外面", center.ExitList.ToList()[11]);
            CollectionAssert.AreEqual(
               new List<string>() {
                   "North Room 在 北方向",
                   "South Room 在 南方向",
                   "East Room 在 东方向",
                   "West Room 在 西方向",
                   "Northeast Room 在 东北方向",
                   "Southwest Room 在 西南方向",
                   "Southeast Room 在 东南方向",
                   "Northwest Room 在 西北方向",
                   "Upper Room 在 上面",
                   "Lower Room 在 下面",
                   "Inside Room 在 里面",
                   "Outside Room 在 外面",
               },
               center.ExitList.ToList());
        }


        /// <summary>
        /// 验证每个房间的名称和返回出口是否正确创建
        /// </summary>
        [TestMethod]
        public void TestReturnExits() {
            var n = center.GetExit(Direction.北);
            Assert.AreEqual("North Room", n.ToString());
            Assert.AreSame(center, n.GetExit(Direction.南));
            Assert.AreEqual(1, n.ExitList.Count());

            var ne = center.GetExit(Direction.东北);
            Assert.AreEqual("Northeast Room", ne.ToString());
            Assert.AreSame(center, ne.GetExit(Direction.西南));

            var sw = center.GetExit(Direction.西南);
            Assert.AreEqual("Southwest Room", sw.ToString());
            Assert.AreSame(center, sw.GetExit(Direction.东北));

            var d = center.GetExit(Direction.下);
            Assert.AreEqual("Lower Room", d.ToString());
            Assert.AreSame(center, d.GetExit(Direction.上));

            var inside = center.GetExit(Direction.里);
            Assert.AreEqual("Inside Room", inside.ToString());
            Assert.AreSame(center, inside.GetExit(Direction.外));
        }



        /// <summary>
        /// 将大厅添加到其中一个房间，并确保正确创建了大厅、房间名称和返回出口
        /// </summary>
        [TestMethod]
        public void TestAddHall() {
            var e = center.GetExit(Direction.东);
            Assert.AreEqual(1, e.ExitList.Count());

            var eastHall1 = new Location("eastHall_1");
            var eastHall2 = new Location("eastHall_2");

            e.AddExit(Direction.东, eastHall1);
            eastHall1.AddExit(Direction.东, eastHall2);

            Assert.AreEqual(2, e.ExitList.Count());
            Assert.AreEqual(2, eastHall1.ExitList.Count());
            Assert.AreEqual(1, eastHall2.ExitList.Count());
        }
    }
}