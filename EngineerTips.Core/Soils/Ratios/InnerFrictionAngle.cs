using System.Collections.Generic;

namespace EngineerTips.Core.Soils.Ratios
{
    // Кут внутр. тертя φ11
    sealed class InnerFrictionAngle
    {
        public sealed class Ratios
        {
            public Ratios(double my, double mq, double mc)
            {
                My = my;
                Mq = mq;
                Mc = mc;
            }
            public double My { get; }     // My
            public double Mq { get; }     // Mq
            public double Mc { get; }     // Mc
        }

        private readonly IDictionary<int, Ratios> _angles;
        private static readonly object _locker = new object();
        private static volatile InnerFrictionAngle _instance;

        public static InnerFrictionAngle Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                            _instance = new InnerFrictionAngle();
                    }
                }

                return _instance;
            }
        }

        public Ratios GetRatios(int angle)
        {
            Ratios ratios;
            return _angles.TryGetValue(angle, out ratios) ? ratios : null;
        }

        private InnerFrictionAngle()
        {
            _angles = new Dictionary<int, Ratios>();
            _angles.Add(0, new Ratios(0, 1, 3.14));
            _angles.Add(1, new Ratios(0.01, 1.06, 3.23));
            _angles.Add(2, new Ratios(0.03, 1.12, 3.32));
            _angles.Add(3, new Ratios(0.04, 1.18, 3.41));
            _angles.Add(4, new Ratios(0.06, 1.25, 3.51));
            _angles.Add(5, new Ratios(0.08, 1.32, 3.61));
            _angles.Add(6, new Ratios(0.1, 1.39, 3.71));
            _angles.Add(7, new Ratios(0.12, 1.47, 3.82));
            _angles.Add(8, new Ratios(0.14, 1.55, 3.93));
            _angles.Add(9, new Ratios(0.16, 1.64, 4.05));
            _angles.Add(10, new Ratios(0.18, 1.73, 4.17));
            _angles.Add(11, new Ratios(0.21, 1.83, 4.29));
            _angles.Add(12, new Ratios(0.23, 1.94, 4.42));
            _angles.Add(13, new Ratios(0.26, 2.05, 4.55));
            _angles.Add(14, new Ratios(0.29, 2.17, 4.69));
            _angles.Add(15, new Ratios(0.32, 2.30, 4.84));
            _angles.Add(16, new Ratios(0.36, 2.43, 4.99));
            _angles.Add(17, new Ratios(0.39, 2.57, 5.15));
            _angles.Add(18, new Ratios(0.43, 2.73, 5.31));
            _angles.Add(19, new Ratios(0.47, 2.89, 5.48));
            _angles.Add(20, new Ratios(0.51, 3.06, 5.66));
            _angles.Add(21, new Ratios(0.56, 3.24, 5.84));
            _angles.Add(22, new Ratios(0.61, 3.44, 6.04));
            _angles.Add(23, new Ratios(0.66, 3.65, 6.24));
            _angles.Add(24, new Ratios(0.72, 3.87, 6.45));
            _angles.Add(25, new Ratios(0.78, 4.11, 6.67));
            _angles.Add(26, new Ratios(0.84, 4.37, 6.90));
            _angles.Add(27, new Ratios(0.91, 4.64, 7.14));
            _angles.Add(28, new Ratios(0.98, 4.93, 7.40));
            _angles.Add(29, new Ratios(1.06, 5.25, 7.67));

            // TODO: Finish the list
        }
    }
}
