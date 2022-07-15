﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem {
    abstract class Bee {//蜜蜂基类

        public abstract float CostPerShift { get; }
        public string Job { get; private set; }
        public Bee(string job) {
            Job = job;
        }

        public void WorkTheNextShift() {
            if (HoneyVault.ConsumeHoney(CostPerShift)) {
                DoJob();
            }
        }

        protected abstract void DoJob();
    }
}
