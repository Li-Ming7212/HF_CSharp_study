using System;

namespace WeaponDamage {
    internal class Program {

        static Random random = new Random();//随机数
        static void Main(string[] args) {

            SwordDamage swordDamage = new SwordDamage(RollDice(3));
            ArrowDamage arrowDamage = new ArrowDamage(RollDice(1));

            while (true) {
                Console.Write("\nS 是剑（sword）, A 是弓（arrow）, 其他按键退出: ");
                char weaponKey = char.ToUpper(Console.ReadKey().KeyChar);
                Console.Write("\n0 为正常伤害, 1 附加魔法伤害, 2 附加燃烧伤害, 3 附加魔法燃烧伤害 , 其他按键退出: ");
                char key = Console.ReadKey().KeyChar;


                switch (weaponKey) {
                    case 'S':
                        //计算剑的伤害
                        swordDamage.Roll = RollDice(3);
                        swordDamage.Magic = (key == '1' || key == '3');
                        swordDamage.Flaming = (key == '2' || key == '3');
                        Console.WriteLine($"\n 投掷 {swordDamage.Roll} 点，造成 {swordDamage.Damage} 伤害\n");
                        break;
                    case 'A':
                        //计算弓的伤害
                        arrowDamage.Roll = RollDice(1);
                        arrowDamage.Magic = (key == '1' || key == '3');
                        arrowDamage.Flaming = (key == '2' || key == '3');
                        Console.WriteLine($"\n投掷 {arrowDamage.Roll} 点，造成 {arrowDamage.Damage} 伤害\n");
                        break;
                    default:
                        break;
                }
            }
        }

        private static int RollDice(int numberOfRolls) {
            //投掷骰子
            int total = 0;
            for (int i = 0; i < numberOfRolls; i++) {
                total += random.Next(1, 7);
            }
            return total;
        }
    }
}