#!/bin/sh
docker kill $(docker ps -q) 2> /dev/null
if [[$! == 0]];then
        docker kill $(docker ps -q)
fi
docker rm $(docker ps -aq) 2> /dev/null
if [[$! == 0]];then
        docker rm $(docker ps -aq)
fi
yes | docker image prune -a
