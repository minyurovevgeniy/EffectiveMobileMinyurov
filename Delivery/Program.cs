using CsvHelper;
using EffectiveMinyurov;
using Serilog;
using System.Globalization;

const string pressEnter = "Для завершения работы нажмите Enter";

string loggerFile = "_deliveryLog";

try
{
    Console.Write("Введите название файла с логами без расширения txt: ");
    loggerFile = Console.ReadLine();
    int noSpacesLoggerFileNameLength = loggerFile.Replace(" ","").Length;
    if (loggerFile.Equals("") || noSpacesLoggerFileNameLength < 1)
    {
        loggerFile = "_deliveryLog";
    }
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
    Log.Logger.Error(exception.Message);
    Console.WriteLine(pressEnter);
    Console.ReadLine();
    Environment.Exit(0);
}
Console.WriteLine("Будет создан файл с логами под именем " + loggerFile + ".txt");
Log.Logger = new LoggerConfiguration().
    WriteTo.File("../../../" + loggerFile + ".txt",
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();
Log.Information("\r\nЛоггирование запущено");

StreamReader sReader = null;
try
{
    sReader = new StreamReader("../../../Delivery.txt");
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
    Log.Logger.Error(exception.Message);
    Console.WriteLine(pressEnter);
    Console.ReadLine();
    Environment.Exit(0);
}

List<Order> orders = new ();

using (var csv = new CsvReader(sReader, CultureInfo.InvariantCulture))
{
    try
    {
        orders = csv.GetRecords<Order>().ToList();
        Log.Logger.Information("Данные из файла загружены");
    }
    catch (CsvHelper.TypeConversion.TypeConverterException exception)
    {
        Console.WriteLine(exception.Message);
        Log.Logger.Error(exception.Message);
        Console.WriteLine(pressEnter);
        Console.ReadLine();
        Environment.Exit(0);
    }
}

int _cityDistrict = 0;
DateTime _firstDeliveryDateTime = DateTime.Now;
DateTime deliveryTimeEnd = DateTime.Now;
DateTime deliveryTimeEnd30MinutesLater = deliveryTimeEnd.AddMinutes(30);

try
{
    Console.Write("Введите номер района: ");
    _cityDistrict = Int32.Parse(Console.ReadLine());
    Log.Logger.Information("Введен номер района: "+ _cityDistrict);
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
    Log.Logger.Error(exception.Message);
    Console.WriteLine(pressEnter);
    Console.ReadLine();
    Environment.Exit(0);
}

/* Дата с НАЧАЛО */
try
{
    Console.Write("Введите время первой доставки: ");
    _firstDeliveryDateTime = DateTime.Parse(Console.ReadLine());

    Log.Logger.Information("Введено время первой доставки: " + _firstDeliveryDateTime);
    deliveryTimeEnd30MinutesLater = _firstDeliveryDateTime.AddMinutes(30);
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
    Log.Logger.Error(exception.Message);
    Console.WriteLine(pressEnter);
    Console.ReadLine();
    Environment.Exit(0);
}
/* Дата с КОНЕЦ */


// Фильтрация за первые 30 минут после первого заказа
var ordersFiltered30 = from o in orders
                       where o.District == _cityDistrict
                       && o.DeliveryTime >= _firstDeliveryDateTime && o.DeliveryTime <= deliveryTimeEnd30MinutesLater
                       select o;

foreach (Order order in ordersFiltered30)
{
    Console.WriteLine("Id = " + order.Id);
    Console.WriteLine("Вес = " + order.Weight);
    Console.WriteLine("Район = " + order.District);
    Console.WriteLine("Время доставки = " + order.DeliveryTime);
    Console.WriteLine();
}

Console.Write("Введите название файла с отфильтрованными заказами без расширения txt: ");
string deliveryFile = "_deliveryOrder";

try
{
    deliveryFile = Console.ReadLine();

    int noSpacesOutputFileNameLength = deliveryFile.Replace(" ", "").Length;
    if (deliveryFile.Equals("") || noSpacesOutputFileNameLength < 1)
    {
        deliveryFile = "_deliveryOrder";
    }
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
    Log.Logger.Error(exception.Message);
    Console.WriteLine(pressEnter);
    Console.ReadLine();
    Environment.Exit(0);
}
Console.WriteLine("Будет создан файл с отфильтрованными заказами под именем " + deliveryFile + ".txt");
try
{
    using (StreamWriter writer = new StreamWriter("../../../" + deliveryFile + ".txt", false))
    {
        foreach (Order order in ordersFiltered30)
        {
            writer.WriteLine(order.Id + "," + order.Weight + "," + order.District + "," + order.DeliveryTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
    Log.Logger.Information("Выходной файл создан");
    Console.WriteLine("Выходной файл создан");
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
    Log.Logger.Error(exception.Message);
    Console.WriteLine(pressEnter);
    Console.ReadLine();
    Environment.Exit(0);
}

Console.WriteLine(pressEnter);
Console.ReadLine();