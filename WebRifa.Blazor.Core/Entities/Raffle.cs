﻿using WebRifa.Blazor.Core.Common;

namespace WebRifa.Blazor.Core.Entities;
public class Raffle : BaseEntity {
    public string Description { get; private set; } = string.Empty;
    public int TotalTicketNumber { get; private set; }
    public decimal TicketPrice { get; private set; }
    public string Observations { get; private set; } = string.Empty;
    public DateTime DrawDateTime { get; set; }

    public List<Ticket.Ticket>? Tickets { get; private set; }


    public Raffle()
    {
        
    }
}