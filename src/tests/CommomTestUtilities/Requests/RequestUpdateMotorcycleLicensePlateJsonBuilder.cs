using Bogus;
using rent.communication.Requests;

namespace CommomTestUtilities.Requests
{
    public class RequestUpdateMotorcycleLicensePlateJsonBuilder
    {
        public static RequestUpdateMotorcycleLicensePlateJson Build()
        {
            return new Faker<RequestUpdateMotorcycleLicensePlateJson>()
                .RuleFor(motorcycle => motorcycle.LicensePlate, f => f.Vehicle.Vin())
                .Generate();
        }
    }
}
