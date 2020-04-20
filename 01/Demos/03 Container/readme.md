# Create containerized solutions

[Docker](https://www.docker.com/products/docker-desktop)

[Docker CLI](https://docs.docker.com/engine/reference/commandline/cli/)

[Kubernetes](https://kubernetes.io/de/)

#### Create Image & Deploy to Dockerhub / Azure

Create Image `FoodUI` or `FoodApi`

`docker build --rm -f "app.prod.dockerfile" -t foodui .`

Tag the Image

`docker tag foodui arambazamba/foodui:1.1.0`

Publish to Dockerhub

`docker push arambazamba/foodui`

Create a Container Registry instance

`az acr create -g az-203 --name apcontainers --sku Basic`

Login to Container Registry & get loginServer

```bash
az acr login --name apcontainers
az acr list --query "[].loginServer" -o tsv
```

Tag img & upload to ACR

```bash
az acr update --name apcontainers --admin-enabled true
az acr credential show -n apcontainers --query "passwords[0].value"
docker tag foodui apcontainers.azurecr.io/foodui:1.1.0
docker push apcontainers.azurecr.io/foodui:1.1.0
```

### Azure Container Instances

List existing containers:

`az container list`

Create container:

`az container create -g az-203 -l westeurope -n foodui --image arambazamba/foodui:1.1.1 --cpu 1 --memory 1 --dns-name-label integrations --port 80`

### AKS

[az aks Commands Overview](https://docs.microsoft.com/en-us/cli/azure/aks?view=azure-cli-latest)

[DevSpaces Intro](https://docs.microsoft.com/en-us/azure/dev-spaces/quickstart-team-development)

#### Create AKS Cluster

Install kubectl command line client locally:

`az aks install-cli`

> Note: You might need to set a path to your system env variables

Create resource group:

`az group create --name az-203 --location westeurope`

Create AKS cluster:

`az aks create --resource-group az-203 --name foodcluster --node-count 1 --enable-addons monitoring --generate-ssh-keys`

Get credentials for the Kubernets cluster:

`az aks get-credentials --resource-group az-203 --name foodcluster`

Get a list of cluster nodes:

`kubectl get nodes`

Apply the yaml

`kubectl apply -f foodui.yaml`

Get the serive IP and use it on the assigned port

kubectl get service foodui --watch

### Helm

[Helm Documentation](https://helm.sh/)

#### Installation on Windows

[Get Chocolatey](https://chocolatey.org/install)

`choco install kubernetes-helm`

#### Installation on Linux or WSL

```
curl -fsSL -o get_helm.sh https://raw.githubusercontent.com/helm/helm/master/scripts/get-helm-3
chmod 700 get_helm.sh
./get_helm.sh
```
