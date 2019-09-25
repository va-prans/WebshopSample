# WebshopSample

This is a simple "PC component webshop" written in c# that was made for a Database & Testing exam. 
- Coheres to the principles of clean architecture with dependencies flowing inwards toward the Domain layer and adheres to command & query   responsibility segregation principles with the use of MediatR. 
- Uses MySQL to store all the data about users, items & orders.
- Uses Neo4j to provide a reccomendation algorithm to a single degree, so if you're looking at Motherboards it will reccomend items         related to Motherboards such as CPUS & GPUs.
- Uses Docker for deployment.
- Uses Jenkins for Continous Integration.

# How to run? 

- Install docker.
- Run "docker-compose -f docker-compose-ci.yml up --build --force-recreate".

# What happens when you run the above command?

- A Jenkins docker container is started up.
- Jenkins pulls from source.
- Jenkins builds & runs unit tests.
- Jenkins run creates a container for integration tests & deletes it.
- Jenkins builds and runs the application. 
- A swagger generated endpoint documentation page is now available at localhost:8085.
