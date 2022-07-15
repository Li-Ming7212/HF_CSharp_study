using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem {
    internal class Queen : Bee {

        public const float EGGS_PER_SHIFT = 0.45f;
        public const float HONEY_PER_UNASSIGNED_WORKER = 0.5f;

        private Bee[] workers = new Bee[0];
        private float eggs = 0;
        private float unassignedWorkers = 3;//未分配的蜜蜂

        public string StatusReport { get; private set; }
        public override float CostPerShift { get { return 2.15f; } }
        public Queen() : base("Queen") {
            AssignBee("花蜜收集者");
            AssignBee("蜂蜜制造者");
            AssignBee("蜂卵照顾者");
        }

        private void AddWorker(Bee worker) {
            if (unassignedWorkers >= 1) {
                unassignedWorkers--;
                Array.Resize(ref workers, workers.Length + 1);
                workers[workers.Length - 1] = worker;
            }
        }

        private void UpdateStatusReport() {//更新状态
            StatusReport = $"{HoneyVault.StatusReport}" +
            $"\n卵的数量: {eggs:0.0}\n未分配蜜蜂: {unassignedWorkers:0.0}" +
            $"\n{WorkerStatus("Nectar Collector")}\n{WorkerStatus("Honey Manufacturer")}" +
            $"\n{WorkerStatus("Egg Care")}\n总计: {workers.Length}";
        }

        public void AssignBee(string job) {
            switch (job) {
                case "花蜜收集者":
                    AddWorker(new NectarCollector());
                    break;
                case "蜂蜜制造者":
                    AddWorker(new HoneyManufacturer());
                    break;
                case "蜂卵照顾者":
                    AddWorker(new EggCare(this));
                    break;
            }
            UpdateStatusReport();
        }

        public void CareForEggs(float eggsToConvert) {
            if (eggs >= eggsToConvert) {
                eggs -= eggsToConvert;
                unassignedWorkers += eggsToConvert;
            }
        }
        private string WorkerStatus(string job) {
            int count = 0;
            foreach (Bee worker in workers)
                if (worker.Job == job) count++;

            string s = "s";
            if (count == 1) s = "";

            string 职业;
            if (job == "Nectar Collector") 职业 = "花蜜收集者";
            else if(job == "Honey Manufacturer") 职业 = "蜂蜜制造者";
            else 职业 = "蜂卵照顾者";

            return $"{count} {职业} bee{s}";
        }

        protected override void DoJob() {
            eggs += EGGS_PER_SHIFT;
            foreach (Bee worker in workers) {
                worker.WorkTheNextShift();
            }
            HoneyVault.ConsumeHoney(unassignedWorkers * HONEY_PER_UNASSIGNED_WORKER);
            UpdateStatusReport();
        }
    }
}
