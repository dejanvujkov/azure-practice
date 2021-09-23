using System;
using Newtonsoft.Json;

namespace azure_cosmos_db
{
    public class Person
    {
        public Person(Guid id, string firstName, string lastName, Adress adress)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Adress = adress;
        }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("adress")]
        public Adress Adress { get; set; }
    }
}