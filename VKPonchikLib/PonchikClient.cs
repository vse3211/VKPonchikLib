﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace VKPonchikLib
{
    //Данная библиотека создана мной для использования API донат - сервиса "Пончик" в C# приложениях
    //С помощью написанной мной библиотеки вы сможете подключить API донат - сервиса к своему приложению и использовать его без особых усилий
    //Возможно библиотека будет обновлятся, но это зависит только от вашей поддержки
    //Ваш Londonist (StealthKiller#8719, https://vk.com/londonist)

    /// <summary>
    /// Основной класс библиотеки
    /// </summary>
    public class PonchikClient
    {
        #region Main Values
        /// <summary>
        /// Секретный ключ
        /// </summary>
        private string _SecretKey;
        /// <summary>
        /// Ключ подтверждения
        /// </summary>
        private string _ConfirmKey;
        /// <summary>
        /// ID вашей группы
        /// </summary>
        private int _GroupID;
        /// <summary>
        /// Ваш токен API в приложении
        /// </summary>
        private string _APIToken;
        /// <summary>
        /// Версия API. Задается библиотекой
        /// </summary>
        private int _APIVersion;
        #endregion

        /// <summary>
        /// Инициализация приложения
        /// </summary>
        /// <param name="GroupID">VKID группы</param>
        /// <param name="APIToken">Токен приложения</param>
        /// <param name="SecretKey">Секретный ключ из настроек приложения</param>
        /// <param name="ConfirmKey">Код подтверждения (Пример: a1b2c3)</param>
        public PonchikClient(int GroupID, string APIToken, string SecretKey, string ConfirmKey)
        {
            _SecretKey = SecretKey;
            _ConfirmKey = ConfirmKey;
            _GroupID = GroupID;
            _APIToken = APIToken;
            // For API UPDATED
            _APIVersion = 1;
        }

        #region CallBack API
        /// <summary>
        /// Обработчик событий для CallBack API
        /// Обрабатывает 3 типа запросов и событие:
        /// confirmation - Возвращает тип и строку для ответа серверу
        /// new_donate - Возвращает тип, строку для ответа серверу и массив VKPonchikLib.DonateAnswer.Donate в виде объекта для дальнейшего использования
        /// payment_status - Возвращает тип, строку для ответа серверу и массив VKPonchikLib.DonateAnswer.Payment в виде объекта для дальнейшего использования
        /// error - Возвращает тип, строку для ответа серверу и причину ошибки в виде String
        /// 
        /// Подробнее о типах запросов можно узнать на GitHub проекта и vkdonuts.ru/api#callback
        /// </summary>
        /// <param name="type">Тип ответа/запроса</param>
        /// <param name="answer">Строка для ответа серверу</param>
        /// <param name="obj">Объект, приклепленный к событию</param>
        public delegate void CallBackHandler(string type, string answer, object obj = null);
        /// <summary>
        /// Выполняется при входящем запросе подтверждения
        /// </summary>
        public event CallBackHandler CallBackNewConfirmation;
        /// <summary>
        /// Вызывается при входящем донате
        /// </summary>
        public event CallBackHandler CallBackNewDonate;
        /// <summary>
        /// Вызывается при входящем выводе средств
        /// </summary>
        public event CallBackHandler CallBackNewPaymentStatus;
        /// <summary>
        /// Вызывается при возникновении внутренней ошибки в работе CallBack API
        /// </summary>
        public event CallBackHandler CallBackError;
        /// <summary>
        /// Принимает и обрабатывает входящий массив от CallBack API
        /// </summary>
        /// <param name="json">Массив для обработки в виде строки</param>
        public void CallBackInput(string json)
        {
            VKPonchikLib.DonateAnswer.Da DA = VKPonchikLib.DonateAnswer.Da.FromJson(json);
            if (CheckRequest(DA, _SecretKey))
            {
                //DA API version "1"
                if (DA.Type == "confirmation")
                {
                    try
                    {
                        CallBackNewConfirmation?.Invoke("confirmation", VKPonchikLib.Converters.Serialize.ToJson(new ConfirmationJSON { Status = "ok", Code = _ConfirmKey }));
                    }
                    catch (Exception ex)
                    {
                        CallBackError?.Invoke("error", VKPonchikLib.Converters.Serialize.ToJson(new ConfirmationJSON { Status = "error" }), $"Ошибка обработки confirmation: {ex.Message}");
                    }
                }
                else if (DA.Type == "new_donate")
                {
                    try
                    {
                        CallBackNewDonate?.Invoke("new_donate", VKPonchikLib.Converters.Serialize.ToJson(new ConfirmationJSON { Status = "ok" }), DA.Donate);
                    }
                    catch (Exception ex)
                    {
                        CallBackError?.Invoke("error", VKPonchikLib.Converters.Serialize.ToJson(new ConfirmationJSON { Status = "error" }), $"Ошибка обработки new_donate: {ex.Message}");
                    }
                }
                else if (DA.Type == "payment_status")
                {
                    try
                    {
                        CallBackNewPaymentStatus?.Invoke("payment_status", VKPonchikLib.Converters.Serialize.ToJson(new ConfirmationJSON { Status = "ok" }), DA.Payment);
                    }
                    catch (Exception ex)
                    {
                        CallBackError?.Invoke("error", VKPonchikLib.Converters.Serialize.ToJson(new ConfirmationJSON { Status = "error" }), $"Ошибка обработки payment_status: {ex.Message}");
                    }
                }
            }
            else
            {
                CallBackError?.Invoke("error", VKPonchikLib.Converters.Serialize.ToJson(new ConfirmationJSON { Status = "error" }), "Запрос не прошел проверку");
            }
        }

        /// <summary>
        /// Конвертация boolean для проверки на подлинность
        /// </summary>
        /// <param name="bl">Переменная boolean для конвертации в string</param>
        /// <returns></returns>
        private string BoolToStringFC(bool bl)
        {
            if (bl) return "1";
            else return "";
        }

        /// <summary>
        /// Проверка запроса на подлинность
        /// </summary>
        /// <param name="DA">Входящий массив</param>
        /// <param name="key">Секретный ключ</param>
        /// <returns></returns>
        private bool CheckRequest(VKPonchikLib.DonateAnswer.Da DA, string key)
        {
            Dictionary<string, string> m = new Dictionary<string, string>();
            //--- Это можно как-то оптимизировать, но я пока не знаю как.
            m.Add($"{nameof(DA.Group)}", DA.Group);
            m.Add($"{nameof(DA.Type)}", DA.Type);
            //-
            if (DA.Donate != null && DA.Type == "new_donate")
            {
                m.Add($"{nameof(DA.Donate)}/{nameof(DA.Donate.Id)}", DA.Donate.Id.ToString());
                m.Add($"{nameof(DA.Donate)}/{nameof(DA.Donate.User)}", DA.Donate.User.ToString());
                m.Add($"{nameof(DA.Donate)}/{nameof(DA.Donate.Date)}", DA.Donate.Date.ToString());
                m.Add($"{nameof(DA.Donate)}/{nameof(DA.Donate.Amount)}", DA.Donate.Amount.ToString());
                if (DA.Donate.Msg != null) m.Add($"{nameof(DA.Donate)}/{nameof(DA.Donate.Msg)}", DA.Donate.Msg.ToString());
                m.Add($"{nameof(DA.Donate)}/{nameof(DA.Donate.Anonym)}", BoolToStringFC(DA.Donate.Anonym));
                if (DA.Donate.Answer != null) m.Add($"{nameof(DA.Donate)}/{nameof(DA.Donate.Answer)}", DA.Donate.Answer.ToString());
                m.Add($"{nameof(DA.Donate)}/{nameof(DA.Donate.Vkpay)}", BoolToStringFC(DA.Donate.Vkpay));
                m.Add($"{nameof(DA.Donate)}/{nameof(DA.Donate.Status)}", DA.Donate.Status.ToString());
                if (DA.Donate.Reward != null)
                {
                    m.Add($"{nameof(DA.Donate)}/{nameof(DA.Donate.Reward)}/{nameof(DA.Donate.Reward.Id)}", DA.Donate.Reward.Id.ToString());
                    m.Add($"{nameof(DA.Donate)}/{nameof(DA.Donate.Reward)}/{nameof(DA.Donate.Reward.Title)}", DA.Donate.Reward.Title.ToString());
                    m.Add($"{nameof(DA.Donate)}/{nameof(DA.Donate.Reward)}/{nameof(DA.Donate.Reward.Status)}", DA.Donate.Reward.Status.ToString());
                }
            }
            //-
            if (DA.Payment != null && DA.Type == "payment_status")
            {
                m.Add($"{nameof(DA.Payment)}/{nameof(DA.Payment.Id)}", DA.Payment.Id.ToString());
                m.Add($"{nameof(DA.Payment)}/{nameof(DA.Payment.Status)}", DA.Payment.Status.ToString());
                m.Add($"{nameof(DA.Payment)}/{nameof(DA.Payment.Processed)}", DA.Payment.Processed.ToString());
                m.Add($"{nameof(DA.Payment)}/{nameof(DA.Payment.System)}", DA.Payment.System.ToString());
                m.Add($"{nameof(DA.Payment)}/{nameof(DA.Payment.Purse)}", DA.Payment.Purse.ToString());
                m.Add($"{nameof(DA.Payment)}/{nameof(DA.Payment.Amount)}", DA.Payment.Amount.ToString());
                m.Add($"{nameof(DA.Payment)}/{nameof(DA.Payment.User)}", DA.Payment.User.ToString());
            }
            //---
            List<string> keys = new List<string>();
            foreach (string k in m.Keys) keys.Add(k);
            IEnumerable<string> auto = keys.OrderByDescending(s => s);
            string[] s1 = new string[auto.Count()];
            int i = auto.Count() - 1;
            foreach (string st in auto)
            {
                s1[i] = st;
                i--;
            }
            string s2 = null;
            foreach (string st in s1)
            {
                if (String.IsNullOrWhiteSpace(s2)) s2 = m[st];
                else s2 += $",{m[st]}";
            }
            s2 += $",{key}";
            string hash = VKPonchikLib.Converters.CustomConverters.ComputeSha256Hash(s2);
            if (DA.Hash == hash) return true;
            else
            return false;
        }
        #endregion

        #region Main API
        /// <summary>
        /// Получение списка донатов
        /// </summary>
        /// <param name="len">Количество донатов в списке. Максимум 100. По умолчанию 20.</param>
        /// <param name="offset">Смещение по выборе донатов</param>
        /// <param name="start_date">Временная метка по UNIX (в миллисекундах). Задает минимальную дату и время выбираемых донатов.</param>
        /// <param name="end_date">Временная метка по UNIX (в миллисекундах). Задает максимальную дату и время выбираемых донатов.</param>
        /// <param name="sort">Метод сортировки. По умолчанию date. Возможные значения: date - сортировка по дате; amount - сортировка по сумме.</param>
        /// <param name="reverse">Направление сортировки. По умолчанию false. Возможные значения: false - сортировка по убыванию; true - сортировка по возрастанию.</param>
        /// <returns></returns>
        public VKPonchikLib.GetDonates.Response.JSON GetDonatesList(int len = 20, int offset = -999, int start_date = 0, int end_date = 0, string sort = "date", bool reverse = false)
        {
            VKPonchikLib.GetDonates.Request.JSON JSON = new VKPonchikLib.GetDonates.Request.JSON { Group = _GroupID, Token = _APIToken, Version = _APIVersion };
            if (len != 20) JSON.Len = len;
            if (offset != -999) JSON.Offset = offset;
            if (start_date != 0) JSON.StartDate = start_date;
            if (end_date != 0) JSON.EndDate = end_date;
            if (sort != "date") JSON.Sort = sort;
            if (reverse) JSON.Reverse = reverse;

            return VKPonchikLib.GetDonates.Response.JSON.FromJson(SendPostJSON("https://api.vkdonuts.ru/donates/get", VKPonchikLib.Converters.Serialize.ToJson(JSON)));
        }
        #endregion

        #region Functions
        /// <summary>
        /// Отправляет JSON массив через POST запрос на указанный адрес
        /// </summary>
        /// <param name="uri">Ссылка на сервер</param>
        /// <param name="json">Массив в виде строки</param>
        /// <returns></returns>
        public string SendPostJSON(string uri, string json)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }
        /// <summary>
        /// Возвращает описание ошибки по ее коду
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetErrorCodeInfo(int code)
        {
            switch(code)
            {
                case 1000:
                    return "Неизвестный метод.";

                case 1001:
                    return "Не переданы обязательные параметры.";

                case 1002:
                    return "Переданы некорректные значения для некоторых параметров.";

                case 1004:
                    return "Ошибка авторизации. Проверьте правильность параметров \'group\' и \'token\'.";

                case 1005:
                    return "Версия API устарела.";

                case 1006:
                    return "API сервис временно не доступен.";

                case 2000:
                    return "Превышен лимит обращений к API.";

                case 3000:
                    return "В данный момент нет активной кампании.";

                case 3001:
                    return "Кампания с таким ID не найдена.";

                case 3002:
                    return "Заявка на вывод с таким ID не найдена.";

                case 3003:
                    return "Донат с таким ID не найден.";

                case 3004:
                    return "Запрашиваемая сумма выплаты превышает остаток средств на балансе.";

                case 3005:
                    return "Запрашиваемая сумма выплаты ниже минимальной суммы выплаты для данной платежной системы.";

                case 3006:
                    return "Ошибка списания средств. Повторите запрос.";

                case 3007:
                    return "Создание выпдат через API отключено в настройках приложения.";

                case 3008:
                    return "Время окончания указано неправильно. Кампания не может оканчиваться менее чем через три часа от текущего момента.";

                default:
                    return $"Ошибка {code} отсутствует в базе библиотеки";
            }
        }

        #endregion
    }

    #region CallBack response JSON
    /// <summary>
    /// Масив, возвращаемый для CallBack API
    /// </summary>
    public class ConfirmationJSON
    {
        /// <summary>
        /// Состояние обработки запроса
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
        /// <summary>
        /// Код подтверждения (если нужно)
        /// </summary>
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }
    }
    #endregion
}
