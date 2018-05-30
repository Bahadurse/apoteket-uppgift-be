using LinqToDB.Mapping;
using Newtonsoft.Json;

namespace Apoteket.UppgiftBE.Web.Models
{
    public class Product
    {

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Id { get; set; }

        [Column(Name = "Name"), NotNull]
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Price { get; set; }
    }
}
