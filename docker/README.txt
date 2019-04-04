- pull container for SQL_server by using: docker pull mcr.microsoft.com/mssql/server

- run the container using
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=P@$$w0rd' --name mssql -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-CU8-ubuntu

- sudo docker exec -it sql1 "bash"

- /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'P@$$w0rd'


- Create Database: CREATE DATABASE openvoting;


docker run --name mysqldb -p 3306 -e MYSQL_ROOT_PASSWORD=secret  -d mysql:latest