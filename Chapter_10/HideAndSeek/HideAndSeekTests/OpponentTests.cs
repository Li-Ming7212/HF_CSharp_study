

namespace HideAndSeekTests {


    /// <summary>
    /// 模拟随机，用于使用列表返回值的测试
    /// </summary>
    public class MockRandomWithValueList : System.Random {
        private Queue<int> valuesToReturn;
        public MockRandomWithValueList( IEnumerable<int> values ) =>
            valuesToReturn = new Queue<int>(values);
        public int NextValue() {
            var nextValue = valuesToReturn.Dequeue();
            valuesToReturn.Enqueue(nextValue);
            return nextValue;
        }
        public override int Next() => NextValue();
        public override int Next( int maxValue ) => Next(0, maxValue);
        public override int Next( int minValue, int maxValue ) {
            var next = NextValue();
            return next >= minValue && next < maxValue ? next : minValue;
        }
    }


    [TestClass]
    public class OpponentTests {

        [TestMethod]
        public void TestOpponentHiding() {
            var opponent = new Opponent("opponent1");
            Assert.AreEqual("opponent1", opponent.Name);

            House.Random = new MockRandomWithValueList(new int[] { 0, 1 });
            opponent.Hide();
            var room1 = House.GetLocationByName("厨房") as LocationWithHidingPlace;
            CollectionAssert.AreEqual(new[] { opponent }, room1.CheckHidingPlace().ToList());


            var opponent2 = new Opponent("opponent2");
            Assert.AreEqual("opponent2", opponent2.Name);

            House.Random = new MockRandomWithValueList(new int[] { 0, 1, 2, 3, 4 });
            opponent2.Hide();
            var room2 = House.GetLocationByName("二楼浴室") as LocationWithHidingPlace;
            CollectionAssert.AreEqual(new[] { opponent2 }, room2.CheckHidingPlace().ToList());
        }
    }
}
