# Implement batch jobs by using Azure Batch Services

[Azure Batch Explorer](https://azure.github.io/BatchExplorer/)

[Azure Batch REST Reference](https://docs.microsoft.com/en-us/rest/api/batchmanagement/)

[Azure Batch Node SDK](https://docs.microsoft.com/en-us/azure/batch/batch-nodejs-get-started)

[Azure Batch C#](https://docs.microsoft.com/en-us/azure/batch/tutorial-parallel-dotnet)

## Demo

Create environment variables for your resource group and batch resource

```bash
RESOURCE_GROUP="az203-batch"
BATCH_ACCOUNT=batchaccount-$RANDOM
```

Create Resource Group

```bash
az group create $RESOURCE_GROUP
```

Create batch account

```bash
az batch account create \
 --name $BATCH_ACCOUNT \
 --resource-group $RESOURCE_GROUP \
 --location westeurope
```

Sign in to your Azure Batch account:

```bash
az batch account login \
 --name $BATCH_ACCOUNT \
 --resource-group $RESOURCE_GROUP \
 --shared-key-auth
```

Create a pool

```bash
az batch pool create \
 --id mypool --vm-size Standard_A1_v2 \
 --target-dedicated-nodes 3 \
 --image canonical:ubuntuserver:16.04-LTS \
 --node-agent-sku-id "batch.node.ubuntu 16.04"
```

Check allocation state

```bash
az batch pool show --pool-id mypool \
 --query "allocationState"
```

Crate batch job

```bash
az batch job create \
 --id myjob \
 --pool-id mypool
```

Create Batch tasks:

```bash
for i in {1..10}
do
   az batch task create \
    --task-id mytask$i \
    --job-id myjob \
    --command-line "/bin/bash -c 'echo \$(printenv | grep \AZ_BATCH_TASK_ID) processed by; echo \$(printenv | grep \AZ_BATCH_NODE_ID)'"
done
```

Delete Job

```bash
az batch job delete --job-id myjob -y
```

## Monitor Job

Create a new Batch job

```
az batch job create \
 --id myjob2 \
 --pool-id mypool

```

```
for i in {1..10}
do
   az batch task create \
    --task-id mytask$i \
    --job-id myjob2 \
    --command-line "/bin/bash -c 'echo \$(printenv | grep \AZ_BATCH_TASK_ID) processed by; echo \$(printenv | grep \AZ_BATCH_NODE_ID)'"
done
```

Check Status:

```
az batch task show \
 --job-id myjob2 \
 --task-id mytask1

```

Download output using CLI

```
az batch task file list \
 --job-id myjob2 \
 --task-id mytask5 \
 --output table

mkdir taskoutputs && cd taskoutputs

for i in {1..10}
do
az batch task file download \
 --job-id myjob2 \
 --task-id mytask$i \
    --file-path stdout.txt \
    --destination ./stdout$i.txt
done

cat stdout1.txt && cat stdout2.txt

az batch job delete --job-id myjob2 -y

```

Using Batch Explorer

az batch job create \
 --id explorerjob \
 --pool-id mypool

for i in {1..100}
do
az batch task create \
 --task-id mytask\$i \
 --job-id explorerjob \
 --command-line "/bin/bash -c 'printenv; sleep 5s'"
done
