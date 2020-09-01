using System;

namespace SupportCases
{
    public enum PriorityLevel
    {
        Critical,
        Important,
        Medium,
        Low
    }

    public struct SupportTicket
    {
        public long Id;
        public string Description;
        public PriorityLevel Priority;

        public SupportTicket(long id, string description, PriorityLevel priority)
        {
            this.Id = id;
            this.Description = description;
            this.Priority = priority;
        }
    }

    public class Program
    {
        public static PriorityLevel GetPriorityLevel(string priority)
        {
            if (priority == null)
            {
                throw new ArgumentNullException(nameof(priority));
            }

            switch (priority.ToLower().Trim())
            {
                case "critical":
                    return PriorityLevel.Critical;
                case "important":
                    return PriorityLevel.Important;
                case "medium":
                    return PriorityLevel.Medium;
            }

            return PriorityLevel.Low;
        }

        public static void Quick3Sort(SupportTicket[] tickets, int sequenceStart, int sequenceStop)
        {
            if (tickets == null)
            {
                throw new ArgumentNullException(nameof(tickets));
            }

            if (sequenceStart >= sequenceStop)
            {
                return;
            }

            int smallerElementsLeftPosition = sequenceStart;
            int biggerElementsRightPosition = sequenceStop;
            int currentPosition = sequenceStart;
            int pivot = (int)tickets[sequenceStop].Priority;

            while (currentPosition <= biggerElementsRightPosition)
            {
                if ((int)tickets[currentPosition].Priority < pivot)
                {
                    Swap(tickets, smallerElementsLeftPosition, currentPosition);
                    smallerElementsLeftPosition++;
                    currentPosition++;
                }
                else if ((int)tickets[currentPosition].Priority > pivot)
                {
                    Swap(tickets, biggerElementsRightPosition, currentPosition);
                    biggerElementsRightPosition--;
                }
                else
                {
                    currentPosition++;
                }
            }

            Quick3Sort(tickets, sequenceStart, smallerElementsLeftPosition - 1);
            Quick3Sort(tickets, biggerElementsRightPosition + 1, sequenceStop);
        }

        static void Main()
        {
            SupportTicket[] tickets = ReadSupportTickets();
            Quick3Sort(tickets, 0, tickets.Length - 1);
            Print(tickets);
            Console.Read();
        }

        static void Swap(SupportTicket[] tickets, int biggerElementsRightPosition, int currentPosition)
        {
            SupportTicket temp = tickets[biggerElementsRightPosition];
            tickets[biggerElementsRightPosition] = tickets[currentPosition];
            tickets[currentPosition] = temp;
        }

        static void Print(SupportTicket[] tickets)
        {
            for (int i = 0; i < tickets.Length; i++)
            {
                Console.WriteLine(tickets[i].Id + " - " + tickets[i].Description + " - " + tickets[i].Priority);
            }
        }

        static SupportTicket[] ReadSupportTickets()
        {
            const int ticketIdIndex = 0;
            const int descriptionIndex = 1;
            const int priorityLevelIndex = 2;

            int ticketsNumber = Convert.ToInt32(Console.ReadLine());
            SupportTicket[] result = new SupportTicket[ticketsNumber];

            for (int i = 0; i < ticketsNumber; i++)
            {
                string[] ticketData = Console.ReadLine().Split('-');
                long id = Convert.ToInt64(ticketData[ticketIdIndex]);
                result[i] = new SupportTicket(id, ticketData[descriptionIndex].Trim(), GetPriorityLevel(ticketData[priorityLevelIndex]));
            }

            return result;
        }
    }
}
