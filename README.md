# Schaakcompetitie 

Case for Minor: Continuous Delivery in large and complex software sytems @Hogeschool Utrecht / Info Support

## Table of contents
* [Description](#description)
* [Architecture](#Architecture)
* [Functionality](#functionality)
* [Requirements](#requirements)
* [Deliverables](#deliverables)
* [Demo](#demo)

## Description

> An application to keep track of standings in a chess competition. 

In this application, an organizer can add a game result between two chess players in the chess tournament and take a look at the standings.


This project was made for the Minor: "Continuous Delivery in large and complex software systems". The goal of this project was to show that I understood the things I had learned throughout the Minor. I was encouraged to adopt the Test-driven Development approach to make sure that the majority of the functionality was tested. I also had to show that I had a good understanding of Azure Pipelines, Event-driven Architecture and that I was able to work with Kubernetes, and Docker.

The application itself doesn't have complex functionalities because the project was more focused on me being able to implement the architecture in a short amount of time (2 days of 7 hours).


## Architecture

![Architecture](/_images/Architecture.png)

**Communication**

Services and Eventbus

- The services communicatie with the eventbus via amqp

**Database**

- Every service has its own schema in the database

## Functionality

show an overview of the standings
> As an Organiser
> I want to view the standings of the chess tournament
> So that everyone can see how the players are doing in the chess tournament



adding a game result
> As an Organiser 
> I want to be able to add a game result
> So that the overview of the standings can be updated 


## Requirements

- Write code of good quality, and provide good (unit)tests
- Realize the correct functionality and do not write unnecessary code
- Make logical choices when choosing your entities, controllers, and the like
- Ubiquitous language

- Implement a microservice architecture
- Use event bus (RabbitMQ) to communicate with the microservices

## Deliverables

- Docker image(s) in the Azure container registry
- Source code in git repo
- Build pipelines
- Kubernetes file for production

## Demo