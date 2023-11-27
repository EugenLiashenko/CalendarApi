using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;

//Создаем креды и сервис

var credential = GoogleCredential.FromFile("C:/Users/liashenko.y/Downloads/angular-yen-406115-950389e9619e.json")//тут нужно поменять линку на креды!!!!
                                 .CreateScoped(CalendarService.Scope.Calendar);

var calendarId = "yevhen.mechanik@gmail.com";
var service = new CalendarService(new BaseClientService.Initializer()
{
    HttpClientInitializer = credential,
    ApplicationName = "CalendarApiTrial",
});

//Делаем реквест на все ивенты нашего календаря, который мы выбрали

var request = service.Events.List(calendarId);

var response = request.ExecuteAsync().Result;

foreach (var item in response.Items)
{
    Console.WriteLine(item.Summary);
}

//Создаем новый ивент

Event newEvent = new Event()
{
    Summary = "Event with me",
    Description = "This is a sample event description.",
    Start = new EventDateTime { DateTime = DateTime.Now.AddHours(5), TimeZone = "UTC" },
    End = new EventDateTime { DateTime = DateTime.Now.AddHours(6), TimeZone = "UTC" },
};

Event createdEvent = service.Events.Insert(newEvent, calendarId).Execute();

//Удвляем ивент

var eventId = response.Items.FirstOrDefault(items => items.Summary == "Event with me").Id;

service.Events.Delete(calendarId, eventId).Execute();

Console.WriteLine();