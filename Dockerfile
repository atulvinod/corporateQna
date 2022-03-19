FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ENV ASPNETCORE_ENVIRONMENT="Development"
ENV ASPNETCORE_URLS="http://+:5000"

RUN apt update
RUN apt install nodejs npm -y
RUN mkdir source

COPY . /source

WORKDIR /source/CorporateQnA/UI
RUN npm i @angular/cli
RUN npm install
RUN node_modules/.bin/ng build --prod

WORKDIR /source
RUN dotnet publish -c Release -o out
WORKDIR /source/out
EXPOSE 80 443 5000 5001
CMD ASPNETCORE_URLS=http://*:$PORT dotnet CorporateQnA.dll
