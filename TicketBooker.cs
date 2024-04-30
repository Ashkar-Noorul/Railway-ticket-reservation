using System.Security.Principal;
using System.Collections.Generic;

namespace RailwayTicketBooking
{
  public class TicketBooker
  {
    //63 berths(upper ,lower , middle)  + ( 18 RAC passengers) 
    //10 waiting list tickets ->21 L, 21 M, 21U , 18RAC, 10WL
    public static int availableLowerBerths = 1; // Normally 21
    public static int availableMiddleBerths = 1; // Normally 21
    public static int availableUpperBerths = 1; // Normally 21
    public static int availableRacTickets = 1; // Normally 18
    public static int availableWaitingList = 1; // Normally 10

    public static Queue<int> waitingList = new Queue<int>(); // Queue of WL passengers
    public static Queue<int> racList = new Queue<int>(); // Queue of RAC passengers
    public static List<int> bookedTicketList = new List<int>(); // List of booked ticket passengers
    public static List<int> lowerBerthsPositions = new List<int>(new[] { 1 }); // Normally 1,2,...21
    public static List<int> middleBerthsPositions = new List<int>(new[] { 1 }); // Normally 1,2,...21
    public static List<int> upperBerthsPositions = new List<int>(new[] { 1 }); // Normally 1,2,...21
    public static List<int> racPositions = new List<int>(new[] { 1 }); // Normally 1,2,...18
    public static List<int> waitingListPositions = new List<int>(new[] { 1 }); // Normally 1,2,...10

    public static Dictionary<int, Passenger> passengers = new Dictionary<int, Passenger>(); // Map of passenger IDs to passengers

    public void BookTicket(Passenger p, int berthInfo, string allotedBerth)
    {
      p.number = berthInfo;
      p.alloted = allotedBerth;
      passengers[p.passengerId] = p;

      //adding passenger id to the list of booked tickets
      bookedTicketList.Add(p.passengerId);
      Console.WriteLine("--------------------------Booked Successfully");
    }

    public void AddToRAC(Passenger p, int racInfo, string allotedRAC)
    {
      p.number = racInfo;
      p.alloted = allotedRAC;
      //adding passenger to the dict
      passengers[p.passengerId] = p;
      racList.Enqueue(p.passengerId);

      //decrment the available RAC tickets
      availableRacTickets--;

      racPositions.RemoveAt(0);
      Console.WriteLine("--------------------------added to RAC Successfully");
    }

    public void AddToWaitingList(Passenger p, int waitingListInfo, string allotedWL)
    {
      p.number = waitingListInfo;
      p.alloted = allotedWL;
      //adding passenger to the dict
      passengers[p.passengerId] = p;
      waitingList.Enqueue(p.passengerId);

      //decrment the available WL tickets
      availableWaitingList--;

      waitingListPositions.RemoveAt(0);
      Console.WriteLine("--------------------------added to Waiting List Successfully");
    }

    //cancel ticket
    public void CancelTicket(int passengerId)
    {
      Passenger p = passengers[passengerId];
      passengers.Remove(passengerId);
      //remove the booked ticket from the list
      bookedTicketList.Remove(passengerId);

      int positionBooked = p.number;

      Console.WriteLine("---------------cancelled Successfully");


      if (p.alloted.Equals("L"))
      {
        availableLowerBerths++;
        lowerBerthsPositions.Add(positionBooked);
      }
      else if (p.alloted.Equals("M"))
      {
        availableMiddleBerths++;
        middleBerthsPositions.Add(positionBooked);
      }
      else if (p.alloted.Equals("U"))
      {
        availableUpperBerths++;
        upperBerthsPositions.Add(positionBooked);
      }

      //checking if any RAC is there
      if (racList.Count > 0)
      {
        //removing the passenger id from raclist 
        Passenger passengerFromRAC = passengers[racList.Dequeue()];
        int positionRAC = passengerFromRAC.number;
        racPositions.Add(positionRAC);
        availableRacTickets++;

        if (waitingList.Count > 0)
        {
          Passenger passengerFromWaitingList = passengers[waitingList.Dequeue()];
          int waitingListPosition = passengerFromWaitingList.number;
          waitingListPositions.Add(waitingListPosition);
          availableWaitingList++;

          //adding the passenger from waiting list to RAC
          passengerFromWaitingList.number = racPositions[0];
          passengerFromWaitingList.alloted = "RAC";
          racPositions.RemoveAt(0);
          racList.Enqueue(passengerFromWaitingList.passengerId);

          availableRacTickets--;
        }
        //book the ticket for the passenger from RAC
        //booking the cancelled ticket for RAC Passenger
        Program.bookTicket(passengerFromRAC);

      }
    }

    public void PrintAvailableTickets()
    {
      Console.WriteLine("Available Lower Berths " + availableLowerBerths);
      Console.WriteLine("Available Middle Berths " + availableMiddleBerths);
      Console.WriteLine("Available Upper Berths " + availableUpperBerths);
      Console.WriteLine("Available RAC's " + availableRacTickets);
      Console.WriteLine("Available Waiting List " + availableWaitingList);
      Console.WriteLine("--------------------------");
    }

    public void PrintBookedTickets()
    {
      if (passengers.Count == 0)
      {
        Console.WriteLine("No tickets has been booked");
        return;
      }
      foreach (var p in passengers.Values)
      {
        Console.WriteLine("PASSENGER ID " + p.passengerId);
        Console.WriteLine(" Name " + p.name);
        Console.WriteLine(" Age " + p.age);
        Console.WriteLine(" Status " + p.number + p.alloted);
        Console.WriteLine("--------------------------");
      }
    }


  }
}
