# Create Azure App Service Web Apps

## Demo Web Job

```
STORAGE_ACCOUNT_NAME=mslearnwebjobs\$RANDOM

az group create --name mslearn-webjobs --location westeurope

az storage account create \
 --name \$STORAGE_ACCOUNT_NAME \
 --resource-group mslearn-webjobs

STORAGE_ACCOUNT_CONNSTR=$(az storage account show-connection-string --name $STORAGE_ACCOUNT_NAME --query connectionString -o tsv)

echo "Created storage account \$STORAGE_ACCOUNT_NAME"
```

> Note: Deploy MVC App using VS Prof

```
WEB_APP_ID=\$(az webapp list --resource-group mslearn-webjobs --query [0].id --o tsv)

az webapp config set --id \$WEB_APP_ID --always-on true

az webapp config connection-string set --id $WEB_APP_ID --connection-string-type Custom --settings StorageAccount=$STORAGE_ACCOUNT_CONNSTR
```
