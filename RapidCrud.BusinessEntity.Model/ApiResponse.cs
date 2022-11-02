using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidCrud.BusinessEntity.Model
{
    public class ApiResponse
    {
        /// <summary>
        /// Operation state.
        /// </summary>
        [JsonProperty(Order = 0)]
        [Required]
        public bool Success { get; set; }

        /// <summary>
        /// Operation messages.
        /// </summary>
        [JsonProperty(Order = 2)]
        [Required]
        public List<MessageResponse> Messages { get; set; }

        public ApiResponse()
        {
            Messages = new List<MessageResponse>();
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        /// <summary>
        /// Operation data response.
        /// </summary>
        [JsonProperty(Order = 1)]
        public T Data { get; set; }
    }
}
