using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherIO.ViewModels
{
    public class PortugalDistrictsViewModel : BaseViewModel
    {
        public List<Tuple<string,string>> Districts { get; } = new List<Tuple<string, string>>
        {
            Tuple.Create("Viana do Castelo", "PT"),
            Tuple.Create("Braga","PT"),
            Tuple.Create("Vila Real","PT"),
            Tuple.Create("Bragança","PT"),
            Tuple.Create("Porto","PT"),
            Tuple.Create("Aveiro","PT"),
            Tuple.Create("Viseu","PT"),
            Tuple.Create("Guarda","PT"),
            Tuple.Create("Coimbra","PT"),
            Tuple.Create("Castelo Branco","PT"),
            Tuple.Create("Leiria","PT"),
            Tuple.Create("Santarém","PT"),
            Tuple.Create("Portalegre","PT"),
            Tuple.Create("Setúbal","PT"),
            Tuple.Create("Évora","PT"),
            Tuple.Create("Beja","PT"),
        };
    }
}
