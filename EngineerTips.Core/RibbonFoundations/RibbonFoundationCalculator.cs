
using EngineerTips.Core.Soils;
using EngineerTips.Core.Soils.EstimatedSoilResistance;
using System.Linq;
using EngineerTips.Core.Soils.Ratios;

namespace EngineerTips.Core.RibbonFoundations
{
    public class RibbonFoundationCalculator
    {
        private double LDivideH;
        private SoilLayerParameters BaseLayer;

        public void Calculate()
        {
            var @params = new RibbonFoundationParameters
            {
                SoilPropertiesCalculationMethod = SoilPropertiesCalculationMethods.Testing,
                Width = 0.4,
                PaddingDepth = 2.7,
                BasementDepth = 2,
                BasementExists = true,
                SoilLayerHeightHigherFoundationBottom = 0.6,
                BasementFloorHeight = 0.1,
                BasementFloorAverageWeight = 24.5,
                ConstructionHeight = 10,
                ConstructionLength = 40,
                BuildingType = BuildingTypesSuValue.BuildingType.LargeBlockOrBrickWithoutSteel,
                Fv = 90
            };

            LDivideH = @params.ConstructionHeight / @params.ConstructionLength;

            var soilLayers = new SoilLayerParameters[9];
            #region testing Data
            soilLayers[0] = new SoilLayerParameters
            {
                SoilType = SoilTypes.BulkSoilLightSandyLoam,
                hLayer = 2.4,
                Rou = 1.5
            };
            soilLayers[1] = new SoilLayerParameters
            {
                SoilType = SoilTypes.LoamLightSandyLoam,
                hLayer = 2.6,
                Rou = 1.73,
                c = 21,
                E = 9,
                Fai = 19
            };
            soilLayers[2] = new SoilLayerParameters
            {
                SoilType = SoilTypes.LoamLightSandyLoam,
                hLayer = 4.2,
                Rou = 1.82,
                c = 10,
                E = 5.5,
                Fai = 23
            };
            soilLayers[3] = new SoilLayerParameters
            {
                SoilType = SoilTypes.LoamLightSandyLoam,
                hLayer = 4.1,
                Rou = 1.84,
                Sr = 0.84,
                Il = 0.12,
                c = 22,
                E = 12,
                Fai = 17
            };
            soilLayers[4] = new SoilLayerParameters
            {
                SoilType = SoilTypes.LoamLightSandyLoam,
                hLayer = 5.1,
                Rou = 1.89,
                c = 12,
                E = 8.5,
                Fai = 24
            };
            soilLayers[5] = new SoilLayerParameters
            {
                SoilType = SoilTypes.ClayLightSandyLoam,
                hLayer = 1.6,
                Rou = 1.85,
                c = 26,
                E = 15,
                Fai = 16
            };
            soilLayers[6] = new SoilLayerParameters
            {
                SoilType = SoilTypes.LoamLightSandyLoam
            };
            soilLayers[7] = new SoilLayerParameters
            {
                SoilType = SoilTypes.LoamLightSandyLoam
            };
            soilLayers[8] = new SoilLayerParameters
            {
                SoilType = SoilTypes.LoamLightSandyLoam
            };
            #endregion

            soilLayers.CalculateBasicHeights(@params.PaddingDepth);

            var Hc = 6 * @params.Width;
            for (var i = 0; i < soilLayers.Length; i++)
            {
                var layer = soilLayers[i];

                layer.CalculateBasicLayerRatios();

                layer.CalculateGammas(LDivideH);

                layer.CalculateZi(Hc);

                layer.CalculateEiHiZi(Hc);
            }
            var Ec = soilLayers.Sum(x => x.EiHiZi) / 0.5 / Hc / Hc;

            var calculator = new EstimatedSoilResistanceCalculator(@params, soilLayers);

            var resistanceParams = calculator.Calculate();

            //BaseLayer = soilLayers.FindBaseLayer();

            //var resistanceParams = new EstimatedSoilResistanceParameters();

            //resistanceParams.GammaC1 = BaseLayer.Gammac1;
            //resistanceParams.GammaC2 = BaseLayer.Gammac2;

            //resistanceParams.k = @params.SoilPropertiesCalculationMethod == SoilPropertiesCalculationMethods.Testing ? 1 : 1.1;

            //resistanceParams.My = BaseLayer.My;
            //resistanceParams.Mq = BaseLayer.Mq;
            //resistanceParams.Mc = BaseLayer.Mc;
            //resistanceParams.b = @params.Width;
            //resistanceParams.kz = resistanceParams.b < 10 ? 1 : 8 / resistanceParams.b + 0.2;
            //resistanceParams.Gamma11Above = soilLayers.Sum(x => x.hiAbove * x.Rou) / soilLayers.Sum(x => x.hiAbove) * 10;
            //resistanceParams.Gamma11Below = soilLayers.Sum(x => x.hiBelow * x.Rou) / soilLayers.Sum(x => x.hiBelow) * 10;
            //resistanceParams.d1 = @params.BasementExists
            //    ? @params.SoilLayerHeightHigherFoundationBottom + 
            //      @params.BasementFloorHeight * @params.BasementFloorAverageWeight / resistanceParams.Gamma11Below
            //    : @params.PaddingDepth;
            //resistanceParams.db = @params.BasementExists
            //    ? @params.BasementDepth > 2 ? 2 : @params.BasementDepth
            //    : default(double);
            //resistanceParams.c11 = BaseLayer.c;
            //resistanceParams.Fv = @params.Fv;
            //resistanceParams.Su = BuildingTypesSuValue.Instance.GetSuByBuildingType(@params.BuildingType);

            var results = new RibbonFoundationsResults();
            results.EstimatedSoilResistance = resistanceParams.R;
            results.AverageFoundationPressure = (resistanceParams.Fv +
                (@params.PaddingDepth - @params.BasementDepth) * @params.Width * 24.5) / resistanceParams.b;
            results.ResidemationByRosenfeld = 1.44 *
                (results.AverageFoundationPressure - resistanceParams.Gamma11Above * @params.PaddingDepth) *
                resistanceParams.b / (Ec * 1000) * 1000;
            // results.ResidemationByLayersMethod = 
        }
    }
}
