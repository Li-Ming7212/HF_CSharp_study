using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SwordDamage {
    class SwordDamage_WPF {
        public const int BASE_DAMAGE = 3;
        public const int FLAME_DAMAGE = 2;

        public int Damage { get; private set; }

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

        
        int flamingDamage = 0;

        public void CalculateDamage() {
            decimal magicMultiplier = 1M;
            if (Magic) magicMultiplier = 1.75M;

            //Damage = BASE_DAMAGE;
            Damage = (int)(Roll * magicMultiplier) + BASE_DAMAGE;
            if (Flaming) Damage += FLAME_DAMAGE;
            //Debug.WriteLine($"CalculateDamage finished: {Damage} (roll: {Roll})");
        }



        public SwordDamage_WPF(int startingRoll) {
            roll = startingRoll;
            CalculateDamage();
        }
    }
}
