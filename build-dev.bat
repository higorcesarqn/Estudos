docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml down --remove-orphans
docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d --build

docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml ps
docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml logs 