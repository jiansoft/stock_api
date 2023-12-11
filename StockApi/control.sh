#! /bin/bash

function docker_build() {
   docker build -t stock-csharp-image -f Dockerfile .
}

function docker_stop() {
  docker stop stock-csharp-container && docker rm -f stock-csharp-container
}

function docker_start() {
  docker run --name stock-csharp-container -v=/opt/stock_api/logs:/app/logs:rw  -p 7000:7000 -p 7001:7001 -t -d stock-csharp-image
  docker ps
}

function docker_restart() {
  docker_stop
  sleep 1
  docker_start
}

function help() {
  echo "$0 docker_build|docker_stop|docker_start|docker_restart"
}

if [ "$1" == "docker_build" ]; then
  docker_build
elif [ "$1" == "docker_stop" ]; then
  docker_stop
elif [ "$1" == "docker_start" ]; then
  docker_start
elif [ "$1" == "docker_restart" ]; then
  docker_restart
else
  help
fi
