docker-compose build
docker push 192.168.1.151:32000/calculator:1.11.85

#calculator-t30
helm upgrade calculator-calculator -f ./chart/values.yaml -f ./chart/values.calculator-t30.yaml ./chart --namespace default
