dotnet build

cd src
cd TicketSales.User
START cmd /K dotnet run

cd ..
cd TicketSales.Admin
START cmd /K dotnet run

cd ..

cd TicketSales.Core
cd TicketSales.Core.Web
START cmd /K dotnet run