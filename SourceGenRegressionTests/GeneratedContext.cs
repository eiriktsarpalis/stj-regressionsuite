using System.Collections.Generic;
using System.Text.Json.Serialization;

#if NET6_0
namespace System.Text.Json.Tests.SourceGenRegressionTests.Net60
#elif NET7_0
namespace System.Text.Json.Tests.SourceGenRegressionTests.Net70
#elif NET8_0
namespace System.Text.Json.Tests.SourceGenRegressionTests.Net80
#else
#error Target framework needs to be updated
#endif
{
    [JsonSerializable(typeof(WeatherForecastWithPOCOs))]
    [JsonSerializable(typeof(ClassWithCustomConverter))]
    [JsonSerializable(typeof(MyLinkedList))]
#if NET6_0
    public partial class Net60GeneratedContext : JsonSerializerContext {}
#elif NET7_0
    public partial class Net70GeneratedContext : JsonSerializerContext {}
#elif NET8_0
    public partial class Net80GeneratedContext : JsonSerializerContext {}
#endif

    public class WeatherForecastWithPOCOs
    {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string? Summary { get; set; }
        public string? SummaryField;
        public List<DateTimeOffset>? DatesAvailable { get; set; }
        public Dictionary<string, HighLowTemps>? TemperatureRanges { get; set; }
        public string[]? SummaryWords { get; set; }
    }

    public class HighLowTemps
    {
        public int High { get; set; }
        public int Low { get; set; }
    }

    public class MyLinkedList
    {
        public MyLinkedList(int value, MyLinkedList? nested)
        {
            Value = value;
            Nested = nested;
        }

        public int Value { get; set; }
        public MyLinkedList? Nested { get; set; }
    }

    [JsonConverter(typeof(CustomConverter))]
    public class ClassWithCustomConverter
    {
        public int Value { get; set; }

        public class CustomConverter : JsonConverter<ClassWithCustomConverter>
        {
            public override ClassWithCustomConverter? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
                => new ClassWithCustomConverter { Value = reader.GetInt32() - 1 };

            public override void Write(Utf8JsonWriter writer, ClassWithCustomConverter value, JsonSerializerOptions options)
                => writer.WriteNumberValue(value.Value + 1);
        }
    }
}