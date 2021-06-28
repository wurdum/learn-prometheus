Build image

```
docker build -t learn-prometheus:1.0 .
```

Run container

```
docker run --rm -p 5000:80 --name learn-prometheus learn-prometheus:3.0
```

Install resources

```
helm dependency update .\k8s\
kubectl create namespace learn-prometheus
helm install --namespace learn-prometheus learn-prometheus .\k8s\
```

Upgrade resources

```
helm upgrade learn-prometheus .\k8s
```

Delete resources

```
helm uninstall learn-prometheus
```

Access grafana

```
$pass = kubectl get secret --namespace learn-prometheus learn-prometheus-grafana -o jsonpath="{.data.admin-password}"
[System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String($pass))
```

Use password to login to grafana, login is "admin".
