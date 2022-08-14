namespace HideAndSeekTests {
    /// <summary>
    /// 模拟随机，用于始终返回特定值的测试
    /// </summary>
    public class MockRandom : System.Random {
        public int ValueToReturn { get; set; } = 0;
        public override int Next() => ValueToReturn;
        public override int Next( int maxValue ) => ValueToReturn;
        public override int Next( int minValue, int maxValue ) => ValueToReturn;
    }

    [TestClass]
    public class HouseTests {

        [TestMethod]
        public void HouseTest() {

            Assert.AreEqual("入口", House.入口.Name);
            Assert.AreEqual("车库", House.入口.GetExit(Direction.外).Name);

            var 一楼走廊 = House.入口.GetExit(Direction.东);
            var 二楼走廊 = 一楼走廊.GetExit(Direction.上);
            Assert.AreEqual("一楼走廊", 一楼走廊.Name);
            Assert.AreEqual("二楼走廊", 二楼走廊.Name);

            Assert.AreEqual("客厅", 一楼走廊.GetExit(Direction.南).Name);
            Assert.AreEqual("厨房", 一楼走廊.GetExit(Direction.西北).Name);
            Assert.AreEqual("一楼浴室", 一楼走廊.GetExit(Direction.北).Name);


            Assert.AreEqual("大房间", 二楼走廊.GetExit(Direction.西北).Name);
            Assert.AreEqual("二楼浴室", 二楼走廊.GetExit(Direction.西).Name);
            Assert.AreEqual("书房", 二楼走廊.GetExit(Direction.东南).Name);
            Assert.AreEqual("小房间", 二楼走廊.GetExit(Direction.西南).Name);
            Assert.AreEqual("杂物间", 二楼走廊.GetExit(Direction.南).Name);
            Assert.AreEqual("阁楼", 二楼走廊.GetExit(Direction.上).Name);

            Assert.AreEqual("大房间浴室", 二楼走廊.GetExit(Direction.西北).GetExit(Direction.东).Name);

        }

        [TestMethod]
        public void TestGetLocationByName() {
            Assert.AreEqual("入口", House.GetLocationByName("入口").Name);
            Assert.AreEqual("阁楼", House.GetLocationByName("阁楼").Name);
            Assert.AreEqual("车库", House.GetLocationByName("车库").Name);
            Assert.AreEqual("大房间", House.GetLocationByName("大房间").Name);
            Assert.AreEqual("入口", House.GetLocationByName("秘密图书馆").Name);
        }
        [TestMethod]
        public void TestRandomExit() {
            var landing = House.GetLocationByName("二楼走廊");
            House.Random = new MockRandom() { ValueToReturn = 0 };
            Assert.AreEqual("大房间", House.RandomExit(landing).Name);
            House.Random = new MockRandom() { ValueToReturn = 1 };
            Assert.AreEqual("二楼浴室", House.RandomExit(landing).Name);
            House.Random = new MockRandom() { ValueToReturn = 2 };
            Assert.AreEqual("阁楼", House.RandomExit(landing).Name);
            House.Random = new MockRandom() { ValueToReturn = 3 };
            Assert.AreEqual("书房", House.RandomExit(landing).Name);
            House.Random = new MockRandom() { ValueToReturn = 4 };
            Assert.AreEqual("小房间", House.RandomExit(landing).Name);
            House.Random = new MockRandom() { ValueToReturn = 5 };
            Assert.AreEqual("一楼走廊", House.RandomExit(landing).Name);
            House.Random = new MockRandom() { ValueToReturn = 6 };
            Assert.AreEqual("杂物间", House.RandomExit(landing).Name);
            var kitchen = House.GetLocationByName("厨房");
            House.Random = new MockRandom() { ValueToReturn = 0 };
            Assert.AreEqual("一楼走廊", House.RandomExit(kitchen).Name);
        }

        [TestMethod]
        public void TestHidingPlaces() {
            Assert.IsInstanceOfType(House.GetLocationByName("车库"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("厨房"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("客厅"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("一楼浴室"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("大房间"),typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("大房间浴室"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("二楼浴室"),typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("小房间"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("书房"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("杂物间"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("阁楼"), typeof(LocationWithHidingPlace));
        }



        [TestMethod]
        public void TestClearHidingPlaces() {
            var garage = House.GetLocationByName("车库") as LocationWithHidingPlace;
            garage.Hide(new Opponent("Opponent1"));

            var attic = House.GetLocationByName("阁楼") as LocationWithHidingPlace;
            attic.Hide(new Opponent("Opponent2"));
            attic.Hide(new Opponent("Opponent3"));
            attic.Hide(new Opponent("Opponent4"));


            House.ClearHidingPlaces();
            Assert.AreEqual(0, garage.CheckHidingPlace().Count());
            Assert.AreEqual(0, attic.CheckHidingPlace().Count());
        }


    }



}
