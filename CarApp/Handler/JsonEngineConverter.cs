using System.Text.Json;
using System.Text.Json.Serialization;
using CarApp.Model;

namespace CarApp.Handler;

public class JsonEngineConverter : JsonConverter<Engine>
{
    public override Engine Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }
        reader.Read();
        string name = "";
        int ccm = 0;
        int horsePower = 0;
        int torque = 0;
        Engine.FuelType fuel = Engine.FuelType.Benzin;
        int mileage = 0;
        DateTime lastService = DateTime.Now;
        int serviceIntervalMileage = 0;
        int serviceIntervalMonths = 0;
        while (reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }
            string propertyName = reader.GetString();
            reader.Read();
            switch (propertyName)
            {
                case "Name":
                    name = reader.GetString() ?? "";
                    break;
                case "Ccm":
                    ccm = reader.GetInt32();
                    break;
                case "HorsePower":
                    horsePower = reader.GetInt32();
                    break;
                case "Torque":
                    torque = reader.GetInt32();
                    break;
                case "Fuel":
                    fuel = (Engine.FuelType)reader.GetInt32();
                    break;
                case "Mileage":
                    mileage = reader.GetInt32();
                    break;
                case "LastService":
                    lastService = reader.GetDateTime();
                    break;
                case "ServiceIntervalMileage":
                    serviceIntervalMileage = reader.GetInt32();
                    break;
                case "ServiceIntervalMonths":
                    serviceIntervalMonths = reader.GetInt32();
                    break;
                default:
                    throw new JsonException();
            }
            reader.Read();
        }
        return new Engine(name, ccm, horsePower, torque, fuel, mileage, lastService, serviceIntervalMileage, serviceIntervalMonths);
    }
    public override void Write(Utf8JsonWriter writer, Engine value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Name", value.Name);
        writer.WriteNumber("Ccm", value.Ccm ?? 0); // Handle nullable Ccm
        writer.WriteNumber("HorsePower", value.HorsePower);
        writer.WriteNumber("Torque", value.Torque);
        writer.WriteNumber("Fuel", (int)value.Fuel);
        writer.WriteNumber("Mileage", value.Mileage);
        writer.WriteString("LastService", value.LastService);
        writer.WriteNumber("ServiceIntervalMileage", value.ServiceIntervalMileage);
        writer.WriteNumber("ServiceIntervalMonths", value.ServiceIntervalMonths);
        writer.WriteEndObject();
    }
}
