using Bogus;
using rent.domain.Entities;

namespace CommomTestUtilities.Entities
{
    public class MotorcycleBuilder
    {
        public static Motorcycle Build()
        {
            return new Faker<Motorcycle>()
                .RuleFor(motorcycle => motorcycle._id, () => MongoDB.Bson.ObjectId.GenerateNewId())
                .RuleFor(motorcycle => motorcycle.Model, (f) => f.Vehicle.Model())
                .RuleFor(motorcycle => motorcycle.LicensePlate, (f) => f.Vehicle.Vin())
                .RuleFor(motorcycle => motorcycle.Year, (f) => f.Date.Past().Year.ToString())
                .Generate();


        }
    }
}
