using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace System.Text.Json.Tests.SourceGenRegressionTests.Net70
{
    [JsonSerializable(typeof(WeatherForecastWithPOCOs))]
    [JsonSerializable(typeof(ClassWithCustomConverter))]
    [JsonSerializable(typeof(MyLinkedList))]
    public partial class Net70GeneratedContext : JsonSerializerContext { }

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