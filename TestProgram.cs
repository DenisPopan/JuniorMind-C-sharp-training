using System;
using Xunit;

namespace SupportCases.Test
{
    public class TestProgram
    {
        [Fact]
        public void RandomNumberOfTicketsShouldBeSortedCorrectly()
        {
            SupportTicket ticket1 = new SupportTicket(1, "Incorrect behavior", Program.GetPriorityLevel("Medium"));
            SupportTicket ticket2 = new SupportTicket(2, "Device not working", Program.GetPriorityLevel("Important"));
            SupportTicket ticket3 = new SupportTicket(3, "Battery drain", Program.GetPriorityLevel("Important"));
            SupportTicket ticket4 = new SupportTicket(4, "Device immediately turns off", Program.GetPriorityLevel("Critical"));
            SupportTicket ticket5 = new SupportTicket(5, "Strange behavior", Program.GetPriorityLevel("Low"));
            SupportTicket ticket6 = new SupportTicket(6, "Occasionally freeze", Program.GetPriorityLevel("Critical"));
            SupportTicket ticket7 = new SupportTicket(7, "Application not working", Program.GetPriorityLevel("Low"));
            SupportTicket ticket8 = new SupportTicket(8, "Internet connection problems", Program.GetPriorityLevel("Medium"));
            SupportTicket[] tickets = { ticket1, ticket2, ticket3, ticket4, ticket5, ticket6, ticket7, ticket8 };
            SupportTicket[] sortedTickets = { ticket6, ticket4, ticket3, ticket2, ticket8, ticket1, ticket7, ticket5 };
            Program.Quick3Sort(tickets, 0, 7);
            Assert.Equal(tickets, sortedTickets);
        }

        [Fact]
        public void OneTicketShouldReturnSameTicket()
        {
            SupportTicket ticket1 = new SupportTicket(1, "Incorrect behavior", Program.GetPriorityLevel("Medium"));
            SupportTicket[] tickets = { ticket1 };
            SupportTicket[] sortedTickets = { ticket1 };
            Program.Quick3Sort(tickets, 0, 0);
            Assert.Equal(tickets, sortedTickets);
        }

        [Fact]
        public void NoTicketsShouldReturnNothing()
        {
            SupportTicket[] tickets = { };
            SupportTicket[] sortedTickets = { };
            Program.Quick3Sort(tickets, 0, -1);
            Assert.Equal(tickets, sortedTickets);
        }

        [Fact]
        public void ALotOfOneTypeOfTicketsShouldReturnCorrectSortedTickets()
        {
            SupportTicket ticket1 = new SupportTicket(1, "Incorrect behavior", Program.GetPriorityLevel("Important"));
            SupportTicket ticket2 = new SupportTicket(2, "Device not working", Program.GetPriorityLevel("Important"));
            SupportTicket ticket3 = new SupportTicket(3, "Battery drain", Program.GetPriorityLevel("Important"));
            SupportTicket ticket4 = new SupportTicket(4, "Device immediately turns off", Program.GetPriorityLevel("Critical"));
            SupportTicket ticket5 = new SupportTicket(5, "Strange behavior", Program.GetPriorityLevel("Important"));
            SupportTicket ticket6 = new SupportTicket(6, "Occasionally freeze", Program.GetPriorityLevel("Critical"));
            SupportTicket[] tickets = { ticket1, ticket2, ticket3, ticket4, ticket5, ticket6 };
            SupportTicket[] sortedTickets = { ticket6, ticket4, ticket3, ticket5, ticket2, ticket1 };
            Program.Quick3Sort(tickets, 0, 5);
            Assert.Equal(tickets, sortedTickets);
        }

        [Fact]
        public void OnlyOneTypeOfTicketsShouldReturnUnchangedArray()
        {
            SupportTicket ticket1 = new SupportTicket(1, "Incorrect behavior", Program.GetPriorityLevel("Important"));
            SupportTicket ticket2 = new SupportTicket(2, "Device not working", Program.GetPriorityLevel("Important"));
            SupportTicket ticket3 = new SupportTicket(3, "Battery drain", Program.GetPriorityLevel("Important"));
            SupportTicket ticket4 = new SupportTicket(4, "Device immediately turns off", Program.GetPriorityLevel("Important"));
            SupportTicket ticket5 = new SupportTicket(5, "Strange behavior", Program.GetPriorityLevel("Important"));
            SupportTicket ticket6 = new SupportTicket(6, "Occasionally freeze", Program.GetPriorityLevel("Important"));
            SupportTicket[] tickets = { ticket1, ticket2, ticket3, ticket4, ticket5, ticket6 };
            SupportTicket[] sortedTickets = { ticket1, ticket2, ticket3, ticket4, ticket5, ticket6 };
            Program.Quick3Sort(tickets, 0, 5);
            Assert.Equal(tickets, sortedTickets);
        }
    }
}
