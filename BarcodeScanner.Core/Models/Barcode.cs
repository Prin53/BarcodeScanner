using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BarcodeScanner.Core.Models
{
    public class Barcode
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public string Data { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }
    }
}

