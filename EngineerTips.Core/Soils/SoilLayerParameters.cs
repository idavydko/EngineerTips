
namespace EngineerTips.Core.Soils
{
    // Engineering-geological soil characteristics
    // Інженерно-геологічні характеристики грунтa
    public class SoilLayerParameters 
    {
        public SoilTypes SoilType { get; set; }
        public double hLayer { get; set; }     // h слоя
        public double hiAbove { get; set; }     // hi`
        public double hiBelow { get; set; }     // hi
        public double HFromBottom { get; set; } // H*
        public double HFromLevel { get; set; }  // H
        public double Rou { get; set; }         // ρ
        public double RouS { get; set; }        // ρs
        public double RouD { get; set; }        // ρd
        public double W { get; set; }           // W
        public double Sr { get; set; }          // Sr
        public double e { get; set; }           // e
        public double kf { get; set; }          // kf
        public double Il { get; set; }          // Il
        public double Ip { get; set; }          // Ip, %
        public double c { get; set; }           // c
        public double E { get; set; }           // E
        public int Fai { get; set; }            // ϕ

        public double GammacR { get; set; }     // γ сR

        public double My { get; set; }          // My
        public double Mq { get; set; }          // Mq
        public double Mc { get; set; }          // Mc
        public double Gammac1 { get; set; }     // γc1
        public double Gammac2 { get; set; }     // γc2
        //public double Gammac2OneStar { get; set; }       // Gammac2*
        //public double Gammac2TwoStar { get; set; }       // Gammac2**
        //public double Gammac2ThreeStar { get; set; }     // Gammac2***
        public double EiHiZi { get; set; }      // Ei hi Zi
        public double Zi { get; set; }          // zi
    }
}
