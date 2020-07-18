namespace VKPonchikLib.DonateAnswer
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Da
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("donate", NullValueHandling = NullValueHandling.Ignore)]
        public Donate Donate { get; set; }

        [JsonProperty("payment", NullValueHandling = NullValueHandling.Ignore)]
        public Payment Payment { get; set; }

        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public string Group { get; set; }

        [JsonProperty("hash", NullValueHandling = NullValueHandling.Ignore)]
        public string Hash { get; set; }
    }

    #region Donate in Da
    public partial class Donate
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }

        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        public long Date { get; set; }

        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public int Amount { get; set; }

        [JsonProperty("anonym", NullValueHandling = NullValueHandling.Ignore)]
        public bool Anonym { get; set; }

        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }

        [JsonProperty("answer", NullValueHandling = NullValueHandling.Ignore)]
        public string Answer { get; set; }

        [JsonProperty("vkpay", NullValueHandling = NullValueHandling.Ignore)]
        public bool Vkpay { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public int User { get; set; }

        [JsonProperty("reward", NullValueHandling = NullValueHandling.Ignore)]
        public Reward Reward { get; set; }
    }

    public partial class Reward
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public int Title { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public int Status { get; set; }
    }
    #endregion

    #region Payment in Da
    public partial class Payment
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("processed", NullValueHandling = NullValueHandling.Ignore)]
        public int Processed { get; set; }

        [JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
        public string System { get; set; }

        [JsonProperty("purse", NullValueHandling = NullValueHandling.Ignore)]
        public string Purse { get; set; }

        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public int Amount { get; set; }

        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public int User { get; set; }
    }
    #endregion

    public partial class Da
    {
        public static Da FromJson(string json) => JsonConvert.DeserializeObject<Da>(json, DonateAnswer.Converter.Settings);
    }
    public partial class Donate
    {
        public static Dictionary<string, DonateAnswer.Donate> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, DonateAnswer.Donate>>(json, DonateAnswer.Converter.Settings);
    }
    public partial class Payment
    {
        public static Dictionary<string, DonateAnswer.Payment> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, DonateAnswer.Payment>>(json, DonateAnswer.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(object self) => JsonConvert.SerializeObject(self, DonateAnswer.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    public static class CustomConverters
    {
        /// <summary>
        /// SHA256 Converter
        /// </summary>
        /// <param name="rawData">Enter string</param>
        /// <returns></returns>
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}