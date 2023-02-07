docker-compose build
docker push neon-registry.d95f-98d9-33df-f8a6.neoncluster.io/leenet/calculator:1.11.67

#calculator
helm upgrade calculator-calculator -f ./chart/values.yaml -f ./chart/values.calculator.yaml ./chart --namespace leenet
