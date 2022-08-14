using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeek {
    public class LocationWithHidingPlace : Location {
        public readonly string HidingPlace;

        public List<Opponent> hiddenOpponents = new List<Opponent>();

        public LocationWithHidingPlace( string name, string hidingPlace ) : base(name) => HidingPlace = hidingPlace;

        public void Hide( Opponent opponent ) {
            hiddenOpponents.Add(opponent);
        }

        public IEnumerable<Opponent> CheckHidingPlace() {
            var foundOpponents = new List<Opponent>(hiddenOpponents);
            hiddenOpponents.Clear();
            return foundOpponents;
        }
    }
}
