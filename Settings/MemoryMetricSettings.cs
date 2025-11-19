using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.Settings
{
    public class MemoryMetricSettings
    {
        public int CollectionIntervalSeconds { get; set; }
        public double WarningThresholdMB { get; set; }
        public double CriticalThresholdMB { get; set; }

    }
}
