# Schaakcompetitie 

Case for Minor: Continuous Delivery in large and complex software sytems @Hogeschool Utrecht / Info Support

## Table of contents
* [Description](#description)
* [Architecture](#architecture)
* [Functionality](#functionality)
* [Requirements](#requirements)
* [Deliverables](#deliverables)
* [Demo](#demo)

## Description

> An application to keep track of standings in a chess competition. 

In this application, an organizer can add a game result between two chess players in the chess tournament and take a look at the standings.


This project was made for the Minor: "Continuous Delivery in large and complex software systems". The goal of this project was to show that I understood the things I had learned throughout the Minor. I was encouraged to adopt the Test-driven Development approach to make sure that the majority of the functionality was tested. I also had to show that I had a good understanding of Azure Pipelines, Event-driven Architecture and that I was able to work with Kubernetes, and Docker.

The application itself doesn't have complex functionalities because the project was more focused on me being able to implement the architecture in a short amount of time (2 days of 7 hours a day).


## Architecture

![Architecture](/_images/Architecture.png)

**Communication**

Services and Eventbus

- The services communicatie with the eventbus via amqp

**Database**

- Every service has its own schema in the database

## Functionality

Show an overview of the standings
> As an Organiser
>
> I want to view the standings of the chess tournament
>
> So that everyone can see how the players are doing in the chess tournament

<br>

Given the following games in the database:

| Wit            | Zwart          | Uitslag        |
|----------------|----------------|----------------|
| Pietje Jan     | Leslie de Boer | Wit Wint (1)   |
| Jan Hamoen     | Kees de Koning | Zwart wint (2) |
| Leslie de Boer | Joost Paard    | Remise (3)     |

<br>

The standings are as follows:

| Naam           | # | W | R | V | Score |
|----------------|---|---|---|---|-------|
| Pietje Jan     | 1 | 1 | 0 | 0 | 1     |
| Kees de Koning | 1 | 1 | 0 | 0 | 1     |
| Leslie de Boer | 2 | 0 | 1 | 1 | 0.5   |
| Joost Paard    | 1 | 0 | 1 | 0 | 0.5   |
| Jan Hamoen     | 1 | 0 | 0 | 1 | 0     |

<br>

Where # stands for the number of games played, W stands for the number of games won, R stands for the number of games that have been drawn (drawn), 
and V stands for the number of games that have been lost.
The score is calculated as follows: For each game won, the player earns 1 point, each draw is worth half a point, and each game lost is worth 0 points.

<br>

Adding a game result
> As an Organiser 
>
> I want to be able to add a game result
>
> So that the overview of the standings can be updated 

<br>

- Every player has a different name
- You cannot put the same name as white and black.

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

[demo](http://schaakcompetitie.dannycao.io)
