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

#region Donates
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
#endregion

#region Campaigns

namespace VKPonchikLib.Campaigns
{
    /// <summary>
    /// Класс кампании для обработки JSON массива
    /// </summary>
    public class Campaign
    {
        /// <summary>
        /// Основной класс массива
        /// </summary>
        public partial class JSON
        {
            /// <summary>
            /// ID кампании в системе.
            /// </summary>
            [JsonProperty("id")]
            public int Id { get; set; }

            /// <summary>
            /// Заголовок кампании.
            /// </summary>
            [JsonProperty("title")]
            public string Title { get; set; }

            /// <summary>
            /// Статус кампании. draft - черновик; active - активная кампания; archive - кампания архивирована.
            /// </summary>
            [JsonProperty("status")]
            public string Status { get; set; }

            /// <summary>
            /// Временная метка по unix (в миллисекундах) начала кампании.
            /// </summary>
            [JsonProperty("start")]
            public int Start { get; set; }

            /// <summary>
            /// Временная метка по unix (в миллисекундах) окончания кампании.
            /// </summary>
            [JsonProperty("end")]
            public int End { get; set; }

            /// <summary>
            /// Цель по сбору в рублях.
            /// </summary>
            [JsonProperty("point")]
            public int Point { get; set; }

            /// <summary>
            /// Собрано за пределами приложения в рублях.
            /// </summary>
            [JsonProperty("start_received")]
            public int StartReceived { get; set; }

            /// <summary>
            /// Кол-во спонсоров пожертвовавших за пределами приложения.
            /// </summary>
            [JsonProperty("start_backers")]
            public int StartBackers { get; set; }

            /// <summary>
            /// Собрано на данный момент в рублях.
            /// </summary>
            [JsonProperty("received")]
            public int Received { get; set; }

            /// <summary>
            /// Кол-во спонсоров.
            /// </summary>
            [JsonProperty("backers")]
            public int Backers { get; set; }
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
    /// <summary>
    /// Класс вознаграждения для обработки JSON массива
    /// </summary>
    public class Reward
    {
        /// <summary>
        /// Основной класс массива
        /// </summary>
        public partial class JSON
        {
            /// <summary>
            /// ID вознаграждения в системе.
            /// </summary>
            [JsonProperty("id")]
            public int Id { get; set; }

            /// <summary>
            /// Название вознаграждения.
            /// </summary>
            [JsonProperty("title")]
            public string Title { get; set; }

            /// <summary>
            /// Описание вознаграждения.
            /// </summary>
            [JsonProperty("desc")]
            public string Desc { get; set; }

            /// <summary>
            /// Минимальный донат для получения текущего вознаграждения.
            /// </summary>
            [JsonProperty("min_donate")]
            public int MinDonate { get; set; }

            /// <summary>
            /// Ограничение кол-во вознаграждений. Если ограничений нет, данное поле не передается.
            /// </summary>
            [JsonProperty("limits")]
            public int Limits { get; set; }

            /// <summary>
            /// Статус вознаграждения. public - вознаграждение опубликовано; hidden - вознаграждение скрыто.
            /// </summary>
            [JsonProperty("status")]
            public string Status { get; set; }

            /// <summary>
            /// Кол-во спонсоров выбравших данное вознаграждение.
            /// </summary>
            [JsonProperty("backers")]
            public int Backers { get; set; }
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
}

namespace VKPonchikLib.Campaigns.Get.Request
{
    /// <summary>
    /// Основной класс.
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
        /// Можно передать массив системных ID кампаний для выборки конкрентных кампаний. Если данный параметр не передан, то вернутся 20 последних кампаний.
        /// </summary>
        [JsonProperty("ids", NullValueHandling = NullValueHandling.Ignore)]
        public int[] IDs { get; set; }
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
namespace VKPonchikLib.Campaigns.Get.Response
{
    /// <summary>
    /// Основной класс.
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
        /// Список кампаний.
        /// </summary>
        [JsonProperty("list", NullValueHandling = NullValueHandling.Ignore)]
        public List<VKPonchikLib.Campaigns.Campaign.JSON> List { get; set; }
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

// Default request JSON
namespace VKPonchikLib.Campaigns.GetActive.Response
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

        /// <summary>
        /// Объект кампании.
        /// </summary>
        [JsonProperty("campaign", NullValueHandling = NullValueHandling.Ignore)]
        public VKPonchikLib.Campaigns.Campaign.JSON Campaign { get; set; }
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

namespace VKPonchikLib.Campaigns.GetRewards.Request
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

        /// <summary>
        /// ID кампании в системе.
        /// </summary>
        [JsonProperty("campaign")]
        public int Campaign { get; set; }
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
namespace VKPonchikLib.Campaigns.GetRewards.Response
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

        /// <summary>
        /// Массив объектов вознаграждений.
        /// </summary>
        [JsonProperty("list", NullValueHandling = NullValueHandling.Ignore)]
        public List<VKPonchikLib.Campaigns.Reward> List { get; set; }
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

namespace VKPonchikLib.Campaigns.Change.Request
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

        /// <summary>
        /// ID кампании в системе.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Заголовок кампании.
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        /// Статус кампании. draft - черновик; active - активная кампания; archive - кампания архивирована.
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        /// <summary>
        /// Временная метка по unix (в миллисекундах) окончания кампании.
        /// </summary>
        [JsonProperty("end", NullValueHandling = NullValueHandling.Ignore)]
        public long End { get; set; }

        /// <summary>
        /// Цель по сбору в рублях.
        /// </summary>
        [JsonProperty("point", NullValueHandling = NullValueHandling.Ignore)]
        public int Point { get; set; }

        /// <summary>
        /// Собрано за пределами приложения в рублях.
        /// </summary>
        [JsonProperty("start_received", NullValueHandling = NullValueHandling.Ignore)]
        public int StartReceived { get; set; }

        /// <summary>
        /// Кол-во спонсоров пожертвовавших за пределами приложения.
        /// </summary>
        [JsonProperty("start_backers", NullValueHandling = NullValueHandling.Ignore)]
        public int StartBackers { get; set; }
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

namespace VKPonchikLib.Campaigns.ChangeReward.Request
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

        /// <summary>
        /// ID вознаграждения в системе.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Название вознаграждения.
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        /// Описание вознаграждения.
        /// </summary>
        [JsonProperty("desc", NullValueHandling = NullValueHandling.Ignore)]
        public string Desc { get; set; }

        /// <summary>
        /// Минимальный донат для получения текущего вознаграждения.
        /// </summary>
        [JsonProperty("min_donate", NullValueHandling = NullValueHandling.Ignore)]
        public int MinDonate { get; set; }

        /// <summary>
        /// Ограничение кол-во вознаграждений. Если ограничений нет, данное поле не передается.
        /// </summary>
        [JsonProperty("limits", NullValueHandling = NullValueHandling.Ignore)]
        public int Limits { get; set; }

        /// <summary>
        /// Статус вознаграждения. public - вознаграждение опубликовано; hidden - вознаграждение скрыто.
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
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

#endregion