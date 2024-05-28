using Bogus;
using rent.communication.Requests;

namespace CommomTestUtilities.Requests
{
    public class RequestRegisterMotorcycleJsonBuilder
    {
        public static RequestRegisterMotorcycleJson Build()
        {
            return new Faker<RequestRegisterMotorcycleJson>()
                .RuleFor(motorcycle => motorcycle.LicensePlate, f => f.Vehicle.Vin() )
                .RuleFor(motorcycle => motorcycle.Model, f => f.Vehicle.Model())
                .RuleFor(motorcycle => motorcycle.Year, f => f.Date.Past().Year.ToString())
                .Generate();
        }
    }
}
