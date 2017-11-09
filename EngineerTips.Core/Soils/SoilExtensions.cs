
using EngineerTips.Core.Soils.Ratios;
using System;
using System.Linq;

namespace EngineerTips.Core.Soils
{
    static class SoilExtensions
    {
        public static void CalculateBasicHeights(this SoilLayerParameters[] layers, double paddingDepth)
        {
            for (var i = 0; i < layers.Length; i++)
            {
                var layer = layers[i];

                layer.HFromLevel = i == 0
                    ? layer.hLayer
                    : layers[i - 1].HFromLevel + layer.hLayer;

                layer.CalculateHFromBottom(paddingDepth);

                layer.hiBelow = i == 0
                    ? layer.HFromBottom
                    : layers[i - 1].HFromBottom - layer.HFromBottom;

                layer.hiAbove = layer.hLayer - layer.hiBelow;

            }
        }

        private static void CalculateHFromBottom(this SoilLayerParameters layer, double paddingDepth)
        {
            var value = layer.HFromLevel - paddingDepth;

            layer.HFromBottom = value >= 0 ? value : 0;
        }

        public static SoilLayerParameters FindBaseLayer(this SoilLayerParameters[] layers)
        {
            for (var i = 0; i < layers.Length; i++)
            {
                if (layers[i].HFromBottom > 0)
                    return layers[i];
            }

            throw new ArgumentNullException();
        }

        public static void CalculateBasicLayerRatios(this SoilLayerParameters layer)
        {
            var ratios = InnerFrictionAngle.Instance.GetRatios(layer.Fai);

            if (ratios != null)
            {
                layer.My = ratios.My;
                layer.Mq = ratios.Mq;
                layer.Mc = ratios.Mc;
            }
        }

        public static void CalculateGammas(this SoilLayerParameters layer, double LDivideH)
        {
            if (layer.SoilType == SoilTypes.SandGravel ||
                layer.SoilType == SoilTypes.SandBig ||
                layer.SoilType == SoilTypes.SandMiddleBig ||
                layer.SoilType == SoilTypes.SandSmall ||
                layer.SoilType == SoilTypes.SandDust)
            {
                if (layer.SoilType == SoilTypes.SandSmall)
                    layer.FindGammas(WorkingConditionRatios.Types.SandSmall, LDivideH);
                else if (layer.SoilType == SoilTypes.SandDust)
                    layer.FindGammas(layer.Sr > 0.8
                        ? WorkingConditionRatios.Types.SandDustMediumHighHumidity
                        : WorkingConditionRatios.Types.SandDustLowHumidity, LDivideH);
                else
                    layer.FindGammas(WorkingConditionRatios.Types.HighGradeSand, LDivideH);
            }
            else if (layer.Il > 0.5)
                layer.FindGammas(WorkingConditionRatios.Types.ClayFlowIndexHigh, LDivideH);
            else if (layer.Il > 0.25)
                layer.FindGammas(WorkingConditionRatios.Types.ClayFlowIndexMedium, LDivideH);
            else
                layer.FindGammas(WorkingConditionRatios.Types.ClayFlowIndexLow, LDivideH);
        }

        public static void FindGammas(this SoilLayerParameters layer, WorkingConditionRatios.Types type, double LDivideH)
        {
            layer.Gammac1 = WorkingConditionRatios.Instance.GetGammaC1(type);

            if (LDivideH > 4)
                layer.Gammac2 = WorkingConditionRatios.Instance.GetGammaC1LengthBigger(type);
            else if(LDivideH < 1.5)
                layer.Gammac2 = WorkingConditionRatios.Instance.GetGammaC1LengthHeightEquals(type);
            else
            {
                var gammac2OneStar = WorkingConditionRatios.Instance.GetGammaC1LengthBigger(type);
                var gammac2TwoStar = WorkingConditionRatios.Instance.GetGammaC1LengthHeightEquals(type);
                layer.Gammac2 = gammac2OneStar + (gammac2TwoStar - gammac2OneStar) / 2.5 * (LDivideH - 1.5);
            }
        }

        public static void CalculateZi(this SoilLayerParameters layer, double Hc)
        {
            if (Hc < layer.HFromBottom)
                layer.Zi = Hc / 2;
            else if ((Hc - layer.HFromBottom / 2) < 0)
                layer.Zi = 0;
            else
                layer.Zi = Hc - layer.HFromBottom / 2;
        }

