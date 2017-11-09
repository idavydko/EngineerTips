
using System.Linq;
using EngineerTips.Core.RibbonFoundations;
using EngineerTips.Core.Soils.Ratios;

namespace EngineerTips.Core.Soils.Calculators.EstimatedSoilResistance
{
    public sealed class EstimatedSoilResistanceCalculator
    {
        private readonly RibbonFoundationParameters _params;
        private readonly SoilLayerParameters[] _soilLayers;

        public EstimatedSoilResistanceCalculator(RibbonFoundationParameters @params, SoilLayerParameters[] soilLayers)
        {
            _params = @params;
            _soilLayers = soilLayers;
        }

        public EstimatedSoilResistanceParameters Calculate()
        {
            var resistanceParams = new EstimatedSoilResistanceParameters();
            var BaseLayer = _soilLayers.FindBaseLayer();

            resistanceParams.GammaC1 = BaseLayer.Gammac1;
            resistanceParams.GammaC2 = BaseLayer.Gammac2;

            resistanceParams.k = _params.SoilPropertiesCalculationMethod == SoilPropertiesCalculationMethods.Testing ? 1 : 1.1;

            resistanceParams.My = BaseLayer.My;
            resistanceParams.Mq = BaseLayer.Mq;
            resistanceParams.Mc = BaseLayer.Mc;

            resistanceParams.b = _params.Width;
            resistanceParams.kz = resistanceParams.b < 10 ? 1 : 8 / resistanceParams.b + 0.2;

            resistanceParams.Gamma11Above = _soilLayers.Sum(x => x.hiAbove * x.Rou) / _soilLayers.Sum(x => x.hiAbove) * 10;
            resistanceParams.Gamma11Below = _soilLayers.Sum(x => x.hiBelow * x.Rou) / _soilLayers.Sum(x => x.hiBelow) * 10;

            resistanceParams.d1 = _params.BasementExists
                ? _params.SoilLayerHeightHigherFoundationBottom +
                  _params.BasementFloorHeight * _params.BasementFloorAverageWeight / resistanceParams.Gamma11Below
                : _params.PaddingDepth;

            resistanceParams.db = _params.BasementExists
                ? _params.BasementDepth > 2 ? 2 : _params.BasementDepth
                : default(double);

            resistanceParams.c11 = BaseLayer.c;
            resistanceParams.Fv = _params.Fv;
            resistanceParams.Su = BuildingTypesSuValue.Instance.GetSuByBuildingType(_params.BuildingType);
            
            return resistanceParams;
        }
    }
}
