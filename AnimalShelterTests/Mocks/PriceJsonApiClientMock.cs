using Animal_Shelter.Data.ExteranalData;

namespace AnimalShelterTests.Mocks;

public class PriceJsonApiClientMock : IPriceJsonApiClient
{
    public Task<int> GetFoodPrice(string animalTypeValue)
    {
        return Task.FromResult(10);
    }
}