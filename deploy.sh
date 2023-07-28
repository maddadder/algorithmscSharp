docker-compose build
docker push neon-registry.b4c5-2917-0fd3-88a5.neoncluster.io/leenet/calculator:1.11.101

#calculator
helm upgrade calculator-calculator -f ./chart/values.yaml -f ./chart/values.calculator.yaml ./chart --namespace leenet
