using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKPonchikLib
{
    //Данная библиотека создана мной для использования API донат - сервиса "Пончик" в C# приложениях
    //С помощью написанной мной библиотеки вы сможете подключить API донат - сервиса к своему приложению и использовать его без особых усилий
    //Возможно библиотека будет обновлятся, но это зависит только от вашей поддержки
    //Ваш Londonist (StealthKiller#8719, https://vk.com/londonist)

    public class PonchikClient
    {
        /// <summary>
        /// Секретный ключ
        /// </summary>
        private string _SecretKey;
        /// <summary>
        /// Ключ подтверждения
        /// </summary>
        private string _ConfirmKey;

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
        /// Инициализация приложения
        /// </summary>
        /// <param name="SecretKey">Секретный ключ из настроек приложения</param>
        /// <param name="ConfirmKey">Код подтверждения (Пример: a1b2c3)</param>
        public PonchikClient(string SecretKey, string ConfirmKey)
        {
            _SecretKey = SecretKey;
            _ConfirmKey = ConfirmKey;
        }

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
                        CallBackNewConfirmation?.Invoke("confirmation", VKPonchikLib.DonateAnswer.Serialize.ToJson(new ConfirmationJSON { Status = "ok", Code = _ConfirmKey }));
                    }
                    catch (Exception ex)
                    {
                        CallBackError?.Invoke("error", VKPonchikLib.DonateAnswer.Serialize.ToJson(new ConfirmationJSON { Status = "error" }), $"Ошибка обработки confirmation: {ex.Message}");
                    }
                }
                else if (DA.Type == "new_donate")
                {
                    try
                    {
                        CallBackNewDonate?.Invoke("new_donate", VKPonchikLib.DonateAnswer.Serialize.ToJson(new ConfirmationJSON { Status = "ok" }), DA.Donate);
                    }
                    catch (Exception ex)
                    {
                        CallBackError?.Invoke("error", VKPonchikLib.DonateAnswer.Serialize.ToJson(new ConfirmationJSON { Status = "error" }), $"Ошибка обработки new_donate: {ex.Message}");
                    }
                }
                else if (DA.Type == "payment_status")
                {
                    try
                    {
                        CallBackNewPaymentStatus?.Invoke("payment_status", VKPonchikLib.DonateAnswer.Serialize.ToJson(new ConfirmationJSON { Status = "ok" }), DA.Payment);
                    }
                    catch (Exception ex)
                    {
                        CallBackError?.Invoke("error", VKPonchikLib.DonateAnswer.Serialize.ToJson(new ConfirmationJSON { Status = "error" }), $"Ошибка обработки payment_status: {ex.Message}");
                    }
                }
            }
            else
            {
                CallBackError?.Invoke("error", VKPonchikLib.DonateAnswer.Serialize.ToJson(new ConfirmationJSON { Status = "error" }), "Запрос не прошел проверку");
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
            string hash = VKPonchikLib.DonateAnswer.CustomConverters.ComputeSha256Hash(s2);
            if (DA.Hash == hash) return true;
            else
            return false;
        }

    }

    public class ConfirmationJSON
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }
    }
}
