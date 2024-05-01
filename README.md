# EMS
Employee Management System
# DB
I'm using MSSQL Server container, follow the below steps to setup DB in Docker:

Pull the Image:
```
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

Run the image which results in "sqlTest" Container:
```
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=nYHwQreterDDFFu31Upsfq8nz" -p 1433:1433 --name sqlTest --hostname sqlTest -d mcr.microsoft.com/mssql/server:2022-latest
```
Execute bash command in the container:

```
docker exec -it sqlTest "bash"
```

Login to the sqlCmd utility, mto verify the db is set correctly:

```
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "nYHwQreterDDFFu31Upsfq8nz"
```

Check initial DB setup:
```
SELECT name, database_id, create_date FROM sys.databases;  
GO

SELECT * FROM INFORMATION_SCHEMA.TABLES;
GO
```
ConnectionString:
```
Server=127.0.0.1,1433;Database=TestDB;User Id=SA;Password=nYHwQreterDDFFu31Upsfq8nz;Trust Server Certificate=True;
```