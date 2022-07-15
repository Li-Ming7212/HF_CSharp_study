using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponDamage {
    abstract class WeaponDamage {

        public WeaponDamage(int startingRoll) {
            roll = startingRoll;
            CalculateDamage();
        }

        public int Damage { get; protected set; }

        private int roll;
        public int Roll {
            get { return roll; }
            set {
                roll = value;
                CalculateDamage();
            }
        }

        private bool magic;
        public bool Magic {
            get { return magic; }
            set {
                magic = value;
                CalculateDamage();
            }
        }

        private bool flaming;
        public bool Flaming {
            get { return flaming; }
            set {
                flaming = value;
                CalculateDamage();
            }
        }

        public abstract void CalculateDamage();
    }
}
