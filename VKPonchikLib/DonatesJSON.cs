using System;
using System.Collections.Generic;

using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VKPonchikLib.NULL.Request
{
    /// <summary>
    /// Основной класс. Используется для "пустых" запросов с указанием авторризационных данных. Рекомендуется только для тестирования!
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// VK ID группы.
        /// </summary>
        [JsonProperty("group")]
        public int Group { get; set; }

        /// <summary>
        /// Секретный токен.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Номер версии api.
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKPonchikLib.Converters.Converter.Settings);
    }
}
namespace VKPonchikLib.NULL.Response
{
    /// <summary>
    /// Основной класс
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// Успешность обработки запроса сервером. Пустой ответ со всеми стандартными полями. Может быть использован для получения ответа в запросах без ожидания ответа с данными.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Если запрос выполнен с ошибкой, то будет возвращен код ошибки. Используйте GetErrorCodeInfo(IntErrorCode) для получения информации об ошибке или посмотрите список ошибок: https://vkdonuts.ru/api#error-table
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public int Error { get; set; }

        /// <summary>
        /// Текстовое сообщение с результатом выполнения запроса.
        /// </summary>
        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKPonchikLib.Converters.Converter.Settings);
    }
}

namespace VKPonchikLib.Donates.Get.Request
{
    /// <summary>
    /// Основной класс
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// VK ID группы.
        /// </summary>
        [JsonProperty("group")]
        public int Group { get; set; }

        /// <summary>
        /// Секретный токен.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Номер версии api.
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }

        /// <summary>
        /// Количество донатов в списке.
        /// Максимум 100. По умолчанию 20.
        /// </summary>
        [JsonProperty("len", NullValueHandling = NullValueHandling.Ignore)]
        public int Len { get; set; }

        /// <summary>
        /// Смещение по выборе донатов
        /// </summary>
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public int Offset { get; set; }

        /// <summary>
        /// Временная метка по UNIX (в миллисекундах). Задает минимальную дату и время выбираемых донатов.
        /// </summary>
        [JsonProperty("start_date", NullValueHandling = NullValueHandling.Ignore)]
        public long StartDate { get; set; }

        /// <summary>
        /// Временная метка по UNIX (в миллисекундах). Задает максимальную дату и время выбираемых донатов.
        /// </summary>
        [JsonProperty("end_date", NullValueHandling = NullValueHandling.Ignore)]
        public long EndDate { get; set; }

        /// <summary>
        /// Метод сортировки. По умолчанию date. 
        /// Возможные значения: 
        /// date - сортировка по дате; 
        /// amount - сортировка по сумме.
        /// </summary>
        [JsonProperty("sort", NullValueHandling = NullValueHandling.Ignore)]
        public string Sort { get; set; }

        /// <summary>
        /// Направление сортировки. По умолчанию false. 
        /// Возможные значения: 
        /// false - сортировка по убыванию; 
        /// true - сортировка по возрастанию.
        /// </summary>
        [JsonProperty("reverse", NullValueHandling = NullValueHandling.Ignore)]
        public bool Reverse { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKPonchikLib.Converters.Converter.Settings);
    }
}
namespace VKPonchikLib.Donates.Get.Response
{
    /// <summary>
    /// Основной класс
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// Успешность обработки запроса сервером.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Если запрос выполнен с ошибкой, то будет возвращен код ошибки. Используйте GetErrorCodeInfo(IntErrorCode) для получения информации об ошибке или посмотрите список ошибок: https://vkdonuts.ru/api#error-table
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public int Error { get; set; }

        /// <summary>
        /// Текстовое сообщение с результатом выполнения запроса.
        /// </summary>
        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }

        /// <summary>
        /// Список донатов.
        /// </summary>
        [JsonProperty("list", NullValueHandling = NullValueHandling.Ignore)]
        public List<VKPonchikLib.DonateAnswer.Donate> List { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKPonchikLib.Converters.Converter.Settings);
    }
}

namespace VKPonchikLib.Donates.ChangeStatus.Request
{
    /// <summary>
    /// Основной класс. Для обработки ответа используйте NULL.Response.JSON
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// VK ID группы.
        /// </summary>
        [JsonProperty("group")]
        public int Group { get; set; }

        /// <summary>
        /// Секретный токен.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Номер версии api.
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }

        /// <summary>
        /// ID доната в системе.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Статус доната. Возможные значения: public - опубликован; hidden - скрыт.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKPonchikLib.Converters.Converter.Settings);
    }
}
// Default response JSON

namespace VKPonchikLib.Donates.Answer.Request
{
    /// <summary>
    /// Основной класс. Для обработки ответа используйте NULL.Response.JSON
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// VK ID группы.
        /// </summary>
        [JsonProperty("group")]
        public int Group { get; set; }

        /// <summary>
        /// Секретный токен.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Номер версии api.
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }

        /// <summary>
        /// ID доната в системе.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Текст ответа. Для удаления ответа следует передать пустую строку.
        /// </summary>
        [JsonProperty("answer")]
        public string Answer { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKPonchikLib.Converters.Converter.Settings);
    }
}
// Default response JSON

namespace VKPonchikLib.Donates.ChangeRewardStatus.Request
{
    /// <summary>
    /// Основной класс. Для обработки ответа используйте NULL.Response.JSON
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// VK ID группы.
        /// </summary>
        [JsonProperty("group")]
        public int Group { get; set; }

        /// <summary>
        /// Секретный токен.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Номер версии api.
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }

        /// <summary>
        /// ID доната в системе.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Статус выдачи вознаграждения. not_sended - не вадно; sended - выдано.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKPonchikLib.Converters.Converter.Settings);
    }
}
// Default response JSON