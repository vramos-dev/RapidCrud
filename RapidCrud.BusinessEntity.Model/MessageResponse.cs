using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidCrud.BusinessEntity.Model
{
    public class MessageResponse
    {
        /// <summary>
        /// Message type. e.g: S = operation successful, E = Error customer information.
        /// </summary>
        [Required]
        [JsonProperty(Order = 0)]
        public string Type { get; set; }

        /// <summary>
        /// Message description
        /// </summary>
        [Required]
        [JsonProperty(Order = 1)]
        public string Message { get; set; }
    }
}
