namespace Dinex.Business;

public class HistoryFileService : BaseService, IHistoryFileService
{
    private CultureInfo culture = new CultureInfo("pt-BR");
    private readonly IHistoryFileRepository _historyFileRepository;

    public HistoryFileService(IMapper mapper,
        INotificationService notification,
        IHistoryFileRepository historyFileRepository)
        : base(mapper, notification)
    {
        _historyFileRepository = historyFileRepository;
    }

    public async Task<List<InvestingHistoryFile>> CreateAsync(IFormFile fileHistory, Guid queueInId)
    {
        var dictionary = new Dictionary<int, List<dynamic>>();
        var historyFileList = new List<InvestingHistoryFile>();

        using (var stream = fileHistory?.OpenReadStream())
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var isFirstRow = true;
                var row = 0;
                while (reader.Read())
                {
                    if (isFirstRow)
                    {
                        isFirstRow = false;
                        continue;
                    }

                    var listOfColumns = new List<dynamic>();
                    for (int columnIndex = 0; columnIndex < reader.FieldCount; columnIndex++)
                    {
                        var columnType = reader.GetFieldType(columnIndex);
                        var columnValue = reader.GetValue(columnIndex);

                        if (columnType == typeof(string))
                        {
                            string stringValue = columnValue.ToString();
                            listOfColumns.Add(stringValue);
                        }
                        else if (columnType == typeof(int))
                        {
                            int intValue = Convert.ToInt32(columnValue);
                            listOfColumns.Add(intValue);
                        }
                        else if (columnType == typeof(double))
                        {
                            double doubleValue = Convert.ToDouble(columnValue);
                            listOfColumns.Add(doubleValue);
                        }
                        else if (columnType == typeof(float))
                        {
                            float floatValue = Convert.ToSingle(columnValue);
                            listOfColumns.Add(floatValue);
                        }
                        else if (columnType == typeof(DateTime))
                        {
                            var dateTime = Convert.ToDateTime(columnValue);
                            listOfColumns.Add(dateTime);
                        }
                    }
                    dictionary.Add(row, listOfColumns);
                    row++;
                }
                reader.Close();
            }
        }

        for (int selectedKey = 0; selectedKey < dictionary.Count; selectedKey++)
        {
            var historyFile = new InvestingHistoryFile();
            historyFile.QueueId = queueInId;
            historyFile.Applicable = GetApplicable(dictionary.FirstOrDefault(x => x.Key == selectedKey).Value[0]);
            historyFile.Date = DateTime.Parse(dictionary.FirstOrDefault(x => x.Key == selectedKey).Value[1], culture);
            historyFile.ActivityType = GetInvestmentActivityTypeByDescription(dictionary.FirstOrDefault(x => x.Key == selectedKey).Value[2]);
            historyFile.Product = dictionary.FirstOrDefault(x => x.Key == selectedKey).Value[3];
            historyFile.Institution = dictionary.FirstOrDefault(x => x.Key == selectedKey).Value[4];
            historyFile.Quantity = ConvertToInt(dictionary.FirstOrDefault(x => x.Key == selectedKey).Value[5]);
            historyFile.UnitPrice = ConvertToDecimal(dictionary.FirstOrDefault(x => x.Key == selectedKey).Value[6]);
            historyFile.OperationValue = ConvertToDecimal(dictionary.FirstOrDefault(x => x.Key == selectedKey).Value[7]);

            historyFileList.Add(historyFile);
        }
        await _historyFileRepository.AddRangeAsync(historyFileList);
        return historyFileList;
    }

    private static InvestingActivity GetInvestmentActivityTypeByDescription(string? description)
    {
        var enumValues = Enum.GetValues(typeof(InvestingActivity));

        foreach (var enumValue in enumValues)
        {
            if (enumValue is InvestingActivity activityType)
            {
                var enumDescription = GetEnumDescription(activityType); // Método para obter a descrição do enum

                if (enumDescription == description)
                    return activityType;
            }
        }

        throw new ArgumentException($"Investment activity type not found for the given description. - {description}");
    }

    private static string GetEnumDescription(Enum enumValue)
    {
        var descriptionAttribute = enumValue.GetType()
            .GetField(enumValue.ToString())
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .FirstOrDefault() as DescriptionAttribute;

        return descriptionAttribute?.Description ?? enumValue.ToString();
    }

    private static Applicable GetApplicable(string value)
    {
        if (value == "Credito")
        {
            return Applicable.In;
        }
        else
        {
            return Applicable.Out;
        }
    }

    private static int ConvertToInt(dynamic value)
    {
        var stringValue = Convert.ToString(value);

        var isInt = int.TryParse(stringValue, out int result);
        if (!isInt)
            return 0;

        return int.Parse(stringValue);
    }

    private static decimal ConvertToDecimal(dynamic value)
    {
        var stringValue = Convert.ToString(value);

        var isDecimal = decimal.TryParse(stringValue, out decimal result);
        if (!isDecimal)
            return decimal.Zero;

        return result;
    }
}
