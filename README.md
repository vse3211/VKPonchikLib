# VKPonchikLib
Библиотека для использования API донат - сервиса "Пончик" ВКонтакте
Используемое API: https://vkdonuts.ru/api

## Getting Started
Эта инструкция позволит вам просто и быстро начать работать с API.

### Install
Установите Nuget пакет в ваш проект:
**Package Manager**
``` powershell
PM> Install-Package VKPonchikLib
```
**.NET CLI**
``` bash
> dotnet add package VKPonchikLib
```
**Visual Studio Nuget Manager**
```
Проект -> Свойства -> Управление пакетами Nuget -> Обзор -> Поиск -> VKPonchikLib -> Установить
```

### Поддержать разработку проекта
Данная библиотека распространяется бесплатно, но вы можете [поддержать](https://vk.com/app6887721_-138648450) ее разработку, [отправив](https://vk.com/app6887721_-138648450) средства разработчику

### Prerequisites
В библиотеке последней версии реализовано использование CallBack API и отправка запросов к обычному API для получения/изменения информации о донатах
В следующем обновлении запранирована переработка CallBack API, а также добавление новых функций

#### Использование
**Создайте экзеспляр класса PonchikClient для дальнейшего использования API**
```
VKPonchikLib.PonchikClient Client = new VKPonchikLib.PonchikClient(SecretKey, ConfirmKey);
```

## Example
**Использование CallBack API (Рекомендуется использовать в проектах веб-приложения или ASP.NET):**

```c#
/* Эвент загрузки страницы ASP.NET */
public partial class CallBack : System.Web.UI.Page
{
    /* Создаем экземпляр класса PonchikClient и передаем ему секретный ключ и код подтверждения */
    VKPonchikLib.PonchikClient Client = new VKPonchikLib.PonchikClient(Base.CDB.GroupID, Base.CDB.APIToken, Base.CDB.SecretKey, Base.CDB.ConfirmKey);

    protected void Page_Load(object sender, EventArgs e)
    {
        string json;
        /* Принимаем входящий поток */
        using (var reader = new StreamReader(Request.InputStream))
        {
            /* Считываем данные в строку */
            json = reader.ReadToEnd();
        }

        /* Объявляем функцию для эвента CallBackNewConfirmation */
        Client.CallBackNewConfirmation += Confirmation;
        /* Объявляем функцию для эвента CallBackNewDonate */
        Client.CallBackNewDonate += Donate;
        /* Объявляем функцию для эвента CallBackNewPaymentStatus */
        Client.CallBackNewPaymentStatus += PaymentStatus;
        /* Объявляем функцию для эвента CallBackError */
        Client.CallBackError += CBError;

        /* Передаем в обработчик CallBack запросов полученный JSON массив */
        Client.CallBackInput(json);
    }

    /* Объявляем функцию для обработки запросов типа confirmation */
    private void Confirmation(string type, string answer)
    {
        // Поместите свой код здесь
    }
    
    /* Объявляем функцию для обработки запросов типа new_donate */
    private void Donate(string type, string answer, object obj)
    {
        // Поместите свой код здесь
    }
    
    /* Объявляем функцию для обработки запросов типа payment_status */
    private void PaymentStatus(string type, string answer, object obj)
    {
        // Поместите свой код здесь
    }
    
    /* Объявляем функцию для обработки запросов типа error */
    private void CBError(string type, string answer, object obj)
    {
        // Поместите свой код здесь
    }
}
```

**Использование Функций PonchikClient.Donate**
```c#
/* Создаем новый экземпляр класса Donate */
var pl = new VKPonchikLib.PonchikClient.Donate(Convert.ToInt32("01234567"), "APIToken", "SecretKey", "ConfirmKey");
/* Получение списка донатов */
string GetResult = VKPonchikLib.Converters.Serialize.ToJson(pl.Get());
/* Изменить статус доната */
string ChangeStatusResult = VKPonchikLib.Converters.Serialize.ToJson(pl.ChangeStatus(0123456, "Status"));
/* Добавить/изменить ответ сообщества на донат */
string AnswerResult = VKPonchikLib.Converters.Serialize.ToJson(pl.Answer(0123456, "Answer"));
/* Изменить выдачи вознаграждения */
string ChangeRewardStatusResult = VKPonchikLib.Converters.Serialize.ToJson(pl.ChangeRewardStatus(0123456, "Status"));
```

**Использование Функций PonchikClient.Campaign**
```c#
/* Создаем новый экземпляр класса Donate */
var plc = new VKPonchikLib.PonchikClient.Campaign(Convert.ToInt32(GIDbox.Text), SecretKeybox.Text, ConfirmKeybox.Text);
/* Получить список краудфандинговых кампаний (последние 20 кампаний) */
Base.CDB.result = VKPonchikLib.Converters.Serialize.ToJson(plc.Get(new int[3] { 01234560, 01234561, 01234563 }));
/* Получить активную краудфандинговую кампанию */
Base.CDB.result = VKPonchikLib.Converters.Serialize.ToJson(plc.GetActive());
/* Получить список вознаграждений краудфандинговой кампании */
Base.CDB.result = VKPonchikLib.Converters.Serialize.ToJson(plc.GetRewards(0123456));
/* Обновить информацию о краудфандинговой кампании */
Base.CDB.result = VKPonchikLib.Converters.Serialize.ToJson(plc.Change(0123456, "Title", "Status", 0, 11000, 0, 0));
/* Обновить информацию о вознаграждении краудфандинговой кампании */
Base.CDB.result = VKPonchikLib.Converters.Serialize.ToJson(plc.ChangeReward(0123456, "Title", "Desc", 500, 0, "hidden"));
```

**Использование Функции SendPostJSON**
```c#
string response = Client.SendPostJSON("https://example.com/", "{ \"json\" }");
```

**Использование Функции SendPostJSON**
```c#
string response = Client.SendPostJSON("https://example.com/", "{ \"json\" }");
```

**Использование Функции GetErrorCodeInfo**
```c#
string ErrorDescription = Client.GetErrorCodeInfo(IntErrorCode);
```

## TODOs
- [x] Реализовать проверку и использование CallBack API
- [x] Реализовать получение списка донатов
- [x] Реализовать изменение статуса доната
- [x] Реализовать добавление/измение ответа сообщества на донат
- [x] Реализовать изменение выдачи вознаграждения
- [x] Реализовать получение списка краудфандинговых кампаний (последние 20 кампаний)
- [x] Реализовать получение активной краудфандинговой кампании
- [x] Реализовать получение списка вознаграждений краудфандинговой кампании
- [x] Реализовать обновление информации о краудфандинговой кампании
- [x] Реализовать обновление информации о вознаграждении краудфандинговой кампании
- [ ] Реализовать получение списка заявок на выплату (последние 20 заявок)
- [ ] Реализовать создание заявки на выплату
- [ ] Реализовать получение баланса группы
- [ ] Реализация дополнительных функций
- [ ] Публикация ДЕМО проекта с документацией
- [ ] Полная версия ДЕМО проекта
- [ ] Полная версия документации

* [Связатся со мной](https://vk.com/londonist)
* [LonDev WebSite](https://londev.ru)
