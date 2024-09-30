#!/bin/bash
# Start SQL Server
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to start
sleep 30

# Execute the SQL script
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P YourStrong!Passw0rd -d master -i /usr/src/app/sqlscripts/init-database.sql

# Keep the container running
wait