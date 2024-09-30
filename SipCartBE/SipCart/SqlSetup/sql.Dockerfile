# Use the official Microsoft SQL Server 2019 Linux image from the Docker Hub
FROM mcr.microsoft.com/mssql/server:2017-latest

# Set environment variables
ENV SA_PASSWORD=YourStrong!Passw0rd
ENV ACCEPT_EULA=Y
ENV MSSQL_PID=Developer

# Create a directory for SQL scripts
RUN mkdir -p /usr/src/app/sqlscripts

# Copy SQL file into the container
COPY init-database.sql /usr/src/app/sqlscripts/init-database.sql


# Copy the entrypoint script into the container
COPY entrypoint.sh /usr/src/app/entrypoint.sh

# Make the entrypoint script executable
RUN chmod +x /usr/src/app/entrypoint.sh

# Set the entrypoint to run the SQL Server and then execute the script
ENTRYPOINT ["/usr/src/app/entrypoint.sh"]

