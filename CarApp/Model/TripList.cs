namespace CarApp.Model;

public class TripList
{
    public List<Trip> Trips { get; private set; }

    public TripList()
    {
        Trips = new List<Trip>();
    }

    public void AddTrip(Trip trip)
    {
        Trips.Add(trip);
    }

    public double RemoveTrip(Trip trip)
    {
        double distance = trip.Distance;
        Trips.Remove(trip);
        return distance;
    }

    public string GetAllTripToString(Car car)
    {
        string result = "";
        foreach (Trip trip in Trips)
        {
            result += trip.GetTripInfo(car) + "\n";
        }
        return result;
    }
}
