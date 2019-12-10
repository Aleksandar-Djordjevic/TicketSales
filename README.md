# TicketSales

Concret tickets sale solution

Roles:
  - User can buy tickets for various concerts
  - User can view preiously bought tickets
  - Admin can create concerts
  - Admin can view how many tickets are sold for which concert

Services:
  - Core service should accept commands to
    (1) create concert and
    (2) buy concert tickets (and publish events about concerts and tickets)
  - Admin service (web-ui) should have a view
    (1) to display how many tickets are sold for which concert and a view
    (2) to create new concert
  - User service (web-ui) should
    (1) display tickets bought by user and
    (2) offer user to buy new tickets

Domain rules:
  - Admin needs to specify how many tickets are for sale per concert
  - User cannot buy more tickets than available
  
Implement solution using .net core, event based architecture, masstransit. For extra points use clean architecture and event sourcing.
