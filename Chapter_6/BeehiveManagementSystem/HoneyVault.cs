using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem {
    internal static class HoneyVault {//蜂蜜库

        const float NECTAR_CONVERSION_RATIO = .19f;//花蜜转化率
        const float LOW_LEVEL_WARNING = 10f;//蜂蜜警告值

        static float honey = 25f;//蜂蜜
        static float nectar = 100f;//花蜜

        public static string StatusReport {
            get {
                string msg = $"蜂蜜数量为{honey:0.0}\n花蜜数量为{nectar:0.0}";
                if (honey < LOW_LEVEL_WARNING) { msg = msg + "\n警告，蜂蜜过低"; }
                if (nectar < LOW_LEVEL_WARNING) { msg = msg + "\n警告，花蜜过低"; }
                return msg;
            }
        }

        public static void CollectNector(float amount) {
            if (amount > 0) {
                nectar += amount;
            }
        }

        public static void ConvertNectorToHoney(float amount) {

            float nectorToHoney = amount;
            if (nectorToHoney > nectar) nectorToHoney = nectar;

            nectar -= nectorToHoney;
            honey += nectorToHoney * NECTAR_CONVERSION_RATIO;
        }

        public static bool ConsumeHoney(float amount) {
            if (amount <= honey) {
                honey -= amount;
                return true;
            }
            return false;
        }
    }
}
