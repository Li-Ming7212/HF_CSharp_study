namespace HideAndSeekTests {

    //using System.Collections.Generic;
    //using System.Linq;
    [TestClass]
    public class LocationTests {

        private Location center;

        /// <summary>
        /// ͨ�� �� ����ǰ���á������µ�����λ�ò� �� ÿ����������ӷ�������ʼ��ÿ����Ԫ����
        /// </summary>
        [TestInitialize]
        public void Initialize() {
            // You'll use this to create a bunch of locations before each test
            center = new Location("center room");
            Assert.AreSame("center room", center.Name);
            Assert.AreEqual(0, center.ExitList.Count());

            center.AddExit(Direction.��, new Location("North Room"));
            center.AddExit(Direction.����, new Location("Northeast Room"));
            center.AddExit(Direction.��, new Location("East Room"));
            center.AddExit(Direction.����, new Location("Southeast Room"));
            center.AddExit(Direction.��, new Location("South Room"));
            center.AddExit(Direction.����, new Location("Southwest Room"));
            center.AddExit(Direction.��, new Location("West Room"));
            center.AddExit(Direction.����, new Location("Northwest Room"));
            center.AddExit(Direction.��, new Location("Upper Room"));
            center.AddExit(Direction.��, new Location("Lower Room"));
            center.AddExit(Direction.��, new Location("Inside Room"));
            center.AddExit(Direction.��, new Location("Outside Room"));

            Assert.AreEqual(12, center.ExitList.Count());
        }


        /// <summary>
        /// ȷ��GetExit�� �� ĳ�������Ϸ��ظ�λ�ã������λ�ô� �� ��
        /// </summary>
        [TestMethod]
        public void TestGetExit() {
            var upRoom = center.GetExit(Direction.��);

            Assert.AreEqual("Upper Room", upRoom.Name);
            Assert.AreSame(center, upRoom.GetExit(Direction.��));
            Assert.AreSame(upRoom, upRoom.GetExit(Direction.��));
        }


        /// <summary>
        /// ��֤�˳��б��Ƿ���������
        /// </summary>
        [TestMethod]
        public void TestExitList() {
            Assert.AreEqual("North Room �� ������", center.ExitList.ToList()[0]);
            Assert.AreEqual("Northeast Room �� ��������", center.ExitList.ToList()[4]);
            Assert.AreEqual("Southwest Room �� ���Ϸ���", center.ExitList.ToList()[5]);
            Assert.AreEqual("Outside Room �� ����", center.ExitList.ToList()[11]);
            CollectionAssert.AreEqual(
               new List<string>() {
                   "North Room �� ������",
                   "South Room �� �Ϸ���",
                   "East Room �� ������",
                   "West Room �� ������",
                   "Northeast Room �� ��������",
                   "Southwest Room �� ���Ϸ���",
                   "Southeast Room �� ���Ϸ���",
                   "Northwest Room �� ��������",
                   "Upper Room �� ����",
                   "Lower Room �� ����",
                   "Inside Room �� ����",
                   "Outside Room �� ����",
               },
               center.ExitList.ToList());
        }


        /// <summary>
        /// ��֤ÿ����������ƺͷ��س����Ƿ���ȷ����
        /// </summary>
        [TestMethod]
        public void TestReturnExits() {
            var n = center.GetExit(Direction.��);
            Assert.AreEqual("North Room", n.ToString());
            Assert.AreSame(center, n.GetExit(Direction.��));
            Assert.AreEqual(1, n.ExitList.Count());

            var ne = center.GetExit(Direction.����);
            Assert.AreEqual("Northeast Room", ne.ToString());
            Assert.AreSame(center, ne.GetExit(Direction.����));

            var sw = center.GetExit(Direction.����);
            Assert.AreEqual("Southwest Room", sw.ToString());
            Assert.AreSame(center, sw.GetExit(Direction.����));

            var d = center.GetExit(Direction.��);
            Assert.AreEqual("Lower Room", d.ToString());
            Assert.AreSame(center, d.GetExit(Direction.��));

            var inside = center.GetExit(Direction.��);
            Assert.AreEqual("Inside Room", inside.ToString());
            Assert.AreSame(center, inside.GetExit(Direction.��));
        }



        /// <summary>
        /// ��������ӵ�����һ�����䣬��ȷ����ȷ�����˴������������ƺͷ��س���
        /// </summary>
        [TestMethod]
        public void TestAddHall() {
            var e = center.GetExit(Direction.��);
            Assert.AreEqual(1, e.ExitList.Count());

            var eastHall1 = new Location("eastHall_1");
            var eastHall2 = new Location("eastHall_2");

            e.AddExit(Direction.��, eastHall1);
            eastHall1.AddExit(Direction.��, eastHall2);

            Assert.AreEqual(2, e.ExitList.Count());
            Assert.AreEqual(2, eastHall1.ExitList.Count());
            Assert.AreEqual(1, eastHall2.ExitList.Count());
        }
    }
}