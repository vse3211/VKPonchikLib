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

### Prerequisites
На данный момент доступно для использования через данную библиотеку только CallBack API, которое рекомендуется к использованию в Web приложениях.

#### Использование
**Создайте экзеспляр класса PonchikClient для дальнейшего использования API**
```
VKPonchikLib.PonchikClient Client = new VKPonchikLib.PonchikClient(SecretKey, ConfirmKey);
```

## Example
**Использование CallBack API (Рекомендуется использовать в проектах веб-приложения или ASP.NET):**

```c#
/* Эвент загрузки страницы ASP.NET */
protected void Page_Load(object sender, EventArgs e)
{
    /* Создаем экземпляр класса PonchikClient и передаем ему секретный ключ и код подтверждения */
    VKPonchikLib.PonchikClient Client = new VKPonchikLib.PonchikClient(SecretKey, ConfirmKey);
    
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
    Client.CallBackError += Error;
    
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
private void Error(string type, string answer, object obj)
{
    // Поместите свой код здесь
}
        
```

**Использование Функции GetDonatesList**
```c#
VKPonchikLib.GetDonates.Response.JSON Donates = Client.GetDonatesList();
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
- [ ] Реализовать изменение статуса доната
- [ ] Реализовать добавление/измение ответа сообщества на донат
- [ ] Реализовать изменение выдачи вознаграждения
- [ ] Реализовать получение списка краудфандинговых кампаний (последние 20 кампаний)
- [ ] Реализовать получение активной краудфандинговой кампании
- [ ] Реализовать получение списка вознаграждений краудфандинговой кампании
- [ ] Реализовать обновление информации о краудфандинговой кампании
- [ ] Реализовать обновление информации о вознаграждении краудфандинговой кампании
- [ ] Реализовать получение списка заявок на выплату (последние 20 заявок)
- [ ] Реализовать создание заявки на выплату
- [ ] Реализовать получение баланса группы
- [ ] Реализация дополнительных функций
- [ ] Публикация ДЕМО проекта с документацией
- [ ] Полная версия ДЕМО проекта
- [ ] Полная версия документации

* [Связатся со мной](https://vk.com/londonist)
* [LonDev WebSite](https://londev.ru)
