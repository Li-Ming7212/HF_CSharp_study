using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeekTests {
    [TestClass]
    public class LocationWithHidingPlaceTests {
        [TestMethod]
        public void TestHiding() {
            // 构造函数设置名称和隐藏位置属性
            var hidingLocation = new LocationWithHidingPlace("房间", "床底下");
            Assert.AreEqual("房间", hidingLocation.Name);
            Assert.AreEqual("房间", hidingLocation.ToString());
            Assert.AreEqual("床底下", hidingLocation.HidingPlace);
            // 把两个对手藏在房间里，然后检查隐藏的地方
            var opponent1 = new Opponent("Opponent1");
            var opponent2 = new Opponent("Opponent2");
            hidingLocation.Hide(opponent1);
            hidingLocation.Hide(opponent2);
            CollectionAssert.AreEqual(new List<Opponent>() { opponent1, opponent2 },hidingLocation.CheckHidingPlace().ToList());
            // 现在藏身处现在应该是空的
            CollectionAssert.AreEqual(new List<Opponent>(),hidingLocation.CheckHidingPlace().ToList());
        }
    }
}
