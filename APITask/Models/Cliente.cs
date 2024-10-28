using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APITask.Models
{
    public class Cliente
    {
        [JsonPropertyName("customerId")]
        public string CustomerId {  get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phoneMobile")]
        public string PhoneMobile { get; set; }

        [JsonPropertyName("birthday")]
        public DateTime Birthday { get; set; }

        [JsonPropertyName("addresses")]
        public List<Direccion> Addresses { get; set; }
    }

    public class Direccion
    {
        [JsonPropertyName("address1")]
        public string Address1 { get; set; }

        [JsonPropertyName("address2")]
        public string? Address2 { get; set; }

        [JsonPropertyName("addressId")]
        public string AddressId { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("preferred")]
        public bool Preferred { get; set; }
    }
}
