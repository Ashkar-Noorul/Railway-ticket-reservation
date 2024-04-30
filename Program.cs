
using System;


namespace RailwayTicketBooking
{
  class Program
  {

    public static void bookTicket(Passenger p)
    {
      TicketBooker booker = new TicketBooker();

      if (TicketBooker.availableWaitingList == 0)
      {
        Console.WriteLine("No Tickets Available");
        return;
      }

      if ((p.berthPreference.Equals("L") && TicketBooker.availableLowerBerths > 0) ||
          (p.berthPreference.Equals("M") && TicketBooker.availableMiddleBerths > 0) ||
          (p.berthPreference.Equals("U") && TicketBooker.availableUpperBerths > 0))
      {
        Console.WriteLine("Preferred berth Available");
        if (p.berthPreference.Equals("L"))
        {
          Console.WriteLine("Lower berth given");
          booker.BookTicket(p, (TicketBooker.lowerBerthsPositions[0]), "L");
          //remove the booked position from available positions
          TicketBooker.lowerBerthsPositions.RemoveAt(0);
          TicketBooker.availableLowerBerths--;
        }
        else if (p.berthPreference.Equals("M"))
        {
          Console.WriteLine("Middle berth given");
          booker.BookTicket(p, (TicketBooker.middleBerthsPositions[0]), "M");
          //remove the booked position from available positions
          TicketBooker.middleBerthsPositions.RemoveAt(0);
          TicketBooker.availableMiddleBerths--;
        }
        else if (p.berthPreference.Equals("U"))
        {
          Console.WriteLine("Upper berth given");
          booker.BookTicket(p, (TicketBooker.upperBerthsPositions[0]), "U");
          //remove the booked position from available positions
          TicketBooker.upperBerthsPositions.RemoveAt(0);
          TicketBooker.availableUpperBerths--;
        }
      }
      else if (TicketBooker.availableLowerBerths > 0)
      {
        Console.WriteLine("Lower berth given");
        booker.BookTicket(p, (TicketBooker.lowerBerthsPositions[0]), "L");
        //remove the booked position from available positions
        TicketBooker.lowerBerthsPositions.RemoveAt(0);
        TicketBooker.availableLowerBerths--;
      }
      else if (TicketBooker.availableMiddleBerths > 0)
      {
        Console.WriteLine("Middle berth given");
        booker.BookTicket(p, (TicketBooker.middleBerthsPositions[0]), "M");
        //remove the booked position from available positions
        TicketBooker.middleBerthsPositions.RemoveAt(0);
        TicketBooker.availableMiddleBerths--;
      }
      else if (TicketBooker.availableUpperBerths > 0)
      {
        Console.WriteLine("Upper berth given");
        booker.BookTicket(p, (TicketBooker.upperBerthsPositions[0]), "U");
        //remove the booked position from available positions
        TicketBooker.upperBerthsPositions.RemoveAt(0);
        TicketBooker.availableUpperBerths--;
      }

      //if no berth available going to RAC
      else if (TicketBooker.availableRacTickets > 0)
      {
        Console.WriteLine("RAC available");
        booker.AddToRAC(p, (TicketBooker.racPositions[0]), "RAC");
      }

      //if no RAC tickets available move to the WL
      else if (TicketBooker.availableWaitingList > 0)
      {
        Console.WriteLine("Adding to waiting list");
        booker.AddToWaitingList(p, (TicketBooker.waitingListPositions[0]), "WL");
      }
    }

    //cancel ticket function
    public static void cancelTicket(int id)
    {
      TicketBooker booker = new TicketBooker();
      if (!TicketBooker.passengers.ContainsKey(id))
      {
        Console.WriteLine("Passenger detail not found");
      }
      else
        booker.CancelTicket(id);

    }

    static void Main(string[] args)
    {
      // Write your application logic here
      bool loop = true;

      while (loop)
      {
        Console.WriteLine(" 1. Book Ticket \n 2. Cancel Ticket \n 3. Available Tickets \n 4. Booked Tickets \n 5. Exit");
        int choice = int.Parse(Console.ReadLine());
        switch (choice)
        {
          //book ticket
          case 1:
            {
              Console.WriteLine("Enter Passenger name,age and berth preference (L,M or U)");
              string name = Console.ReadLine();
              int age = int.Parse(Console.ReadLine());
              string berthPreference = Console.ReadLine();
              Passenger p = new Passenger(name, age, berthPreference);
              bookTicket(p);

            }
            break;
          case 2:
            {
              //get pasenger id to cancel
              Console.WriteLine("Enter passenger Id to cancel");
              int id = int.Parse(Console.ReadLine());
              cancelTicket(id);
            }
            break;
          //Print available tickets
          case 3:
            {
              TicketBooker booker = new TicketBooker();
              booker.PrintAvailableTickets();

            }
            break;
          //print booked tickets
          case 4:
            {
              TicketBooker booker = new TicketBooker();
              booker.PrintBookedTickets();
            }
            break;
          //exit
          case 5:
            {
              loop = false;
            }
            break;
          default:
            break;
        }
      }

    }
  }
}
