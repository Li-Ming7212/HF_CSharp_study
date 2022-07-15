using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponDamage {
    internal class ArrowDamage : WeaponDamage {

        const decimal BASE_MUTIPLIER = 0.35M;
        const decimal MAGIC_MUTIPLIER = 2.5M;
        const decimal FLAME_DAMAGE = 1.25M;

        public ArrowDamage(int startingRoll) : base(startingRoll) { }
        public override void CalculateDamage() {
            //伤害计算
            decimal bassDamage = Roll * BASE_MUTIPLIER;
            if (Magic) bassDamage *= MAGIC_MUTIPLIER;
            if (Flaming) Damage = (int)Math.Ceiling(bassDamage + FLAME_DAMAGE);
            else Damage = (int)Math.Ceiling(bassDamage);
        }


    }
}

