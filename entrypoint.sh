#!/bin/bash
/opt/mssql/bin/sqlservr &
sleep 90s
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P AtulVinod_@7 -C -d master -i /source/CorporateQnA.sql 
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P AtulVinod_@7 -C -d corporateqa -i /source/tables.sql
cd /source/CorporateQnA
/root/.dotnet/tools/dotnet-ef database update --context AppDbContext
cd /source/out 
dotnet CorporateQnA.dll