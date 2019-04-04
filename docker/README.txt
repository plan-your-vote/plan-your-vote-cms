- pull container for SQL_server by using: docker pull mcr.microsoft.com/mssql/server

- run the container using
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=P@$$w0rd' --name mssql -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-CU8-ubuntu

- sudo docker exec -it sql1 "bash"

- /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'P@$$w0rd'


- Create Database: CREATE DATABASE openvoting;
dotnet ef migrations add "OpenVoting" -c ApplicationDbContext

docker run --name mysqldb -p 3306 -e MYSQL_ROOT_PASSWORD=secret  -d mysql:latest

docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=P@$$w0rd' \
   -p 1433:1433 --name mssql \
   -d mcr.microsoft.com/mssql/server:2017-latest


   docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=P@$$w0rd' -p 1433 --name sqlserver -d microsoft/mssql-server-linux


   docker run \
   -e 'ACCEPT_EULA=Y' \
   -e 'MSSQL_SA_PASSWORD=P@$$w0rd' \
   -p 1401:1433 \
   --name sql1 \
   -d microsoft/mssql-server-linux



   docker run -e 'ACCEPT_EULA=Y' --name mssql -e 'MSSQL_SA_PASSWORD=P@$$w0rd' -p 1401:1433 -d mcr.microsoft.com/mssql/server:2017-latest


   docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=P@$$w0rd' --name mssql -p 1401:1433 -d mcr.microsoft.com/mssql/server:2017-CU8-ubuntu

   /opt/mssql-tools/bin/sqlcmd -S 127.0.0.1,1401 -U sa -P P@$$w0rd
