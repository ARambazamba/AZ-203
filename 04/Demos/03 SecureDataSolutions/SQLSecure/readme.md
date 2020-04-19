# SQL Security

# Set an admin login and password for your database

export ADMINLOGIN="sqlalexander"
export PASSWORD="TiTp4learn1234"

# Set the logical SQL server name. We'll add a random string as it needs to be globally unique.

export SERVERNAME=server\$RANDOM
export RESOURCEGROUP=learn-b708b75b-0ad6-442b-9fdc-040c15c62cc3

# Set the location, we'll pull the location from our resource group.

export LOCATION=$(az group show --name $RESOURCEGROUP | jq -r '.location')

Create SQL Server

```
az sql server create \
    --name $SERVERNAME \
    --resource-group $RESOURCEGROUP \
    --location $LOCATION \
    --admin-user $ADMINLOGIN \
    --admin-password $PASSWORD

az sql db create --resource-group $RESOURCEGROUP \
    --server $SERVERNAME \
    --name marketplaceDb \
    --sample-name AdventureWorksLT \
    --service-objective Basic

az sql db show-connection-string --client sqlcmd --name marketplaceDb --server $SERVERNAME | jq -r

sqlcmd -S tcp:server18744.database.windows.net,1433 -d marketplaceDb -U <username> -P <password> -N -l 30
```

Create a Linux VM & Connect

```
az vm create \
 --resource-group \$RESOURCEGROUP \
 --name appServer \
 --image UbuntuLTS \
 --size Standard_DS2_v2 \
 --generate-ssh-keys

ssh 104.42.196.123

echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev
```

Enable Firewall & Connect from Shell

```
EXECUTE sp_set_database_firewall_rule N'My Firewall Rule', '104.42.196.123', '104.42.196.123'

sqlcmd -S tcp:server18744.database.windows.net,1433 -d marketplaceDb -U 'sqlalexander' -P 'TiTp4learn1234' -N -l 30
```

Create Application User & DB

```
CREATE USER ApplicationUser WITH PASSWORD = 'YourStrongPassword1@';
GO
ALTER ROLE db_datareader ADD MEMBER ApplicationUser;
ALTER ROLE db_datawriter ADD MEMBER ApplicationUser;
GO
DENY SELECT ON SalesLT.Address TO ApplicationUser;
GO
SELECT FirstName, LastName, EmailAddress, Phone FROM SalesLT.Customer;
GO

```
