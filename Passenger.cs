namespace RailwayTicketBooking
{
  public class Passenger
  {
    public static int id = 1;
    public string name;
    public int age;
    public string berthPreference; //U or L or M
    public int passengerId; //id will be created automatically
    public string alloted; //alloted type (L,U,M,RAC,WL)
    public int number; //seat number
    public Passenger(string name, int age, string berthPreference)
    {
      this.name = name;
      this.age = age;
      this.berthPreference = berthPreference;
      this.passengerId = id++;
      this.alloted = "";
      this.number = -1;
    }



  }
}
