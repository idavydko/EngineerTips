
namespace EngineerTips.Core.Soils.Calculators.EstimatedSoilResistance
{
    public class EstimatedSoilResistanceParameters
    {
        public double R { get; set; } // Розрахунковий опір грунту
        public double GammaC1 { get; set; } // Коефіцієнт умов роботи
        public double GammaC2 { get; set; } // Коефіцієнт умов роботи
        public double k { get; set; } // Коефіцієнт методу визначення міцнісних характеристик грунту
        public double My { get; set; } // Коефіцієнти. залежні від кута внутрішнього тертя
        public double Mq { get; set; } // Коефіцієнти. залежні від кута внутрішнього тертя
        public double Mc { get; set; } // Коефіцієнти. залежні від кута внутрішнього тертя
        public double kz { get; set; } // Коефіцієнт. залежний від ширини фундаменту
        public double b { get; set; } // Ширина фундаменту
        public double Gamma11Above { get; set; } // γ11 Усереднене значення ваги грунтів вище підошви фундаменту
        public double Gamma11Below { get; set; } // γ11' Усереднене значення ваги грунтів нижче підошви фундаменту
        public double d1 { get; set; } // Приведена глибина закладання фундаментів
        public double db { get; set; } // Глибина підвалу
        public double c11 { get; set; } // Розрахункове значення питомого зчеплення грунту. що залягає безпосередньо під підошвою фундаменту
        public double Fv { get; set; } // Навантаження на погонний метр стрічкового фундаменту та призначення будівлі
        public double Su { get; set; }
    }
}
