
using System.Collections.Generic;

namespace EngineerTips.Core.Soils.Ratios
{
    // Коефіцієнти умов роботи
    sealed class WorkingConditionRatios
    {
        public enum Types
        {
            HighGradeSand,              // Великоуламкові з піщаним заповнювачем і піщані. крім дрібних і пилуватих
            SandSmall,                  // Піски дрібні
            SandDustLowHumidity,        // Піски пилуваті малого і середнього ступеня вологості
            SandDustMediumHighHumidity, // Піски пилуваті насичені водою
            ClayFlowIndexLow,           // Глинисті з показником текучості IL ≤0.25
            ClayFlowIndexMedium,        // Глинисті з показником текучості 0.25≤IL≤0.50
            ClayFlowIndexHigh           // Глинисті з показником текучості IL ≥0.50
        }

        public sealed class Gammas
        {
            public double GammaC1 { get; set; }
            public double GammaC1LengthBigger { get; set; }         // L/H > 4
            public double GammaC1LengthHeightEquals { get; set; }   // L/H < 1.5
        }

        private readonly IDictionary<Types, Gammas> _ratios;
        private static readonly object _locker = new object();
        private static volatile WorkingConditionRatios _instance;

        public static WorkingConditionRatios Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                            _instance = new WorkingConditionRatios();
                    }
                }

                return _instance;
            }
        }

        public double GetGammaC1(Types type)
        {
            Gammas value;
            return _ratios.TryGetValue(type, out value) ? value.GammaC1 : default(double);
        }

        public double GetGammaC1LengthBigger(Types type)
        {
            Gammas value;
            return _ratios.TryGetValue(type, out value) ? value.GammaC1LengthBigger : default(double);
        }

        public double GetGammaC1LengthHeightEquals(Types type)
        {
            Gammas value;
            return _ratios.TryGetValue(type, out value) ? value.GammaC1LengthHeightEquals : default(double);
        }

        private WorkingConditionRatios()
        {
            _ratios = new Dictionary<Types, Gammas>();
            // TODO: Fill the list
        }
    }
}
