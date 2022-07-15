using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WeaponDamage {
    class SwordDamage : WeaponDamage {
        public const int BASE_DAMAGE = 3;//基础伤害
        public const int FLAME_DAMAGE = 2;//火焰附加伤害

        public SwordDamage(int startingRoll) : base(startingRoll) { }
        public override void CalculateDamage() {
            //伤害计算
            decimal magicMultiplier = 1M;
            if (Magic) magicMultiplier = 1.75M;

            Damage = (int)(Roll * magicMultiplier) + BASE_DAMAGE;
            if (Flaming) Damage += FLAME_DAMAGE;
        }


    }
}
