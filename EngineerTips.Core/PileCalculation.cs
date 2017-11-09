using EngineerTips.Core.Soils;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineerTips.Core
{
    public class PileCalculation
    {
        int N = 300;
        int Fd = 420;

        public PileCalculation()
        {
            var method = PileImmersionMethods.Scoring;
            var soils = new Soil[5];

            soils[0] =new Soil
            {
                SoilType = SoilTypes.ClayLightSandyLoam,
                SoilProperties = new SoilParameters
                {
                    hLayout = 2.4,
                    hi = 1.9,
                    H = 1.9,
                    Rou = 1.6,
                    Il = 0.36,
                    Ip = 21,
                    Fai = 18
                }
            };
            soils[1] =new Soil
            {
                SoilType = SoilTypes.LoamLightSandyLoam,
                SoilProperties = new SoilParameters
                {
                    hLayout = 3.4,
                    hi = 3.4,
                    H = 5.3,
                    Rou = 1.9,
                    Il = 0.3,
                    Fai = 30
                }
            };
            soils[2] =new Soil
            {
                SoilType = SoilTypes.LoamLightDust,
                SoilProperties = new SoilParameters
                {
                    hLayout = 2.8,
                    hi = 2.8,
                    H = 8.1,
                    Rou = 1.9,
                    Il = 0.64,
                    Ip = 9,
                    Fai = 24
                }
            };
            soils[3] =new Soil
            {
                SoilType = SoilTypes.LoamHeavyDust,
                SoilProperties = new SoilParameters
                {
                    hLayout = 5.9,
                    hi = 5.9,
                    H = 14,
                    Rou = 1.86,
                    Il = 0.38,
                    Ip = 15,
                    Fai = 22
                }
            };
            soils[4] =new Soil
            {
                SoilType = SoilTypes.LoamLightDust,
                SoilProperties = new SoilParameters()
            };

            //soils.CalculateHeight();
            soils.CalculateGammaR(method);
        }

        public void Calculate()
        {

        }
    }
}