        public static void CalculateEiHiZi(this SoilLayerParameters layer, double Hc)
        {
            var hi = layer.hiBelow < Hc ? layer.hiBelow : Hc;
            layer.EiHiZi = layer.E * hi * layer.Zi;
        }

        public static double CalculateAverageHiAbove(this SoilLayerParameters[] layers)
        {
            return layers.Sum(x => x.hiAbove * x.Rou) / layers.Sum(x => x.hiAbove) * 10;
        }

        public static double CalculateAverageHiAbove(this SoilLayerParameters[] layers)
        {
            return layers.Sum(x => x.hiAbove * x.Rou) / layers.Sum(x => x.hiAbove) * 10;
        }

        //public static void CalculateHeight(this Soil[] soils)
        //{
        //    for (var i = 0; i < soils.Length; i++)
        //    {
        //        if (i == 0)
        //        {
        //            soils[i].SoilProperties.H = soils[i].SoilProperties.hi;
        //        }
        //        else if (soils[i].SoilProperties.hi != default(double))
        //        {
        //            var prevSoil = soils[i - 1];
        //            soils[i].SoilProperties.H = prevSoil.SoilProperties.H + soils[i].SoilProperties.hi;
        //        }
        //    }
        //}

        public static void CalculateGammaR(this Soil[] soils, PileImmersionMethods method)
        {
            for (var i = 0; i < soils.Length; i++)
            {
                if (method == PileImmersionMethods.Scoring)
                {
                    soils[i].SoilProperties.GammacR = 1;
                }
                else if (method == PileImmersionMethods.Injection)
                {
                    if (soils[i].SoilType == SoilTypes.SandGravel ||
                        soils[i].SoilType == SoilTypes.SandBig ||
                        soils[i].SoilType == SoilTypes.SandMiddleBig ||
                        soils[i].SoilType == SoilTypes.SandSmall ||
                        soils[i].SoilType == SoilTypes.SandDust ||
                        soils[i].SoilProperties.Il < 0.5)
                    {
                        soils[i].SoilProperties.GammacR = 1;
                    }
                    else
                    {
                        soils[i].SoilProperties.GammacR = 1.1;
                    }
                }
                else if (method == PileImmersionMethods.VibrationDip)
                {
                    if (soils[i].SoilType == SoilTypes.SandGravel ||
                        soils[i].SoilType == SoilTypes.SandBig ||
                        soils[i].SoilType == SoilTypes.SandMiddleBig ||
                        soils[i].SoilType == SoilTypes.SandSmall ||
                        soils[i].SoilType == SoilTypes.SandDust)
                    {
                        if (soils[i].SoilType == SoilTypes.SandDust)
                        {
                            soils[i].SoilProperties.GammacR = 1;
                        }
                        else if (soils[i].SoilType == SoilTypes.SandSmall)
                        {
                            soils[i].SoilProperties.GammacR = 1.1;
                        }
                        else
                            soils[i].SoilProperties.GammacR = 1.2;

                    }
                    else if (soils[i].SoilProperties.Il < 0.01)
                    {
                        soils[i].SoilProperties.GammacR = 1;
                    }
                    else if (soils[i].SoilType == SoilTypes.SandGravel ||
                             soils[i].SoilType == SoilTypes.SandBig)
                    {
                        soils[i].SoilProperties.GammacR = soils[i].SoilProperties.Il > 0.49
                            ? 0.9
                            : 0.9 + 0.2 * (0.5 - soils[i].SoilProperties.Il);
                    }
                    else if (soils[i].SoilType == SoilTypes.LoamLightDust ||
                            soils[i].SoilType == SoilTypes.LoamLightSandyLoam ||
                            soils[i].SoilType == SoilTypes.LoamHeavyDust ||
                            soils[i].SoilType == SoilTypes.LoamHeavySandyLoam)
                    {
                        soils[i].SoilProperties.GammacR = soils[i].SoilProperties.Il > 0.49
                            ? 0.9
                            : 0.8 + 0.4 * (0.5 - soils[i].SoilProperties.Il);
                    }
                    else if (soils[i].SoilType == SoilTypes.ClayLightDust ||
                            soils[i].SoilType == SoilTypes.ClayLightSandyLoam)
                    {
                        soils[i].SoilProperties.GammacR = soils[i].SoilProperties.Il > 0.49
                            ? 0.7
                            : 0.7 + 0.6 * (0.5 - soils[i].SoilProperties.Il);
                    }
                    else
                        soils[i].SoilProperties.GammacR = 0;
                }
                else
                    soils[i].SoilProperties.GammacR = 0;

            }
        }
    }
}
