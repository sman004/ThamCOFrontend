# Fictitious Thamco Frontend container

##  Table of contents
- [Overview](#overview)
- [Features](#features)
- [Start](#start)
- [Installations](#installations)
- [Configurations](#Configurations)
- [Deployment](#Deployment)
- [Contributions](#Contributions)
- [license](#licence)

## Overview
#### This a mvc client facing app and it is a microservice users can use to signin and browse products. It  makes api calls through http to  the products api both deployed in the same resource group and app service plan on azure. This containerized client app is built using .NET and deployed to azure using github actions.  The yaml file for automatic CI/CD pipeline can be found at the root of the project. 

## Features
#### register and login, browse products, unfortunately this app has crashed after adding authentication and authorization, all efforst to make it work was unsuccesful.

## Getting Started
#### Follow the steps below to set up and run the containerized client app locally or in a production environment.
### Prerequisites
#### .NET Runtime (for local development, optional)

## Installation
#### Clone this repository, ensure the build.yaml file is configured correctly and update to the latest version if required. Ensure all Entity framework core libraries are installed and authentication and authorization libraries are downloaded.


## Deployment
#### This project has being deployed on azure, all resource groups and app service plans would be deleted once submitted

## Configuration
#### You can configure the container by using an .env file or environment variables directly. Key variables can be found in appsettings.json.
#### Appsettings.json: Set for Production.
#### Appsettings.Development.json: set for development environment
#### PORT: Port number on which the application runs.

### Contribution
#### To contribute to this project: Contributions are not allowed as this is a university assignment
